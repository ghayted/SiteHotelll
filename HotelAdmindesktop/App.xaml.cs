using System.Configuration;
using System.Data;
using System.Windows;


namespace HotelAdmin
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Démarre avec le menu principal
            var mainMenu = new MainMenu();
            mainMenu.Show();
        }
    }
}