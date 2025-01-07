using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdmin.Models
{
    public class TypeChambre
    {
        public int ID { get; set; }
        public string NomType { get; set; } // Nom du type de chambre (ex: "Suite", "Double")
        public decimal Prix { get; set; } // Prix par nuit

        // Propriété de navigation vers les chambres associées
        public ICollection<Chambre> ListeChambres { get; set; }
    }
}
