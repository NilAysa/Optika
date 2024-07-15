using System;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Optika_Lens
{
    public partial class CreateNewRecordPage : Page
    {
        string connectionString = "Data Source=DESKTOP-K1UG5OI\\SQLEXPRESS;Initial Catalog=optika_lens;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
        private int userId;

        // Konstruktor koji prima userId kao argument
        public CreateNewRecordPage(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void BtnSaveRecord_Click(object sender, RoutedEventArgs e)
        {
            // Uzimanje vrijednosti unesenih u polja za unos
            string proDistLongaOS = txtProDistLongaOS.Text.Trim();
            string proDistLongaOD = txtProDistLongaOD.Text.Trim();
            string proDistMediaOS = txtProDistMediaOS.Text.Trim();
            string proDistMediaOD = txtProDistMediaOD.Text.Trim();
            string proDistPropriaOS = txtProDistPropriaOS.Text.Trim();
            string proDistPropriaOD = txtProDistPropriaOD.Text.Trim();
            string distPupill = txtDistPupill.Text.Trim();
            string vrstaStakla = txtVrstaStakla.Text.Trim();
            string proizvodjacStakla = txtProizvodjacStakla.Text.Trim();
            string datumPregleda = txtDatumPregleda.Text.Trim();
            string doktor = txtDoktor.Text.Trim();
            string napomena = txtNapomena.Text.Trim();

            // Validacija unosa (prazna polja i sl.)
            if (string.IsNullOrWhiteSpace(proDistLongaOS) || string.IsNullOrWhiteSpace(proDistLongaOD) || string.IsNullOrWhiteSpace(proDistMediaOS) ||
                string.IsNullOrWhiteSpace(proDistMediaOD) || string.IsNullOrWhiteSpace(proDistPropriaOS) || string.IsNullOrWhiteSpace(proDistPropriaOD) ||
                string.IsNullOrWhiteSpace(distPupill) || string.IsNullOrWhiteSpace(vrstaStakla) || string.IsNullOrWhiteSpace(proizvodjacStakla) ||
                string.IsNullOrWhiteSpace(datumPregleda) || string.IsNullOrWhiteSpace(doktor) || string.IsNullOrWhiteSpace(napomena))
            {
                MessageBox.Show("Molimo vas da popunite sva obavezna polja.");
                return;
            }

            // Spašavanje novog kartona u bazu podataka
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Karton (Id_korisnika, Pro_dist_longa_OS, Pro_dist_longa_OD, Pro_dist_media_OS, Pro_dist_media_OD, 
                                                        Pro_dist_propria_OS, Pro_dist_propria_OD, Dist_pupill, Vrsta_stakla, Proizvodjac_stakla, 
                                                        Datum_pregleda, Doktor, Napomena) 
                                     VALUES (@IdKorisnika, @ProDistLongaOS, @ProDistLongaOD, @ProDistMediaOS, @ProDistMediaOD, 
                                             @ProDistPropriaOS, @ProDistPropriaOD, @DistPupill, @VrstaStakla, @ProizvodjacStakla, 
                                             @DatumPregleda, @Doktor, @Napomena)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdKorisnika", userId);
                    command.Parameters.AddWithValue("@ProDistLongaOS", proDistLongaOS);
                    command.Parameters.AddWithValue("@ProDistLongaOD", proDistLongaOD);
                    command.Parameters.AddWithValue("@ProDistMediaOS", proDistMediaOS);
                    command.Parameters.AddWithValue("@ProDistMediaOD", proDistMediaOD);
                    command.Parameters.AddWithValue("@ProDistPropriaOS", proDistPropriaOS);
                    command.Parameters.AddWithValue("@ProDistPropriaOD", proDistPropriaOD);
                    command.Parameters.AddWithValue("@DistPupill", distPupill);
                    command.Parameters.AddWithValue("@VrstaStakla", vrstaStakla);
                    command.Parameters.AddWithValue("@ProizvodjacStakla", proizvodjacStakla);
                    command.Parameters.AddWithValue("@DatumPregleda", datumPregleda);
                    command.Parameters.AddWithValue("@Doktor", doktor);
                    command.Parameters.AddWithValue("@Napomena", napomena);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Novi karton je uspješno dodan.");
                        // Ovdje možete dodati logiku za resetovanje polja za unos
                    }
                    else
                    {
                        MessageBox.Show("Došlo je do greške prilikom spašavanja novog kartona.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške prilikom spašavanja novog kartona: " + ex.Message);
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
                    "txtProDistLongaOS" => "Pro_dist_longa_OS...",
                    "txtProDistLongaOD" => "Pro_dist_longa_OD...",
                    "txtProDistMediaOS" => "Pro_dist_media_OS...",
                    "txtProDistMediaOD" => "Pro_dist_media_OD...",
                    "txtProDistPropriaOS" => "Pro_dist_propria_OS...",
                    "txtProDistPropriaOD" => "Pro_dist_propria_OD...",
                    "txtDistPupill" => "Dist_pupill...",
                    "txtVrstaStakla" => "Vrsta_stakla...",
                    "txtProizvodjacStakla" => "Proizvodjac_stakla...",
                    "txtDatumPregleda" => "Datum_pregleda (dd.MM.yyyy)...",
                    "txtDoktor" => "Doktor...",
                    "txtNapomena" => "Napomena...",
                    _ => throw new ArgumentException("Unknown TextBox"),
                };
                textBox.Foreground = Brushes.Gray;
            }
        }
    }
}
