using SiteHotel.Data;
using SiteHotel.Models;
using System.Linq;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SiteHotel.Services
{
    public class ReservationService
    {
        private readonly HotelData2 _context;
        public ReservationService(HotelData2 context)
        {
            _context = context;
        }
        public void CreateReservation(string firstName, string lastName, string email, string telephone, string address, Reservation reservation)
        {
            var existingClient = _context.ClientDB.FirstOrDefault(c => c.Email == email);

            int clientId;

            if (existingClient != null)
            {
                // Client existant
                existingClient.EstArchive = false;
                _context.SaveChanges();
                clientId = existingClient.ID;
            }
            else
            {
                // Créer un nouveau client
                var newClient = new Client
                {
                    Nom = firstName + " " + lastName,
                    Email = email,
                    Telephone = telephone,
                    Adresse = address,
                    EstArchive = false
                };

                _context.ClientDB.Add(newClient);
                _context.SaveChanges();
                clientId = newClient.ID;
            }

            // Calculer le montant total
            int nombreJours = (reservation.DateDepart - reservation.DateArrivee).Days;  // Correction ici : DateDepart - DateArrive
            var chambre = _context.ChambreDB.FirstOrDefault(c => c.ID == reservation.ChambreID);
            if (chambre == null)
            {
                throw new Exception("Chambre introuvable");
            }

            // Déterminer le prix par jour en fonction du type de chambre
            
            decimal prixParJour = chambre.TypeChambreID == 1 ? 300 : 150;
            decimal montantTotal = nombreJours * prixParJour;

            // Créer la réservation
            var newReservation = new Reservation
            {
                ClientID = clientId,
                ChambreID = reservation.ChambreID,
                DateArrivee = reservation.DateArrivee,
                DateDepart = reservation.DateDepart,
                Montant = montantTotal,
                EstArchive = false,
                EstPayer = true,
                Statut = reservation.Statut ?? "Active",  // Assurez-vous que le statut est défini
                EmployeID = 3,
                DateCreation = DateTime.Now
            };

            _context.ReservationDB.Add(newReservation);
            _context.SaveChanges();
        }
        public Reservation GetReservationById(int id)
        {
            return _context.ReservationDB.Find(id);
        }
    }
}
