#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 1 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace POE_PROG6221_ST10023767_GR01
{
    public class WorkerClass
    {
        /// <summary>
        /// Initializes a private instance of the IngredientClass, which is used to get and store 
        /// information about the ingredients in the recipe.
        /// </summary>
        private IngredientClass ingredientClass = new IngredientClass();

        /// <summary>
        /// Initializes a private instance of the StepClass used to manage and store cooking instructions.
        /// </summary>
        private StepClass stepClass = new StepClass();

        /// <summary>
        /// Holds the number of ingredients 
        /// </summary>
        private int numberOfIngredients = 0;

        /// <summary>
        /// Holds the number of steps
        /// </summary>
        private int numberOfSteps = 0;

        /// <summary>
        /// An array of IngredientClass objects
        /// </summary>
        public IngredientClass[] arrIngredientClasses;

        /// <summary>
        /// An array of StepClass objects
        /// </summary>
        public StepClass[] arrSteps;

        /// <summary>
        /// An array that will store the original ingredient quantities, which is used when the user 
        /// wants to reset the recipe
        /// </summary>
        public double[] arrOriginalQuantities;

        /// <summary>
        /// An array that will store the original ingredient units, which is used when the user 
        /// wants to reset the recipe
        /// </summary>
        public string[] arrOriginalUnits;

        /// <summary>
        /// An array that holds the possible scaling factors.
        /// </summary>
        public double[] arrFactor = { 0.5, 2, 3, 4 };

        /// <summary>
        /// Holds the constant welcome string message 
        /// </summary>
        public const string WELCOME = "Welcome aboard! Thank you for choosing our application.";

        /// <summary>
        /// Holds the constant farewell string message 
        /// </summary>
        public const string FAREWELL = "We appreciate you using our application. Have a great day!";

        /// <summary>
        /// Instantiates a new instance of the Validation class. The Validation class
        /// can now be used to perform validation tasks throughout the rest of your code.
        /// /// </summary>
        public Validation validate = new Validation();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for WorkerClass.
        /// </summary>
        public WorkerClass(ClockTimerClass clockTimerClass)
        {
            WelcomeMessage(clockTimerClass);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method displays the welcome message on the console screen, changes the console background and 
        /// foreground colors, calls the DisplayBlock method to clear a block of characters on the screen, and 
        /// then resets the background color.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        public void WelcomeMessage(ClockTimerClass clockTimerClass)
        {
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            string welcome = "welcome"; 
            Console.Write(WELCOME);

            DisplayBlock(welcome);

            Console.WriteLine();
            clockTimerClass.ChangeBack();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method initializes and populates arrays with information about the ingredients.It first calls 
        /// GetNumOfIngredients method to get the number of ingredients, and then creates three arrays
        /// of IngredientClass, double and string types to store the ingredients, their quantities and units.
        /// It then prompts the user to provide details for each ingredient, and calls the GetIngredientName, 
        /// GetIngredientQuantity, and GetIngredientUnit methods to get the name, quantity and unit of each 
        /// ingredient.The obtained values are assigned to the respective properties of the IngredientClass object, 
        /// which is then added to the arrIngredientClasses array. the method stores the original quantities and 
        /// units of the ingredients in the arrOriginalQuantities and arrOriginalUnits arrays respectively.
        /// </summary>
        public void WriteIngredientsToArrays(ClockTimerClass clockTimerClass)
        {
            // Get the number of ingredients from the user using the GetNumOfIngredients method
            int number = ingredientClass.GetNumOfIngredients(clockTimerClass);
            this.numberOfIngredients = number;

            arrIngredientClasses = new IngredientClass[number];
            arrOriginalQuantities = new double[number];
            arrOriginalUnits = new string[number];

            //Prompt the user to enter the details for each ingredient
            for (int i = 0; i < number; i++)
            {
                var ingredient = new IngredientClass();

                clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
                clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);

                Console.WriteLine($"\r\nPlease provide the details for ingredient #{i + 1}:");

                clockTimerClass.ChangeBackColor(clockTimerClass.selectedColor);
                clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);

                arrIngredientClasses[i] = new IngredientClass();

                arrIngredientClasses[i].Name = ingredientClass.GetIngredientName(clockTimerClass);
                arrIngredientClasses[i].Quantity = ingredientClass.GetIngredientQuantity(clockTimerClass);
                arrIngredientClasses[i].Unit = ingredientClass.GetIngredientUnit(clockTimerClass);

                arrOriginalQuantities[i] = arrIngredientClasses[i].Quantity;
                arrOriginalUnits[i] = arrIngredientClasses[i].Unit;
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Prints a horizontal line made of Unicode character "\u2500"
        /// The length of the line is 100 characters
        /// </summary>
        public void DisplayLine()
        {
            // Initialize variable
            string line = "\u2500";

            for (int i = 0; i < 100; i++)
            {
                Console.Write(line);
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method is used to display a block of white spaces on the console at a specific position.
        /// If the argument "length" is equal to "welcome", it sets the cursor position to the end of the welcome message.
        /// Otherwise, it calculates the end line of the console output and sets the cursor position to the beginning of 
        /// the "BYE" message, then displays a block of 60 white spaces to overwrite any previous text in that line.
        /// </summary>
        /// <param name="length"></param>
        public void DisplayBlock(string length)
        {
            if (length == "welcome")
            {
                Console.SetCursorPosition(WELCOME.Length, 0);
                Console.Write(new string(' ', 60));
            }
            else
            {
                if (length == "farewell")
                {
                    int endLine = (Console.CursorTop) - 1;
                    Console.SetCursorPosition(FAREWELL.Length, endLine);
                    Console.Write(new string(' ', 60));
                }
                else
                {
                    int endLine = (Console.CursorTop) - 1;
                    Console.SetCursorPosition(length.Length, endLine);
                    Console.Write(new string(' ', 100));
                }
                
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prints a report of all the ingredients in the recipe.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        public void PrintIngredients(ClockTimerClass clockTimerClass)
        {
            // Initialize variable
            string recipe = "Recipe:";

            Console.WriteLine();
            DisplayLine();
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine();
            Console.WriteLine(recipe);
            DisplayBlock(recipe);
            Console.WriteLine();
            Console.WriteLine("\r\nIngredients:\r\n");
            Console.WriteLine($"{"Quantities",-16} \t {"Units",-16} \t {"Names"}\n");
            clockTimerClass.ChangeBack();
            IngredientReport();
        }
        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prints all the ingredients in the recipe, displaying their quantities, units, and names in 
        /// a formatted manner.
        /// </summary>
        public void IngredientReport()
        {
            for (int i = 0; i < this.numberOfIngredients; i++)
            {
                string quantity = validate.Convert_Numerical_Value_To_Corresponding_Text(arrIngredientClasses[i].Quantity);
                string unit = " " + arrIngredientClasses[i].Unit;
                string name = " " + arrIngredientClasses[i].Name;
                //the max length of units is 11 so the space must at least be 15
                Console.WriteLine($"{quantity,-16}\t{unit,-16}\t{name}");
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prints the recipe and provides options for editing the recipe. It validates user input and executes
        /// the corresponding option using the GetValidUserChoice method. It uses the clockTimerClass to change the
        /// console color and prompt the user for input. If the user chooses to clear the recipe, the method clears
        /// the recipe using the Array.Clear method and then calls the Application method to restart the program.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        public void PrintRecipe(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            string userInput = string.Empty;
            int userChoice = 0;

            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.Write("\r\nPlease select an option by entering its corresponding number: ");
            clockTimerClass.ChangeBack();
            Console.Write("\r\n1. Print Recipe \r\n2. Scale Quantities \r\n3. Clear Recipe \r\n4. Quit \n>");
            userInput = Console.ReadLine();

            userChoice = GetValidUserChoice(userInput, validate, clockTimerClass);

            switch (userChoice)
            {
                case 1:
                    PrintIngredients(clockTimerClass);
                    PrintSteps(clockTimerClass);
                    PrintRecipe(clockTimerClass);
                    break;
                case 2:
                    ScaleQuantities(clockTimerClass);
                    PrintRecipe(clockTimerClass);
                    break;
                case 3:
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nDo you still want to proceed with clearing your recipe,considering " +
                                    "\r\nthat this action is irreversible? (Yes/No):");
                    string newInput = Console.ReadLine();
                    clockTimerClass.ChangeBack();
                    if (validate.Validate_Yes_Or_No(newInput) == true)
                    {
                        if (newInput.Trim().ToUpper().Equals("NO"))
                        {
                            clockTimerClass.ChangeToErrorColor();
                            Console.WriteLine("\r\nThe request to enter a new recipe has been canceled.");
                            clockTimerClass.ChangeBack();
                            PrintRecipe(clockTimerClass);
                        }
                        else
                        {
                            //Clearing the arrays
                            Array.Clear(arrIngredientClasses, 0, arrIngredientClasses.Length);
                            Array.Clear(arrOriginalQuantities, 0, arrOriginalQuantities.Length);
                            Array.Clear(arrOriginalUnits, 0, arrOriginalUnits.Length);
                            Array.Clear(arrSteps, 0, arrSteps.Length);
                            Application(clockTimerClass);

                            PrintRecipe(clockTimerClass);
                        }
                    }
                    break;
                case 4:
                    PrintQuitMessage(clockTimerClass);
                    break;
                default:
                    break;
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method takes in a user input, a validation object, and a clock timer object, and returns a valid 
        /// user choice as an integer between 1 and 4. It validates the user input to ensure that it is a positive 
        /// integer between 1 and 4, and prompts the user for valid input if necessary.It also changes the console 
        /// color to indicate an error if the user enters invalid input.
        /// </summary>
        /// <param name="userInput"></param>
        /// <param name="validate"></param>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        /// <returns></returns>
        private int GetValidUserChoice(string userInput, Validation validate, ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            int number = 0;
            bool valid = false;

            do
            {
                if (validate.Validate_Integer(userInput) == true)
                {
                    number = Convert.ToInt32(userInput);
                    if ((number > 0) && (number < 5))
                    {
                        valid = true;
                    }
                }

                if (!valid)
                {
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nPlease select an option by entering its corresponding number: ");
                    clockTimerClass.ChangeBack();
                    Console.Write("\r\n1. Print Recipe \r\n2. Scale Quantities \r\n3. Clear Recipe \r\n4. Quit \n>");
                    userInput = Console.ReadLine();
                }

            } while (!valid);

            return number;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method that takes a ClockTimerClass object as an argument and prompts the user to enter a scaling factor.
        /// It then adjusts the quantity and unit of each ingredient in a list based on the selected factor.
        /// The method uses a loop to iterate through the list of ingredients and a switch statement to determine how
        /// to adjust each ingredient's unit based on its current unit and quantity.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        public void ScaleQuantities(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            double factor = 0.0f;
            string userInput = string.Empty; 
            bool valid = false; 
            int number = 0;

            // change the console colors
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);

            // prompt the user for the scaling factor they want to use
            Console.Write("\r\nWhat scaling factor would you like to use? Please enter a number to indicate the desired factor: ");

            // change the console colors back
            clockTimerClass.ChangeBack();

            // show the options the user can choose from
            Console.Write("\n1. Half\n2. Double\n3. Triple\n4. Reset\n>");

            // read the user's input
            userInput = Console.ReadLine();

            // validate the user's input
            do
            {
                if ((validate.Validate_Integer(userInput) == true))
                {
                    // if the user's input is valid, convert it to an integer
                    number = Convert.ToInt32(userInput);

                    if (number > 0 && number < 5)
                    {
                        // if the user's input is within the range of valid options, set the scaling factor
                        factor = arrFactor[(number - 1)];

                        // get the number of ingredients in the recipe
                        int arrayLength = arrIngredientClasses.Count();

                        if (number > 0 && number < 4)
                        {
                            // if the scaling factor is not a reset, scale the quantities of each ingredient
                            for (int i = 0; i < arrayLength; i++)
                            {
                                // update the unit of measure for each ingredient based on the new quantity
                                arrIngredientClasses[i].Quantity =  Math.Round((arrIngredientClasses[i].Quantity * (double)factor), 2);

                                for (int j = 0; j < 2; j++)
                                {
                                    switch (arrIngredientClasses[i].Unit.ToLower())
                                    {
                                        // for teaspoons, adjust to tablespoon if the quantity is 1 or 0.5, otherwise use tablespoons
                                        // else convert to teaspoon/teaspoons
                                        case "tsp":
                                        case "teaspoon":
                                        case "teaspoons":
                                            if (arrIngredientClasses[i].Quantity >= 3)
                                            {
                                                arrIngredientClasses[i].Quantity = Math.Round((arrIngredientClasses[i].Quantity / 3), 2);
                                                if (arrIngredientClasses[i].Quantity == 1 || arrIngredientClasses[i].Quantity == 0.5)
                                                {
                                                    arrIngredientClasses[i].Unit = "tablespoon";
                                                }
                                                else
                                                {
                                                    arrIngredientClasses[i].Unit = "tablespoons";
                                                }
                                            }
                                            else if (arrIngredientClasses[i].Quantity >= (double)0.5)
                                            {
                                                if (arrIngredientClasses[i].Quantity == 1 || arrIngredientClasses[i].Quantity == 0.5)
                                                {
                                                    arrIngredientClasses[i].Unit = "teaspoon";
                                                }
                                                else
                                                {
                                                    arrIngredientClasses[i].Unit = "teaspoons";
                                                }
                                            }
                                            else
                                            {
                                                arrIngredientClasses[i].Quantity = Math.Round((arrIngredientClasses[i].Quantity * 3), 2);
                                                arrIngredientClasses[i].Unit = "teaspoons";
                                            }
                                            continue;

                                        // for tablespoons, adjust to cups if the quantity is more than 16, otherwise to cup if the 
                                        // quantity after dividing with 16 is 1 or 0.5
                                        // else convert to tablespoons if quantity is more than 2, else convert to tablespoon if 
                                        // quantity is 1 or 0.5, otherwise multiply with three and convert to teaspoons
                                        case "tbsp":
                                        case "tablespoon":
                                        case "tablespoons":
                                            if (arrIngredientClasses[i].Quantity >= 16)
                                            {
                                                arrIngredientClasses[i].Quantity = Math.Round((arrIngredientClasses[i].Quantity / 16),2);
                                                if (arrIngredientClasses[i].Quantity == 1 || arrIngredientClasses[i].Quantity == 0.5)
                                                {
                                                    arrIngredientClasses[i].Unit = "cup";
                                                }
                                                else
                                                {
                                                    arrIngredientClasses[i].Unit = "cups";
                                                }
                                            }
                                            else if (arrIngredientClasses[i].Quantity >= 3)
                                            {
                                                //arrIngredientClasses[i].Quantity /= 3;// was /= 
                                                arrIngredientClasses[i].Unit = "tablespoons";
                                            }
                                            else if (arrIngredientClasses[i].Quantity >= (double)0.5)
                                            {
                                                if (arrIngredientClasses[i].Quantity == 1 || arrIngredientClasses[i].Quantity == 0.5)
                                                {
                                                    arrIngredientClasses[i].Unit = "tablespoon";
                                                }
                                                else
                                                {
                                                    arrIngredientClasses[i].Unit = "tablespoons";
                                                }
                                            }
                                            else
                                            {
                                                arrIngredientClasses[i].Quantity = Math.Round((arrIngredientClasses[i].Quantity * 3), 2);
                                                arrIngredientClasses[i].Unit = "teaspoons";
                                            }
                                            continue;

                                        // for ounces, adjust to pounds if the quantity is more than 16, otherwise to ounce if the 
                                        // quantity is 1 or 0.5, if it is not 1 or 0.5 but it is more than 0.5 then change to ounces.
                                        // else convert to ounces and multiply with 16.
                                        case "oz":
                                        case "ounce":
                                        case "ounces":
                                            if (arrIngredientClasses[i].Quantity >= 16)
                                            {
                                                arrIngredientClasses[i].Quantity = Math.Round((arrIngredientClasses[i].Quantity / 16), 2);
                                                arrIngredientClasses[i].Unit = "pounds";

                                            }
                                            else if (arrIngredientClasses[i].Quantity >= (double)0.5)
                                            {
                                                if (arrIngredientClasses[i].Quantity == 1 || arrIngredientClasses[i].Quantity == 0.5)
                                                {
                                                    arrIngredientClasses[i].Unit = "ounce";
                                                }
                                                else
                                                {
                                                    arrIngredientClasses[i].Unit = "ounces";
                                                }
                                            }
                                            else
                                            {
                                                arrIngredientClasses[i].Quantity = Math.Round((arrIngredientClasses[i].Quantity * 16), 2);
                                                arrIngredientClasses[i].Unit = "ounces";
                                            }
                                            continue;

                                        // for pounds, adjust to pounds if the quantity is more than 1, otherwise to pound if the 
                                        // quantity is 1 or 0.5, if it is smaller than 1, multiply with 16 and change to ounces.
                                        case "lb":
                                        case "pound":
                                        case "pounds":
                                            if (arrIngredientClasses[i].Quantity >= 1)
                                            {
                                                if (arrIngredientClasses[i].Quantity == 1 || arrIngredientClasses[i].Quantity == 0.5)
                                                {
                                                    arrIngredientClasses[i].Unit = "pound";
                                                }
                                                else
                                                {
                                                    arrIngredientClasses[i].Unit = "pounds";
                                                }
                                            }
                                            else
                                            {
                                                arrIngredientClasses[i].Quantity = Math.Round((arrIngredientClasses[i].Quantity * 16), 2);
                                                arrIngredientClasses[i].Unit = "ounces";
                                            }
                                            continue;

                                        // for cups, adjust to gallons if the quantity is more than 16, otherwise to cup if the 
                                        // quantity is 1 or 0.5, or to cups if the quantity is not 1 or 0.5 but still more than
                                        // 0.25 and less than 16. otherwise multiply with 48 and convert to teaspoons
                                        case "cup":
                                        case "cups":
                                            if (arrIngredientClasses[i].Quantity >= 16)
                                            {
                                                arrIngredientClasses[i].Quantity = Math.Round((arrIngredientClasses[i].Quantity / 16), 2);
                                                arrIngredientClasses[i].Unit = "gallons";
                                            }
                                            else if (arrIngredientClasses[i].Quantity >= (double)0.25)
                                            {
                                                if (arrIngredientClasses[i].Quantity == 1 || arrIngredientClasses[i].Quantity == 0.5)
                                                {
                                                    arrIngredientClasses[i].Unit = "cup";
                                                }
                                                else
                                                {
                                                    arrIngredientClasses[i].Unit = "cups";
                                                }
                                            }
                                            else
                                            {
                                                arrIngredientClasses[i].Quantity = Math.Round((arrIngredientClasses[i].Quantity * 48), 2);
                                                arrIngredientClasses[i].Unit = "teaspoons";
                                            }
                                            continue;

                                        // for gallons, adjust to gallons if the quantity is more than 1, otherwise to gallon if the 
                                        // quantity is 1 or 0.5
                                        // else multiply with 16 and convert to cups
                                        case "gal":
                                        case "gallon":
                                        case "gallons":
                                            if (arrIngredientClasses[i].Quantity >= 1)
                                            {
                                                if (arrIngredientClasses[i].Quantity == 1 || arrIngredientClasses[i].Quantity == 0.5)
                                                {
                                                    arrIngredientClasses[i].Unit = "gallon";
                                                }
                                                else
                                                {
                                                    arrIngredientClasses[i].Unit = "gallons";
                                                }
                                            }
                                            else
                                            {
                                                arrIngredientClasses[i].Quantity = Math.Round((arrIngredientClasses[i].Quantity * 16), 2);
                                                arrIngredientClasses[i].Unit = "cups";
                                            }
                                            continue;
                                        default:
                                            Console.WriteLine("We don't recognize this unit of measurement. Please try again using a different unit.");
                                            break;
                                    }
                                }
                            }
                            valid = true;
                        }
                        else
                        {
                            if (number == 4)
                            {
                                //If the value of number is equal to 4, then the program will reset the Quantity and Unit values
                                //in the array of objects arrIngredientClasses to their original values.
                                for (int k = 0; k < arrayLength; k++)
                                {
                                    arrIngredientClasses[k].Quantity = arrOriginalQuantities[k];
                                    arrIngredientClasses[k].Unit = arrOriginalUnits[k];
                                }
                                valid = true;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        valid = true;
                    }
                    else
                    {
                        //If the users input an invalid number reask
                        valid = false;
                        clockTimerClass.ChangeToErrorColor();
                        Console.Write("\r\nWhat scaling factor would you like to use? Please enter a number to indicate the desired factor: ");
                        clockTimerClass.ChangeBack();
                        Console.Write("\n1. Half\n2. Double\n3. Triple\n4. Reset\n>");
                        userInput = Console.ReadLine();
                    }
                }
                else
                {
                    //If the users input an invalid number reask
                    valid = false;
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nWhat scaling factor would you like to use? Please enter a number to indicate the desired factor: ");
                    clockTimerClass.ChangeBack();
                    Console.Write("\n1. Half\n2. Double\n3. Triple\n4. Reset\n>");
                    userInput = Console.ReadLine();
                }
            } while (valid == false);

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method is the main entry point for the recipe application. It prompts the user to select between two options:
        /// enter a new recipe or quit. If the user chooses to enter a recipe, it asks for confirmation and then calls 
        /// the WriteRecipeToArrays method to write the recipe information to arrays. It then calls the PrintRecipe method
        /// to display the recipe information to the console. If the user chooses to quit, it calls the PrintQuitMessage 
        /// method to display a quit message. The ClockTimerClass parameter is used to change the color of the console output.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        public void Application(ClockTimerClass clockTimerClass)
        {
            // Get the valid user choice from the user using the GetValidUserChoice method
            int userChoice = GetValidUserChoice(clockTimerClass);

            if (userChoice == 1)
            {
                string userInput = GetValidYesOrNoInput(clockTimerClass, "\r\nDo you want to enter a recipe? (Yes or No): ");

                if (userInput.Trim().ToUpper().Equals("NO"))
                {
                    Application(clockTimerClass);
                }
                else
                {
                    WriteRecipeToArrays(clockTimerClass);
                    PrintRecipe(clockTimerClass);
                }
            }
            else
            {
                PrintQuitMessage(clockTimerClass);
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to enter a valid integer choice 1 or 2 for the menu options. Returns 
        /// the user's choice. If the user enters an invalid input, displays an error message and prompts 
        /// again until a valid input is received, It returns the user's choice as an integer.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        /// <returns>The integer value of the users choice</returns>
        private int GetValidUserChoice(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            string userInput;
            int userChoice;

            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.Write("\r\nPlease select an option by entering its corresponding number: ");
            clockTimerClass.ChangeBack();
            Console.Write("\r\n1. Enter a new Recipe \r\n2. Quit \r\n>");
            userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out userChoice) || userChoice < 1 || userChoice > 2)
            {
                clockTimerClass.ChangeToErrorColor();
                Console.Write("\r\nPlease select an option by entering its corresponding number: ");
                clockTimerClass.ChangeBack();
                Console.Write("\r\n1. Enter a new Recipe \r\n2. Quit \r\n>");
                userInput = Console.ReadLine();
            }

            return userChoice;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user with a message and validates their input to ensure they enter either "Yes" or "No". 
        /// It returns the user's input as a string
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        /// <param name="message"></param>
        /// <returns>The string value of the users input that must be yes or no</returns>
        private string GetValidYesOrNoInput(ClockTimerClass clockTimerClass, string message)
        {
            // Initialize variable
            string userInput;

            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.Write(message);
            userInput = Console.ReadLine();
            clockTimerClass.ChangeBack();

            while (validate.Validate_Yes_Or_No(userInput) == false)
            {
                clockTimerClass.ChangeToErrorColor();
                Console.Write(message);
                userInput = Console.ReadLine();
                clockTimerClass.ChangeBack();
            }

            return userInput;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method writes the recipe ingredients and steps entered by the user to their arrays. It is called 
        /// within the application flow when the user chooses to enter a new recipe.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        private void WriteRecipeToArrays(ClockTimerClass clockTimerClass)
        {
            WriteIngredientsToArrays(clockTimerClass);
            WriteStepsToArray(clockTimerClass);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Prints a thank you message and sets the console color to black with white text. Also calls 
        /// DisplayBlock() method to display a horizontal line. After printing the message, the console 
        /// color is reset to its previous state and waits for user input.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        private void PrintQuitMessage(ClockTimerClass clockTimerClass)
        {
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine("\r\n"+ FAREWELL);
            string farewell = "farewell";
            DisplayBlock(farewell);
            clockTimerClass.ChangeBack();
            Console.WriteLine();
            Console.ReadLine();
            Environment.Exit(0);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method prompts the user to input a description for each step of a process.
        /// The input is validated and stored in an array of StepClass objects. The method uses an instance of
        /// the Validation class to ensure that the input is not empty or null. The method also takes an 
        /// instance of the ClockTimerClass to change the console colors during the input process.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        public void WriteStepsToArray(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            bool valid = false;
            int number = stepClass.GetNumOfSteps(clockTimerClass);
            this.numberOfSteps = number;
            string userInput = string.Empty;

            arrSteps = new StepClass[number];
            Console.WriteLine();
            for (int i = 0; i < number; i++)
            {
                var step = new StepClass();

                clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
                clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
                Console.Write($"\r\nPlease provide the description for step #{i + 1}: ");
                clockTimerClass.ChangeBack();
                Console.Write($"\r\nStep {i + 1}: ");
                do
                {
                    userInput = stepClass.GetStep();
                    valid = validate.Validate_String(userInput);
                    if (valid == true)
                    {
                        arrSteps[i] = new StepClass();
                        arrSteps[i].Step = userInput;
                    }
                    else
                    {
                        clockTimerClass.ChangeToErrorColor();
                        Console.Write($"\r\nPlease provide the description for step {i + 1} again: ");
                        clockTimerClass.ChangeBack();
                        Console.Write($"\r\nStep {i + 1}: ");
                    }
                } while (valid == false);
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Prints a report of the steps
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass to control the color of the console window</param>
        public void PrintSteps(ClockTimerClass clockTimerClass)
        {
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine("\r\nSteps:\r\n");
            clockTimerClass.ChangeBack();
            StepReport();
            Console.WriteLine();
            DisplayLine();
            Console.WriteLine();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Prints the steps. It does so by iterating over an array arrSteps and printing the Step
        /// property of each element, along with its corresponding step number.
        /// </summary>
        public void StepReport()
        {
            for (int i = 0; i < this.numberOfSteps; i++)
            {
                Console.WriteLine($"{i + 1}.\t {arrSteps[i].Step}");
            }
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//