using System;
using System.Collections.ObjectModel;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Optika_Lens
{
    public partial class PrikazKorisnickihKartonaPage : Page
    {
        string connectionString = "Data Source=DESKTOP-K1UG5OI\\SQLEXPRESS;Initial Catalog=optika_lens;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        public PrikazKorisnickihKartonaPage(int korisnikId)
        {
            InitializeComponent();
            LoadKartoni(korisnikId);
        }

        private void LoadKartoni(int korisnikId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Karton WHERE Id_korisnika = @Id_korisnika";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id_korisnika", korisnikId);
                        connection.Open();
                        ObservableCollection<Karton> kartoni = new ObservableCollection<Karton>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Karton karton = new Karton
                                {
                                    Id = (int)reader["Id"],
                                    Id_korisnika = (int)reader["Id_korisnika"],
                                    Pro_dist_longa_OS = ReadNullableString(reader, "Pro_dist_longa_OS"),
                                    Pro_dist_longa_OD = ReadNullableString(reader, "Pro_dist_longa_OD"),
                                    Pro_dist_media_OS = ReadNullableString(reader, "Pro_dist_media_OS"),
                                    Pro_dist_media_OD = ReadNullableString(reader, "Pro_dist_media_OD"),
                                    Pro_dist_propria_OS = ReadNullableString(reader, "Pro_dist_propria_OS"),
                                    Pro_dist_propria_OD = ReadNullableString(reader, "Pro_dist_propria_OD"),
                                    Dist_pupill = ReadNullableString(reader, "Dist_pupill"),
                                    Vrsta_stakla = ReadNullableString(reader, "Vrsta_stakla"),
                                    Proizvodjac_stakla = ReadNullableString(reader, "Proizvodjac_stakla"),
                                    Datum_pregleda = ReadNullableString(reader, "Datum_pregleda"),
                                    Doktor = ReadNullableString(reader, "Doktor"),
                                    Napomena = ReadNullableString(reader, "Napomena")
                                };

                                kartoni.Add(karton);
                            }
                        }
                        lvKartoni.ItemsSource = kartoni;
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Došlo je do greške prilikom učitavanja kartona: " + ex.Message);
            }
        }

        private string ReadNullableString(SqlDataReader reader, string columnName)
        {
            object value = reader[columnName];
            return value == DBNull.Value ? null : value.ToString();
        }
    }
}
