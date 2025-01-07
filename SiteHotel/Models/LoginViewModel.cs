using System.ComponentModel.DataAnnotations;
using SiteHotel.Models;
using SiteHotel.Controllers;
namespace SiteHotel.Models
{
    public class LoginViewModel
    {
        // Propriété pour l'email
        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
        public string Email { get; set; }

        // Propriété pour le mot de passe
        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
