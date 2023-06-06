#region
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
using System.Windows.Forms;
using POE_PROG6221_ST10023767_GR01.Classes;
using System.Runtime.InteropServices;

namespace POE_PROG6221_ST10023767_GR01
{
    /// <summary>
    /// Delegate for notifying the user about the calories of a recipe.
    /// </summary>
    /// <param name="recipeName">The name of the recipe.</param>
    public delegate void RecipeNotificationDelegate(double calories);

    public class WorkerClass
    {
        /// <summary>
        /// Initializes a private instance of the DisplayClass 
        /// </summary>
        private readonly DisplayClass displayClass = new DisplayClass();

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
        /// Holds the list of ingredients for the recipe.
        /// </summary>
        public List<IngredientClass> IngredientList { get; set; }

        /// <summary>
        /// Holds the list of steps for the recipe.
        /// </summary>
        public List<StepClass> StepList { get; set; }

        /// <summary>
        /// Flag indicating whether the user wants to enter another recipe.
        /// </summary>
        private bool AnotherRecipe = false;

        /// <summary>
        /// An array that holds the possible scaling factors.
        /// </summary>
        public double[] arrFactor = { 0.5, 2, 3, 4 };

        /// <summary>
        /// Initializes a private instance of the RecipeClass.
        /// </summary>
        private RecipeClass recipeClass = new RecipeClass();

        /// <summary>
        /// Instantiates a new instance of the Validation class. The Validation class can now be used to 
        /// perform validation tasks throughout the rest of the code.
        /// /// </summary>
        public Validation validate = new Validation();

        /// <summary>
        /// Holds the list of names of the recipes.
        /// </summary>
        private List<string> RecipeNames { get; set; }

        /// <summary>
        /// Holds the list of collections of steps for each recipe.
        /// </summary>
        private List<List<string>> StepCollections { get; set; }

        /// <summary>
        /// Holds the list of collections of ingredients for each recipe.
        /// </summary>
        private List<List<(string, double, string, double, string, double, double, string)>> IngredientCollections { get; set; }

        /// <summary>
        /// Holds the list of recipes.
        /// </summary>
        private List<RecipeClass> RecipeList = new List<RecipeClass>();

        /// <summary>
        /// Holds the list total calories for each recipe.
        /// </summary>
        private List<double> TotalCaloriesList = new List<double>();

