using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiteHotel.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
namespace SiteHotel.Data
{
    public class HotelData2 : DbContext
    {
        public HotelData2(DbContextOptions<HotelData2> options)
            : base(options)
        { }

        public DbSet<Client> ClientDB { get; set; }
        public DbSet<Chambre> ChambreDB { get; set; }
        public DbSet<Reservation> ReservationDB { get; set; }
        public DbSet<Paiement> PaymentDB { get; set; }
        public DbSet<Login> LoginDB { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapper les tables

            modelBuilder.Entity<Chambre>().ToTable("Chambre");

            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<Paiement>().ToTable("Paiement");
            modelBuilder.Entity<Login>().ToTable("Login");
        }

    }
}