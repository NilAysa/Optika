using System;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Optika_Lens
{
    public partial class AddPatientPage : Page
    {
        string connectionString = "Data Source=DESKTOP-K1UG5OI\\SQLEXPRESS;Initial Catalog=optika_lens;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public AddPatientPage()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string imePrezime = txtImePrezime.Text.Trim();
            string datumRodjenja = txtDatumRodjenja.Text.Trim();
            string brojTelefona = txtBrojTelefona.Text.Trim();

            // Validacija unosa (npr. provjera praznih polja, formata datuma itd.)
            if (string.IsNullOrWhiteSpace(imePrezime) || string.IsNullOrWhiteSpace(datumRodjenja))
            {
                MessageBox.Show("Molimo vas da popunite sva obavezna polja.");
                return;
            }

            // Spašavanje novog pacijenta u bazu podataka
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Korisnik (Ime_Prezime, Datum_rodjenja, Broj_telefona) VALUES (@ImePrezime, @DatumRodjenja, @BrojTelefona); SELECT SCOPE_IDENTITY();";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ImePrezime", imePrezime);
                    command.Parameters.AddWithValue("@DatumRodjenja", datumRodjenja);
                    command.Parameters.AddWithValue("@BrojTelefona", brojTelefona);

                    connection.Open();
                    int newUserId = Convert.ToInt32(command.ExecuteScalar());
                    connection.Close();

                    if (newUserId > 0)
                    {
                        MessageBox.Show("Novi korisnik je uspješno dodan.");

                        // Otvaranje CreateNewRecordPage sa novim korisnikom
                        CreateNewRecordPage createNewRecordPage = new CreateNewRecordPage(newUserId);
                        this.NavigationService.Navigate(createNewRecordPage);
                    }
                    else
                    {
                        MessageBox.Show("Došlo je do greške prilikom spašavanja novog korisnika.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške prilikom spašavanja novog korisnika: " + ex.Message);
            }
        }

        private void TxtGotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Foreground == Brushes.Gray)
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TxtLostKeyboardFocus(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = textBox.Name switch
                {
                    "txtImePrezime" => "Unesite ime i prezime...",
                    "txtDatumRodjenja" => "Unesite datum rođenja (dd.MM.yyyy)...",
                    "txtBrojTelefona" => "Unesite broj telefona...",
                    _ => throw new ArgumentException("Unknown TextBox"),
                };
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void txtDatumRodjenja_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
