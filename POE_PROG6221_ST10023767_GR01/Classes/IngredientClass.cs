#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 1 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace POE_PROG6221_ST10023767_GR01
{
    public class IngredientClass
    {
        /// <summary>
        /// Holds the name of the ingredient 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Holds the quantity of the ingredient in string format e.g. 1 = 'one'
        /// </summary>
        public double Quantity { get; set; } = (double)0.0;

        /// <summary>
        /// Holds the unit of measurement
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// Holds the number of ingredients 
        /// </summary>
        public int NumOfIngredients { get; set; } = 0;

        /// <summary>
        /// Instantiates a new instance of the Validation class. The Validation class
        /// can now be used to perform validation tasks throughout the rest of your code.
        /// /// </summary>
        public Validation validate = new Validation();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// PART 2
        /// Constructor method of the IngredientClass class. It takes four parameters, which are the name of the
        /// ingredient, its quantity, its unit of measurement, and the number of ingredients. It initializes 
        /// the corresponding properties of the class with these parameter values.
        /// </summary>
        public IngredientClass(string name, double quantity, string unit, int numOfIngredients)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Unit = unit;
            this.NumOfIngredients = numOfIngredients;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for IngredientClass.
        /// </summary>
        public IngredientClass()
        {


        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// PART 2
        /// This method prompts the user to enter ingredient data and returns an IngredientClass object with the data entered.
        /// The method first calls GetNumOfIngredients to get the number of ingredients to be entered. It then calls 
        /// GetIngredientName, GetIngredientQuantity, and GetIngredientUnit to get the name, quantity, and unit of each
        /// ingredient respectively. It creates and returns a new IngredientClass object with the data entered.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        /// <returns>a new IngredientClass object with the data entered</returns>
        public IngredientClass GetIngredientData(ClockTimerClass clockTimerClass)
        {
            int numOfIngredients = GetNumOfIngredients(clockTimerClass);
            string name = GetIngredientName(clockTimerClass);
            double quantity = GetIngredientQuantity(clockTimerClass);
            string unit = GetIngredientUnit(clockTimerClass);


            return new IngredientClass(name, quantity, unit, numOfIngredients);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method retrieves the number of ingredients from the user input as a whole number. It first 
        /// initializes the necessary variables and then prompts the user to enter the number of ingredients. 
        /// It then checks whether the input is a valid integer using the Validate_Integer method. If the input
        /// is not valid, it prompts the user to re-enter the number of ingredients. The method continues to loop
        /// until a valid input is received. The method then returns the valid number of ingredients as an integer.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        /// <returns>The valid number of ingredients as an integer</returns>
        public int GetNumOfIngredients(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            bool valid = false;
            string userInput;
            int number = 0;

            // Prompt the user to enter the number of ingredients as a whole number
            Console.Write("\r\nPlease enter the number of ingredients as a whole number: ");
            userInput = Console.ReadLine();

            // Validate the user's input
            do
            {
                try
                {
                    valid = validate.Validate_Integer(userInput);
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                }

                if (valid == true)
                {
                    number = Convert.ToInt32(userInput);
                }
                // If the input is invalid, prompt the user to re-enter the ingredient unit and change the console
                // color to indicate an error
                else
                {
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nPlease re-enter the number of ingredients as a whole number: ");
                    userInput = Console.ReadLine();
                    clockTimerClass.ChangeBack();
                }
            } while (valid == false);

            // Return the integer of the entered number of ingredients 
            return number;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to enter a text string representing the name of an ingredient, 
        /// validates the input to ensure it is a valid string, and returns the input as a string.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        /// <returns>The string representing the entered ingredient name</returns>
        public String GetIngredientName(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            bool valid;
            string userInput;
            string name = string.Empty;

            // Prompt the user to enter the ingredient name as text
            Console.Write("Please enter the ingredient name as text (e.g. 'Sugar'):   \t");
            userInput = Console.ReadLine();

            // Validate the user's input
            do
            {
                valid = validate.Validate_String(userInput);

                if (valid == true)
                {
                    name = userInput;
                }
                // If the input is invalid, prompt the user to re-enter the ingredient unit and change the console
                // color to indicate an error
                else
                {
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nPlease re-enter the ingredient name as text (e.g. 'Sugar'):   \t");
                    userInput = Console.ReadLine();
                    clockTimerClass.ChangeBack();
                }
            } while (valid == false);

            // Return the string of the entered ingredient name
            return name;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to enter an ingredient unit as text (e.g. "cup") and validates the input to
        /// ensure it is a valid string and a valid unit of measurement. It returns the validated unit as a string.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        /// <returns>The string representing the entered ingredient unit</returns>
        public String GetIngredientUnit(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            bool valid1;
            bool valid2;
            string userInput;
            string unit = string.Empty;

            // Prompt the user to enter the ingredient unit as text
            Console.Write("Please enter the ingredient unit as text (e.g. 'cup'):   \t");
            userInput = Console.ReadLine();

            // Validate the user's input
            do
            {
                valid1 = validate.Validate_String(userInput);
                valid2 = validate.Validate_Unit_Of_Measurement(userInput);

                if (valid1 == true && valid2 == true)
                {
                    unit = userInput;
                }
                // If the input is invalid, prompt the user to re-enter the ingredient unit and change the console
                // color to indicate an error
                else
                {
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nPlease re-enter the ingredient unit as text (e.g. 'cup'):   \t");
                    userInput = Console.ReadLine();
                    clockTimerClass.ChangeBack();
                }
            } while (valid1 == false || valid2 == false);

            // Return the string of the entered ingredient unit
            return unit;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to enter an ingredient quantity as text (e.g. "one") and converts it to 
        /// a numerical value.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        /// <returns>The numerical value of the entered ingredient quantity.</returns>
        public double GetIngredientQuantity(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            bool valid;
            string userInput;
            double quantity = -9999;

            int userChoice;
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.Write("\r\nPlease select an option by entering its corresponding number: ");
            clockTimerClass.ChangeBack();
            Console.Write("\r\n1. Enter the ingredient quantity as text (e.g. 'one')" +
                "\r\n2. Enter the ingredient quantity as a numerical number (e.g. 1)\r\n>");
            userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out userChoice) || userChoice < 1 || userChoice > 2)
            {
                clockTimerClass.ChangeToErrorColor();
                Console.Write("\r\nPlease select an option by entering its corresponding number: ");
                clockTimerClass.ChangeBack();
                Console.Write("\r\n1. Enter the ingredient quantity as text (e.g. 'one')" +
                    "\r\n2. Enter the ingredient quantity as a numerical number (e.g. 1)\r\n>");
                userInput = Console.ReadLine();
            }

            //// Prompt the user to enter the ingredient quantity as text
            //Console.Write("Please enter the ingredient quantity as text (e.g. 'one'): \t");
            //userInput = Console.ReadLine();

            // Validate the user's input
            if (userChoice == 1)
            {
                clockTimerClass.ChangeBackColor(clockTimerClass.selectedColor);
                clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
                Console.Write("\r\nPlease enter the ingredient quantity as text (e.g. 'one'): \t");
                userInput = Console.ReadLine();
                clockTimerClass.ChangeBack();
                do
                {

                    // If the input is invalid, prompt the user to re-enter the ingredient quantity and change the console
                    // color to indicate an error
                    if (!validate.Validate_String(userInput) || validate.Validate_Float(userInput))
                    {
                        valid = false;
                        clockTimerClass.ChangeToErrorColor();
                        Console.Write("\r\nPlease re-enter the ingredient quantity as text (e.g. 'one'): \t");
                        userInput = Console.ReadLine();
                        clockTimerClass.ChangeBack();
                    }
                    else
                    {
                        quantity = Math.Round(validate.Convert_Text_To_Corresponding_Numerical_Value(userInput), 2);
                        if (quantity == -9999)
                        {
                            valid = false;
                            clockTimerClass.ChangeToErrorColor();
                            Console.Write("\r\nPlease re-enter the ingredient quantity as text (e.g. 'one'): \t");
                            userInput = Console.ReadLine();
                            clockTimerClass.ChangeBack();
                        }
                        else
                        {
                            valid = true;
                        }
                    }
                } while (valid == false);
            }
            else
            {
                clockTimerClass.ChangeBackColor(clockTimerClass.selectedColor);
                clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
                Console.Write("\r\nPlease re-enter the ingredient quantity as a numerical number (e.g. 1): \t");
                userInput = Console.ReadLine();
                clockTimerClass.ChangeBack();
                do
                {


                    // If the input is invalid, prompt the user to re-enter the ingredient quantity and change the console
                    // color to indicate an error
                    if (!validate.Validate_String(userInput) || !validate.Validate_Float(userInput))
                    {
                        valid = false;
                        clockTimerClass.ChangeToErrorColor();
                        Console.Write("\r\nPlease re-enter the ingredient quantity as a numerical number (e.g. 1): \t");
                        userInput = Console.ReadLine();
                        clockTimerClass.ChangeBack();
                    }
                    else
                    {

                        quantity = Math.Round(Convert.ToDouble(userInput), 2);
                        if (quantity == -9999)
                        {
                            valid = false;
                            clockTimerClass.ChangeToErrorColor();
                            Console.Write("\r\nPlease re-enter the ingredient quantity as a numerical number (e.g. 1): \t");
                            userInput = Console.ReadLine();
                            clockTimerClass.ChangeBack();
                        }
                        else
                        {
                            valid = true;
                        }
                    }
                } while (valid == false);
            }
            // Return the numerical value of the entered ingredient quantity
            return quantity;
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
