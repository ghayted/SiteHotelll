using System.Windows;
using HotelAdmin.Models;
using HotelAdmin.Services;
using System.Collections.Generic;
using HotelAdmin.Data;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace HotelAdmin
{
    public partial class ClientView : Window
    {
        private ClientService _clientService;
        private bool _afficherClientsArchives = false;
        public ClientView()
        {
            InitializeComponent();
            _clientService = new ClientService();
            ChargerClients();
        }

       
        private void ChargerClients()
        {
            List<Client> clients;

            if (_afficherClientsArchives)
            {
                clients = _clientService.ObtenirTousLesClients()
                    .Where(c => c.EstArchive == true)
                    .ToList();
            }
            else
            {
                clients = _clientService.ObtenirTousLesClients()
                    .Where(c => c.EstArchive == false)
                    .ToList();
            }

            ClientListView.ItemsSource = clients;
        }
        private void VoirClientsArchive_Click(object sender, RoutedEventArgs e)
        {
            _afficherClientsArchives = true; 
            ChargerClients(); 
        }

        // Ajouter un client
        private bool TousLesChampsSontRemplis()
        {
            return !string.IsNullOrWhiteSpace(NomTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(EmailTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(TelephoneTextBox.Text) &&
                   !string.IsNullOrWhiteSpace(AdresseTextBox.Text);
        }










        private void AjouterClient_Click(object sender, RoutedEventArgs e)
        {
            if (!TousLesChampsSontRemplis())
            {
                MessageBox.Show("Tous les champs doivent être remplis avant de valider.");
                return;
            }

            using (var context = new HotelDbContext())
            {
                bool emailExiste = context.ClientsDB.Any(c => c.Email == EmailTextBox.Text);
                bool telephoneExiste = context.ClientsDB.Any(c => c.Telephone == TelephoneTextBox.Text);

                if (telephoneExiste)
                {
                    var clientActif = context.ClientsDB.FirstOrDefault(c => c.Telephone == TelephoneTextBox.Text);
                    if (clientActif != null && clientActif.EstArchive == false)
                    {
                        MessageBox.Show("Le client existe deja.");
                        return; 
                    }
                    MessageBoxResult result = MessageBox.Show(
                        "Le numéro de téléphone existe déjà. Voulez-vous restaurer le client ?",
                        "Téléphone existant",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                       
                        var clientExistant = context.ClientsDB.FirstOrDefault(c => c.Telephone == TelephoneTextBox.Text);

                        if (clientExistant != null)
                        {
                           
                            clientExistant.EstArchive = false;

                            context.SaveChanges();

                            MessageBox.Show("Le client a été restauré avec succès !");
                           EnvoyerEmailBienvenue(clientExistant.Email, clientExistant.Nom);
                        }
                        else
                        {
                            MessageBox.Show("Client introuvable pour restauration.");
                        }

                        return; 
                    }
                    else
                    {
                        return; 
                    }
                }

                var nouveauClient = new Client
                {
                    Nom = NomTextBox.Text,
                    Email = EmailTextBox.Text,
                    Telephone = TelephoneTextBox.Text,
                    Adresse = AdresseTextBox.Text,
                    EstArchive = false
                };
                if(EnvoyerEmailBienvenue(nouveauClient.Email, nouveauClient.Nom)) { 
                
                _clientService.AjouterClient(nouveauClient);
                ChargerClients();
                MessageBox.Show("Client ajouté avec succès !");
                }
                else
                {
                    MessageBox.Show("l'email du client n'est pas envoyer correctement ou l'email n'est pas valide !");

                }
                
            }
        }





        private void RechercherClient_Click(object sender, RoutedEventArgs e)
        {
            string recherche = SearchTextBox.Text.ToLower();

            if (!string.IsNullOrEmpty(recherche))
            {
                var clients = _clientService.ObtenirTousLesClients()
                                            .Where(c => c.Nom.ToLower().Contains(recherche) ||
                                                        c.Telephone.Contains(recherche))
                                            .ToList();

                if (clients.Count > 0)
                {
                    ClientListView.ItemsSource = clients; 
                }
                else
                {
                    MessageBox.Show("Aucun client trouvé.");
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un nom ou un téléphone pour effectuer la recherche.");
            }
        }
        private void ReinitialiserListe_Click(object sender, RoutedEventArgs e)
        {
            _afficherClientsArchives = false;
            ChargerClients(); 
            SearchTextBox.Clear(); 
        }
     



        private void ClientListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
       
            if (ClientListView.SelectedItem is Client client)
            {
                
                NomTextBox.Text = client.Nom ?? string.Empty;
                EmailTextBox.Text = client.Email ?? string.Empty;
                TelephoneTextBox.Text = client.Telephone ?? string.Empty;
                AdresseTextBox.Text = client.Adresse ?? string.Empty;
            }
            else
            {
               
                NomTextBox.Text = string.Empty;
                EmailTextBox.Text = string.Empty;
                TelephoneTextBox.Text = string.Empty;
                AdresseTextBox.Text = string.Empty;
            }
        }

        private void ModifierClient_Click(object sender, RoutedEventArgs e)
        {
            if (!TousLesChampsSontRemplis())
            {
                MessageBox.Show("Tous les champs doivent être remplis avant de valider.");
                return;
            }


            if (ClientListView.SelectedItem is Client client)
            {
                client.Nom = NomTextBox.Text;
                client.Email = EmailTextBox.Text;
                client.Telephone = TelephoneTextBox.Text;
                client.Adresse = AdresseTextBox.Text;

                _clientService.MettreAJourClient(client);
                ChargerClients();
                MessageBox.Show("Client modifié avec succès !");
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client à modifier.");
            }
        }







        // Supprimer un client
        private void SupprimerClient_Click(object sender, RoutedEventArgs e)
        {
            if (ClientListView.SelectedItem is Client client)
            {
                _clientService.SupprimerClient(client.ID);
                ChargerClients();
                
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un client à supprimer.");
            }
        }
        private bool EnvoyerEmailBienvenue(string email, string nom)
        {
            try
            {
                // Validation de l'email
                if (string.IsNullOrWhiteSpace(email) || !EstEmailValide(email))
                {
                    MessageBox.Show("L'adresse email saisie est invalide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }

                // Configuration du client SMTP (utilisant SendGrid)
                var smtpClient = new SmtpClient("smtp.sendgrid.net")
                {
                    Port = 587, // Port SMTP pour STARTTLS
                 //la place du cle d'api
                    EnableSsl = true // Active la sécurité SSL
                };

                // Contenu de l'email
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("dafalighayt@gmail.com", "Hotel Ghayt et Reda"), // Utilisez l'email vérifié sur SendGrid
                    Subject = "Bienvenue chez Hotel Ghayt et Reda",
                    Body = $"Bonjour {nom},\n\nMerci de rejoindre notre hôtel. Nous sommes ravis de vous accueillir !\n\nCordialement,\nL'équipe Ghayt et Reda",
                    IsBodyHtml = false // Changez en true si vous voulez envoyer un email HTML
                };

                // Ajouter l'email du client comme destinataire
                mailMessage.To.Add(email);

                // Envoyer l'email
                smtpClient.Send(mailMessage);

                // Message de succès
                MessageBox.Show("L'email de bienvenue a été envoyé avec succès !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                return true;
            }
            catch (Exception ex)
            {
                // Message d'erreur
                MessageBox.Show($"Erreur lors de l'envoi de l'email : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        // Méthode de validation d'email
        private bool EstEmailValide(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }


    }
}
