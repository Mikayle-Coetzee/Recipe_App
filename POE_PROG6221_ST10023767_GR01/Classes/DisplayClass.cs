#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 1 and 2
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace POE_PROG6221_ST10023767_GR01.Classes
{
    public class DisplayClass
    {
        /// <summary>
        /// Instantiates a new instance of the Validation class. The Validation class can now be used to 
        /// perform validation tasks throughout the rest of the code.
        /// /// </summary>
        public Validation validate = new Validation();

        /// <summary>
        /// Holds the constant welcome string message 
        /// </summary>
        public const string WELCOME = "Welcome aboard! Thank you for choosing our application.";

        /// <summary>
        /// Holds the constant farewell string message 
        /// </summary>
        public const string FAREWELL = "We appreciate you using our application. Have a great day!";

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for DisplayClass.
        /// </summary>
        public DisplayClass()
        {

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prints the ingredients of a selected recipe. 
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass</param>
        /// <param name="selectedRecipe">The name of the selected recipe</param>
        /// <param name="ingredientTuples">The list of ingredient tuples</param>
        /// <param name="TotalCaloriesList">The list of total calories for each recipe</param>
        /// <param name="recipeIndex">The index of the selected recipe</param>
        public void PrintIngredients(ClockTimerClass clockTimerClass, string selectedRecipe, 
            List<(string, double, string, double, string)> ingredientTuples, List<double> TotalCaloriesList,
             int recipeIndex)
        {
            // Initialize variable
            string recipe = "Recipe: " + selectedRecipe;

            Console.WriteLine();
            DisplayLine();
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine();
            Console.WriteLine(recipe);
            DisplayBlock(recipe);
            Console.WriteLine();
            Console.WriteLine("\r\nIngredients:\r\n");
           
            clockTimerClass.ChangeBack();

            for (int i = 0; i < ingredientTuples.Count; i++)
            {
                string quantity = (validate.Convert_Numerical_Value_To_Corresponding_Text(ingredientTuples[i].Item2));
                quantity = quantity[0].ToString().ToUpper() + quantity.Substring(1);
                string unit = ingredientTuples[i].Item3;
                string name = ingredientTuples[i].Item1;
                string foodgroup =  ingredientTuples[i].Item5;
                string calories = Convert.ToString(ingredientTuples[i].Item4);

                Console.WriteLine($"{i + 1} >\t{quantity} {unit} of {name} (Food Group: {foodgroup} | Calories = {calories})");
            }

            Console.WriteLine("\r\nThe total number of calories is: " + Convert.ToString(TotalCaloriesList[recipeIndex]));
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prints the steps of a recipe. Changes the console background and foreground colors to highlight 
        /// the steps. Prints each step with its corresponding index.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass</param>
        /// <param name="steps">The list of steps for the recipe</param>
        public void PrintSteps(ClockTimerClass clockTimerClass, List<string> steps)
        {
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine("\r\nSteps:\r\n");
            clockTimerClass.ChangeBack();

            for (int i = 0; i < steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}.\t{steps[i]}");
            }

            Console.WriteLine();

            DisplayLine();

            Console.WriteLine();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method displays the selected recipe along with its ingredients, total calories, and steps. Takes in 
        /// various parameters. Retrieves the selected recipe and relevant information based on the recipe index.
        /// Prints the ingredients, total calories, and steps of the selected recipe.
        /// </summary>
        /// <param name="sortedRecipeNames">The sorted list of recipe names</param>
        /// <param name="recipeIndex">The index of the selected recipe</param>
        /// <param name="clockTimerClass">Instance of the clock timer class</param>
        /// <param name="stepCollections">The collections of steps for each recipe</param>
        /// <param name="ingredientCollections">The collections of ingredients for each recipe</param>
        /// <param name="RecipeList">The list of recipe objects</param>
        /// <param name="TotalCaloriesList">The list of total calories for each recipe</param>
        public void DisplaySelectedRecipe(List<string> sortedRecipeNames, int recipeIndex, ClockTimerClass clockTimerClass,
            List<List<string>> stepCollections, List<List<(string, double, string, double, string)>> ingredientCollections,
            List<RecipeClass> RecipeList, List<double> TotalCaloriesList)
        {
            string selectedRecipeName = sortedRecipeNames[recipeIndex];
            RecipeClass selectedRecipe = RecipeList.Find(recipe => recipe.RecipeName == selectedRecipeName);
            List<string> steps = stepCollections[recipeIndex];
            List<(string, double, string, double, string)> ingredientTuples = ingredientCollections[recipeIndex];

            // Display the selected recipe
            PrintIngredients(clockTimerClass, selectedRecipe.RecipeName, ingredientTuples, TotalCaloriesList, recipeIndex);

            PrintSteps(clockTimerClass, steps);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prints a thank you message and sets the console color to black with white text. Also calls 
        /// DisplayBlock() method to display a horizontal line. After printing the message, the console 
        /// color is reset to its previous state and waits for user input.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass</param>
        public void PrintQuitMessage(ClockTimerClass clockTimerClass)
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
        /// Method prints a horizontal line made of Unicode character "\u2500". The length of the line is 100 
        /// characters.
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
        /// Method displays the welcome message on the console screen, changes the console background and 
        /// foreground colors, calls the DisplayBlock method to clear a block of characters on the screen, and 
        /// then resets the background color.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass</param>
        public void WelcomeMessage(ClockTimerClass clockTimerClass)
        {
            try
            {
                clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
                clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
                string welcome = "welcome";
                Console.Write(WELCOME);

                DisplayBlock(welcome);

                Console.WriteLine();
                clockTimerClass.ChangeBack();
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while displaying the welcome message: " + e.Message);
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prints the names of the recipes in the given list. Displays the recipe names with corresponding 
        /// numbers, followed by an option to go back. 
        /// </summary>
        /// <param name="recipeNames">The list of recipe names</param>
        public void PrintRecipeNames(List<string> recipeNames)
        {
            for (int i = 0; i < recipeNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipeNames[i]}");
            }
            Console.WriteLine($"{recipeNames.Count + 1}. Back");
            Console.Write(">");
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
