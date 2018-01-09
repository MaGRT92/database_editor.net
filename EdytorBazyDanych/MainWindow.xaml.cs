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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace EdytorBazyDanych
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static bool IsOpen = false;
        private MySqlConnection conn;
        //**** Przechowuje nazwe panstwa/kraju ************
        string CountryName = "";
        //**** Przechowuje nazwe ligi/turnieju ********
        string TournamentName = "";

        string TeamName = String.Empty;

        public MainWindow()
        {
            InitializeComponent();

           string  connectionString = "database=sport; user=sslclient;" +
               "Certificate Store Location= CurrentUser; SSL Mode=Required";
            conn = new MySqlConnection(connectionString);
           
            AddCountries();

        }

        public MainWindow(bool _IsOpen) : this()
        {
            IsOpen = _IsOpen;
           
        }

        private void OpenConnection()
        {
            try
            {              
                    conn.Open();
                
            }
            catch(MySqlException ex)
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
        
        
        //******** Dodawanie krajow do listbox *********

        public void AddCountries()
        {
            countriesListBox.Items.Clear();

            OpenConnection();

            MySqlCommand cmd = new MySqlCommand("Select Country_Name from countries order by Country_Name ", conn);

            MySqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                countriesListBox.Items.Add(rd.GetString(0));
            }

            CloseConnection();           
        }

        //******** Dodawanie lig do listbox *********

        public void AddTournaments(string _CountryName)
        {
            tournamentsListBox.Items.Clear();
            OpenConnection();

            MySqlCommand cmd = new MySqlCommand("Select Distinct Tournament_Name from tournaments join countries where Tournament_Country_ID = (select Country_ID from countries where Country_Name ='" + _CountryName + "')", conn);

            MySqlDataReader rd = cmd.ExecuteReader();

            while (rd.Read())
                {
                    tournamentsListBox.Items.Add(rd.GetString(0));
                }

            CloseConnection();
        }

        //******** Dodawanie druzyn do listbox *********

        private void AddTeams(string _TournamentName)
        {

            OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select distinct Team_Name from teams join tournaments where Team_Tournament_ID =(select Tournament_ID from tournaments where Tournament_Name ='" + _TournamentName + "')", conn);

                MySqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    teamsListBox.Items.Add(rd.GetString(0));
                }

            CloseConnection();
        }

   
        private void button_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

       

        //***** LPM na listbox wybiera turniej/ligę **********
        private void countrieslistBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            teamsListBox.Items.Clear();
            tournamentsListBox.Items.Clear();

            CountryName = countriesListBox.SelectedItem.ToString();

            if(CountryName != "")
            AddTournaments(CountryName);
        }
      

        //***** LPM na listbox wybiera druzyny z danej ligi *********
        private void tournamentsListBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            teamsListBox.Items.Clear();
            if(tournamentsListBox.SelectedItems.Count != 0)
            TournamentName = tournamentsListBox.SelectedItem.ToString();

            if(TournamentName != "")
            AddTeams(TournamentName);
        }

       //****** Button Dodawania Kraju ***************
        private void addCountryButton_Click(object sender, RoutedEventArgs e)
        {
            AddCountry countryWindow = new AddCountry();
            countryWindow.Owner = this;
            if (IsOpen == false)
            {
                countryWindow.deleteButton.Visibility = Visibility.Collapsed;
                countryWindow.Show();
                countryWindow.Topmost = true;
                IsOpen = true;
            }
          
          
           
        }
        //********* Button Dodawanie ligi ***************
        private void addTournamentButton_Click(object sender, RoutedEventArgs e)
        {
            AddTournament tournamentWindow = new AddTournament();
            tournamentWindow.Owner = this;
            if (IsOpen == false)
            {
                tournamentWindow.deleteButton.Visibility = Visibility.Collapsed;
                tournamentWindow.Show();
                tournamentWindow.Topmost = true;
                IsOpen = true;
            }
        }

        //******** Button Edycja kraju ***********
        private void editCountryButton_Click(object sender, RoutedEventArgs e)
        {
            string id = "";
            string name = "";
            string flag = "";
            string logo = "";
            if (countriesListBox.SelectedItems.Count == 1)
            {
                OpenConnection();

                MySqlCommand cmd = new MySqlCommand("Select * from countries  where Country_ID = (select Country_ID from countries where Country_Name ='" + CountryName + "')", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    id = rd.GetString(0);
                    name = rd.GetString(1);
                    flag = rd.GetString(2);
                    logo = rd.GetString(3);
                }

                CloseConnection();

                AddCountry addCountryWindow = new AddCountry(id, name, flag, logo, true);
                addCountryWindow.Owner = this;
                if (IsOpen == false)
                {
                    addCountryWindow.Show();
                    IsOpen = true;
                }
            }
            else
            {
                MessageBox.Show("Prosze zaznaczyć kraj");
            }
        }
        //**** Pobranie ID kraju na podstawie jego nazwy **********
        private int GetCountryID()
        {
            int countryId = 0;

            try
            {
                OpenConnection();
                MySqlCommand cmd = new MySqlCommand("Select Country_ID from countries where Country_Name='" + CountryName + "' ", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    countryId = Convert.ToInt32(rd.GetString(0));
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
                tournamentName = tournamentsListBox.SelectedItem.ToString();
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

        //********* Button Edycja lig ***************
        private void editTournamentButton_Click(object sender, RoutedEventArgs e)
        {
            string id = "";
            string name = "";
            string logo = "";

            if (tournamentsListBox.SelectedItems.Count == 1)
            {
                OpenConnection();

                MySqlCommand cmd = new MySqlCommand("Select * from tournaments  where Tournament_ID = (select Tournament_ID from tournaments where Tournament_Name ='" + TournamentName + "')", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    id = rd.GetString(0);
                    name = rd.GetString(1);                 
                    logo = rd.GetString(2);
                }

                CloseConnection();

                AddTournament addTournamentWindow = new AddTournament(GetCountryID(), CountryName, id, name, logo, true);
                addTournamentWindow.Owner = this;
                if (IsOpen == false)
                {
                    addTournamentWindow.Show();
                    IsOpen = true;
                }
            }
            else
            {
                MessageBox.Show("Prosze zaznaczyć ligę");
            }
        }

        private void addTeamButton_Click(object sender, RoutedEventArgs e)
        {
            AddTeam addTeamWindow = new AddTeam();
            addTeamWindow.Owner = this;
            if (IsOpen == false)
            {
                //addTeamWindow.deleteButton.Visibility = Visibility.Collapsed;
                addTeamWindow.Show();
                addTeamWindow.Topmost = true;
                IsOpen = true;
            }
        }

        private void playersWithoutClubButton_Click(object sender, RoutedEventArgs e)
        {
            PlayersWithoutClub playersWithoutClubWindow = new PlayersWithoutClub();
            playersWithoutClubWindow.Owner = this;
            if (IsOpen == false)
            {
                //addTeamWindow.deleteButton.Visibility = Visibility.Collapsed;
                playersWithoutClubWindow.Show();
                playersWithoutClubWindow.Topmost = true;
                IsOpen = true;
            }
        }

       

        private void editTeamButton_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            string name = "";
            string establishment = string.Empty;
            string logo = "";
            int teamTournament_ID = 0;

            if (teamsListBox.SelectedItems.Count == 1)
            {
                TeamName = teamsListBox.SelectedItem.ToString();

                OpenConnection();

                MySqlCommand cmd = new MySqlCommand("Select * from teams  where Team_ID = (select Team_ID from teams where Team_Name ='" + TeamName + "')", conn);

                MySqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    id = rd.GetInt32(0);                   
                    name = rd.GetString(1);
                    establishment = rd.GetString(2);
                    logo = rd.GetString(3);
                    teamTournament_ID = rd.GetInt32(4);
                }

                CloseConnection();

                AddTeam addTeamWindow = new AddTeam(GetCountryID(), CountryName, GetTournamentName(), id, name, establishment, logo, true);
                addTeamWindow.Owner = this;
                if (IsOpen == false)
                {
                    addTeamWindow.Show();
                    IsOpen = true;
                }
            }
            else
            {
                MessageBox.Show("Prosze zaznaczyć drużynę");
            }
        }

        private void addPlayerCoachButton_Click(object sender, RoutedEventArgs e)
        {
            AddPlayerWindow addPlayerWindow = new AddPlayerWindow();
            addPlayerWindow.Owner = this;
            if (IsOpen == false)
            {
                //addTeamWindow.deleteButton.Visibility = Visibility.Collapsed;
                addPlayerWindow.Show();
                addPlayerWindow.Topmost = true;
                IsOpen = true;
            }
        }


        private void coachWithoutClubButton_Click(object sender, RoutedEventArgs e)
        {
            CoachesWithoutClub coachesWithoutClubWindow = new CoachesWithoutClub();
            coachesWithoutClubWindow.Owner = this;
            if (IsOpen == false)
            {
                //addTeamWindow.deleteButton.Visibility = Visibility.Collapsed;
                coachesWithoutClubWindow.Show();
                coachesWithoutClubWindow.Topmost = true;
                IsOpen = true;
            }
        }

        private void searchPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow();
            searchWindow.Owner = this;
            if (IsOpen == false)
            {
                //addTeamWindow.deleteButton.Visibility = Visibility.Collapsed;
                searchWindow.Show();
                searchWindow.Topmost = true;
                IsOpen = true;
            }
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow();
            searchWindow.Owner = this;
            if (IsOpen == false)
            {
                //addTeamWindow.deleteButton.Visibility = Visibility.Collapsed;
                searchWindow.Show();
                searchWindow.Topmost = true;
                IsOpen = true;
            }
        }
    }
}
