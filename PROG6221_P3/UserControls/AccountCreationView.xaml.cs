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
        POE_PROG6221_ST10023767_GR01.Validation validation = new POE_PROG6221_ST10023767_GR01.Validation();


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
             
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the entered username and password
            string name = txtLoginName.Text;
            string surname = txtLoginSurname.Text;

            if (validation.Validate_String(name) ==  true||validation.Validate_String(surname) == true) 
            { 
                //if it is valid login then redirect to the main page view
                MainPageView mainPageView = new MainPageView(name,surname);

                Window.GetWindow(this).Content = mainPageView;
            }
            else
            {
                // Clear the input fields
                txtLoginName.Text = string.Empty;
                txtLoginSurname.Text = string.Empty;
            }

        }
    }
}