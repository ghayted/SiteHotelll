namespace SiteHotel.Models
{
    public class Paiement
    {
        public int ID { get; set; }
        public decimal Montant { get; set; }
        public string ModePaiement { get; set; } = "Carte";

        public DateTime DatePaiement    { get; set; }
        public int ReservationID { get; set; }

       
        
    }
}
