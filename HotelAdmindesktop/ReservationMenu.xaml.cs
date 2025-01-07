using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using HotelAdmin.Data;
using HotelAdmin.Models;
using HotelAdmin.Services;

namespace HotelAdmin
{
    public partial class ReservationMenu : Window
    {
        public ReservationMenu()
        {
            InitializeComponent();
            ChambreTypeComboBox.SelectionChanged += ChambreTypeComboBox_SelectionChanged;
        }
        private void BtnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterPanel.Visibility = Visibility.Visible;      // Affiche la section "Ajout"
            AfficherPanel.Visibility = Visibility.Collapsed;  // Masque la section "Affichage"
            SupprimerPanel.Visibility = Visibility.Collapsed; // Masque la section "Suppression"
        }

        // Lorsque l'utilisateur clique sur "Affichage"
        private void BtnAfficher_Click(object sender, RoutedEventArgs e)
        {
            AjouterPanel.Visibility = Visibility.Collapsed;  // Masque la section "Ajout"
            AfficherPanel.Visibility = Visibility.Visible;    // Affiche la section "Affichage"
            SupprimerPanel.Visibility = Visibility.Collapsed; // Masque la section "Suppression"
        }

        // Lorsque l'utilisateur clique sur "Suppression"
        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            AjouterPanel.Visibility = Visibility.Collapsed;  // Masque la section "Ajout"
            AfficherPanel.Visibility = Visibility.Collapsed;  // Masque la section "Affichage"
            SupprimerPanel.Visibility = Visibility.Visible;   // Affiche la section "Suppression"
        }

        // Gérer la sélection du type de chambre
        private void ChambreTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = ChambreTypeComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                int chambreTypeID = 0;

                if (selectedItem.Content.ToString() == "Suite")
                {
                    chambreTypeID = 1;  // ID pour "Suite"
                }
                else if (selectedItem.Content.ToString() == "Double")
                {
                    chambreTypeID = 2;  // ID pour "Double"
                }

                DateTime? dateArrivee = DateArriveePicker.SelectedDate;
                DateTime? dateDepart = DateDepartPicker.SelectedDate;

                if (dateArrivee.HasValue && dateDepart.HasValue)
                { //test
                    ChargerChambresDisponibles(chambreTypeID, dateArrivee.Value, dateDepart.Value);
                    CalculerPrix(chambreTypeID, dateArrivee.Value, dateDepart.Value);
                }
            }
        }

        // Afficher les chambres disponibles selon le type et les dates
        private void AfficherChambresDisponibles_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ChambreTypeComboBox.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                int chambreTypeID = 0;

                if (selectedItem.Content.ToString() == "Suite")
                {
                    chambreTypeID = 1;  // ID pour "Suite"
                }
                else if (selectedItem.Content.ToString() == "Double")
                {
                    chambreTypeID = 2;  // ID pour "Double"
                }

                DateTime? dateArrivee = DateArriveePicker.SelectedDate;
                DateTime? dateDepart = DateDepartPicker.SelectedDate;

                if (dateArrivee.HasValue && dateDepart.HasValue)
                {
                    ChargerChambresDisponibles(chambreTypeID, dateArrivee.Value, dateDepart.Value);
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner une date d'arrivée et de départ.");
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un type de chambre.");
            }
        }

        // Charger les chambres disponibles pour le type de chambre sélectionné
        private void ChargerChambresDisponibles(int chambreTypeID, DateTime dateArrivee, DateTime dateDepart)
        {
            using (var context = new HotelDbContext())
            {
                // Recherche des chambres disponibles en fonction du type de chambre et des dates
                var chambresDisponibles = context.ChambresDB
     .Where(c =>
         c.TypeChambreID == chambreTypeID &&
         !context.ReservationsDB.Any(r =>
             r.ChambreID == c.ID &&
             ((r.DateArrivee <= dateDepart && r.DateDepart >= dateArrivee) &&
              r.Statut == "Active" // Vérifie que la réservation est active
             )
         )
     )
     .ToList(); ;

                // Mettre à jour le ComboBox avec les chambres disponibles
                AvailableChambresComboBox.Items.Clear();
                if (chambresDisponibles.Any())
                {
                    foreach (var chambre in chambresDisponibles)
                    {
                        AvailableChambresComboBox.Items.Add(new { ChambreID = chambre.ID, Chambrenum = chambre.Numero });
                    }
                }
                else
                {
                    MessageBox.Show("Aucune chambre disponible pour cette période.");
                }
            }
        }

        // Calculer le prix en fonction du type de chambre et de la durée du séjour
        private void CalculerPrix(int chambreTypeID, DateTime dateArrivee, DateTime dateDepart)
        {
            int prixParNuit = chambreTypeID == 1 ? 300 : 150;  // Prix de la chambre (300 MAD pour Suite, 150 MAD pour Double)
            int dureeSejour = (dateDepart - dateArrivee).Days;

            if (dureeSejour > 0)
            {
                int prixTotal = dureeSejour * prixParNuit;
                PrixTextBlock.Text = $"Prix : {prixTotal} MAD";
            }
            else
            {
                PrixTextBlock.Text = "Prix : 0 MAD";
            }
        }

        // Ajouter la réservation
        private void AjouterReservation_Click(object sender, RoutedEventArgs e)
        {
            // Vérifier que tous les champs nécessaires sont remplis
            if (string.IsNullOrEmpty(ClientIDTextBox.Text) || AvailableChambresComboBox.SelectedItem == null ||
                !DateArriveePicker.SelectedDate.HasValue || !DateDepartPicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            var clientt = new ClientService();
            int clientID = int.Parse(ClientIDTextBox.Text);
            var existId = clientt.ClientExisteParID(clientID);
            if (!existId)
            {
                MessageBox.Show("L'ID du client est introuvable.");
                return;
            }

            int chambreID = (AvailableChambresComboBox.SelectedItem as dynamic).ChambreID;
            DateTime dateArrivee = DateArriveePicker.SelectedDate.Value;
            DateTime dateDepart = DateDepartPicker.SelectedDate.Value;

            using (var context = new HotelDbContext())
            {
                // Vérifier l'existence de la chambre
                var chambree = context.ChambresDB.Find(chambreID);
                if (chambree == null)
                {
                    MessageBox.Show("Chambre inexistante.");
                    return;
                }

                // Gestion des clients archivés
                var clientArchiver = context.ClientsDB.FirstOrDefault(c => c.ID == clientID && c.EstArchive == true);
                if (clientArchiver != null)
                {
                    // Demander si l'utilisateur souhaite restaurer le client archivé
                    MessageBoxResult result = MessageBox.Show(
                        "Le client est archivé. Voulez-vous le restaurer ?",
                        "Client archivé",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        clientArchiver.EstArchive = false;
                        context.SaveChanges();
                        MessageBox.Show("Le client a été restauré avec succès.");
                    }
                    else
                    {
                        MessageBox.Show("Opération annulée.");
                        return;
                    }
                }

                // Calculer le montant de la réservation
                int prixParNuit = chambree.TypeChambreID == 1 ? 300 : 150; // 300 MAD pour Suite, 150 MAD pour Double
                int dureeSejour = (dateDepart - dateArrivee).Days;
                int montant = dureeSejour > 0 ? dureeSejour * prixParNuit : 0;

                // Ajouter la réservation dans la base de données
                var reservation = new Reservation
                {
                    ClientID = clientID,
                    ChambreID = chambreID,
                    DateCreation = DateTime.Now,
                    EmployeID = 3,
                    DateArrivee = dateArrivee,
                    DateDepart = dateDepart,
                    Statut = "Active",
                    EstPayer = false,
                    Montant = montant // Enregistrer le montant calculé
                };

                context.ReservationsDB.Add(reservation);
                context.SaveChanges();

                MessageBox.Show($"La réservation a été ajoutée avec succès. Montant total : {montant} MAD.");
                this.Close();
            }
        }






        private void AfficherReservations(List<Reservation> reservations)
        {
            // Efface les éléments précédents dans la ListBox
            ReservationsListBox.Items.Clear();

            // Ajoute chaque réservation à la ListBox
            foreach (var reservation in reservations)
            {
                var affichage =
                    $"ID: {reservation.ID}\n" +
                    $"Client ID: {reservation.ClientID}\n" +
                    $"Date Arrivée: {reservation.DateArrivee:dd/MM/yyyy}\n" +
                    $"Date Départ: {reservation.DateDepart:dd/MM/yyyy}\n" +
                    $"Statut: {reservation.Statut}\n" +
                    $"Payé: {(reservation.EstPayer ? "Oui" : "Non")}\n" +
                    $"Montant: {reservation.Montant} MAD\n" +
                    $"------------------------------";

                ReservationsListBox.Items.Add(affichage);
            }
        }

        private void MarquerCommePayee_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationsListBox.SelectedItem != null)
            {
                var selectedReservation = ReservationsListBox.SelectedItem.ToString();
                int reservationID = ExtractReservationID(selectedReservation);

                using (var context = new HotelDbContext())
                {
                    var reservation = context.ReservationsDB.Find(reservationID);

                    if (reservation != null)
                    {
                        reservation.EstPayer = true; // Marque comme payée
                        context.SaveChanges();
                        MessageBox.Show($"La réservation ID: {reservationID} a été marquée comme payée.");

                        // Rafraîchir les réservations affichées
                        AfficherToutesLesReservations_Click(sender, e); // Appelle la méthode existante pour afficher toutes les réservations
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une réservation.");
            }
        }
        private void MarquerCommeNonPayee_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationsListBox.SelectedItem != null)
            {
                var selectedReservation = ReservationsListBox.SelectedItem.ToString();
                int reservationID = ExtractReservationID(selectedReservation);

                using (var context = new HotelDbContext())
                {
                    var reservation = context.ReservationsDB.Find(reservationID);

                    if (reservation != null)
                    {
                        reservation.EstPayer = false; // Marque comme non payée
                        context.SaveChanges();
                        MessageBox.Show($"La réservation ID: {reservationID} a été marquée comme non payée.");

                        // Rafraîchir les réservations affichées
                        AfficherToutesLesReservations_Click(sender, e); // Appelle la méthode existante pour afficher toutes les réservations
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une réservation.");
            }
        }
        private void AfficherMontantDu_Click(object sender, RoutedEventArgs e)
        {
            if (ReservationsListBox.SelectedItem != null)
            {
                var selectedReservation = ReservationsListBox.SelectedItem.ToString();
                int reservationID = ExtractReservationID(selectedReservation);

                using (var context = new HotelDbContext())
                {
                    var reservation = context.ReservationsDB.Find(reservationID);

                    if (reservation != null)
                    {
                        // Affiche le montant dû dans le TextBlock
                        MontantDuTextBlock.Text = $"Montant Dû : {reservation.Montant} MAD";
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une réservation.");
            }
        }
        private void AfficherPaiementsRegles_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                // Récupère toutes les réservations payées
                var paiementsRegles = context.ReservationsDB
                                             .Where(r => r.EstPayer) // Filtrer celles qui sont payées
                                             .OrderBy(r => r.DateCreation)
                                             .ToList();

                // Affiche les réservations dans la ListBox
                AfficherReservations(paiementsRegles);
            }
        }
        private void AfficherPaiementsEnAttente_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                // Récupère toutes les réservations non payées
                var paiementsEnAttente = context.ReservationsDB
                                                .Where(r => !r.EstPayer) // Filtrer celles qui ne sont pas payées
                                                .OrderBy(r => r.DateCreation)
                                                .ToList();

                // Affiche les réservations dans la ListBox
                AfficherReservations(paiementsEnAttente);
            }
        }

        private int ExtractReservationID(string reservationDetails)
        {
            // Extraction simple de l'ID depuis la chaîne
            var parts = reservationDetails.Split('\n');
            var idLine = parts.FirstOrDefault(line => line.StartsWith("ID:"));
            return int.Parse(idLine.Split(':')[1].Trim());
        }
        private void AfficherTotalMontants_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                // Calculez la somme de tous les montants dans la table des réservations
                var totalMontant = context.ReservationsDB.Sum(r => r.Montant);

                // Affichez le montant total dans le TextBlock
                TotalMontantsTextBlock.Text = $"Total des Montants : {totalMontant} MAD";
            }
        }


        private void AfficherToutesLesReservations_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                var toutesLesReservations = context.ReservationsDB.OrderBy(r => r.DateArrivee).ToList();

                // Appel de la fonction pour afficher toutes les réservations
                AfficherReservations(toutesLesReservations);
            }
        }


        private void TrierReservations_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                var reservationsNonPayees = context.ReservationsDB.OrderBy(r => r.DateCreation).ToList();

                // Appel de la fonction pour afficher les réservations non payées
                AfficherReservations(reservationsNonPayees);
            }
        }

        private void TrierReservationsNonPayeesParDate_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                // Filtrer les réservations non payées et les trier par DateArrivee
                var reservationsNonPayees = context.ReservationsDB
                                                   .Where(r => !r.EstPayer) // Sélectionner uniquement les réservations non payées
                                                   .OrderBy(r => r.DateCreation) // Trier par DateArrivee
                                                   .ToList();

                // Affichage des résultats
                AfficherReservations(reservationsNonPayees);
            }
        }
        private void VoirLesReservationArchiver(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                // Filtrer les réservations non payées et les trier par DateArrivee
                var reservationsActives = context.ReservationsDB
                                                  .Where(r => r.Statut == "Active")
                                                  .OrderBy(r => r.DateCreation)
                                                  .ToList();

                // Affichage des résultats
                AfficherReservations(reservationsActives);
            }
        }

        private void RechercherParClientID_Click(object sender, RoutedEventArgs e)
        {
            int clientID;
            if (int.TryParse(RechercherIDClientTextBox.Text, out clientID))
            {
                using (var context = new HotelDbContext())
                {
                    var reservations = context.ReservationsDB.Where(r => r.ClientID == clientID).ToList();

                    // Affichage des résultats
                    AfficherReservations(reservations);
                }
            }
            else
            {
                MessageBox.Show("Veuillez entrer un ID Client valide.");
            }
        }

        private void SupprimerReservation_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void AfficherReservationsActives_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                // Récupère uniquement les réservations actives
                var reservationsActives = context.ReservationsDB
                                                 .Where(r => r.Statut == "Active")
                                                 .OrderBy(r => r.DateCreation)
                                                 .ToList();

                // Efface les éléments précédents et affiche les nouvelles réservations
                ActiveReservationsListBox.Items.Clear();
                foreach (var reservation in reservationsActives)
                {
                    var affichage =
                        $"ID: {reservation.ID} - Client: {reservation.ClientID} - Chambre: {reservation.ChambreID}\n" +
                        $"Arrivée: {reservation.DateArrivee:dd/MM/yyyy} - Départ: {reservation.DateDepart:dd/MM/yyyy}";
                    ActiveReservationsListBox.Items.Add(affichage);
                }

                if (!reservationsActives.Any())
                {
                    MessageBox.Show("Aucune réservation active trouvée.");
                }
            }
        }
        private void AfficherReservationsNonActives_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new HotelDbContext())
            {
                // Filtrer les réservations avec un statut différent de "Active"
                var reservationsNonActives = context.ReservationsDB
                                                    .Where(r => r.Statut != "Active") // Ici, nous filtrons celles qui ne sont pas "Active"
                                                    .OrderBy(r => r.DateCreation)
                                                    .ToList();

                // Efface les éléments précédents dans la ListBox
                ReservationsListBox.Items.Clear();

                // Ajoute les résultats dans la ListBox
                foreach (var reservation in reservationsNonActives)
                {
                    var affichage =
                        $"ID: {reservation.ID}\n" +
                        $"Client ID: {reservation.ClientID}\n" +
                        $"Date Arrivée: {reservation.DateArrivee:dd/MM/yyyy}\n" +
                        $"Date Départ: {reservation.DateDepart:dd/MM/yyyy}\n" +
                        $"Statut: {reservation.Statut}\n" +
                        $"Payé: {(reservation.EstPayer ? "Oui" : "Non")}\n" +
                        $"Montant: {reservation.Montant} MAD\n" +
                        $"------------------------------";

                    ReservationsListBox.Items.Add(affichage);
                }

                // Message si aucune réservation non active n'est trouvée
                if (!reservationsNonActives.Any())
                {
                    MessageBox.Show("Aucune réservation non active trouvée.");
                }
            }
        }
       

        private void RafraichirReservationsActives_Click(object sender, RoutedEventArgs e)
        {
            RafraichirReservationsActives();
        }
        private void RafraichirReservationsActives()
        {
            using (var context = new HotelDbContext())
            {
                // Filtrer les réservations actives (statut "Actif")
                var reservationsActives = context.ReservationsDB
                    .Where(r => r.Statut == "Actif")
                    .ToList();

                // Efface les anciens éléments.
                ActiveReservationsListBox.Items.Clear();

                // Ajouter chaque réservation active à la ListBox.
                foreach (var reservation in reservationsActives)
                {
                    ActiveReservationsListBox.Items.Add(
                        $"ID: {reservation.ID} | Client ID: {reservation.ClientID} | Arrivée: {reservation.DateArrivee:dd/MM/yyyy} | Départ: {reservation.DateDepart:dd/MM/yyyy}"
                    );
                }
            }
        }
        

        private void SupprimerReservationActive_Click(object sender, RoutedEventArgs e)
        {
            // Vérifie si une réservation est sélectionnée
            if (string.IsNullOrWhiteSpace(ReservationIDTextBox.Text))
            {
                MessageLabel.Content = "Veuillez entrer un ID valide pour supprimer une réservation.";
                return;
            }

            // Essayer de convertir l'ID en entier
            if (!int.TryParse(ReservationIDTextBox.Text, out int id))
            {
                MessageLabel.Content = "L'ID doit être un nombre entier.";
                return;
            }

         
            using (var context = new HotelDbContext())
            {
                var reservation = context.ReservationsDB.Find(id);

                if (reservation == null)
                {
                    MessageBox.Show("Réservation introuvable.");
                    return;
                }

                DateTime today = DateTime.Now;

                if (reservation.DateArrivee <= today && reservation.DateDepart >= today)
                {
                    // Réservation en cours
                    MessageBox.Show("Impossible de supprimer une réservation en cours.");
                }
                else if (reservation.DateDepart < today)
                {
                    reservation.EstArchive = true;
                    reservation.Statut = "Confirmée";
              
           
                    context.SaveChanges();
                    MessageBox.Show("La réservation a été mise à jour comme Confirmée.");
                }
                else if (reservation.DateArrivee > today)
                {
                    // Réservation future : suppression
                    reservation.Statut = "Annulée";
                    context.SaveChanges();
                    MessageBox.Show("La réservation a été supprimée avec succès.");
                }

                // Rafraîchir la liste des réservations actives
                AfficherReservationsActives_Click(sender, e);
            }
        }
    }

    }
