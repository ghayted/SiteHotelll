using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteHotel.Data;
using Microsoft.AspNetCore.Mvc;
using SiteHotel.Models;
using System.Linq;
namespace SiteHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly HotelData2 _context;

        public HomeController(HotelData2 context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            return View(); // Affiche la page principale Index
        }

        [HttpPost]
        public IActionResult Index(DateTime startDate, DateTime endDate, int adults)
        {
            var chambresDisponibles = _context.ChambreDB
                .Where(c =>
                    c.Disponibilite &&
                    !_context.ReservationDB.Any(r =>
                        r.ChambreID == c.ID &&
                        ((r.DateArrivee <= endDate && r.DateDepart >= startDate) && r.Statut == "Active")
                    )
                )
                .ToList();

            ViewBag.StartDate = startDate;
            ViewBag.EndDate = endDate;
            ViewBag.Guests = adults;

            return View("~/Views/Reservation/Reservation.cshtml", chambresDisponibles);
        }
    }
}