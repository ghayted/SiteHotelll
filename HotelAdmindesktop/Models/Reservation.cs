using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdmin.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        public DateTime DateCreation { get; set; } 
        public string Statut { get; set; } 
        public DateTime DateArrivee { get; set; } 
        public DateTime DateDepart { get; set; }

        public decimal Montant { get; set; }
        public bool EstArchive { get; set; } 
        public bool EstPayer { get;set;}
        public int ClientID { get; set; }
        public int ChambreID { get; set; }
        public int EmployeID { get; set; }


        public Client ClientAssocie { get; set; }
        public Chambre ChambreAssociee { get; set; }
        public Employe EmployeAssocie { get; set; }
        public ICollection<Paiement> PaiementsAssocies { get; set; }


    }

}
