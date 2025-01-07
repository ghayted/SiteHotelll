using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdmin.Models
{
    public class Employe
    {
        public int ID { get; set; }
        public string Nom { get; set; } // Nom de l'employé
        public string Poste { get; set; } // Poste de l'employé

        // Propriété de navigation vers les réservations gérées
        public ICollection<Reservation> ReservationsGerees { get; set; }
    }

}
