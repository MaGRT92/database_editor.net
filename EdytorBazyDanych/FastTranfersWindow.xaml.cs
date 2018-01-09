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
    /// Interaction logic for FastTranfersWindow.xaml
    /// </summary>
    public partial class FastTranfersWindow : Window
    {
        private MySqlConnection conn;
        private static string countryName;
        private static string tournamentName;
        private static string teamName = "";
        private bool isWithoutClub = false;
        private static string mainTeamName;

        private string playerFirstName;
        private string playerLastName;
        private int personID;

        public FastTranfersWindow()
        {
            InitializeComponent();

            string connectionString = "database=sport; user=sslclient;" +
              "Certificate Store Location= CurrentUser; SSL Mode=Required";
            conn = new MySqlConnection(connectionString);

            FillCountryComboBox();
          

        }

        public FastTranfersWindow(bool _isWithoutClub) :this()
        {
            this.isWithoutClub = _isWithoutClub;
            AddPlayerToRightListBox();
        }

        public FastTranfersWindow(bool _isWithoutClub, string _mainTeamName) : this()
        {
            this.isWithoutClub = _isWithoutClub;
            mainTeamName = _mainTeamName;
            AddPlayerToRightListBox();
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
                team_ComboBox.Items.Clear();
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

                rd.Dispose();
                playersListBox.ItemsSource = items;

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

        private string GetQuery(bool _isWithoutClub)
        {
            string query = "";

            if (_isWithoutClub)
            {
                query = "Select Person_First_Name, Person_Last_Name, Position_Name, Player_Price from people inner join positions inner join players on Person_Position_ID = Position_ID and Person_ID = Player_Person_ID and Player_Team_ID is null";
            }
            else
            {
                query = "Select Person_First_Name, Person_Last_Name, Position_Name, Player_Price from people inner join positions inner join players on Person_Position_ID = Position_ID and Person_ID = Player_Person_ID and Player_Team_ID ='" + GetTeamID(mainTeamName) + "'";
            }

            return query;
        }

        private void AddPlayerToRightListBox()
        {
            try
            {

                List<lbItems> items = new List<lbItems>();
                
                MySqlCommand cmd = new MySqlCommand(GetQuery(isWithoutClub), conn);

                OpenConnection();
                MySqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {

                        items.Add(new lbItems() { FirstName = rd.GetString(0), Name = rd.GetString(1), Position = rd.GetString(2), Price = rd.GetString(3) });
                    }
                   // CloseConnection();

              


                


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

        //**** Pobranie ID kraju na podstawie jego nazwy **********
        private int GetCountryID()
        {
            int countryId = 0;

            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Country_ID from countries where Country_Name='" + countryName + "' ", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    countryId = rd.GetInt32(0);
                }
                return countryId;

            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
                return countryId;
            }
            finally
            {
                CloseConnection();

            }
        }

        private string GetTournamentName()
        {
            string tournamentName = "";

            try
            {
                if (tournamentComboBox.Text != null)
                    tournamentName = tournamentComboBox.SelectedItem.ToString();
                return tournamentName;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                return tournamentName;
            }
            finally
            {
                CloseConnection();

            }
        }

        private int GetTeamID(string _teamName)
        {
            int teamID = 0;

            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Team_ID from teams where Team_Name='" + _teamName + "' ", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    teamID = rd.GetInt32(0);
                }
                return teamID;

            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
                return teamID;
            }
            finally
            {
                CloseConnection();

            }


        }

        //******** Dodawanie druzyn do listbox *********

        private void AddTeams(string _TournamentName)
        {
            team_ComboBox.Items.Clear();

            OpenConnection();
            MySqlCommand cmd = new MySqlCommand("Select distinct Team_Name from teams join tournaments where Team_Tournament_ID =(select Tournament_ID from tournaments where Tournament_Name ='" + _TournamentName + "')", conn);

            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                team_ComboBox.Items.Add(rd.GetString(0));
            }

            CloseConnection();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            countryName = countryComboBox.SelectedItem.ToString();
            FillTournamentComboBox(GetCountryID());
        }

        private void tournamentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tournamentComboBox.SelectedIndex > -1)
            {

                tournamentName = GetTournamentName();
                if (tournamentName != "")
                    AddTeams(tournamentName);
            }
        }

        private void team_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            teamName = team_ComboBox.SelectedItem.ToString();

            if (team_ComboBox.SelectedIndex > -1)
            {
                if (teamName != "")
                    AddPlayerToListBox(GetTeamID(teamName));
            }
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
        

        private void transferToTeamButton_Click(object sender, RoutedEventArgs e)
        {
            if (playersListBox.SelectedItems.Count == 1 && teamName != "")
            {
                personID = GetPersonID(playerFirstName, playerLastName);

                MySqlCommand cmd;

                try
                {

                    OpenConnection();


                    cmd = conn.CreateCommand();

                    cmd.CommandText = "Update players SET Player_Team_ID=@Player_Team_ID where Player_Person_ID='" + personID + "'" ;
                    if(isWithoutClub)
                    {
                        cmd.Parameters.AddWithValue("@Player_Team_ID", null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Player_Team_ID", GetTeamID(mainTeamName));
                    }
                   

                    if (conn.State == System.Data.ConnectionState.Closed)
                    {
                        CloseConnection();
                        OpenConnection();
                        cmd.ExecuteNonQuery();
                        CloseConnection();

                    }
                    else
                    {
                        cmd.ExecuteNonQuery();
                        CloseConnection();
                    }


                }
                catch (MySqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
                finally
                {

                    CloseConnection();
                    AddPlayerToRightListBox();
                    AddPlayerToListBox(GetTeamID(teamName));

                }

            }
            else
            {
                MessageBox.Show("Proszę zaznaczyć zawodnika i/lub wybrać drużyne");
            }
        }

        private void tranfertFromTeamButton_Click(object sender, RoutedEventArgs e)
        {
            if (playersClubListBox.SelectedItems.Count == 1 && teamName != "" && team_ComboBox.Text != "")
            {
                 personID = GetPersonID(playerFirstName, playerLastName);

                MySqlCommand cmd;

                try
                {
                    
                        OpenConnection();


                        cmd = conn.CreateCommand();

                        cmd.CommandText = "Update players SET Player_Team_ID=@Player_Team_ID where Player_Person_ID='"+personID+"'";
                        cmd.Parameters.AddWithValue("@Player_Team_ID", GetTeamID(teamName));
                        
                        if (conn.State == System.Data.ConnectionState.Closed)
                        {
                            CloseConnection();
                            OpenConnection();
                            cmd.ExecuteNonQuery();
                            CloseConnection();

                        }
                        else
                        {
                            cmd.ExecuteNonQuery();
                            CloseConnection();
                        }
               
                    
                }
                catch (MySqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
                finally
                {

                    CloseConnection();
                    AddPlayerToRightListBox();
                    AddPlayerToListBox(GetTeamID(teamName));
                }

            }
            else
            {
                MessageBox.Show("Proszę zaznaczyć zawodnika i/lub wybrać drużyne");
            }
        }

        private void playersClubListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (playersClubListBox.SelectedIndex != -1)
            {
                playerFirstName = firstNameRightListboxLabel.Content.ToString();
                playerLastName = lastNameRightListboxLabel.Content.ToString();
            }
        }

        private void playersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (playersListBox.SelectedIndex != -1)
            {
                playerFirstName = firstNameLeftListboxlabel.Content.ToString();
                playerLastName = lastNameLeftListboxlabel.Content.ToString();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
