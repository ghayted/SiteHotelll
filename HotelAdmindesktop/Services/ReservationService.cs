using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelAdmin.Data;
using HotelAdmin.Models;
using System.Windows;

namespace HotelAdmin.Services
{
    public class ReservationService
    {
        // Ajouter une réservation
        public void AjouterReservation(Reservation reservation)
        {
            using (var context = new HotelDbContext())
            {
                context.ReservationsDB.Add(reservation);
                context.SaveChanges();
                MessageBox.Show("La réservation a été ajoutée avec succès.");
            }
        }

        // Obtenir toutes les réservations
        public List<Reservation> ObtenirToutesLesReservations()
        {
            using (var context = new HotelDbContext())
            {
                return context.ReservationsDB.ToList();
            }
        }

        // Obtenir une réservation par IDD
        public Reservation ObtenirReservationParID(int id)
        {
            using (var context = new HotelDbContext())
            {
                return context.ReservationsDB.Find(id);
            }
        }

        // Mettre à jour une réservation
        public void MettreAJourReservation(Reservation reservation)
        {
            using (var context = new HotelDbContext())
            {
                var existingReservation = context.ReservationsDB.Find(reservation.ID);
                if (existingReservation != null)
                {
                    existingReservation.ClientID = reservation.ClientID;
                    existingReservation.ChambreID = reservation.ChambreID;
                    existingReservation.DateArrivee = reservation.DateArrivee;
                    existingReservation.DateDepart = reservation.DateDepart;
                    existingReservation.Statut = reservation.Statut;
                    // Mettre à jour d'autres champs si nécessaire
                    context.SaveChanges();
                    MessageBox.Show("La réservation a été mise à jour avec succès.");
                }
                else
                {
                    MessageBox.Show("Réservation non trouvée.");
                }
            }
        }

        // Supprimer une réservation
        public void SupprimerReservation(int id)
        {
            using (var context = new HotelDbContext())
            {
                var reservation = context.ReservationsDB.Find(id);
                if (reservation != null)
                {
                    if (reservation.DateArrivee >= DateTime.Now)
                    {
                       
                        
                        context.ReservationsDB.Remove(reservation);  
                        context.SaveChanges();
                        MessageBox.Show("La réservation a été supprimée avec succès.");
                    }
                    else if (reservation.DateDepart <= DateTime.Now)
                    {
                        // Si la réservation est terminée (date de départ dans le passé)
                        try
                        {
                            reservation.EstArchive = true;
                            reservation.Statut = "Confirmée";
                            context.SaveChanges();
                            MessageBox.Show("La réservation a été confirmée et archivée avec succès.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erreur : {ex.Message}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("La réservation ne peux etre supprimer , le client occupe toujours la chambre .");
                    }
                }
                else
                {
                    MessageBox.Show("Réservation non trouvée.");
                }
            }
        }

      
    }
}
