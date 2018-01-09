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

namespace EdytorBazyDanych
{
    /// <summary>
    /// Interaction logic for SearchWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        private string searchName = "";
        private bool isCoach = false;

        public SearchWindow()
        {
            InitializeComponent();
        }

        public SearchWindow(bool _isCoach)
        {
            InitializeComponent();
            isCoach = _isCoach;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            searchName = searchTextBox.Text;
            if(searchName != "")
            {
                CoachesWithoutClub coachesWithoutClub = new CoachesWithoutClub(searchName, true);
                this.Close();
                coachesWithoutClub.Show();
                coachesWithoutClub.Topmost = true;
            }
        }
    }
}
