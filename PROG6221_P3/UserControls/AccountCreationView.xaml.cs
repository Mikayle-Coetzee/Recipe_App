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

namespace PROG6221_P3.UserControls
{
    /// <summary>
    /// Interaction logic for AccountCreationView.xaml
    /// </summary>
    public partial class AccountCreationView : UserControl
    {
        public AccountCreationView()
        {
            InitializeComponent();
        }

        private void btnBackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            // Hide the AccountCreationView and show the main view (MainWindow)
            Visibility = Visibility.Collapsed;
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.Show();
        }

        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            // Validate the input and perform necessary actions
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please fill in all the fields.");
            }
            else if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
            }
            else
            {
                MessageBox.Show("Account created successfully!");

                // Clear the input fields
                UsernameTextBox.Text = string.Empty;
                EmailTextBox.Text = string.Empty;
                PasswordBox.Password = string.Empty;
                ConfirmPasswordBox.Password = string.Empty;

                ///go to the main page view
                MainPageView mainPageView = new MainPageView();
                //MainPageView.SetWelcomeMessage(username);

                Window.GetWindow(this).Content = mainPageView;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the entered username and password
            string username = LoginUsernameTextBox.Text;
            string password = LoginPasswordBox.Password;

            // code validation and login logic

            // Clear the input fields
            LoginUsernameTextBox.Text = string.Empty;
            LoginPasswordBox.Password = string.Empty;

            //if it is valid login then redirect to the main page view
            MainPageView mainPageView = new MainPageView();
            //MainPageView.SetWelcomeMessage(username);

            Window.GetWindow(this).Content = mainPageView;
        }

    }
}