using SiteHotel.Models;

namespace SiteHotel.Controllers
{
    public class ReservationViewModel
    {
        public int ID { get; set; }
        public DateTime DateCreation { get; set; }
        public string Statut { get; set; }
        public DateTime DateArrivee { get; set; }
        public DateTime DateDepart { get; set; }

        public decimal Montant { get; set; }
        public bool EstArchive { get; set; }
        public bool EstPayer { get; set; }
        public int ClientID { get; set; }
        public int ChambreID { get; set; }
        public int EmployeID { get; set; }


        public Client ClientAssocie { get; set; }
        public Chambre ChambreAssociee { get; set; }

        public ICollection<Paiement> PaiementsAssocies { get; set; }
    }
}
