using PROG6221_P3;
using PROG6221_P3.UserControls;
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
//using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PROG6221_P3.Classes;

namespace PROG6221_P3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Instance of the AccountCreationView
        /// </summary>
        private AccountCreationView accountCreationView;

        /// <summary>
        /// SharedViewModel property of type MainViewModel
        /// </summary>
        public MainViewModel SharedViewModel { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            // Instantiate the AccountCreationView and set its visibility to collapsed
            accountCreationView = new AccountCreationView();
            accountCreationView.Visibility = Visibility.Collapsed;

            // Add the AccountCreationView to the MainGrid
            MainGrid.Children.Add(accountCreationView);

            // Set the DataContext of the MainWindow to a new instance of MainViewModel
            DataContext = new MainViewModel();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogIn_Click_1(object sender, RoutedEventArgs e)
        {
            // Navigate to the login view
            AccountCreationView loginView = new AccountCreationView();

            // Set the visibility of the login section to visible
            loginView.LoginStackPanel.Visibility = Visibility.Visible;

            // Navigate to the loginView using the MainFrame
            MainFrame.Navigate(loginView);
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
