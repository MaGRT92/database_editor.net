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
    /// Interaction logic for CoachesWithoutClub.xaml
    /// </summary>
    public partial class CoachesWithoutClub : Window
    {
        private MySqlConnection conn;
        private string searchName = "";

        private static string coachFirstName = string.Empty;
        private static string coachLastName = string.Empty;
        private bool isSearch;

        public CoachesWithoutClub()
        {
            InitializeComponent();
            string connectionString = "database=sport; user=sslclient;" +
              "Certificate Store Location= CurrentUser; SSL Mode=Required";
            conn = new MySqlConnection(connectionString);

            ListboxCoachesWithoutClubFill();
        }

        public CoachesWithoutClub(string _searchName, bool _isSearch)
        {
            InitializeComponent();
            string connectionString = "database=sport; user=sslclient;" +
              "Certificate Store Location= CurrentUser; SSL Mode=Required";
            conn = new MySqlConnection(connectionString);

            searchName = _searchName;
            isSearch = _isSearch;

            ListboxSearchCoachesFill();
        }

        private void OpenConnection()
        {
            try
            {
                conn.Close();
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

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);

            this.Close();
        }
        /// <summary>
        /// Get id person who was selected to edit
        /// </summary>
        /// <param name="_playerFirstName"></param>
        /// <param name="_playerLastName"></param>
        /// <returns></returns>
        private int GetPersonID(string _playerFirstName, string _playerLastName)
        {
            int personId = 0;

            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Person_ID from people where Person_First_Name='" + _playerFirstName + "' AND Person_Last_Name='" + _playerLastName + "'", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    personId = rd.GetInt32(0);
                }
                return personId;

            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
                return personId;
            }
            finally
            {
                CloseConnection();

            }
        }

        private string GetCountryName(int _countryId)
        {
            string countryName = string.Empty;

            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Country_Name from countries where Country_ID='" + _countryId + "' ", conn);
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    CloseConnection();
                    OpenConnection();
                    MySqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        countryName = rd.GetString(0);
                    }
                    CloseConnection();

                }
                else
                {
                    MySqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        countryName = rd.GetString(0);
                    }
                    CloseConnection();
                }



                return countryName;

            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
                return countryName;
            }
            finally
            {
                CloseConnection();

            }
        }

        private void editCoachButton_Click(object sender, RoutedEventArgs e)
        {
            int personID = 0;
            string firstName = "";
            string lastName = string.Empty;
            string birthDate = string.Empty;
            string positionName = "";
            
            int countryID = 0;
            string personImage = string.Empty;
            
            
           

            if (coachesWithoutClubListBox.SelectedItems.Count == 1)
            {
                
                coachFirstName = coachFirstNameLabel.Content.ToString();
                coachLastName = coachNameLabel.Content.ToString();
              

                OpenConnection();

                MySqlCommand cmd = new MySqlCommand("Select Person_ID, Person_First_Name, Person_Last_Name, Person_Birth_Date, Person_Country_ID, Person_Image from people where Person_ID ='" + GetPersonID(coachFirstName, coachLastName) + "'", conn);
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    CloseConnection();
                    OpenConnection();
                    MySqlDataReader rd = cmd.ExecuteReader();


                    while (rd.Read())
                    {
                        personID = rd.GetInt32(0);
                        firstName = rd.GetString(1);
                        lastName = rd.GetString(2);
                        birthDate = rd.GetString(3);
                                   
                        countryID = rd.GetInt32(4);
                        personImage = rd.GetString(5);
                       

                    }
                    CloseConnection();

                }
                else
                {
                    MySqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        personID = rd.GetInt32(0);
                        firstName = rd.GetString(1);
                        lastName = rd.GetString(2);
                        birthDate = rd.GetString(3);
                        positionName = rd.GetString(4);                
                        countryID = rd.GetInt32(5);
                        personImage = rd.GetString(6);
                        
                    }
                    CloseConnection();
                }




                CloseConnection();
               

                AddPlayerWindow addPlayerWindow = new AddPlayerWindow(personID, GetCountryName(countryID), firstName, lastName, birthDate, personImage, true);
                addPlayerWindow.Owner = this;

                addPlayerWindow.Show();


            }
            else
            {
                MessageBox.Show("Prosze zaznaczyć trenera");
            }
        

    }

    private void coachesWithoutClubListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListboxCoachesWithoutClubFill()
        {
            try
            {
                List<lbItems> items = new List<lbItems>();
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Person_First_Name, Person_Last_Name from people inner join coaches on Person_ID = Coach_Person_ID and Coach_Team_ID is null", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    items.Add(new lbItems() {  FirstName = rd.GetString(0), Name = rd.GetString(1) });
                }

                coachesWithoutClubListBox.ItemsSource = items;

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

        private void ListboxSearchCoachesFill()
        {
            try
            {
                List<lbItems> items = new List<lbItems>();
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Person_First_Name, Person_Last_Name from people inner join coaches on Person_ID = Coach_Person_ID where Person_Last_Name='"+searchName+"'", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    items.Add(new lbItems() { FirstName = rd.GetString(0), Name = rd.GetString(1) });
                }

                coachesWithoutClubListBox.ItemsSource = items;

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
    }

   
}
