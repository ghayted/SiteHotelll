using System.ComponentModel.DataAnnotations;

namespace SiteHotel.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        public DateTime DateCreation { get; set; }

        public string Statut { get; set; } = "Active";
        public DateTime DateArrivee { get; set; }
        public DateTime DateDepart { get; set; }
       
        public decimal Montant { get; set; }
        public bool EstArchive { get; set; }
        public bool EstPayer { get; set; }
        public int ClientID { get; set; }
        public int ChambreID { get; set; }
        public int EmployeID { get; set; }
        public string? CompteEmail { get; set; }
        public Chambre? Chambre { get; set; }

    }


}
