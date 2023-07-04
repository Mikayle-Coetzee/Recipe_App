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
        /// <summary>
        /// This method displays an input dialog box with the specified message and title.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public string ShowInputDialog(string message, string title)
        {
            // Create an input text box
            var inputBox = new TextBox();

            // Set the properties of the input text box
            inputBox.FontSize = 18;
            inputBox.FontFamily = new FontFamily("Segoe Print");
            inputBox.Foreground = Brushes.White;
            inputBox.BorderBrush = Brushes.White;
            inputBox.BorderThickness = new Thickness(1);
            inputBox.Background = Brushes.Black;

            // Create a new window for the input dialog
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

            // Create an OK button
            var okButton = new Button()
            {
                Content = "OK",
                Width = double.NaN,  // Stretch button width
                FontSize = 18,
                FontFamily = new FontFamily("Segoe Print"),
                Foreground = Brushes.Black,
                Background = Brushes.White
            };

            // Event handler for OK button click
            okButton.Click += (sender, e) =>
            {
                // Set the dialog result to true and close the window
                window.DialogResult = true;
                window.Close();
            };

            // Create a panel for the OK button
            var buttonPanel = new StackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            // Add the OK button to the button panel
            buttonPanel.Children.Add(okButton);

            // Set the content of the window to a stack panel containing the message, input text box, and button panel
            window.Content = new StackPanel()
            {
                Children =
                {
                    new TextBlock() { Text = message, FontSize = 18, FontFamily = new FontFamily("Segoe Print"), 
                        Foreground = Brushes.White },inputBox, buttonPanel}
            };

            // Set the size of the window based on its content
            window.SizeToContent = SizeToContent.WidthAndHeight;

            // Show the input dialog and wait for the window to close
            window.ShowDialog();

            // Return the input text if the dialog result is true; otherwise, return "Exit"
            return window.DialogResult == true ? inputBox.Text : "Exit";
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method displays a notification box with the specified message and title.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public void ShowNotificationBox(string message, string title)
        {
            // Create a new window for the notification box
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

            // Create an OK button
            var okButton = new Button()
            {
                Content = "OK",
                Width = double.NaN,  // Stretch button width
                FontSize = 18,
                FontFamily = new FontFamily("Segoe Print"),
                Foreground = Brushes.Black,
                Background = Brushes.White
            };

            // Event handler for OK button click
            okButton.Click += (sender, e) =>
            {
                // Close the window
                window.Close();
            };

            // Create a panel for the OK button
            var buttonPanel = new StackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            // Add the OK button to the button panel
            buttonPanel.Children.Add(okButton);

            // Set the content of the window to a stack panel containing the message and button panel
            window.Content = new StackPanel()
            {
                Children =
                {
                    new TextBlock() { Text = message, FontSize = 18, FontFamily = new FontFamily("Segoe Print"), 
                        Foreground = Brushes.White }, buttonPanel }
            };

            // Set the size of the window based on its content
            window.SizeToContent = SizeToContent.WidthAndHeight;

            // Show the notification box and wait for the window to close
            window.ShowDialog();
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
