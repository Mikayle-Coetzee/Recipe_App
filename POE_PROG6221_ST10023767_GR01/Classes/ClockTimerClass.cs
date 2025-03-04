﻿#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 1 and 2 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace POE_PROG6221_ST10023767_GR01
{
    public class ClockTimerClass
    {
        /// <summary>
        /// Instantiates a new System.Timers.Timer object and assigns it to the MyTimer variable.
        /// This private timer will be used to execute a method at specified intervals.
        /// </summary>
        private System.Timers.Timer MyTimer = new System.Timers.Timer();

        /// <summary>
        /// String holds the current time
        /// </summary>
        private string CurrentTime = string.Empty;

        /// <summary>
        /// Randomly select a background color from one of the arrays
        /// </summary>
        private Random RandomNumberMe = new Random();

        /// <summary>
        /// Array holds the avaliable light colors 
        /// </summary>
        private readonly ConsoleColor[] arrAvailableColorsLight = new ConsoleColor[] { ConsoleColor.Yellow, 
            ConsoleColor.Cyan, ConsoleColor.Gray, ConsoleColor.Cyan};

        /// <summary>
        /// Array holds the avaliable dark colors 
        /// </summary>
        private readonly ConsoleColor[] arrAvailableColorsDark = new ConsoleColor[] { ConsoleColor.DarkCyan, 
            ConsoleColor.DarkGray, ConsoleColor.DarkMagenta, ConsoleColor.DarkGreen, ConsoleColor.DarkBlue };

        /// <summary>
        /// Holds the selected text color
        /// </summary>
        public ConsoleColor selectedForeColor;

        /// <summary>
        /// Holds the selected background color
        /// </summary>
        public ConsoleColor selectedColor;

        /// <summary>
        /// Holds the selected background color for the text
        /// </summary>
        public ConsoleColor selectedTextBackgroundColor;

        /// <summary>
        /// Instantiates a new instance of the Validation class. The Validation class can now be used to 
        /// perform validation tasks throughout the rest of the code.
        /// </summary>
        public Validation validate = new Validation();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for ClockTimerClass.
        /// </summary>
        public ClockTimerClass() { }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method includes a timer that updates the console title with the current time every second
        /// </summary>
        public void StartTimer()
        {
            this.MyTimer.Interval = 1000;
            this.MyTimer.Elapsed += this.MyTimer_Elapsed;
            this.MyTimer.Start();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// A timer object has a private string variable called CurrentTime that holds the current time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.CurrentTime = DateTime.Now.ToLongTimeString() + ", " + DateTime.Now.ToLongDateString();
            Console.Title = this.CurrentTime;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method that prompts the user to select a color scheme and changes the console background and
        /// foreground colors accordingly.
        /// </summary>
        public void ChangeColor()
        {
            // Initialize variable
            int selection = GetValidSelection();

            if (selection == 1)
            {
                this.selectedColor = arrAvailableColorsDark[RandomNumberMe.Next(arrAvailableColorsDark.Length)];
                this.selectedForeColor = ConsoleColor.White;
                this.selectedTextBackgroundColor = ConsoleColor.Black;
                ChangeForeColor(selectedForeColor);
            }
            else
            {
                this.selectedColor = arrAvailableColorsLight[RandomNumberMe.Next(arrAvailableColorsLight.Length)];
                this.selectedForeColor = ConsoleColor.Black;
                this.selectedTextBackgroundColor = ConsoleColor.White;
                ChangeForeColor(selectedForeColor);
            }

            // Set the console background color to the selected color
            Console.BackgroundColor = this.selectedColor;
            Console.Clear();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method that checks if the users input is valid 
        /// </summary>
        /// <returns>The integer of the users selection</returns>
        private int GetValidSelection()
        {
            // Initialize variable
            bool valid = false; 

            // change the console colors
            ChangeBackColor(ConsoleColor.DarkMagenta);
            ChangeForeColor(ConsoleColor.White);

            // prompt the user for selecting an option
            Console.WriteLine("\r\nPlease select an option by entering its corresponding number: ");

            // change the console colors back
            ChangeBackColor(ConsoleColor.White);
            ChangeForeColor(ConsoleColor.Black);

            // show the options the user can choose from
            Console.Write("1. Dark theme \r\n2. Light theme \r\n>");

            // read the user's input
            string userInput = validate.GetUserInput();

            // validate the user's input
            while (!valid)
            {
                if (validate.Validate_Integer(userInput))
                {
                    // if the user's input is valid, convert it to an integer
                    int selection = Convert.ToInt32(userInput);

                    // if the user's input is within the range of valid options, return the selection
                    if (selection > 0 && selection < 3)
                    {
                        return selection;
                    }
                }

                ChangeToErrorColor();

                Console.WriteLine("\r\nPlease re-select an option by entering its corresponding number: ");

                ChangeBackColor(ConsoleColor.White);
                ChangeForeColor(ConsoleColor.Black);

                Console.Write("1. Dark theme \r\n2. Light theme \r\n>");

                userInput = validate.GetUserInput();
            }

            // this will never happen, but it needs it to be here
            return 2;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method sets the console background color to the specified color.
        /// </summary>
        /// <param name="color"></param>
        public void ChangeBackColor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method sets the console foreground color to the specified color.
        /// </summary>
        /// <param name="color"></param>
        public void ChangeForeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method sets the console background color to white and the foreground color to red, used to indicate 
        /// an error.
        /// </summary>
        public void ChangeToErrorColor()
        {
            switch (selectedForeColor)
            {
                case ConsoleColor.White:
                    ChangeBackColor(ConsoleColor.Red);
                    ChangeForeColor(selectedForeColor);
                    break;
                default:
                    ChangeBackColor(ConsoleColor.DarkRed);
                    ChangeForeColor(ConsoleColor.White);
                    break;
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method sets the console background and foreground colors back to the originally selected color scheme.
        /// </summary>
        public void ChangeBack()
        {
            this.ChangeBackColor(this.selectedColor);
            this.ChangeForeColor(this.selectedForeColor);
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//