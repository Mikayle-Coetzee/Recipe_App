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

namespace PROG6221_P3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private AccountCreationView accountCreationView;
      

        public MainWindow()
        {
            InitializeComponent();
            accountCreationView = new AccountCreationView();
            accountCreationView.Visibility = Visibility.Collapsed;
            MainGrid.Children.Add(accountCreationView);
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            accountCreationView.Visibility = Visibility.Visible;
        }

        private void btnLogIn_Click_1(object sender, RoutedEventArgs e)
        {
            // Navigate to the login view
            AccountCreationView loginView = new AccountCreationView();
            // Set the visibility of the create account section to collapsed
            loginView.CreateAccountStackPanel.Visibility = Visibility.Collapsed;
            // Set the visibility of the login section to visible
            loginView.LoginStackPanel.Visibility = Visibility.Visible;
            MainFrame.Navigate(loginView);
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
