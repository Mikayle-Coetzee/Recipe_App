﻿#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 1 and 2 
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
        //public IngredientClass[] arrIngredientClasses;
        public List<IngredientClass> IngredientList { get; set; }

        /// <summary>
        /// An array of StepClass objects
        /// </summary>
        // public StepClass[] arrSteps;
        public List<StepClass> StepList { get; set; }

        private string AnotherRecipe = "No"; 

        /// <summary>
        /// An array that will store the original ingredient quantities, which is used when the user 
        /// wants to reset the recipe
        /// </summary>
        //public double[] arrOriginalQuantities;
        //public List<double> OriginalQuantitiesList { get; set; }

        /// <summary>
        /// An array that will store the original ingredient units, which is used when the user 
        /// wants to reset the recipe
        /// </summary>
        //public string[] arrOriginalUnits;
        //public List<string> OriginalUnitsList { get; set; }

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


        private List<List<(string, double, string, double, string)>> ingredientCollectionsOriginal;

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
        public void WriteIngredientsToArrays(ClockTimerClass clockTimerClass) // change the methods name to WriteIngredientsToCollections
        {
            int number = ingredientClass.GetNumOfIngredients(clockTimerClass);
            this.numberOfIngredients = number;

            IngredientList = new List<IngredientClass>();
            //OriginalQuantitiesList = new List<double>();
            //OriginalUnitsList = new List<string>();

            for (int i = 0; i < number; i++)
            {
                var ingredient = new IngredientClass();

                clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
                clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);

                Console.WriteLine($"\r\nPlease provide the details for ingredient #{i + 1}:");

                clockTimerClass.ChangeBackColor(clockTimerClass.selectedColor);
                clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);

                var newIngredient = new IngredientClass
                {
                    Name = ingredientClass.GetIngredientName(clockTimerClass),
                    Quantity = ingredientClass.GetIngredientQuantity(clockTimerClass),
                    Unit = ingredientClass.GetIngredientUnit(clockTimerClass),
                    IngredientCalories = ingredientClass.GetIngredientCalories(clockTimerClass),
                    FoodGroup = ingredientClass.GetFoodGroup(clockTimerClass)
                };

                IngredientList.Add(newIngredient);
                //OriginalQuantitiesList.Add(newIngredient.Quantity);
                //OriginalUnitsList.Add(newIngredient.Unit);
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
        /// If the argument "length" is equal to "welcome", it sets the cursor position to the end of the welcome 
        /// message. Otherwise, it calculates the end line of the console output and sets the cursor position to 
        /// the beginning of the "BYE" message, then displays a block of 60 white spaces to overwrite any previous
        /// text in that line.
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
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>
        public void PrintIngredients(ClockTimerClass clockTimerClass, string selectedRecipe)
        {
            // Initialize variable
            string recipe = "Recipe: "+ selectedRecipe;

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
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prints the recipe and provides options for editing the recipe. It validates user input and executes
        /// the corresponding option using the GetValidUserChoice method. It uses the clockTimerClass to change the
        /// console color and prompt the user for input. If the user chooses to clear the recipe, the method clears
        /// the recipe using the Array.Clear method and then calls the Application method to restart the program.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>
        public void PrintRecipe(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            string userInput = string.Empty;
            int userChoice = 0;

            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.Write("\r\nPlease select an option by entering its corresponding number: ");
            clockTimerClass.ChangeBack();
            Console.Write("\r\n1. Print A Specific Recipe \r\n2. Scale Quantities \r\n3. Enter Another Recipe \r\n4. Clear Recipe \r\n5. Quit \n>");
            userInput = Console.ReadLine();

            userChoice = GetValidUserChoice(userInput, validate, clockTimerClass);

            switch (userChoice)
            {
                case 1:
                    DisplayRecipeNames(clockTimerClass); /// < ---------------------------------------------------------------
                    PrintRecipe(clockTimerClass);
                    break;
                case 2:
                    ScaleQuantities(clockTimerClass);
                    PrintRecipe(clockTimerClass);
                    break;
                case 3:
                    AnotherRecipe = "Yes"; 
                    WriteRecipeToArrays(clockTimerClass);
                    PrintRecipe(clockTimerClass);
                    break;
                case 4:
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nDo you still want to proceed with clearing your recipe,considering " +
                                    "\r\nthat this action is irreversible? (Yes/No):");
                    string newInput = Console.ReadLine();
                    clockTimerClass.ChangeBack();

                    while (validate.Validate_Yes_Or_No(newInput) == false)
                    {
                        clockTimerClass.ChangeToErrorColor();
                        Console.Write("\r\nDo you still want to proceed with clearing your recipe,considering " +
                                        "\r\nthat this action is irreversible? (Yes/No):");
                        newInput = Console.ReadLine();
                        clockTimerClass.ChangeBack();

                    }

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
                        //Array.Clear(arrIngredientClasses, 0, arrIngredientClasses.Length);
                        //Array.Clear(arrOriginalQuantities, 0, arrOriginalQuantities.Length);
                        //Array.Clear(arrOriginalUnits, 0, arrOriginalUnits.Length);
                        //Array.Clear(arrSteps, 0, arrSteps.Length);

                        // Clearing the generic collections

                        if (IngredientList != null && StepList != null && totalCaloriesList != null)
                        {
                            IngredientList.Clear();
                            StepList.Clear();
                            totalCaloriesList.Clear();
                            recipeNames.Clear();
                            stepCollections.Clear();
                            ingredientCollections.Clear();
                            ingredientCollectionsOriginal.Clear();
                            RecipeList.Clear();
                        }

                        Application(clockTimerClass);

                        PrintRecipe(clockTimerClass);
                    }
                    break;
                case 5:
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
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>
        /// <returns></returns>
        private int GetValidUserChoice(string userInput, Validation validate, ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            int number = 0;
            bool valid = false;

            do
            {
                if (validate.Validate_Integer(userInput))
                {
                    number = Convert.ToInt32(userInput);
                    if ((number > 0) && (number < 6))
                    {
                        valid = true;
                    }
                }

                if (!valid)
                {
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nPlease select an option by entering its corresponding number: ");
                    clockTimerClass.ChangeBack();
                    Console.Write("\r\n1. Print A Specific Recipe \r\n2. Scale Quantities \r\n3. Enter Another Recipe \r\n4. Clear Recipe \r\n5. Quit \n>");
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
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>

        public void ScaleQuantities(ClockTimerClass clockTimerClass)
        {
            double factor = 0.0;
            string userInput = string.Empty;
            bool valid = false;
            int number = 0;

            string userInput2;
            int userChoice;
            int recipeIndex = 1;

            // Retrieve and display the recipe names in alphabetical order
            List<string> recipeNames = GetRecipeNames();
            List<List<string>> stepCollections = GetStepCollections();
            List<List<(string, double, string, double, string)>> ingredientCollections = GetIngredientCollections();

            // Create a deep copy of ingredientCollections if ingredientCollectionsOriginal is null
            if (ingredientCollectionsOriginal == null)
            {
                ingredientCollectionsOriginal = new List<List<(string, double, string, double, string)>>();

                foreach (var recipe in ingredientCollections)
                {
                    var copiedRecipe = new List<(string, double, string, double, string)>(recipe);
                    ingredientCollectionsOriginal.Add(copiedRecipe);
                }
            }

            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine("\r\nPlease select the recipe to view by entering its corresponding number: ");
            clockTimerClass.ChangeBack();

            // Sort the recipe names in alphabetical order
            List<string> sortedRecipeNames = recipeNames.OrderBy(name => name).ToList();

            for (int i = 0; i < sortedRecipeNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sortedRecipeNames[i]}");
            }
            Console.WriteLine($"{sortedRecipeNames.Count + 1}. Back");
            Console.Write(">");

            userInput2 = Console.ReadLine();

            if (int.TryParse(userInput2, out userChoice))
            {
                if (userChoice >= 1 && userChoice <= sortedRecipeNames.Count)
                {
                    recipeIndex = userChoice - 1;

                    clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
                    clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);

                    Console.Write("\r\nWhat scaling factor would you like to use? Please enter a number to indicate the desired factor: ");

                    clockTimerClass.ChangeBack();
                    Console.Write("\n1. Half\n2. Double\n3. Triple\n4. Reset\n>");

                    userInput = Console.ReadLine();

                    do
                    {
                        if (int.TryParse(userInput, out number))
                        {
                            if (number > 0 && number < 5)
                            {
                                factor = arrFactor[(number - 1)];

                                int arrayLength = ingredientCollections[recipeIndex].Count;

                                if (number > 0 && number < 4)
                                {
                                    for (int i = 0; i < arrayLength; i++)
                                    {
                                        ingredientCollections[recipeIndex][i] = (
                                            ingredientCollections[recipeIndex][i].Item1,
                                            Math.Round((ingredientCollections[recipeIndex][i].Item2 * factor), 2),
                                            ingredientCollections[recipeIndex][i].Item3,
                                            ingredientCollections[recipeIndex][i].Item4,
                                            ingredientCollections[recipeIndex][i].Item5
                                        );

                                        for (int j = 0; j < 2; j++)
                                        {
                                            switch (ingredientCollections[recipeIndex][i].Item3.ToLower())
                                            {
                                                case "tsp":
                                                case "teaspoon":
                                                case "teaspoons":
                                                    if (ingredientCollections[recipeIndex][i].Item2 >= 3)
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            Math.Round((ingredientCollections[recipeIndex][i].Item2 / 3), 2),
                                                            (ingredientCollections[recipeIndex][i].Item2 == 1 || ingredientCollections[recipeIndex][i].Item2 == 0.5) ? "tablespoon" : "tablespoons",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5);

                                                    }
                                                    else if (ingredientCollections[recipeIndex][i].Item2 >= (double)0.5)
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            ingredientCollections[recipeIndex][i].Item2,
                                                            (ingredientCollections[recipeIndex][i].Item2 == 1 || ingredientCollections[recipeIndex][i].Item2 == 0.5) ? "teaspoon" : "teaspoons",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5);
                                                    }
                                                    else
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            Math.Round((ingredientCollections[recipeIndex][i].Item2 * 3), 2),
                                                            "teaspoons",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5
                                                        );
                                                    }
                                                    continue;

                                                case "tbsp":
                                                case "tablespoon":
                                                case "tablespoons":
                                                    if (ingredientCollections[recipeIndex][i].Item2 >= 16)
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            Math.Round((ingredientCollections[recipeIndex][i].Item2 / 16), 2),
                                                            (ingredientCollections[recipeIndex][i].Item2 == 1 || ingredientCollections[recipeIndex][i].Item2 == 0.5) ? "cup" : "cups",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5);
                                                    }
                                                    else if (ingredientCollections[recipeIndex][i].Item2 >= 3)
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            ingredientCollections[recipeIndex][i].Item2,
                                                            "tablespoons",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5);
                                                    }
                                                    else if (ingredientCollections[recipeIndex][i].Item2 >= (double)0.5)
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            ingredientCollections[recipeIndex][i].Item2,
                                                            (ingredientCollections[recipeIndex][i].Item2 == 1 || ingredientCollections[recipeIndex][i].Item2 == 0.5) ? "tablespoon" : "tablespoons",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5);
                                                    }
                                                    else
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            Math.Round((ingredientCollections[recipeIndex][i].Item2 * 3), 2),
                                                            "teaspoons",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5);
                                                    }
                                                    continue;

                                                case "oz":
                                                case "ounce":
                                                case "ounces":
                                                    if (ingredientCollections[recipeIndex][i].Item2 >= 16)
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            Math.Round((ingredientCollections[recipeIndex][i].Item2 / 16), 2),
                                                            "pounds",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5);
                                                    }
                                                    else if (ingredientCollections[recipeIndex][i].Item2 >= (double)0.5)
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            ingredientCollections[recipeIndex][i].Item2,
                                                            (ingredientCollections[recipeIndex][i].Item2 == 1 || ingredientCollections[recipeIndex][i].Item2 == 0.5) ? "ounce" : "ounces",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5);
                                                    }
                                                    else
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            Math.Round((ingredientCollections[recipeIndex][i].Item2 * 16), 2),
                                                            "ounces",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5);
                                                    }
                                                    continue;

                                                case "lb":
                                                case "pound":
                                                case "pounds":
                                                    if (ingredientCollections[recipeIndex][i].Item2 >= 1)
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            ingredientCollections[recipeIndex][i].Item2,
                                                            (ingredientCollections[recipeIndex][i].Item2 == 1 || ingredientCollections[recipeIndex][i].Item2 == 0.5) ? "pound" : "pounds",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5);
                                                    }
                                                    else
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            Math.Round((ingredientCollections[recipeIndex][i].Item2 * 16), 2),
                                                            "ounces",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5);
                                                    }
                                                    continue;

                                                case "cup":
                                                case "cups":
                                                    if (ingredientCollections[recipeIndex][i].Item2 >= 16)
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            Math.Round((ingredientCollections[recipeIndex][i].Item2 / 16), 2),
                                                            "gallons",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5);
                                                    }
                                                    else if (ingredientCollections[recipeIndex][i].Item2 >= (double)0.25)
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            ingredientCollections[recipeIndex][i].Item2,
                                                            (ingredientCollections[recipeIndex][i].Item2 == 1 || ingredientCollections[recipeIndex][i].Item2 == 0.5) ? "cup" : "cups",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5
                                                        );
                                                    }
                                                    else
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            Math.Round((ingredientCollections[recipeIndex][i].Item2 * 48), 2),
                                                            "teaspoons",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5
                                                        );
                                                    }
                                                    continue;

                                                case "gal":
                                                case "gallon":
                                                case "gallons":
                                                    if (ingredientCollections[recipeIndex][i].Item2 >= 1)
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            ingredientCollections[recipeIndex][i].Item2,
                                                            (ingredientCollections[recipeIndex][i].Item2 == 1 || ingredientCollections[recipeIndex][i].Item2 == 0.5) ? "gallon" : "gallons",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5
                                                        );
                                                    }
                                                    else
                                                    {
                                                        ingredientCollections[recipeIndex][i] = (
                                                            ingredientCollections[recipeIndex][i].Item1,
                                                            Math.Round((ingredientCollections[recipeIndex][i].Item2 * 16), 2),
                                                            "cups",
                                                            ingredientCollections[recipeIndex][i].Item4,
                                                            ingredientCollections[recipeIndex][i].Item5
                                                        );
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
                                else/// the reset code 4th option must be here 
                                {
                                    if (number == 4)
                                    {
                                        //If the value of number is equal to 4, then the program will reset the Quantity
                                        //and Unit values in the array of objects arrIngredientClasses to their original values.
                                        ingredientCollections[recipeIndex] = new List<(string, double, string, double, string)>(ingredientCollectionsOriginal[recipeIndex]);
                                        valid = true;
                                    }
                                    else
                                    {
                                        valid = false;
                                        clockTimerClass.ChangeToErrorColor();
                                        Console.Write("\r\nWhat scaling factor would you like to use? Please enter a number to indicate the desired factor: ");
                                        clockTimerClass.ChangeBack();
                                        Console.Write("\n1. Half\n2. Double\n3. Triple\n4. Reset\n>");
                                        userInput = Console.ReadLine();
                                    }
                                }
                            }
                            else
                            {
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
                            valid = false;
                            clockTimerClass.ChangeToErrorColor();
                            Console.Write("\r\nWhat scaling factor would you like to use? Please enter a number to indicate the desired factor: ");
                            clockTimerClass.ChangeBack();
                            Console.Write("\n1. Half\n2. Double\n3. Triple\n4. Reset\n>");
                            userInput = Console.ReadLine();
                        }
                    } while (!valid);

                    string selectedRecipeName = sortedRecipeNames[recipeIndex];
                    RecipeClass selectedRecipe = RecipeList.Find(recipe => recipe.RecipeName == selectedRecipeName);
                    List<string> steps = stepCollections[recipeIndex];
                    List<(string, double, string, double, string)> ingredientTuples = ingredientCollections[recipeIndex];

                    // Display the selected recipe
                    //Console.WriteLine($"Recipe: {selectedRecipe.RecipeName}");

                    PrintIngredients(clockTimerClass, selectedRecipe.RecipeName);

                    for (int i = 0; i < ingredientTuples.Count; i++)
                    {
                        string quantity = validate.Convert_Numerical_Value_To_Corresponding_Text(ingredientTuples[i].Item2);
                        string unit = " " + ingredientTuples[i].Item3;
                        string name = " " + ingredientTuples[i].Item1;
                        string foodgroup = " " + selectedRecipe.IngredientListIn[i].FoodGroup;
                        string calories = " " + Convert.ToString(selectedRecipe.IngredientListIn[i].IngredientCalories);

                        Console.WriteLine($"{quantity,-16}\t{unit,-16}\t{name,-16}\t{foodgroup,-16}\t{calories}");
                    }

                    PrintSteps(clockTimerClass);

                    for (int i = 0; i < steps.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}.\t{steps[i]}");
                    }

                    Console.WriteLine();
                    DisplayLine();
                    Console.WriteLine();

                }
                else if (userChoice == sortedRecipeNames.Count + 1)
                {
                    ingredientCollections[recipeIndex] = new List<(string, double, string, double, string)>(ingredientCollectionsOriginal[recipeIndex]);
                    PrintRecipe(clockTimerClass);
                }
                else
                {
                    Console.WriteLine("\r\nInvalid selection. Please try again.");
                    ScaleQuantities(clockTimerClass);
                }
            }
            else
            {
                Console.WriteLine("\r\nInvalid selection. Please try again.");
                ScaleQuantities(clockTimerClass);
            }


        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method is the main entry point for the recipe application. It prompts the user to select between two 
        /// options: enter a new recipe or quit. If the user chooses to enter a recipe, it asks for confirmation 
        /// and then calls the WriteRecipeToArrays method to write the recipe information to arrays. It then calls
        /// the PrintRecipe method to display the recipe information to the console. If the user chooses to quit,
        /// it calls the PrintQuitMessage method to display a quit message. The ClockTimerClass parameter is used 
        /// to change the color of the console output.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>
        public void Application(ClockTimerClass clockTimerClass)
        {
            // Get the valid user choice from the user using the GetValidUserChoice method
            int userChoice = GetValidUserChoice(clockTimerClass);

            if (userChoice == 1)
            {
                string userInput = GetValidYesOrNoInput(clockTimerClass, "\r\nDo you want to go back? " +
                    "(Yes or No): ");

                if (userInput.Trim().ToUpper().Equals("YES"))
                {
                    Application(clockTimerClass);
                }
                else
                {
                    WriteRecipeToArrays(clockTimerClass);
                    if (ingredientCollectionsOriginal != null)
                    {
                        ingredientCollectionsOriginal.Clear();
                    }

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
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>
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
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>
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
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>
        private void WriteRecipeToArrays(ClockTimerClass clockTimerClass)
        {
            //WriteIngredientsToArrays(clockTimerClass);
            //WriteStepsToArray(clockTimerClass);
            if (AnotherRecipe == "Yes")
            {
                recipeNames = recipeNames;
                stepCollections = stepCollections;
                ingredientCollections = ingredientCollections;
            }
            else
            {
                recipeNames = new List<string>();
                stepCollections = new List<List<string>>();
                ingredientCollections = new List<List<(string, double, string, double, string)>>();
            }

            string userInput = GetValidYesOrNoInput(clockTimerClass, "\r\nDo you want to enter a recipe? " +
                    "(Yes or No): ");

            do
            {
                if (userInput.Trim().ToUpper().Equals("NO"))
                {
                    PrintRecipe(clockTimerClass);
                }
                else
                {
                    AddRecipe(clockTimerClass);   //<---------------------------------------------------------
                    userInput = GetValidYesOrNoInput(clockTimerClass, "\r\nDo you want to enter a recipe? " +
                       "(Yes or No): ");
                }
            } while (userInput.Trim().ToUpper().Equals("YES"));
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Prints a thank you message and sets the console color to black with white text. Also calls 
        /// DisplayBlock() method to display a horizontal line. After printing the message, the console 
        /// color is reset to its previous state and waits for user input.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>
        private void PrintQuitMessage(ClockTimerClass clockTimerClass)
        {
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine("\r\n" + FAREWELL);
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
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>
        public void WriteStepsToArray(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            bool valid = false;
            int number = stepClass.GetNumOfSteps(clockTimerClass);
            this.numberOfSteps = number;
            string userInput = string.Empty;

            StepList = new List<StepClass>();
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
                        StepList.Add(new StepClass { Step = userInput });
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
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>
        public void PrintSteps(ClockTimerClass clockTimerClass)
        {
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine("\r\nSteps:\r\n");
            clockTimerClass.ChangeBack();
            //StepReport();
            //Console.WriteLine();
            //DisplayLine();
            //Console.WriteLine();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//

        private List<string> recipeNames { get; set; }
        private List<List<string>> stepCollections { get; set; }
        private List<List<(string, double, string, double, string)>> ingredientCollections { get; set; }

        private List<RecipeClass> RecipeList = new List<RecipeClass>();

        public List<string> GetRecipeNames()
        {
            return recipeNames;
        }

        public List<List<string>> GetStepCollections()
        {
            return stepCollections;
        }

        public List<List<(string, double, string, double, string)>> GetIngredientCollections()
        {
            return ingredientCollections;
        }

        public void StoreRecipes(List<List<string>> stepCollections, List<List<(string, double, string, double, string)>> ingredientCollections)
        {
            // Retrieve the generic collection from the WorkerClass
            List<string> recipeNames = GetRecipeNames();

            // Clear the existing recipe list
            RecipeList.Clear();

            // Iterate over the collections and create Recipe objects to store in the list
            for (int i = 0; i < recipeNames.Count; i++)
            {
                RecipeClass recipe = new RecipeClass();
                recipe.RecipeName = recipeNames[i];

                // Check if the stepCollections and ingredientCollections have elements at the current index
                if (i < stepCollections.Count && i < ingredientCollections.Count)
                {
                    List<string> steps = stepCollections[i];
                    List<(string, double, string, double, string)> ingredientTuples = ingredientCollections[i];

                    // Add steps to the recipe
                    foreach (string step in steps)
                    {
                        StepClass stepObj = new StepClass();
                        stepObj.Step = step;
                        recipe.StepListIn.Add(stepObj);
                    }

                    // Add ingredients to the recipe
                    foreach ((string name, double quantity, string unit, double calories, string foodgroup) in ingredientTuples)
                    {
                        IngredientClass ingredientObj = new IngredientClass();
                        ingredientObj.Name = name;
                        ingredientObj.Quantity = quantity;
                        ingredientObj.Unit = unit;
                        ingredientObj.IngredientCalories = calories;
                        ingredientObj.FoodGroup = foodgroup;
                        recipe.IngredientListIn.Add(ingredientObj);
                    }
                }
                else
                {
                    // error handling
                }

                // Add the recipe to the list
                RecipeList.Add(recipe);
            }
        }

        private List<double> totalCaloriesList = new List<double>();

        public void AddRecipe(ClockTimerClass clockTimerClass)
        {
            RecipeClass recipeClass = new RecipeClass();
            string newRecipeName = recipeClass.GetRecipeName(clockTimerClass);

            // Write the steps and ingredients to the arrays
            WriteIngredientsToArrays(clockTimerClass);
            WriteStepsToArray(clockTimerClass);

            double totalCalories = 0.0f;
            // Populate stepCollections and ingredientCollections based on the arrays
            List<string> newSteps = new List<string>(StepList.Select(step => step.Step));
            List<(string, double, string, double, string)> newIngredients = new List<(string, double, string, double, string)>();
            for (int i = 0; i < IngredientList.Count; i++)
            {
                var ingredient = IngredientList[i];
                newIngredients.Add((ingredient.Name, ingredient.Quantity, ingredient.Unit, ingredient.IngredientCalories, ingredient.FoodGroup));
                totalCalories += ingredient.IngredientCalories;
            }

            totalCaloriesList.Add(totalCalories);

            // Update the recipe data
            recipeNames.Add(newRecipeName);
            stepCollections.Add(newSteps);
            ingredientCollections.Add(newIngredients);

            // Store the updated recipes
            StoreRecipes(stepCollections, ingredientCollections);
        }

        private void DisplayRecipeNames(ClockTimerClass clockTimerClass)
        {
            string userInput;
            int userChoice;

            // Retrieve and display the recipe names in alphabetical order
            List<string> recipeNames = GetRecipeNames();
            List<List<string>> stepCollections = GetStepCollections();
            List<List<(string, double, string, double, string)>> ingredientCollections = GetIngredientCollections();

            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine("\r\nPlease select the recipe to view by entering its corresponding number: ");
            clockTimerClass.ChangeBack();

            // Sort the recipe names in alphabetical order
            List<string> sortedRecipeNames = recipeNames.OrderBy(name => name).ToList();

            for (int i = 0; i < sortedRecipeNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sortedRecipeNames[i]}");
            }
            Console.WriteLine($"{sortedRecipeNames.Count + 1}. Back");
            Console.Write(">");

            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out userChoice))
            {
                if (userChoice >= 1 && userChoice <= sortedRecipeNames.Count)
                {
                    int recipeIndex = userChoice - 1;

                    if (recipeIndex >= 0 && recipeIndex < stepCollections.Count)
                    {
                        string selectedRecipeName = sortedRecipeNames[recipeIndex];
                        RecipeClass selectedRecipe = RecipeList.Find(recipe => recipe.RecipeName == selectedRecipeName);
                        List<string> steps = stepCollections[recipeIndex];
                        List<(string, double, string, double, string)> ingredientTuples = ingredientCollections[recipeIndex];

                        // Display the selected recipe
                        //Console.WriteLine($"Recipe: {selectedRecipe.RecipeName}");

                        PrintIngredients(clockTimerClass, selectedRecipe.RecipeName);

                        for (int i = 0; i < selectedRecipe.IngredientListIn.Count; i++)
                        {
                            string quantity = validate.Convert_Numerical_Value_To_Corresponding_Text(selectedRecipe.IngredientListIn[i].Quantity);
                            string unit = " " + selectedRecipe.IngredientListIn[i].Unit;
                            string name = " " + selectedRecipe.IngredientListIn[i].Name;
                            string foodgroup = " " + selectedRecipe.IngredientListIn[i].FoodGroup;
                            string calories = " " + Convert.ToString(selectedRecipe.IngredientListIn[i].IngredientCalories);

                            Console.WriteLine($"{quantity,-16}\t{unit,-16}\t{name,-16}\t{foodgroup,-16}\t{calories}");
                        }

                        Console.WriteLine("\r\n The total number of calories is: " + Convert.ToString(totalCaloriesList[recipeIndex]));//<---------checking if it works 

                        PrintSteps(clockTimerClass);

                        for (int i = 0; i < steps.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}.\t{steps[i]}");
                        }

                        Console.WriteLine();
                        DisplayLine();
                        Console.WriteLine();
                    }
                    else
                    {
                        // error handling
                    }
                }
                else if (userChoice == sortedRecipeNames.Count + 1)
                {
                    // User chose to go back
                    PrintRecipe(clockTimerClass);
                }
                else
                {
                    // error handling
                }
            }
            else
            {
                // error handling
            }
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//