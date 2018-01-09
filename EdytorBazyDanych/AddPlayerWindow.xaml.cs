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
    /// Interaction logic for AddPlayerWindow.xaml
    /// </summary>
    public partial class AddPlayerWindow : Window
    {
        private MySqlConnection conn;
        private static bool isUpdate = false;
        private static string nationalityName = string.Empty;
        private static string positionName = string.Empty;
        private static int idCountry = 0;
        private static string countryName = "";
        private static string tournamentName = string.Empty;
        private static int idTournament = 0;
        private static int idTeam = 0;
        private static string teamName = string.Empty;
        private static bool isCoach = false;
        private static bool isRepresentative = false;
        private static bool hasTeam = true;
        private bool isSearch = false;
        private bool hasPlayerTeamID = false;

        private static int personID;
        private string firstName;
        private string lastName;
        private string birthDate;
        private string positionName1;
        private string personImage;
        private int playerPrice;
        private int playerHeight;
        private bool playerIsRep;
       

        public AddPlayerWindow()
        {
            InitializeComponent();

            string connectionString = "database=sport; user=sslclient;" +
              "Certificate Store Location= CurrentUser; SSL Mode=Required";
            conn = new MySqlConnection(connectionString);

            FillCountryComboBox();
            FillPositionComboBox();

           
        }

        public AddPlayerWindow(int _personId, string _countryName, string firstName, string lastName, string birthDate, string personImage, bool _isUpdate) : this()
        {
            personID = _personId;
            countryName = _countryName;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
           
            this.personImage = personImage;
            
            
            isUpdate = _isUpdate;

            coachCheckBox.IsChecked = true;

            InsertDataInTextboxes();

            heightTextBox.Text = "";
            priceTextBox.Text = "";
        }

        public AddPlayerWindow(int _personId,string _countryName, string firstName, string lastName, string birthDate, string positionName1, string personImage, int playerPrice, int playerHeight, string _teamName, bool _playerIsRep, bool _isUpdate) : this()
        {
            personID = _personId;
            countryName = _countryName;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
            this.positionName1 = positionName1;
            this.personImage = personImage;
            this.playerPrice = playerPrice;
            this.playerHeight = playerHeight;
            playerIsRep = _playerIsRep;
            isUpdate = _isUpdate;
            teamName = _teamName;
            isSearch = false;
            if (playerIsRep)
            {
                representativeCheckBox.IsChecked = true;
            }
            else
            {
                representativeCheckBox.IsChecked = false;
            }

            InsertDataInTextboxes();
        }

        public AddPlayerWindow( int _personID, string _countryName, string firstName, string lastName, string birthDate, string positionName1, string personImage, int playerPrice, int playerHeight, bool _playeIsRep, bool _isSearch, bool _isUpdate) : this()
        {
            personID = _personID;
            countryName = _countryName;
            this.firstName = firstName;
            this.lastName = lastName;
            this.birthDate = birthDate;
            this.positionName1 = positionName1;
            this.personImage = personImage;
            this.playerPrice = playerPrice;
            this.playerHeight = playerHeight;
            isUpdate = _isUpdate;
            isSearch = _isSearch;
            
            playerIsRep = _playeIsRep;

            if (!isSearch)
            {
                withoutClubCheckBox.IsChecked = true;
            }
            InsertDataInTextboxes();
            if (playerIsRep)
            {
                representativeCheckBox.IsChecked = true;
            }
            else
            {
                representativeCheckBox.IsChecked = false;
            }
           
        }

        private void InsertDataInTextboxes()
        {
            countryComboBox.Text = countryName;
            first_Name_TextBox.Text = firstName;
            last_Name_TextBox.Text = lastName;
            birthDatePicker.Text = birthDate;
            image_TextBox.Text = personImage;
            positionComboBox.Text = positionName1;
            heightTextBox.Text = playerHeight.ToString();
            priceTextBox.Text = playerPrice.ToString();
           
            team_ComboBox.Text = teamName;
          
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

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);

            this.Close();
        }

        private void FillPositionComboBox()
        {
            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Position_Name from positions", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    positionComboBox.Items.Add(rd.GetString(0));                 
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
                    country_Team_ComboBox.Items.Add(rd.GetString(0));
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

        private int GetCountryIDPerson()
        {
            int countryId = 0;

            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Country_ID from countries where Country_Name='" + nationalityName + "' ", conn);

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

        private int GetPositionID()
        {
            int positionId = 0;
            if (isCoach)
            {
                return 13;
            }
            else
            {
             try
                {
                    OpenConnection();
                    MySqlCommand cmd = new MySqlCommand("Select Position_ID from positions where Position_Name='" + positionName + "' ", conn);

                    MySqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        positionId = rd.GetInt32(0);
                    }
                    return positionId;

                }
                catch (MySqlException ex)
                {

                    MessageBox.Show(ex.Message);
                    return positionId;
                }
                finally
                {
                    CloseConnection();

                }
            }
        }

        private int GetPersonID()
        {
            int personId = 0;

            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Person_ID from people where Person_First_Name='" + first_Name_TextBox.Text + "' AND Person_Last_Name ='" + last_Name_TextBox.Text + "' ", conn);

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

        private string GetIfPlayerRepresentative()
        {
            string rep = null;

            if(isRepresentative)
            {
                rep = "1";
            }
            else
            {
                rep = "2";
            }

            return rep;

        }

        private string GetTeamID()
        {
            string teamId = null;
            if (hasTeam)
            {
                try
                {
                    OpenConnection();
                    MySqlCommand cmd = new MySqlCommand("Select Team_ID from teams where Team_Name='" + team_ComboBox.Text + "' ", conn);

                    MySqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        teamId = rd.GetString(0);
                    }
                    return teamId;

                }
                catch (MySqlException ex)
                {

                    MessageBox.Show(ex.Message);
                    return teamId;
                }
                finally
                {
                    CloseConnection();

                }
            }
            else
            {
                teamId = null;
                return teamId;
            }
        }


        private string GetTournamentName()
        {
            string tournamentName = "";

            try
            {
                if(tournament_ComboBox.Text != null)
                tournamentName = tournament_ComboBox.SelectedItem.ToString();
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

        private void FillTournamentComboBox(int _idCountry)
        {
            try
            {
                tournament_ComboBox.Items.Clear();
                team_ComboBox.Items.Clear();
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Tournament_Name from tournaments where Tournament_Country_ID='" + _idCountry + "' ", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    tournament_ComboBox.Items.Add(rd.GetString(0));
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


        private void countryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            nationalityName = countryComboBox.SelectedItem.ToString();
            
        }

        private void country_Team_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (country_Team_ComboBox.Text != null)
            {
                countryName = country_Team_ComboBox.SelectedItem.ToString();
                if (countryName != "")
                {
                    idCountry = GetCountryID();
                    FillTournamentComboBox(idCountry);
                }
            }
        }

        private void tournament_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tournament_ComboBox.SelectedIndex > -1)
            {
                
                tournamentName = GetTournamentName();
                if(tournamentName != "")
                AddTeams(tournamentName);
            }
        }


        private void coachCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            representativeCheckBox.IsEnabled = false;
            positionComboBox.IsEnabled = false;
            heightTextBox.IsEnabled = false;
            priceTextBox.IsEnabled = false;
            isCoach = true;
            label10.Visibility = Visibility.Hidden;
            heightTextBox.Visibility = Visibility.Hidden;
            label11.Visibility = Visibility.Hidden;
            priceTextBox.Visibility = Visibility.Hidden;
            
        }

        private void coachCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            representativeCheckBox.IsEnabled = true;
            positionComboBox.IsEnabled = true;
            heightTextBox.IsEnabled = true;
            priceTextBox.IsEnabled = true;
            isCoach = false;
            label10.Visibility = Visibility.Visible;
            heightTextBox.Visibility = Visibility.Visible;
            label11.Visibility = Visibility.Visible;
            priceTextBox.Visibility = Visibility.Visible;
           
        }

        private void representativeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            coachCheckBox.IsEnabled = false;
            isRepresentative = true;
        }

        private void representativeCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            coachCheckBox.IsEnabled = true;
            isRepresentative = false;
        }

        private void withoutClubCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            country_Team_ComboBox.IsEnabled = false;
            tournament_ComboBox.IsEnabled = false;
            team_ComboBox.IsEnabled = false;
            hasTeam = false;
        }

        private void withoutClubCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            country_Team_ComboBox.IsEnabled = true;
            tournament_ComboBox.IsEnabled = true;
            team_ComboBox.IsEnabled = true;
            hasTeam = true;
        }

        private bool checkTextbox()
        {
            if (isCoach)
            {
                if (countryComboBox.Text != "" && first_Name_TextBox.Text != "" && last_Name_TextBox.Text != "" && image_TextBox.Text != "")
                    return true;
                else
                    return false;
            }
            else if (countryComboBox.Text != "" && first_Name_TextBox.Text != "" && last_Name_TextBox.Text != "" && image_TextBox.Text != "" && heightTextBox.Text != "" && priceTextBox.Text != "")
                return true;
            else
                return false;

        }

        private void InsertPerson()
        {
            MySqlCommand cmd;
           
         try
            {
                if (checkTextbox())
                {
                    OpenConnection();


                    cmd = conn.CreateCommand();
                    
                        cmd.CommandText = GetQueryPerson(isUpdate);
                        cmd.Parameters.AddWithValue("@Person_First_Name", first_Name_TextBox.Text);
                        cmd.Parameters.AddWithValue("@Person_Last_Name", last_Name_TextBox.Text);
                        cmd.Parameters.AddWithValue("@Person_Birth_Date", birthDatePicker.SelectedDate.Value.ToString("yyyy/MM/dd"));
                        cmd.Parameters.AddWithValue("@Person_Position_ID", GetPositionID());
                        cmd.Parameters.AddWithValue("@Person_Country_ID", GetCountryIDPerson());
                        cmd.Parameters.AddWithValue("@Person_Image", image_TextBox.Text);
                    if(conn.State == System.Data.ConnectionState.Closed)
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

                    if (isCoach)
                    {
                        InsertCoach();
                    }
                    else
                    {
                        InsertPlayer();
                    }


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
                
            }

            
        }

        private void InsertPlayer()
        {
            MySqlCommand cmd;

            try
            {
                if (countryComboBox.Text != "" && first_Name_TextBox.Text != "" && last_Name_TextBox.Text != "" && image_TextBox.Text != "" && heightTextBox.Text != "" && priceTextBox.Text != "" && positionComboBox.Text != "")
                {
                    OpenConnection();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = GetQueryPlayer(isUpdate);
                    cmd.Parameters.AddWithValue("@Player_Person_ID", GetPersonID());
                    cmd.Parameters.AddWithValue("@Height", heightTextBox.Text);
                    cmd.Parameters.AddWithValue("@Player_Price", priceTextBox.Text);
                    cmd.Parameters.AddWithValue("@Player_Representative", GetIfPlayerRepresentative());
                    cmd.Parameters.AddWithValue("@Player_Team_ID", GetTeamID());
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


                    this.Close();

                   
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

        private void InsertCoach()
        {
            MySqlCommand cmd;

            try
            {
                if (countryComboBox.Text != "" && first_Name_TextBox.Text != "" && last_Name_TextBox.Text != "" && image_TextBox.Text != "" )
                {
                    OpenConnection();
                    cmd = conn.CreateCommand();
                    cmd.CommandText = GetQueryCoach(isUpdate);
                    cmd.Parameters.AddWithValue("@Coach_Person_ID", GetPersonID());
                    cmd.Parameters.AddWithValue("@Coach_Team_ID", GetTeamID());
                    //cmd.Parameters.AddWithValue("@Coach_Country_ID", GetCountryID());
                    

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

                    this.Close();


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

        private string GetQueryPerson(bool _isUpdate)
        {
            string query = "";

            if (_isUpdate)
            {
                query = "UPDATE people SET Person_First_Name=@Person_First_Name, Person_Last_Name=@Person_Last_Name, Person_Birth_Date=@Person_Birth_Date, Person_Position_ID=@Person_Position_ID, Person_Country_ID=@Person_Country_ID, Person_Image=@Person_Image where Person_ID = '" + personID + "' ";
            }
            else
            {
                query = "INSERT INTO people(Person_First_Name, Person_Last_Name, Person_Birth_Date, Person_Position_ID, Person_Country_ID, Person_Image) VALUES(@Person_First_Name, @Person_Last_Name ,@Person_Birth_Date, @Person_Position_ID, @Person_Country_ID, @Person_Image)";
            }

            return query;
        }

        private string GetQueryPlayer(bool _isUpdate)
        {
            string query = "";

            if (_isUpdate)
            {
                query = "UPDATE players SET Height=@Height, Player_Price=@Player_Price, Player_Representative=@Player_Representative, Player_Team_ID=@Player_Team_ID where Player_Person_ID = '" + personID + "' ";
            }
            else
            {
                query = "INSERT INTO players(Player_Person_ID, Height, Player_Price, Player_Representative, Player_Team_ID) VALUES(@Player_Person_ID, @Height ,@Player_Price, @Player_Representative, @Player_Team_ID)";
            }

            return query;
        }

        private string GetQueryCoach(bool _isUpdate)
        {
            string query = "";

            if (_isUpdate)
            {
                query = "UPDATE teams SET Team_Name=@Team_Name, Team_Establishment=@Team_Establishment, Team_Logo=@Team_Logo, Team_Tournament_ID=@Team_Tournament_ID where Team_ID = '" + idTeam + "' ";
            }
            else
            {
                query = "INSERT INTO coaches(Coach_Person_ID, Coach_Team_ID) VALUES(@Coach_Person_ID, @Coach_Team_ID)";
            }

            return query;
        }


        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);
            if (withoutClubCheckBox.IsChecked == false && team_ComboBox.Text == string.Empty)
            {
                MessageBoxResult result = MessageBox.Show("Klub nie został wybrany! Czy chcesz by zawodnik był bez klubu?", "Uwaga", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No)
                {

                }
                else
                {
                    InsertPerson();
                }
            }
            else
            {
                InsertPerson();
            }

           
        }

        private void positionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            positionName = positionComboBox.SelectedItem.ToString();
        }
    }
}
