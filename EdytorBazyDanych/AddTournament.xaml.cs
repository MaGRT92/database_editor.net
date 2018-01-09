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
using MySql.Data.MySqlClient;

namespace EdytorBazyDanych
{
    /// <summary>
    /// Interaction logic for AddTournament.xaml
    /// </summary>
    public partial class AddTournament : Window
    {
        private MySqlConnection conn;
        private static bool isUpdate = false;
        private static string idTournament = "";
        private static int idCountry;
        private static string countryName = "";

        public AddTournament()
        {
            InitializeComponent();

            string connectionString = "database=sport; user=sslclient;" +
              "Certificate Store Location= CurrentUser; SSL Mode=Required";
            conn = new MySqlConnection(connectionString);
            FillCountryComboBox();
        }

        public AddTournament( int _idCountry, string _countryName ,string _idTournament, string _name, string _logo, bool _isUpdate) : this()
        {
            idCountry = _idCountry;
            idTournament = _idTournament;
            nameTextBox.Text = _name;
            logoTextBox.Text = _logo;
            isUpdate = _isUpdate;
            countryName = _countryName;
            countryComboBox.Text = countryName;

        }

        private void OpenConnection()
        {
            try
            {
                conn.Open();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CloseConnection()
        {
            try
            {
                conn.Close();
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);
        }

        private void FillCountryComboBox()
        {
            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Country_Name from countries order by Country_Name ", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    countryComboBox.Items.Add(rd.GetString(0));
                }


            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        private string GetQuery(bool _isUpdate)
        {
            string query = "";

            if (_isUpdate)
            {
                query = "UPDATE tournaments SET Tournament_Name=@Tournament_Name, Tournament_Logo=@Tournament_Logo, Tournament_Country_ID=@Tournament_Country_ID where Tournament_ID = '" + idTournament + "' ";
            }
            else
            {
                query = "INSERT INTO tournaments(Tournament_Name, Tournament_Logo, Tournament_Country_ID) VALUES(@Tournament_Name, @Tournament_Logo, @Tournament_Country_ID)";
            }

            return query;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);

            this.Close();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);

            MySqlCommand cmd;

            try
            {
                if (countryComboBox.Text != "" && nameTextBox.Text != "" && logoTextBox.Text != "")
                {
                    OpenConnection();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = GetQuery(isUpdate); ;
                    cmd.Parameters.AddWithValue("@Tournament_Country_ID", idCountry);
                    cmd.Parameters.AddWithValue("@Tournament_Name", nameTextBox.Text);
                    cmd.Parameters.AddWithValue("@Tournament_Logo", logoTextBox.Text);
                    cmd.ExecuteNonQuery();


                    this.Close();

                    var myObject = this.Owner as MainWindow;
                    myObject.AddTournaments(countryName);
                }
                else
                {
                    MessageBox.Show("Proszę wypełnić wszystkie pola");
                }
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
                isUpdate = false;
            }
        }

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Country_ID from countries where Country_Name='" + countryComboBox.SelectedItem + "' ", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                   idCountry = Convert.ToInt32(rd.GetString(0));
                }


            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "DELETE FROM tournaments WHERE Tournament_ID ='" + idTournament + "'";

            var dialogResult = MessageBox.Show("Napewno chcesz usunąć wybrany kraj z bazy?", "Usuwanie", MessageBoxButton.YesNo);

            if (dialogResult == MessageBoxResult.Yes)
            {
                if (nameTextBox.Text != "")
                {
                    OpenConnection();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    CloseConnection();

                    this.Close();

                    var myObject = this.Owner as MainWindow;
                    myObject.AddTournaments(countryName);
                }

                else
                {
                    MessageBox.Show("Brak podanej nazwy kraju");
                }
            }
        }
    }
}
