using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteHotel.Data;
using SiteHotel.Models;
using SiteHotel.Services;
using System.Linq;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
namespace SiteHotel.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HotelData2 _context;
        private readonly ReservationService _reservationService;
       


        public ReservationController(HotelData2 context)
        {
            _context = context;
        }
        public IActionResult Reservation()
        {
            return View();
        }
        public IActionResult Confirmation(int chambreId, DateTime startDate, DateTime endDate)
        {
            var chambre = _context.ChambreDB.FirstOrDefault(c => c.ID == chambreId);
            if (chambre == null)
            {
                return NotFound();
            }
            ViewBag.Chambre = chambre;
            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;

         

            return View(chambre);
        }
        public IActionResult MesReservation()
        {
            // Récupérer l'email du compte connecté depuis la session
            string emailCompte = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(emailCompte))
            {
                // Si l'utilisateur n'est pas connecté, rediriger vers la page de connexion
                return RedirectToAction("Login", "Compte");
            }

            // Récupérer les réservations associées à l'email
            var reservations = _context.ReservationDB
                                .Where(r => r.CompteEmail == emailCompte)
                                .Include(r => r.Chambre) // Inclure les détails de la chambre
                                .ToList();


            return View(reservations);
        }


        [HttpPost]
        public IActionResult Confirmation(string FirstName, string LastName, string Email, string Telephone, string Address, DateTime DateArrivee, DateTime DateDepart, int ChambreID, Reservation reservation )
        {

            try
            {
                if (ModelState.IsValid)
                {
                  
                    reservation.Statut = reservation.Statut ?? "Active";

                    int clientId;

                    
                    var existingClient = _context.ClientDB.FirstOrDefault(c => c.Email == Email);
                    if (existingClient != null)
                    {
                        // Client existant : mise à jour
                        existingClient.EstArchive = false;
                        _context.SaveChanges();
                        clientId = existingClient.ID;
                    }
                    else
                    {
                        // Créer un nouveau client
                        var newClient = new Client
                        {
                            Nom = FirstName + " " + LastName,
                            Email = Email,
                            Telephone = Telephone,
                            Adresse = Address,
                            EstArchive = false
                        };

                        _context.ClientDB.Add(newClient);
                        _context.SaveChanges();
                        clientId = newClient.ID;
                    }

                    int nombreJours = (DateDepart - DateArrivee).Days;

                    var chambre = _context.ChambreDB.FirstOrDefault(c => c.ID == ChambreID);
                    if (chambre == null)
                    {
                        throw new Exception("Chambre introuvable");
                    }
                    ViewBag.Chambre = chambre;
                    ViewBag.StartDate = DateArrivee;
                    ViewBag.EndDate = DateDepart;
                    

                    decimal prixParJour = chambre.TypeChambreID == 1 ? 300 : 150;
                    decimal montantTotal = nombreJours * prixParJour;

                    // Créer la réservation
                    var newReservation = new Reservation
                    {
                        ClientID = clientId,
                        ChambreID = ChambreID,
                        DateArrivee = DateArrivee,
                        DateDepart = DateDepart,
                        Montant = montantTotal,
                        EstArchive = false,
                        EstPayer = true,
                        Statut = "Active",
                        EmployeID = 3,
                        DateCreation = DateTime.Now,
                        CompteEmail = HttpContext.Session.GetString("UserEmail") ?? "Non spécifié"
                    };
                    _context.ReservationDB.Add(newReservation);
                    _context.SaveChanges();
                    var newpayer = new Paiement {
                        Montant = montantTotal,
                        ModePaiement = "Carte",
                        ReservationID = newReservation.ID,
                        DatePaiement = DateTime.Now
                    };

                   
                    _context.PaymentDB.Add(newpayer);
                    _context.SaveChanges();
                    bool emailEnvoye = EnvoyerEmailReservation(
                      Email,
                      FirstName,
                      LastName,
                      DateArrivee,
                      DateDepart,
                      montantTotal,
                      "Carte"
                  );

                    if (emailEnvoye)
                    {
                        ViewBag.Message = "Réservation réussie ! Un email de confirmation vous a été envoyé.";
                    }
                    else
                    {
                        ViewBag.Message = "Réservation réussie, mais l'email de confirmation n'a pas pu être envoyé.";
                    }

                    // Confirmation
                    ViewBag.Message = "Réservation réussie !";
                    return View();
                }
                else
                {
                    // Affichage des erreurs de validation
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                    ViewBag.Message = "Erreur dans le formulaire. Veuillez vérifier vos informations.";
                }
            } 
            catch (Exception ex)
            {
                // Gestion des erreurs
                ViewBag.Message = "Une erreur s'est produite lors de la réservation : " + ex.Message;
            }

            // Retourner à la vue si problème
            return View(reservation);
        }
        public bool EnvoyerEmailReservation(string email, string firstName, string lastName, DateTime dateArrivee, DateTime dateDepart, decimal montantTotal, string modePaiement)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || !EstEmailValide(email))
                {
                    return false; // Email invalide
                }

                // Générer un code de réservation
                string codeReservation = GenererCodeReservation();

                // Générer le PDF
                byte[] pdfBytes = GenererPDFReservation(codeReservation, firstName, lastName, dateArrivee, dateDepart, montantTotal, modePaiement);

                // Contenu de l'email
                string body = $@"
        Bonjour {firstName} {lastName},<br/><br/>
        Votre réservation a été confirmée avec succès.<br/>
        Voici les détails de votre réservation :<br/><br/>
        <strong>Code Réservation :</strong> {codeReservation}<br/>
        <strong>Date d'arrivée :</strong> {dateArrivee:dd/MM/yyyy}<br/>
        <strong>Date de départ :</strong> {dateDepart:dd/MM/yyyy}<br/>
        <strong>Montant total :</strong> {montantTotal:C} (Mode de paiement : {modePaiement})<br/><br/>
        Nous vous remercions d'avoir choisi notre hôtel.<br/>
        Cordialement,<br/>
        <strong>L'équipe de l'hôtel Ghayt, Reda et Amine</strong>
        ";

                // Configuration SMTP
                var smtpClient = new SmtpClient("smtp.sendgrid.net")
                {
                    Port = 587,
                    
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("dafalighayt@gmail.com", "Hotel Ghayt, Reda et Amine"),
                    Subject = "Confirmation de votre réservation - Hotel Ghayt",
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(email);

                // Joindre le PDF
                var attachment = new Attachment(new MemoryStream(pdfBytes), "ConfirmationReservation.pdf", "application/pdf");
                mailMessage.Attachments.Add(attachment);

                // Envoi de l'email
                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'envoi de l'email : {ex.Message}");
                return false;
            }
        }

        private bool EstEmailValide(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private byte[] GenererPDFReservation(string codeReservation, string firstName, string lastName, DateTime dateArrivee, DateTime dateDepart, decimal montantTotal, string modePaiement)
        {
            using (var ms = new MemoryStream())
            {
                // Créer un document PDF
                var document = new iTextSharp.text.Document();
                var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, ms);
                document.Open();

                // Ajouter le contenu
                document.Add(new iTextSharp.text.Paragraph("Confirmation de Réservation"));
                document.Add(new iTextSharp.text.Paragraph($"Code Réservation : {codeReservation}"));
                document.Add(new iTextSharp.text.Paragraph($"Nom : {firstName} {lastName}"));
                document.Add(new iTextSharp.text.Paragraph($"Date d'arrivée : {dateArrivee:dd/MM/yyyy}"));
                document.Add(new iTextSharp.text.Paragraph($"Date de départ : {dateDepart:dd/MM/yyyy}"));
                document.Add(new iTextSharp.text.Paragraph($"Montant Total : {montantTotal:C}"));
                document.Add(new iTextSharp.text.Paragraph($"Mode de Paiement : {modePaiement}"));

                // Fermer le document
                document.Close();

                return ms.ToArray();
            }
        }
        private string GenererCodeReservation()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
      
    }


}





