namespace SiteHotel.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public bool EstArchive { get; set; }
    }

}
