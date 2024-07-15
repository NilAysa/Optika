using System;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Optika_Lens
{
    public partial class SearchPage : Page
    {
        string connectionString = "Data Source=DESKTOP-K1UG5OI\\SQLEXPRESS;Initial Catalog=optika_lens;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public SearchPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Id, Ime_Prezime, Broj_telefona FROM korisnik";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        ObservableCollection<Korisnik> users = new ObservableCollection<Korisnik>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            int index = 1;
                            while (reader.Read())
                            {
                                users.Add(new Korisnik
                                {
                                    Index = index++,
                                    Ime_Prezime = reader["Ime_Prezime"].ToString(),
                                    Broj_Telefona = reader["Broj_telefona"].ToString(),
                                    Id = (int)reader["Id"]
                                });
                            }
                        }
                        lvUsers.ItemsSource = users;
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške prilikom učitavanja korisnika: " + ex.Message);
            }
        }


        private void TxtSearch_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Enter user name...")
            {
                txtSearch.Text = string.Empty;
                txtSearch.Foreground = SystemColors.ControlTextBrush;
            }
        }

        private void TxtSearch_LostKeyboardFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Enter user name...";
                txtSearch.Foreground = Brushes.Gray;
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Ime_Prezime, Broj_telefona FROM korisnik";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    ObservableCollection<Korisnik> searchResults = new ObservableCollection<Korisnik>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int index = 1;
                        while (reader.Read())
                        {
                            string imePrezime = reader["Ime_Prezime"].ToString();
                            if (ImePrezimeContains(imePrezime, searchTerm))
                            {
                                searchResults.Add(new Korisnik
                                {
                                    Index = index++,
                                    Ime_Prezime = imePrezime,
                                    Broj_Telefona = reader["Broj_telefona"].ToString()
                                });
                            }
                        }
                    }
                    lvUsers.ItemsSource = searchResults;
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške prilikom pretrage korisnika: " + ex.Message);
            }
        }

        private bool ImePrezimeContains(string imePrezime, string searchTerm)
        {
            // Podijeli uneseni tekst na dijelove
            string[] parts = searchTerm.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Provjeri da li ime ili prezime počinju sa svakim dijelom unesenog teksta
            foreach (string part in parts)
            {
                bool found = false;
                string[] imePrezimeParts = imePrezime.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string imePrezimePart in imePrezimeParts)
                {
                    if (imePrezimePart.StartsWith(part, StringComparison.OrdinalIgnoreCase))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    return false;
                }
            }
            return true;
        }

        private void BtnShowAll_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        private void LvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvUsers.SelectedItem != null)
            {
                Korisnik selectedUser = (Korisnik)lvUsers.SelectedItem;
                PrikazKorisnickihKartonaPage prikazKartonaPage = new PrikazKorisnickihKartonaPage(selectedUser.Id);
                this.NavigationService.Navigate(prikazKartonaPage);
            }
        }


    }

}
