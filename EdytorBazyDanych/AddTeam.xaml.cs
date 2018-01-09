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
    /// Interaction logic for AddTeam.xaml
    /// </summary>
    public partial class AddTeam : Window
    {
        private MySqlConnection conn;
        private static bool isUpdate = false;
        private static int idCountry = 0;
        private static string countryName = "";
        private static string tournamentName = string.Empty;
        private static int idTournament = 0;
        private static int idTeam;
        private static string teamName = string.Empty;
        private static string teamEstablishment = string.Empty;
        private static string teamLogo = string.Empty;
        private static int idCoach = 0;
        private static string playerLastName = string.Empty;
        private static string playerFirstName = string.Empty;



        /// <summary>
        /// Constructor run methods to fill comboboxes
        /// </summary>
        public AddTeam()
        {
            InitializeComponent();

            string connectionString = "database=sport; user=sslclient;" +
              "Certificate Store Location= CurrentUser; SSL Mode=Required";
            conn = new MySqlConnection(connectionString);

            FillCountryComboBox();
            countryComboBox.SelectedIndex = 0;
            FillCoachComboBox();

           
        }

        public AddTeam(int _idCountry, string _countryName, string _tournamentName, int _idTeam, string _teamName, string _teamEstablishment, string _teamLogo, bool _isUpdate) : this()
        {
            idCountry = _idCountry;
            countryName = _countryName;
            tournamentName = _tournamentName;
            idTeam = _idTeam;
            teamName = _teamName;
            teamEstablishment = _teamEstablishment;
            teamLogo = _teamLogo;
            isUpdate = _isUpdate;
           
            InsertDataInTextboxes();
            AddPlayerToListBox(idTeam);
        }

        private void OpenConnection()
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Close();
                    conn.Open();
                }

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

        private void InsertDataInTextboxes()
        {
            countryComboBox.Text = countryName;
            tournamentComboBox.Text = tournamentName;
            nameTextBox.Text = teamName;
            establishmentTextBox.Text = teamEstablishment;
            logoTextBox.Text = teamLogo;
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

        private void FillTournamentComboBox(int _idCountry)
        {
            try
            {
                tournamentComboBox.Items.Clear();
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Tournament_Name from tournaments where Tournament_Country_ID='" + _idCountry + "' ", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    tournamentComboBox.Items.Add(rd.GetString(0));
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

        private void FillCoachComboBox()
        {
            try
            {
               
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("select Person_Last_Name from people inner join coaches on Person_ID = Coach_Person_ID", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {   
                    coachComboBox.Items.Add(rd.GetString(0));
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

       

        private void AddPlayerToListBox(int _teamID)
        {
            try
            {
                
                List<lbItems> items = new List<lbItems>();
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Person_First_Name, Person_Last_Name, Position_Name, Player_Price from people inner join positions inner join players on Person_Position_ID = Position_ID and Person_ID = Player_Person_ID and Player_Team_ID ='" + _teamID + "'", conn);

                MySqlDataReader rd = cmd.ExecuteReader();
               
                while (rd.Read())
                {
                    
                    items.Add(new lbItems() { FirstName = rd.GetString(0), Name = rd.GetString(1), Position = rd.GetString(2), Price = rd.GetString(3) });
                }

                
                playersClubListBox.ItemsSource = items;

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

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Country_ID from countries where Country_Name ='" + countryComboBox.SelectedItem + "' ", conn);

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
            
            FillTournamentComboBox(idCountry);
        }

        private void tournamentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Tournament_ID from tournaments where Tournament_Name ='" + tournamentComboBox.SelectedItem + "' ", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    idTournament = rd.GetInt32(0);
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

        private void coachComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private string GetQuery(bool _isUpdate)
        {
            string query = "";
           
            if (_isUpdate)
            {
                query = "UPDATE teams SET Team_Name=@Team_Name, Team_Establishment=@Team_Establishment, Team_Logo=@Team_Logo, Team_Tournament_ID=@Team_Tournament_ID where Team_ID = '" + idTeam + "' ";
            }
            else
            {
                query = "INSERT INTO teams(Team_Name, Team_Establishment, Team_Logo, Team_Tournament_ID) VALUES(@Team_Name, @Team_Establishment ,@Team_Logo, @Team_Tournament_ID)";
            }

            return query;
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
                    cmd.CommandText = GetQuery(isUpdate); 
                    cmd.Parameters.AddWithValue("@Team_Tournament_ID", idTournament);
                    cmd.Parameters.AddWithValue("@Team_Name", nameTextBox.Text);
                    cmd.Parameters.AddWithValue("@Team_Establishment", establishmentTextBox.Text);
                    cmd.Parameters.AddWithValue("@Team_Logo", logoTextBox.Text);
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

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);

            this.Close();
        }

        private void tournamentComboBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
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
                MySqlCommand cmd = new MySqlCommand("Select Person_ID from people where Person_First_Name='" + _playerFirstName +  "' AND Person_Last_Name='" + _playerLastName + "'", conn);

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

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    countryName = rd.GetString(0);
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

            if (playersClubListBox.SelectedItems.Count == 1)
            {
                playerFirstName = playerFirstNameLabel.Content.ToString();
                playerLastName = playerNameLabel.Content.ToString();


                OpenConnection();

                MySqlCommand cmd = new MySqlCommand("Select Person_ID, Person_First_Name, Person_Last_Name, Person_Birth_Date, Position_Name, Player_Price, Person_Country_ID, Person_Image, Height, Player_Representative, Player_Team_ID from people join players join positions on Person_ID = Player_Person_ID AND Person_Position_ID = Position_ID where Person_ID ='" + GetPersonID(playerFirstName, playerLastName) + "'", conn);
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
                        teamID = rd.GetInt32(10);

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
                        teamID = rd.GetInt32(10);
                    }
                    CloseConnection();
                }



                
                CloseConnection();
                if (playerRepresentative == 1)
                {
                    playerIsRep = true;
                }
                else
                {
                    playerIsRep = false;
                }
                
                AddPlayerWindow addPlayerWindow = new AddPlayerWindow(personID, GetCountryName(countryID), firstName, lastName, birthDate, positionName, personImage, playerPrice, playerHeight, GetTeamName(teamID), playerIsRep, true);
                addPlayerWindow.Owner = this;

                addPlayerWindow.Show();


            }
            else
            {
                MessageBox.Show("Prosze zaznaczyć zawodnika");
            }
        }

        private void addPlayerCoachButton_Click(object sender, RoutedEventArgs e)
        {
            AddPlayerWindow addPlayerWindow = new AddPlayerWindow();
            addPlayerWindow.Show();
            addPlayerWindow.Topmost = true;

        }

        private void transferButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);
            this.Close();
            FastTranfersWindow fastTranfersWindow = new FastTranfersWindow(false, GetTeamName(idTeam));
            fastTranfersWindow.Show();
            fastTranfersWindow.Topmost = true;
        }
    }

   
}
