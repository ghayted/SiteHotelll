using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace HotelAdmin
{
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void OuvrirClientView_Click(object sender, RoutedEventArgs e)
        {
            // Ouvre la fenêtre de gestion des clients
            var clientView = new ClientView();
            clientView.Show();

            // Optionnel : ferme le menu principal
           
        }
        private void OuvrirReservationView_Click(object sender, RoutedEventArgs e)
        {
            // Ouvre la fenêtre de gestion des clients
            var reservationView = new ReservationMenu();
            reservationView.Show();

            // Optionnel : ferme le menu principal

        }
        private void OuvrirChambreView_Click(object sender, RoutedEventArgs e)
        {
            // Ouvre la fenêtre de gestion des chambres
            var chambreView = new ChambreMenu();
            chambreView.Show();

            
        }

    }
}
