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
using PROG6221_P3.Classes;

namespace PROG6221_P3.UserControls
{
    /// <summary>
    /// Interaction logic for MainPageView.xaml
    /// </summary>
    public partial class MainPageView : UserControl
    {
        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for MainPageView.
        /// </summary>
        public MainPageView(string name, string surname)
        {
            InitializeComponent();
            WelcomeTextBlock.Text = "Welcome aboard "+name +" "+surname+"!";
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This is a event handler for the selection change event of the TabControl.
        /// Handles the logic when the 'Sign Out' tab is selected. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is TabItem selectedTab)
            {
                if (selectedTab.Header.ToString() == "Sign Out")
                {
                    MainWindow window = new MainWindow();

                    // Get the current window state
                    WindowState windowState = Window.GetWindow(this) ?.WindowState ?? WindowState.Normal;

                    // Set the window state of the new MainWindow instance
                    window.WindowState = windowState;

                    // Set the new MainWindow instance as the current applications main window
                    Application.Current.MainWindow = window;

                    // Close the user control
                    Window.GetWindow(this).Close();

                    // Show the new MainWindow instance
                    window.Show();
                }
            }
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
