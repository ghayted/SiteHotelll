using System.ComponentModel.DataAnnotations;

namespace SiteHotel.Models
{
    public class Chambre
    {
        [Key]
        public int ID { get; set; }

        public string Numero { get; set; } // Numéro de la chambre
        public bool Disponibilite { get; set; }
        public int TypeChambreID { get; set; }

        // Propriété de navigation vers TypeChambre
      
    }
}
