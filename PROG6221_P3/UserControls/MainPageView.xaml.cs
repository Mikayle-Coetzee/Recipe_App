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
        public MainPageView(string name, string surname)
        {
            InitializeComponent();
            WelcomeTextBlock.Text = "Welcome aboard "+name +" "+surname+"!";
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is TabItem selectedTab)
            {
                if (selectedTab.Header.ToString() == "Sign Out")
                {
                    // Close the user control
                    MainWindow window = new MainWindow();
                    WindowState windowState = Window.GetWindow(this) ?.WindowState ?? WindowState.Normal;
                    window.WindowState = windowState;
                    Application.Current.MainWindow = window;
                    Window.GetWindow(this).Close();
                    window.Show();
                }
            }
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
