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
using System.IO;

namespace EdytorBazyDanych
{
    /// <summary>
    /// Interaction logic for AddCountry.xaml
    /// </summary>
    public partial class AddCountry : Window
    {
        private MySqlConnection conn;
        private static bool isUpdate = false;
        private static string id = ""; 
       

        public AddCountry()
        {
            InitializeComponent();

            string connectionString = "database=sport; user=sslclient;" +
               "Certificate Store Location= CurrentUser; SSL Mode=Required";
            conn = new MySqlConnection(connectionString);

        }

        public AddCountry(string _id, string _Name, string _Flag, string _Logo , bool _isUpdate) : this()
        {
            id = _id;
            NameTextBox.Text = _Name;
            FlagTextBox.Text = _Flag;
            LogoTextBox.Text = _Logo;
            isUpdate = _isUpdate;

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


        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);
           
            this.Close();
           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);
        }

        private string GetQuery(bool _isUpdate)
        {
            string query = "";

            if (_isUpdate)
            {
                query = "UPDATE countries SET Country_Name=@Country_Name, Country_Flag=@Country_Flag, Country_Association_Flag=@Country_Association_Flag where Country_ID = '" + id + "' ";
            }
            else
            {
                query = "INSERT INTO countries(Country_Name, Country_Flag, Country_Association_Flag) VALUES(@Country_Name, @Country_Flag, @Country_Association_Flag)";
            }

            return query;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);
            
            MySqlCommand cmd;
   
        try
            {
                if (NameTextBox.Text != "" && FlagTextBox.Text != "" && LogoTextBox.Text != "")
                {
                    OpenConnection();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = GetQuery(isUpdate); ;
                    cmd.Parameters.AddWithValue("@Country_Name", NameTextBox.Text);
                    cmd.Parameters.AddWithValue("@Country_Flag", FlagTextBox.Text);
                    cmd.Parameters.AddWithValue("@Country_Association_Flag", LogoTextBox.Text);
                    cmd.ExecuteNonQuery();


                    this.Close();

                    var myObject = this.Owner as MainWindow;
                    myObject.AddCountries();
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

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            string query = "DELETE FROM countries WHERE Country_ID ='" + id + "'";

            var dialogResult = MessageBox.Show("Napewno chcesz usunąć wybrany kraj z bazy?", "Usuwanie", MessageBoxButton.YesNo);

            if (dialogResult == MessageBoxResult.Yes)
            {
                if (NameTextBox.Text != "")
                {
                    OpenConnection();
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                    CloseConnection();

                    this.Close();

                    var myObject = this.Owner as MainWindow;
                    myObject.AddCountries();
                }

                else
                {
                    MessageBox.Show("Brak podanej nazwy kraju");
                }
            }
        }
    }
}