        /// <summary>
        /// Event that is triggered when a recipe exceeds the calorie limit.
        /// </summary>
        public event RecipeNotificationDelegate RecipeExceedsCaloriesEvent;

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for WorkerClass.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        public WorkerClass(ClockTimerClass clockTimerClass)
        {
            try
            {
                displayClass.WelcomeMessage(clockTimerClass);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while displaying the welcome message: " + ex.Message);
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method initializes and populates a list with information about the ingredients.It first calls 
        /// GetNumOfIngredients method to get the number of ingredients. It then prompts the user to provide details 
        /// for each ingredient, and calls the GetIngredientName, GetIngredientQuantity, GetIngredientUnit,
        /// GetIngredientCalories, and GetFoodGroup methods to get the name, quantity, unit, calories and food group
        /// of each ingredient. The information of the ingredient is then added to the list.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        public void WriteIngredientsToList(ClockTimerClass clockTimerClass)
        {
            int number = ingredientClass.GetNumOfIngredients(clockTimerClass);
            this.numberOfIngredients = number;

            IngredientList = new List<IngredientClass>();


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
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prints the menu and provides options for user selection.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        public void PrintMenu(ClockTimerClass clockTimerClass)
        {
            // Change console colors for better visibility
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);

            // Prompt the user to select an option
            Console.Write("\r\nPlease select an option by entering its corresponding number: ");
            clockTimerClass.ChangeBack();
            Console.Write("\r\n1. Print A Specific Recipe \r\n2. Scale Quantities \r\n3. Enter Another Recipe \r\n4. Clear Recipe \r\n5. Quit \n>");

            // Read user input
            string userInput = validate.GetUserInput();

            // Get valid user choice
            int userChoice = GetValidUserChoice(userInput, clockTimerClass);

            switch (userChoice)
            {
                case 1:
                    SelectRecipeNameToView(clockTimerClass);
                    PrintMenu(clockTimerClass);
                    break;
                case 2:
                    ScaleQuantities(clockTimerClass);
                    PrintMenu(clockTimerClass);
                    break;
                case 3:
                    AnotherRecipe = true;
                    WriteRecipeToList(clockTimerClass);
                    PrintMenu(clockTimerClass);
                    break;
                case 4:
                    // Prompt user for confirmation before clearing the recipe
                    ClearRecipe(clockTimerClass);
                    PrintMenu(clockTimerClass);
                    break;
                case 5:
                    // Print the quit message and exit the program
                    displayClass.PrintQuitMessage(clockTimerClass);
                    break;
                default:
                    break;
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method clears a selected recipe 
        /// </summary>
        /// <param name="clockTimerClass"></param>
        private void ClearRecipe(ClockTimerClass clockTimerClass)
        {
            bool valid = false;

            // Retrieve and display the recipe names in alphabetical order
            List<string> recipeNames = GetRecipeNames();
            List<List<string>> stepCollections = GetStepCollections();
            List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections = GetIngredientCollections();

            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine("\r\nPlease select the recipe to clear by entering its corresponding number: ");

            do
            {
                // Sort the recipe names in alphabetical order
                List<string> sortedRecipeNames = recipeNames.OrderBy(name => name).ToList();

                displayClass.PrintRecipeNames(sortedRecipeNames, clockTimerClass);

                string userInput = validate.GetUserInput();
                int userChoice = sortedRecipeNames.Count + 1;

                if (validate.ValidateUserInput(userInput, 1, sortedRecipeNames.Count))
                {
                    userChoice = Convert.ToInt32(userInput);
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nDo you still want to proceed with clearing your recipe, considering " +
                                    "\r\nthat this action is irreversible? (Yes/No):");
                    string newInput = validate.GetUserInput();
                    clockTimerClass.ChangeBack();

                    while (validate.Validate_Yes_Or_No(newInput) == false)
                    {
                        clockTimerClass.ChangeToErrorColor();
                        Console.Write("\r\nDo you still want to proceed with clearing your recipe, considering " +
                                        "\r\nthat this action is irreversible? (Yes/No):");
                        newInput = validate.GetUserInput();
                        clockTimerClass.ChangeBack();
                    }

                    if (newInput.Trim().ToUpper().Equals("NO"))
                    {
                        // Cancel the request to enter a new recipe
                        clockTimerClass.ChangeToErrorColor();
                        Console.WriteLine("\r\nThe request to enter a new recipe has been canceled.");
                        clockTimerClass.ChangeBack();
                        PrintMenu(clockTimerClass);
                    }
                    else
                    {
                        int recipeIndex = userChoice - 1;
                        // Code to revert the ordered recipe to the unsorted one...
                        string selectedRecipeName = sortedRecipeNames[recipeIndex];
                        int recipeIndex2 = RecipeList.IndexOf(RecipeList.Find(recipe => recipe.RecipeName == selectedRecipeName));

                        // Remove the selected recipe from the collections
                        recipeNames.RemoveAt(recipeIndex2);
                        stepCollections.RemoveAt(recipeIndex2);
                        ingredientCollections.RemoveAt(recipeIndex2);
                        TotalCaloriesList.RemoveAt(recipeIndex2);
                        RecipeList.RemoveAt(recipeIndex2);

                        Console.WriteLine("\r\nRecipe '" + selectedRecipeName + "' was sucsessfully deleted.");
                    }
                    valid = true;
                }
                else
                {
                    if (userChoice == sortedRecipeNames.Count + 1)
                    {
                        PrintMenu(clockTimerClass);
                        valid = true;
                    }
                    else
                    {
                        clockTimerClass.ChangeToErrorColor();
                        Console.WriteLine("\r\nPlease re-select the recipe to clear by entering its corresponding number: ");
                        clockTimerClass.ChangeBack();
                        valid = false;
                    }
                }
            } while (valid == false);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method takes in a user input, a validation object, and a clock timer object, and returns a valid 
        /// user choice as an integer between 1 and 5. It validates the user input to ensure that it is a positive 
        /// integer between 1 and 5, and prompts the user for valid input if necessary.It also changes the console 
        /// color to indicate an error if the user enters invalid input.
        /// </summary>
        /// <param name="userInput">The user input to validate.</param>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        /// <returns>The valid user choice</returns>
        private int GetValidUserChoice(string userInput, ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            int number = 0;
            bool valid = false;

            do
            {
                if (validate.ValidateUserInput(userInput, 1, 5))
                {
                    number = Convert.ToInt32(userInput);
                    valid = true;
                }

                if (!valid)
                {
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nPlease select an option by entering its corresponding number: ");
                    clockTimerClass.ChangeBack();
                    Console.Write("\r\n1. Print A Specific Recipe \r\n2. Scale Quantities \r\n3. Enter Another Recipe \r\n4. Clear Recipe \r\n5. Quit \n>");
                    userInput = validate.GetUserInput();
                }

            } while (!valid);

            return number;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method that takes a ClockTimerClass object as an argument and prompts the user to enter a scaling factor.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        public void ScaleQuantities(ClockTimerClass clockTimerClass)
        {
            double factor = 0.0;
            string userInput = string.Empty;
            bool valid = false;
            string userInput2;
            int recipeIndex = 1;

            // Retrieve and display the recipe names in alphabetical order
            List<string> recipeNames = GetRecipeNames();
            List<List<string>> stepCollections = GetStepCollections();
            List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections = GetIngredientCollections();

            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine("\r\nPlease select the recipe to scale by entering its corresponding number: ");
            clockTimerClass.ChangeBack();

            do
            {
                //// Sort the recipe names in alphabetical order
                List<string> sortedRecipeNames = recipeNames.OrderBy(name => name).ToList();

                displayClass.PrintRecipeNames(sortedRecipeNames, clockTimerClass);
                userInput2 = validate.GetUserInput();
                int userChoice =  sortedRecipeNames.Count + 1;

                if (validate.ValidateUserInput(userInput2, 1, sortedRecipeNames.Count))
                {
                    userChoice = Convert.ToInt32(userInput2);

                    recipeIndex = userChoice - 1;

                    string selectedRecipeName = sortedRecipeNames[recipeIndex];
                    int recipeIndex2 = RecipeList.IndexOf(RecipeList.Find(recipe => recipe.RecipeName == selectedRecipeName));

                    displayClass.DisplayScalingFactorOptions(clockTimerClass);

                    factor = GetScalingFactor(clockTimerClass);

                    int listLength = ingredientCollections[recipeIndex2].Count;
                    TotalCaloriesList[recipeIndex2] = Math.Round((TotalCaloriesList[recipeIndex2] * factor), 2);

                    ApplyScalingFactor(recipeIndex2, factor, ingredientCollections);


                    RecipeClass selectedRecipe = RecipeList.Find(recipe => recipe.RecipeName == selectedRecipeName);
                    List<string> steps = stepCollections[recipeIndex2];

                    List<(string, double, string, double, string, double, double, string)> ingredientTuples = ingredientCollections[recipeIndex2];
                    displayClass.PrintIngredients(clockTimerClass, selectedRecipe.RecipeName, ingredientTuples,
                        TotalCaloriesList, recipeIndex2);

                    displayClass.PrintSteps(clockTimerClass, steps);

                    double totalCalories = TotalCaloriesList[recipeIndex2]; 

                    NotifyUser(totalCalories);

                    break;
                }
                else if (userChoice == sortedRecipeNames.Count + 1)
                {
                    PrintMenu(clockTimerClass);
                    valid = true;
                }
                else
                {
                    clockTimerClass.ChangeToErrorColor();
                    Console.WriteLine("\r\nPlease re-select the recipe to scale by entering its corresponding number: ");
                    clockTimerClass.ChangeBack();
                    valid = false;
                }
            
            } while (valid == false);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method adjusts the quantity and unit of each ingredient in a list based on the selected factor.
        /// The method uses a loop to iterate through the list of ingredients and a switch statement to determine how
        /// to adjust each ingredients unit based on its current unit and quantity.
        /// </summary>
        /// <param name="recipeIndex2">The index of the selected recipe</param>
        /// <param name="factor">The factor that the user wants to scale to</param>
        /// <param name="ingredientCollections">The list of ingredients</param>
        private void ApplyScalingFactor(int recipeIndex2, double factor,
            List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections)
        {
            if (factor >= 0 && factor < 4)
            {
                for (int i = 0; i < ingredientCollections[recipeIndex2].Count; i++)
                {
                    ingredientCollections[recipeIndex2][i] = (
                        ingredientCollections[recipeIndex2][i].Item1,
                        Math.Round((ingredientCollections[recipeIndex2][i].Item2 * factor), 2),
                        ingredientCollections[recipeIndex2][i].Item3,
                        Math.Round((ingredientCollections[recipeIndex2][i].Item4 * factor), 2),
                        ingredientCollections[recipeIndex2][i].Item5,
                        ingredientCollections[recipeIndex2][i].Item6,
                        ingredientCollections[recipeIndex2][i].Item7,
                        ingredientCollections[recipeIndex2][i].Item8
                    );

                    for (int j = 0; j < 2; j++)
                    {
                        switch (ingredientCollections[recipeIndex2][i].Item3.ToLower())
                        {
                            case "tsp":
                            case "teaspoon":
                            case "teaspoons":
                                if (ingredientCollections[recipeIndex2][i].Item2 >= 3)
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        Math.Round((ingredientCollections[recipeIndex2][i].Item2 / 3), 2),
                                        (ingredientCollections[recipeIndex2][i].Item2 == 1 || ingredientCollections[recipeIndex2][i].Item2 == 0.5) ? "tablespoon" : "tablespoons",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8);

                                }
                                else if (ingredientCollections[recipeIndex2][i].Item2 >= (double)0.5)
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        ingredientCollections[recipeIndex2][i].Item2,
                                        (ingredientCollections[recipeIndex2][i].Item2 == 1 || ingredientCollections[recipeIndex2][i].Item2 == 0.5) ? "teaspoon" : "teaspoons",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8);
                                }
                                else
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        Math.Round((ingredientCollections[recipeIndex2][i].Item2 * 3), 2),
                                        "teaspoons",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8
                                    );
                                }
                                continue;

                            case "tbsp":
                            case "tablespoon":
                            case "tablespoons":
                                if (ingredientCollections[recipeIndex2][i].Item2 >= 16)
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        Math.Round((ingredientCollections[recipeIndex2][i].Item2 / 16), 2),
                                        (ingredientCollections[recipeIndex2][i].Item2 == 1 || ingredientCollections[recipeIndex2][i].Item2 == 0.5) ? "cup" : "cups",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8);
                                }
                                else if (ingredientCollections[recipeIndex2][i].Item2 >= 3)
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        ingredientCollections[recipeIndex2][i].Item2,
                                        "tablespoons",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8);
                                }
                                else if (ingredientCollections[recipeIndex2][i].Item2 >= (double)0.5)
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        ingredientCollections[recipeIndex2][i].Item2,
                                        (ingredientCollections[recipeIndex2][i].Item2 == 1 || ingredientCollections[recipeIndex2][i].Item2 == 0.5) ? "tablespoon" : "tablespoons",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8);
                                }
                                else
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        Math.Round((ingredientCollections[recipeIndex2][i].Item2 * 3), 2),
                                        "teaspoons",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8);
                                }
                                continue;

                            case "oz":
                            case "ounce":
                            case "ounces":
                                if (ingredientCollections[recipeIndex2][i].Item2 >= 16)
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        Math.Round((ingredientCollections[recipeIndex2][i].Item2 / 16), 2),
                                        "pounds",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8);
                                }
                                else if (ingredientCollections[recipeIndex2][i].Item2 >= (double)0.5)
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        ingredientCollections[recipeIndex2][i].Item2,
                                        (ingredientCollections[recipeIndex2][i].Item2 == 1 || ingredientCollections[recipeIndex2][i].Item2 == 0.5) ? "ounce" : "ounces",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8);
                                }
                                else
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        Math.Round((ingredientCollections[recipeIndex2][i].Item2 * 16), 2),
                                        "ounces",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8);
                                }
                                continue;

                            case "lb":
                            case "pound":
                            case "pounds":
                                if (ingredientCollections[recipeIndex2][i].Item2 >= 1)
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        ingredientCollections[recipeIndex2][i].Item2,
                                        (ingredientCollections[recipeIndex2][i].Item2 == 1 || ingredientCollections[recipeIndex2][i].Item2 == 0.5) ? "pound" : "pounds",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8);
                                }
                                else
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        Math.Round((ingredientCollections[recipeIndex2][i].Item2 * 16), 2),
                                        "ounces",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8);
                                }
                                continue;

                            case "cup":
                            case "cups":
                                if (ingredientCollections[recipeIndex2][i].Item2 >= 16)
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        Math.Round((ingredientCollections[recipeIndex2][i].Item2 / 16), 2),
                                        "gallons",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8);
                                }
                                else if (ingredientCollections[recipeIndex2][i].Item2 >= (double)0.25)
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        ingredientCollections[recipeIndex2][i].Item2,
                                        (ingredientCollections[recipeIndex2][i].Item2 == 1 || ingredientCollections[recipeIndex2][i].Item2 == 0.5) ? "cup" : "cups",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8
                                    );
                                }
                                else
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        Math.Round((ingredientCollections[recipeIndex2][i].Item2 * 48), 2),
                                        "teaspoons",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8
                                    );
                                }
                                continue;

                            case "gal":
                            case "gallon":
                            case "gallons":
                                if (ingredientCollections[recipeIndex2][i].Item2 >= 1)
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        ingredientCollections[recipeIndex2][i].Item2,
                                        (ingredientCollections[recipeIndex2][i].Item2 == 1 || ingredientCollections[recipeIndex2][i].Item2 == 0.5) ? "gallon" : "gallons",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8
                                    );
                                }
                                else
                                {
                                    ingredientCollections[recipeIndex2][i] = (
                                        ingredientCollections[recipeIndex2][i].Item1,
                                        Math.Round((ingredientCollections[recipeIndex2][i].Item2 * 16), 2), "cups",
                                        ingredientCollections[recipeIndex2][i].Item4,
                                        ingredientCollections[recipeIndex2][i].Item5,
                                        ingredientCollections[recipeIndex2][i].Item6,
                                        ingredientCollections[recipeIndex2][i].Item7,
                                        ingredientCollections[recipeIndex2][i].Item8
                                    );
                                }
                                continue;
                            default:
                                Console.WriteLine("We don't recognize this unit of measurement. Please try again using a different unit.");
                                break;
                        }
                    }
                }
            }
            else
            {
                if (factor == 4)
                {
                    double numberOfCalories = 0.0f;
                    for (var i = 0; i < ingredientCollections[recipeIndex2].Count; i++)
                    {
                        ingredientCollections[recipeIndex2][i] = (
                            ingredientCollections[recipeIndex2][i].Item1,
                            ingredientCollections[recipeIndex2][i].Item6,
                            ingredientCollections[recipeIndex2][i].Item8,
                            ingredientCollections[recipeIndex2][i].Item7,
                            ingredientCollections[recipeIndex2][i].Item5,
                            ingredientCollections[recipeIndex2][i].Item6,
                            ingredientCollections[recipeIndex2][i].Item7,
                            ingredientCollections[recipeIndex2][i].Item8);

                        numberOfCalories += ingredientCollections[recipeIndex2][i].Item7;
                    }

                    TotalCaloriesList[recipeIndex2] = numberOfCalories;
                }
            }
        }


        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method will get the scaling factor selected from the user
        /// </summary>
        /// <param name="clockTimerClass"></param>
        /// <returns>The scaling factor</returns>
        private double GetScalingFactor(ClockTimerClass clockTimerClass)
        {
            string userInput = validate.GetUserInput();

            while (!validate.ValidateUserInput(userInput,1,4))
            {
                clockTimerClass.ChangeToErrorColor();
                Console.Write("\r\nWhat scaling factor would you like to use? Please re-enter a number to indicate the desired factor: ");
                clockTimerClass.ChangeBack();
                Console.Write("\n1. Half\n2. Double\n3. Triple\n4. Reset\n>");
                userInput = validate.GetUserInput();
            }

            return GetScalingFactorValue(userInput);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method will return the scaling factor 
        /// </summary>
        /// <param name="userInput"></param>
        /// <returns>The scaling factor</returns>
        private double GetScalingFactorValue(string userInput)
        {
            double[] arrFactor = { 0.5, 2, 3, 4 };
            int number = Convert.ToInt32(userInput);
            return arrFactor[number - 1];
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method is the main entry point for the recipe application. It prompts the user to select between two 
        /// options: enter a new recipe or quit. If the user chooses to enter a recipe, it asks for confirmation 
        /// and then calls the method to write the recipe information to lists. It then calls the PrintMenu method
        /// to display the recipe information to the console. If the user chooses to quit, it calls the 
        /// PrintQuitMessage method to display a quit message. The ClockTimerClass parameter is used to change 
        /// the color of the console output.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
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
                    WriteRecipeToList(clockTimerClass);
                    PrintMenu(clockTimerClass);
                }
            }
            else
            {
                displayClass.PrintQuitMessage(clockTimerClass);
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to enter a valid integer choice 1 or 2 for the menu options. Returns 
        /// the users choice. If the user enters an invalid input, displays an error message and prompts 
        /// again until a valid input is received, It returns the users choice as an integer.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
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
            userInput = validate.GetUserInput();
            
            while (!validate.ValidateUserInput(userInput,1,2))
            {
                clockTimerClass.ChangeToErrorColor();
                Console.Write("\r\nPlease select an option by entering its corresponding number: ");
                clockTimerClass.ChangeBack();
                Console.Write("\r\n1. Enter a new Recipe \r\n2. Quit \r\n>");
                userInput = validate.GetUserInput();
            }

            userChoice = Convert.ToInt32(userInput);

            return userChoice;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user with a message and validates their input to ensure they enter either "Yes" 
        /// or "No". It returns the user's input as a string
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        /// <param name="message">Message that is displayed to the user.</param>
        /// <returns>The string value of the users input that must be yes or no</returns>
        private string GetValidYesOrNoInput(ClockTimerClass clockTimerClass, string message)
        {
            // Initialize variable
            string userInput;

            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.Write(message);
            userInput = validate.GetUserInput();
            clockTimerClass.ChangeBack();

            while (validate.Validate_Yes_Or_No(userInput) == false)
            {
                clockTimerClass.ChangeToErrorColor();
                Console.Write(message);
                userInput = validate.GetUserInput();
                clockTimerClass.ChangeBack();
            }

            return userInput;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method writes the recipe details to lists for storage.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        private void WriteRecipeToList(ClockTimerClass clockTimerClass)
        {
            if (AnotherRecipe == true)
            {
                RecipeNames = RecipeNames;
                StepCollections = StepCollections;
                IngredientCollections = IngredientCollections;
            }
            else
            {
                RecipeNames = new List<string>();
                StepCollections = new List<List<string>>();
                IngredientCollections = new List<List<(string, double, string, double, string, double, double, string)>>();
            }

            string userInput = GetValidYesOrNoInput(clockTimerClass, "\r\nDo you want to enter a recipe? " +
                    "(Yes or No): ");

            if (userInput.Trim().ToUpper().Equals("NO"))
            {
                AnotherRecipe = false;
                PrintMenu(clockTimerClass);
            }
            else
            {
                AddRecipe(clockTimerClass);
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method prompts the user to input a description for each step of a process.
        /// The input is validated and stored in a list. The method uses the Validation class to ensure that the
        /// input is not empty or null. The method also takes an instance of the ClockTimerClass to change the
        /// console colors during the input process.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        public void WriteStepsToList(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            bool valid;
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
        /// Method returns the list of recipe names.
        /// </summary>
        /// <returns>The list of recipe names.</returns>
        public List<string> GetRecipeNames()
        {
            return RecipeNames;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method returns the collections of steps for each recipe.
        /// </summary>
        /// <returns>The collections of steps for each recipe.</returns>
        public List<List<string>> GetStepCollections()
        {
            return StepCollections;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method returns the collections of ingredients for each recipe.
        /// </summary>
        /// <returns>The collections of ingredients for each recipe.</returns>
        public List<List<(string, double, string, double, string, double, double, string)>> GetIngredientCollections()
        {
            return IngredientCollections;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Stores the recipes by populating the RecipeList based on the provided stepCollections and 
        /// ingredientCollections.
        /// </summary>
        /// <param name="stepCollections">The collections of steps for each recipe.</param>
        /// <param name="ingredientCollections">The collections of ingredients for each recipe.</param>
        public void StoreRecipes(List<List<string>> stepCollections,
            List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections)
        {
            RecipeNames = GetRecipeNames();
            RecipeList.Clear();

            for (int i = 0; i < RecipeNames.Count; i++)
            {
                RecipeClass recipe = new RecipeClass
                {
                    RecipeName = RecipeNames[i]
                };

                // Check if the stepCollections and ingredientCollections have elements at the current index
                if (i < stepCollections.Count && i < ingredientCollections.Count)
                {
                    List<string> steps = stepCollections[i];
                    List<(string, double, string, double, string, double, double, string)> ingredientTuples = ingredientCollections[i];

                    // Add steps to the recipe
                    foreach (string step in steps)
                    {
                        StepClass stepObj = new StepClass
                        {
                            Step = step
                        };
                        recipe.StepListIn.Add(stepObj);
                    }

                    // Add ingredients to the recipe
                    foreach ((string name, double quantity, string unit, double calories, string foodgroup, double originalQuantity,
                        double originalCalories, string originalUnit) in ingredientTuples)
                    {
                        IngredientClass ingredientObj = new IngredientClass
                        {
                            Name = name,
                            Quantity = quantity,
                            Unit = unit,
                            IngredientCalories = calories,
                            FoodGroup = foodgroup
                        };
                        recipe.IngredientListIn.Add(ingredientObj);
                    }
                }
                else
                {
                    Console.WriteLine("\r\nError: Incomplete data for recipe " + recipe.RecipeName);
                    StoreRecipes(stepCollections, ingredientCollections);
                }

                // Add the recipe to the list
                RecipeList.Add(recipe);
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Notifies the user about a recipe.
        /// </summary>
        /// <param name="recipeName">The name of the recipe.</param>
        protected virtual void NotifyUser(double calories)
        {
            RecipeExceedsCaloriesEvent?.Invoke(calories);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Handles the event when a recipe exceeds the calorie limit.
        /// </summary>
        /// <param name="recipeName">The name of the recipe.</param>
        private void HandleRecipeExceedsCalories(double calories)
        {
            if (calories > 300)
            {
                DialogResult result = MessageBox.Show($"The recipe exceeds 300 calories. Total calories are {calories}", "Recipe Notification",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method adds a new recipe to the recipe collections.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        public void AddRecipe(ClockTimerClass clockTimerClass)
        {
            RecipeClass recipeClass = new RecipeClass();
            string newRecipeName = recipeClass.GetRecipeName(clockTimerClass);

            // Write the steps and ingredients to the lists
            WriteIngredientsToList(clockTimerClass);
            WriteStepsToList(clockTimerClass);

            // Update the recipe data
            RecipeNames.Add(newRecipeName);
            StepCollections.Add(StepList.Select(step => step.Step).ToList());
            IngredientCollections.Add(IngredientList.Select(ingredient =>
                (ingredient.Name, ingredient.Quantity, ingredient.Unit, ingredient.IngredientCalories,
                ingredient.FoodGroup, ingredient.Quantity, ingredient.IngredientCalories, ingredient.Unit)).ToList());

            TotalCaloriesList = CalculateTotalCalories(IngredientCollections);

            double totalCalories = TotalCaloriesList.LastOrDefault(); // Get the total calories of the last added recipe

            RecipeExceedsCaloriesEvent += HandleRecipeExceedsCalories;
            NotifyUser(totalCalories);

            StoreRecipes(StepCollections, IngredientCollections);
        }


        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Calculates the total calories of a list of ingredients.
        /// </summary>
        /// <param name="IngredientList">The list of ingredients.</param>
        /// <returns>The total calories.</returns>
        public List<double> CalculateTotalCalories(List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections)
        {
            if (ingredientCollections == null)
            {
                return new List<double>();
            }

            List<double> recipeCalories = new List<double>();

            foreach (var ingredientList in ingredientCollections)
            {
                double recipeTotalCalories = 0.0;

                foreach (var ingredient in ingredientList)
                {
                    // Ensure the ingredient's calorie value is non-negative
                    double ingredientCalories = Math.Max(ingredient.Item4, 0);
                    recipeTotalCalories += ingredientCalories;
                }

                recipeCalories.Add(recipeTotalCalories);
            }

            return recipeCalories;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Displays the list of recipe names and allows the user to select a recipe to view its details.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        private void SelectRecipeNameToView(ClockTimerClass clockTimerClass)
        {
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine("\r\nPlease select the recipe to view by entering its corresponding number: ");
            clockTimerClass.ChangeBack();
            bool valid2 = false;
            // Retrieve and display the recipe names in alphabetical order
            List<string> recipeNames = GetRecipeNames();
            List<List<string>> stepCollections = GetStepCollections();
            List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections = GetIngredientCollections();

            do
            {
                // Sort the recipe names in alphabetical order
                List<string> sortedRecipeNames = recipeNames.OrderBy(name => name).ToList();

                displayClass.PrintRecipeNames(sortedRecipeNames,clockTimerClass);

                string userInput = validate.GetUserInput();

                bool valid = validate.ValidateUserInput(userInput,1, sortedRecipeNames.Count+1);
                while (!valid)
                {
                    PrintErrorSelection(clockTimerClass, sortedRecipeNames);
                    userInput = validate.GetUserInput();
                    valid = validate.ValidateUserInput(userInput,1, sortedRecipeNames.Count+1);
                }

                int userChoice = int.Parse(userInput);

                if (userChoice == sortedRecipeNames.Count + 1)
                {
                    // User chose to go back
                    PrintMenu(clockTimerClass);
                    return;
                }

                int recipeIndex = userChoice - 1;
                string selectedRecipeName = sortedRecipeNames[recipeIndex];
                int recipeIndex2 = RecipeList.IndexOf(RecipeList.Find(recipe => recipe.RecipeName == selectedRecipeName));
                if (recipeIndex2 >= 0 && recipeIndex2 < RecipeList.Count)
                {
                    RecipeClass selectedRecipe = RecipeList.Find(recipe => recipe.RecipeName == selectedRecipeName);
                    List<string> steps = stepCollections[recipeIndex2];
                    List<(string, double, string, double, string, double, double, string)> ingredientTuples = ingredientCollections[recipeIndex2];

                    // Display the selected recipe
                    displayClass.PrintIngredients(clockTimerClass, selectedRecipe.RecipeName, ingredientTuples, TotalCaloriesList, recipeIndex2);

                    displayClass.PrintSteps(clockTimerClass, steps);
                    valid2 = true;
                }
                else
                {
                    PrintErrorSelection(clockTimerClass, sortedRecipeNames);
                    valid2 = false;
                }
            } while (valid2 == false);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Prints an error message for an invalid recipe selection and prompts the user to enter a valid selection.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        /// <param name="sortedRecipeNames">A list of recipe names in sorted order.</param>
        private void PrintErrorSelection(ClockTimerClass clockTimerClass, List<string> sortedRecipeNames)
        {
            // Change console color to indicate error
            clockTimerClass.ChangeToErrorColor();

            Console.WriteLine("\r\nPlease re-select the recipe to view by entering its corresponding number: ");
            clockTimerClass.ChangeBack();

            // Print the sorted recipe names again
            displayClass.PrintRecipeNames(sortedRecipeNames, clockTimerClass);
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//