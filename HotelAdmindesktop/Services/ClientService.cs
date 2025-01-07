using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelAdmin.Data;
using HotelAdmin.Models;
using System.Net;
using System.Net.Mail;
using System.Windows;
namespace HotelAdmin.Services

{
    public class ClientService
    {
        // Ajouter un client
        public void AjouterClient(Client client)
        {
            using (var context = new HotelDbContext())
            {
                context.ClientsDB.Add(client);
                context.SaveChanges();
            }
        }

        // Obtenir tous les clients
        public List<Client> ObtenirTousLesClients()
        {
            using (var context = new HotelDbContext())
            {
                return context.ClientsDB.ToList();
            }
        }

        // Obtenir un client par ID
        public Client ObtenirClientParID(int id)
        {
            using (var context = new HotelDbContext())
            {
                return context.ClientsDB.Find(id);
            }
        }
        public bool ClientExisteParID(int id)
        {
            using (var context = new HotelDbContext())
            {
                return context.ClientsDB.Find(id) != null;
            }
        }
        // Mettre à jour un client
        public void MettreAJourClient(Client client)
        {
            using (var context = new HotelDbContext())
            {
                context.ClientsDB.Update(client);
                context.SaveChanges();
            }
        }

        // Supprimer un client
        public void SupprimerClient(int id)
        {
            using (var context = new HotelDbContext())
            {
                var client = context.ClientsDB.Find(id);
                if (client != null)
                {
                    bool aUneReserv = context.ReservationsDB.Any(r=>r.ClientID == id);
                    if (!aUneReserv)
                    {
                        context.ClientsDB.Remove(client);
                        context.SaveChanges();
                        MessageBox.Show("Le client a etais supprimer avec succee da la base de donne.");
                    }
                    else
                    {
                        var reservationActive = context.ReservationsDB.Where(r => r.ClientID == id && r.DateDepart >= DateTime.Now).FirstOrDefault();
                        if (reservationActive != null)
                        { 
                            MessageBox.Show("Le client a des réservations actives et ne peut pas être Supprimer.");
                        }
                        else
                        {
                            var reservationTerminee = context.ReservationsDB.Where(r => r.ClientID == id && r.DateDepart < DateTime.Now).FirstOrDefault(); 
                                 if (reservationTerminee != null)
                            {
                                reservationTerminee.EstArchive = true ;
                                reservationTerminee.Statut = "Confirmée";
                                client.EstArchive = true;
                                context.SaveChanges();
                                MessageBox.Show("Le client et sa réservation ont été archivés.");
                            }
                        }
                       
                    }
                }
            }
        }

    }
}