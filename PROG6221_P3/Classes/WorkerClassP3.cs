#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 3
#endregion

using POE_PROG6221_ST10023767_GR01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PROG6221_P3.Classes
{
    public class WorkerClassP3
    {
        public string ShowInputDialog(string message, string title)
        {
            var inputBox = new TextBox();
            inputBox.FontSize = 18;
            inputBox.FontFamily = new FontFamily("Segoe Print");
            inputBox.Foreground = Brushes.White;
            inputBox.BorderBrush = Brushes.White;
            inputBox.BorderThickness = new Thickness(1);
            inputBox.Background = Brushes.Black;

            var window = new Window()
            {
                Title = title,
                Width = 400,
                Height = 200,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.SingleBorderWindow,
                Background = Brushes.Black,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow
            };

            var okButton = new Button()
            {
                Content = "OK",
                Width = double.NaN,  // Stretch button width
                FontSize = 18,
                FontFamily = new FontFamily("Segoe Print"),
                Foreground = Brushes.Black,
                Background = Brushes.White
            };

            okButton.Click += (sender, e) =>
            {
                window.DialogResult = true;
                window.Close();
            };

            var buttonPanel = new StackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            buttonPanel.Children.Add(okButton);

            window.Content = new StackPanel()
            {
                Children =
        {
            new TextBlock() { Text = message, FontSize = 18, FontFamily = new FontFamily("Segoe Print"), Foreground = Brushes.White },
            inputBox,
            buttonPanel
        }
            };

            window.SizeToContent = SizeToContent.WidthAndHeight;

            window.ShowDialog();

            return window.DialogResult == true ? inputBox.Text : "Exit";
        }


        public void ShowNotificationBox(string message, string title)
        {
            var window = new Window()
            {
                Title = title,
                Width = 400,
                Height = 200,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.SingleBorderWindow,
                Background = Brushes.Black,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow
            };

            var okButton = new Button()
            {
                Content = "OK",
                Width = double.NaN,  // Stretch button width
                FontSize = 18,
                FontFamily = new FontFamily("Segoe Print"),
                Foreground = Brushes.Black,
                Background = Brushes.White
            };

            okButton.Click += (sender, e) =>
            {
                window.Close();
            };

            var buttonPanel = new StackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            buttonPanel.Children.Add(okButton);

            window.Content = new StackPanel()
            {
                Children =
        {
            new TextBlock() { Text = message, FontSize = 18, FontFamily = new FontFamily("Segoe Print"), Foreground = Brushes.White },
            buttonPanel
        }
            };

            window.SizeToContent = SizeToContent.WidthAndHeight;

            window.ShowDialog();
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
