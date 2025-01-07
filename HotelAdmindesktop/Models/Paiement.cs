using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdmin.Models
{
    public class Paiement
    {
        public int ID { get; set; }
        public decimal Montant { get; set; } // Montant du paiement
        public string Methode { get; set; } // Méthode de paiement

        // Clé étrangère vers Reservation
        public int ReservationID { get; set; }

        // Propriété de navigation vers Reservation
        public Reservation ReservationAssociee { get; set; }
    }

}
