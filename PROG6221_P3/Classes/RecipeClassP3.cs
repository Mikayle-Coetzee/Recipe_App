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

namespace PROG6221_P3.Classes
{
    public class RecipeClassP3
    {
        /// <summary>
        /// Holds the recipe name
        /// </summary>
        public string RecipeName { get; set; }

        /// <summary>
        /// Holds the list of steps
        /// </summary>
        public List<StepClassP3> StepListIn { get; set; }

        /// <summary>
        /// Holds the list of ingredients
        /// </summary>
        public List<IngredientClassP3> IngredientListIn { get; set; }

        /// <summary>
        /// Holds the list of recipes.
        /// </summary>
        public List<RecipeClassP3> RecipeList { get; set; }

        /// <summary>
        /// Holds the list of total calories
        /// </summary>
        public List<double> TotalCalories { get; set; }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for the RecipeClass. Initializes the step list, ingredient list, recipe list, 
        /// and total calories list.
        /// </summary>
        public RecipeClassP3()
        {
            StepListIn = new List<StepClassP3>();
            IngredientListIn = new List<IngredientClassP3>();
            RecipeList = new List<RecipeClassP3>();
            TotalCalories = new List<double>();
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
