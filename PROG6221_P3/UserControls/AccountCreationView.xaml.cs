#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 3
#endregion

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
        /// <summary>
        /// Instantiates a new instance of the Validation class thats in the part 2 project. The Validation class 
        /// can now be used to perform validation tasks throughout the rest of the code.
        /// /// </summary>
        private POE_PROG6221_ST10023767_GR01.Validation validation = new POE_PROG6221_ST10023767_GR01.Validation();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for AccountCreationView.
        /// </summary>
        public AccountCreationView()
        {
            InitializeComponent();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This button will redirect to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            // Collapse the AccountCreationView and show the main view 
            Visibility = Visibility.Collapsed;
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.Show();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This button will get and validate the user name and surname and if it is both a valid string it will
        /// redirect to the main page view and empty the text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve the entered user name and user surname
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
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//