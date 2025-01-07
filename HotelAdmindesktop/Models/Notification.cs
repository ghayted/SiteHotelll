using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAdmin.Models
{
    public class Notification
    {
        public int ID { get; set; }
        public string TypeNotification { get; set; } // Type de notification (ex: "Email", "SMS")
        public string Message { get; set; } // Contenu de la notification
        public DateTime DateEnvoi { get; set; } // Date d'envoi

        // Clé étrangère vers Client
        public int ClientID { get; set; }

        // Propriété de navigation vers Client
        public Client ClientAssocie { get; set; }   
    }

}
