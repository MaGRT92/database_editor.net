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
    /// Interaction logic for PlayersWithoutClub.xaml
    /// </summary>
    public partial class PlayersWithoutClub : Window
    {

        private MySqlConnection conn;
        private static bool isSearch = false;
        private static string searchName = "";
       
        private static string playerFirstName = string.Empty;
        private static string playerLastName = string.Empty;
       
       

        public PlayersWithoutClub()
        {
            InitializeComponent();
            transferButton.Visibility = Visibility.Visible;
            string connectionString = "database=sport; user=sslclient;" +
              "Certificate Store Location= CurrentUser; SSL Mode=Required";
            conn = new MySqlConnection(connectionString);

            
            ListboxPlayersWithoutClubFill();
        }

        public PlayersWithoutClub(string _searchName, bool _isSearch) 
        {
            InitializeComponent();
            transferButton.Visibility = Visibility.Hidden;
            string connectionString = "database=sport; user=sslclient;" +
              "Certificate Store Location= CurrentUser; SSL Mode=Required";
            conn = new MySqlConnection(connectionString);

            searchName = _searchName;
            isSearch = _isSearch;
           

            ListboxSearchPlayersFill();
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


        private void ListboxPlayersWithoutClubFill()
        {
            try
            {
                List<lbItems> items = new List<lbItems>();
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Person_First_Name, Person_Last_Name, Position_Name, Player_Price from people inner join positions inner join players on Person_Position_ID = Position_ID and Person_ID = Player_Person_ID and Player_Team_ID is null", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    items.Add(new lbItems() { FirstName = rd.GetString(0), Name = rd.GetString(1), Position = rd.GetString(2), Price = rd.GetString(3)});
                }

                playersWithoutClubListBox.ItemsSource = items;

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

        private void ListboxSearchPlayersFill()
        {
            try
            {
                List<lbItems> items = new List<lbItems>();
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Person_First_Name, Person_Last_Name, Position_Name, Player_Price from people inner join positions inner join players on Person_Position_ID = Position_ID and Person_ID = Player_Person_ID where Person_Last_Name='"+searchName+"' AND Player_Team_ID IS NOT NULL", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    items.Add(new lbItems() { FirstName = rd.GetString(0), Name = rd.GetString(1), Position = rd.GetString(2), Price = rd.GetString(3) });
                }

                playersWithoutClubListBox.ItemsSource = items;

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

        private string GetTeamName(int _teamID)
        {
            string teamName = string.Empty;

            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Team_Name from teams where Team_ID='" + _teamID + "' ", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    teamName = rd.GetString(0);
                }
                return teamName;

            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
                return teamName;
            }
            finally
            {
                CloseConnection();

            }


        }

        private void editPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            int personID = 0;
            string firstName = "";
            string lastName = string.Empty;
            string birthDate = string.Empty;
            string positionName = "";
            int playerPrice = 0;
            int countryID = 0;
            string personImage = string.Empty;
            int playerHeight = 0;
            int teamID = 0;
            int playerRepresentative = 0;
            bool playerIsRep = false;

            if (playersWithoutClubListBox.SelectedItems.Count == 1)
            {
                playerFirstName = playerFirstNameLabel.Content.ToString();
                playerLastName = playerNameLabel.Content.ToString();


                OpenConnection();

                MySqlCommand cmd = new MySqlCommand("Select Person_ID, Person_First_Name, Person_Last_Name, Person_Birth_Date, Position_Name, Player_Price, Person_Country_ID, Person_Image, Height, Player_Representative from people join players join positions on Person_ID = Player_Person_ID AND Person_Position_ID = Position_ID  where Person_ID ='" + GetPersonID(playerFirstName, playerLastName) + "'", conn);
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
                        positionName = rd.GetString(4);
                        playerPrice = rd.GetInt32(5);
                        countryID = rd.GetInt32(6);
                        personImage = rd.GetString(7);
                        playerHeight = rd.GetInt32(8);
                        playerRepresentative = rd.GetInt32(9);
                      
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
                        playerPrice = rd.GetInt32(5);
                        countryID = rd.GetInt32(6);
                        personImage = rd.GetString(7);
                        playerHeight = rd.GetInt32(8);
                        playerRepresentative = rd.GetInt32(9);
                    }
                    CloseConnection();
                }

                


                CloseConnection();
               
                if(playerRepresentative == 1)
                {
                    playerIsRep = true;
                }
                else if(playerRepresentative == 2)
                {
                    playerIsRep = false;
                }

                AddPlayerWindow addPlayerWindow = new AddPlayerWindow( personID, GetCountryName(countryID), firstName, lastName, birthDate, positionName, personImage, playerPrice, playerHeight, playerIsRep, isSearch, true);
                addPlayerWindow.Owner = this;

                addPlayerWindow.Show();


            }
            else
            {
                MessageBox.Show("Prosze zaznaczyć zawodnika");
            }
        }

        private void playersWithoutClubListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(GetPersonID(playerFirstNameLabel.Content.ToString(),playerNameLabel.Content.ToString()).ToString() );
        }

        private void transferButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);
            this.Close();
            FastTranfersWindow fastTranfersWindow = new FastTranfersWindow(true);
            fastTranfersWindow.Show();
            fastTranfersWindow.Topmost = true;
        }
    }

    public class lbItems
    {
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Price { get; set; }
    }

}
