namespace SiteHotel.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  // Ne pas stocker le mot de passe en clair
    }

}
