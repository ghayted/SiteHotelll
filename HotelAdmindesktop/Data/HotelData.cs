using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelAdmin.Models;

using System.Reflection.Emit;
namespace HotelAdmin.Data
{



    public class HotelDbContext : DbContext
    {
        // Déclaration des DbSets pour chaque table de la base de données
        public DbSet<TypeChambre> TypeChambresDB { get; set; }
        public DbSet<Chambre> ChambresDB { get; set; }
        public DbSet<Employe> EmployesDB { get; set; }
        public DbSet<Client> ClientsDB { get; set; }
        public DbSet<Reservation> ReservationsDB { get; set; }
        public DbSet<Paiement> PaiementsDB { get; set; }
        public DbSet<Notification> NotificationsDB { get; set; }

        // Configuration des relations dans le modèle
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-D3A68EI\\MSSQLSERVER01;Database=DataHotel;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //apper les tables
            modelBuilder.Entity<TypeChambre>().ToTable("TypeChambre");
            modelBuilder.Entity<Chambre>().ToTable("Chambre");
            modelBuilder.Entity<Employe>().ToTable("Employe");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<Paiement>().ToTable("Paiement");
            modelBuilder.Entity<Notification>().ToTable("Notification");

           

            modelBuilder.Entity<Chambre>()
                .Property(c => c.EstDisponible)  // Propriété de l'entité
                .HasColumnName("Disponibilite");
            modelBuilder.Entity<Chambre>()
                .HasOne(c => c.TypeChambreAssocie)
                .WithMany(tc => tc.ListeChambres)
                .HasForeignKey(c => c.TypeChambreID);

            // Relation : Reservation -> Client
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ClientAssocie)
                .WithMany(c => c.ReservationsAssociees)
                .HasForeignKey(r => r.ClientID);

            // Relation : Reservation -> Chambre
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ChambreAssociee)
                .WithMany()
                .HasForeignKey(r => r.ChambreID);

            // Relation : Reservation -> Employe
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.EmployeAssocie)
                .WithMany(e => e.ReservationsGerees)
                .HasForeignKey(r => r.EmployeID);

            // Relation : Paiement -> Reservation
            modelBuilder.Entity<Paiement>()
                .HasOne(p => p.ReservationAssociee)
                .WithMany(r => r.PaiementsAssocies)
                .HasForeignKey(p => p.ReservationID);

            // Relation : Notification -> Client
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.ClientAssocie)
                .WithMany(c => c.NotificationsAssociees)
                .HasForeignKey(n => n.ClientID);
            modelBuilder.Entity<Reservation>()
    .HasOne(r => r.ClientAssocie)
    .WithMany(c => c.ReservationsAssociees)
    .HasForeignKey(r => r.ClientID)
               .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.ClientAssocie)
                .WithMany(c => c.NotificationsAssociees)
                .HasForeignKey(n => n.ClientID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}

