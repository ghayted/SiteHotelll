using SiteHotel.Data;
using SiteHotel.Models;
using System.Linq;
using System.Net.Mail;
using System.Net;

namespace SiteHotel.Services
{
    public class CompteService
    {
        private readonly HotelData2 _context;

        public CompteService(HotelData2 context)
        {
            _context = context;
        }

        public Login Authentification(string email, string password)
        {
            return _context.LoginDB.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
        public Login Verficication(string email) { 
           return _context.LoginDB.FirstOrDefault(u => u.Email == email);
        }
        public void RegisterUser(Login newUser)
        {
            _context.LoginDB.Add(newUser);
            _context.SaveChanges();
        }


        public bool EnvoyerEmailBienvenue(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || !EstEmailValide(email))
                {
                    return false; // Email invalide
                }

                var smtpClient = new SmtpClient("smtp.sendgrid.net")
                {
                    Port = 587,
                  
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("dafalighayt@gmail.com", "Hotel Ghayt,Reda et Amin"),
                    Subject = "Bienvenue chez Hotel Ghayt et Reda et Amine",
                    Body = $"Bonjour {email},\n\nMerci de rejoindre notre hôtel. Nous sommes ravis de vous accueillir !\n\nCordialement,\nL'équipe Ghayt et Reda et Amine",
                    IsBodyHtml = false
                };

                mailMessage.To.Add(email);

                smtpClient.Send(mailMessage);

                return true; 
            }
            catch (Exception)
            {
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

    }
}
