using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using HotelAdmin.Data;
using HotelAdmin.Models;
using System.Windows.Controls;

namespace HotelAdmin
{
    public partial class ChambreMenu : Window
    {
        public ChambreMenu()
        {
            InitializeComponent();
        }

        // Ajouter une chambre
        private void AjouterChambre_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                // Vérifier si le numéro de chambre est valide
                if (string.IsNullOrWhiteSpace(ChambreIDTextBox.Text) || !int.TryParse(ChambreIDTextBox.Text, out int numeroChambre))
                {
                    MessageBox.Show("Veuillez entrer un numéro de chambre valide.");
                    return;
                }

                // Vérifier si la chambre existe déjà
                if (context.ChambresDB.Any(c => c.Numero == numeroChambre))
                {
                    MessageBox.Show($"Le numéro de chambre {numeroChambre} existe déjà.");
                    return;
                }

                // Vérifier les dates d'arrivée et de départ
                DateTime? dateArrivee = DateArriveePicker.SelectedDate;
                DateTime? dateDepart = DateDepartPicker.SelectedDate;

                if (dateArrivee.HasValue && dateDepart.HasValue)
                {
                    var reservationsExistantes = context.ReservationsDB
                        .Where(r => r.ChambreID == numeroChambre && r.DateArrivee < dateDepart && r.DateDepart > dateArrivee)
                        .ToList();

                    if (reservationsExistantes.Any())
                    {
                        MessageBox.Show("Cette chambre est déjà réservée pour cette période.");
                        return;
                    }
                }

                // Récupérer le type de chambre
                var selectedType = TypeChambreComboBox.SelectedItem as ComboBoxItem;
                if (selectedType == null)
                {
                    MessageBox.Show("Veuillez sélectionner un type de chambre.");
                    return;
                }
                int typeID = int.Parse(selectedType.Tag.ToString());

                // Récupérer le statut de la chambre
                var selectedStatut = StatutChambreComboBox.SelectedItem as ComboBoxItem;
                if (selectedStatut == null)
                {
                    MessageBox.Show("Veuillez sélectionner un statut pour la chambre.");
                    return;
                }
                bool estDisponible = selectedStatut.Tag.ToString() == "true";

                // Créer la chambre
                var nouvelleChambre = new Chambre
                {
                    Numero = numeroChambre,
                    TypeChambreID = typeID,
                    EstDisponible = estDisponible
                };

                // Ajouter à la base de données
                context.ChambresDB.Add(nouvelleChambre);
                context.SaveChanges();

                MessageBox.Show($"Chambre ajoutée avec succès ! Numéro: {numeroChambre}");
                AfficherChambres_Click(sender, e); // Rafraîchir la liste
            }
        }


        // Afficher les chambres
        private void AfficherChambres_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                // Charger les chambres avec leurs types associés
                var chambres = context.ChambresDB
                                      .Include(c => c.TypeChambreAssocie)
                                      .ToList();

                // Effacer la liste avant de la recharger
                ChambresListBox.Items.Clear();

                foreach (var chambre in chambres)
                {
                    string statut = chambre.EstDisponible ? "Disponible" : "Occupée";
                    string type = chambre.TypeChambreAssocie?.NomType ?? "Non défini";
                    decimal prix = chambre.TypeChambreAssocie?.Prix ?? 0;

                    // Ajouter les informations dans la ListBox
                    ChambresListBox.Items.Add($"ID: {chambre.ID} | Numéro: {chambre.Numero} | Type: {type} | Prix: {prix} MAD | Statut: {statut}");
                }
            }
        }

        // Supprimer une chambre
        private void SupprimerChambre_Click(object sender, RoutedEventArgs e)
        {
            if (ChambresListBox.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner une chambre à supprimer.");
                return;
            }

            var selectedChambre = ChambresListBox.SelectedItem.ToString();
            int chambreID = ExtractIDFromSelection(selectedChambre);

            using (var context = new HotelDbContext())
            {
                // Vérifier si la chambre est occupée
                var reservationsActuelles = context.ReservationsDB
                    .Where(r => r.ChambreID == chambreID && r.DateDepart > DateTime.Now)
                    .ToList();

                if (reservationsActuelles.Any())
                {
                    MessageBox.Show("La chambre est occupée et ne peut pas être supprimée.");
                    return;
                }

                var chambre = context.ChambresDB.Find(chambreID);
                if (chambre != null)
                {
                    context.ChambresDB.Remove(chambre);
                    context.SaveChanges();
                    MessageBox.Show("Chambre supprimée avec succès !");
                    AfficherChambres_Click(sender, e); // Rafraîchir la liste
                }
                else
                {
                    MessageBox.Show("Chambre non trouvée.");
                }
            }
        }


        // Extraire l'ID de la chambre depuis la sélection
        private int ExtractIDFromSelection(string selection)
        {
            var parts = selection.Split('|')[0].Trim().Split(':');
            return int.Parse(parts[1]);
        }
    }
}
