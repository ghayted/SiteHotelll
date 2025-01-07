using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteHotel.Models;
using SiteHotel.Services;

namespace SiteHotel.Controllers
{
    public class CompteController : Controller
    {
        private readonly CompteService _compteService;

        public CompteController(CompteService compteService)
        {
            _compteService = compteService;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _compteService.Authentification(loginViewModel.Email, loginViewModel.Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Identifiants invalides.");
                }
            }

            return View(loginViewModel);
        }


      [HttpPost]
public IActionResult Register(RegisterViewModel registerViewModel)
{
    if (!ModelState.IsValid)
    {
        return View(registerViewModel);
    }

    // Vérifier si l'utilisateur existe déjà
    var existingUser = _compteService.Verficication(registerViewModel.Email);
    if (existingUser != null)
    {
        ModelState.AddModelError("Email", "Cet email est déjà utilisé.");
        return View(registerViewModel);
    }

    // Créer un nouvel utilisateur
    var newUser = new Login
    {
        Email = registerViewModel.Email,
        Password = registerViewModel.Password // ATTENTION : Ne pas stocker le mot de passe en clair !
    };
            bool emailEnvoye = _compteService.EnvoyerEmailBienvenue(registerViewModel.Email);
            if (!emailEnvoye)
            {
                ModelState.AddModelError("Email", "echec de l'envoi.");
                return View(registerViewModel);
            }
            else
            {
                _compteService.RegisterUser(newUser);
               
                TempData["SuccessMessage"] = "Votre compte a été créé avec succès et un email de bienvenue a été envoyé."; 
               
            }
         
    return RedirectToAction("Login", "Compte");
}

    }
}
