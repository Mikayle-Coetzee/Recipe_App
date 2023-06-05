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

namespace POE_PROG6221_ST10023767_GR01
{
    public class RecipeClass
    {
        /// <summary>
        /// Holds the recipe name
        /// </summary>
        public string RecipeName { get; set; }

        /// <summary>
        /// Holds the list of steps
        /// </summary>
        public List<StepClass> StepListIn { get; set; }

        /// <summary>
        /// Holds the list of ingredients
        /// </summary>
        public List<IngredientClass> IngredientListIn { get; set; }

        /// <summary>
        /// Holds the list of recipes.
        /// </summary>
        public List<RecipeClass> RecipeList { get; set; }

        /// <summary>
        /// Instantiates a new instance of the Validation class. The Validation class can now be used to 
        /// perform validation tasks throughout the rest of the code.
        /// /// </summary>
        public Validation validate = new Validation();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for the RecipeClass. Initializes the step list, ingredient list, and recipe list.
        /// </summary>
        public RecipeClass()
        {
            StepListIn = new List<StepClass>();
            IngredientListIn = new List<IngredientClass>();
            RecipeList = new List<RecipeClass>();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to enter the name of the recipe and retrieves it as a string.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass</param>
        /// <returns>The name of the recipe as a string</returns>
        public string GetRecipeName(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            bool valid;
            string userInput;
            string name = string.Empty;

            Console.Write("\nPlease enter the recipe name as text (e.g. 'Cake'):   \t");
            userInput = validate.GetUserInput();

            do
            {
                valid = validate.Validate_String(userInput);

                if (valid)
                {
                    name = userInput;
                }
                else
                {
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nPlease re-enter the recipe name as text (e.g. 'Cake'):   \t");
                    userInput = validate.GetUserInput();
                    clockTimerClass.ChangeBack();
                }
            } while (!valid);

            return name;
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
