using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdmin.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Nom { get; set; } 
        public string Email { get; set; } 
        public string Telephone { get; set; } 
        public string Adresse { get; set; } 
        public bool EstArchive { get; set; } = false;

  
        public ICollection<Reservation> ReservationsAssociees { get; set; }
        public ICollection<Notification> NotificationsAssociees { get; set; }

    }

}
