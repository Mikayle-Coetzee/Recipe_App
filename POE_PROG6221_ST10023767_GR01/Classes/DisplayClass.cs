#region
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

namespace POE_PROG6221_ST10023767_GR01.Classes
{
    public class DisplayClass
    {

        /// <summary>
        /// Instantiates a new instance of the Validation class. The Validation class
        /// can now be used to perform validation tasks throughout the rest of your code.
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
        /// Method prints a report of all the ingredients in the recipe.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>
        public void PrintIngredients(ClockTimerClass clockTimerClass, string selectedRecipe, List<(string, double, string, double, string)> ingredientTuples)
        {
            // Initialize variable
            string recipe = "Recipe: " + selectedRecipe;

            Console.WriteLine();
            //DisplayLine();
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine();
            Console.WriteLine(recipe);
            //DisplayBlock(recipe);
            Console.WriteLine();
            Console.WriteLine("\r\nIngredients:\r\n");
            Console.WriteLine($"{"Quantities",-16} \t {"Units",-16} \t {"Names"}\n");
            clockTimerClass.ChangeBack();

            for (int i = 0; i < ingredientTuples.Count; i++)
            {
                string quantity = validate.Convert_Numerical_Value_To_Corresponding_Text(ingredientTuples[i].Item2);
                string unit = " " + ingredientTuples[i].Item3;
                string name = " " + ingredientTuples[i].Item1;
                string foodgroup = " " + ingredientTuples[i].Item5;
                string calories = " " + Convert.ToString(ingredientTuples[i].Item4);

                Console.WriteLine($"{quantity,-16}\t{unit,-16}\t{name,-16}\t{foodgroup,-16}\t{calories}");
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Prints a report of the steps
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>
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

            //DisplayLine();

            Console.WriteLine();
        }


        public void DisplaySelectedRecipe(List<string> sortedRecipeNames, int recipeIndex, ClockTimerClass clockTimerClass,
            List<List<string>> stepCollections, List<List<(string, double, string, double, string)>> ingredientCollections,
            List<RecipeClass> RecipeList, List<double> TotalCaloriesList)
        {
            string selectedRecipeName = sortedRecipeNames[recipeIndex];
            RecipeClass selectedRecipe = RecipeList.Find(recipe => recipe.RecipeName == selectedRecipeName);
            List<string> steps = stepCollections[recipeIndex];
            List<(string, double, string, double, string)> ingredientTuples = ingredientCollections[recipeIndex];

            // Display the selected recipe
            PrintIngredients(clockTimerClass, selectedRecipe.RecipeName, ingredientTuples);

            Console.WriteLine("\r\nThe total number of calories is: " + Convert.ToString(TotalCaloriesList[recipeIndex]));

            PrintSteps(clockTimerClass, steps);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Prints a thank you message and sets the console color to black with white text. Also calls 
        /// DisplayBlock() method to display a horizontal line. After printing the message, the console 
        /// color is reset to its previous state and waits for user input.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass, control the color of the console</param>
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
