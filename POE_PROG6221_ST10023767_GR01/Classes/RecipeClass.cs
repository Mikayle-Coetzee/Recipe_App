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

namespace POE_PROG6221_ST10023767_GR01
{
    public class RecipeClass
    {
        /// <summary>
        /// Holds the recipe name
        /// </summary>
        public string RecipeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<StepClass> StepListIn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<IngredientClass> IngredientListIn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RecipeClass> RecipeList { get; set; }

        /// <summary>
        /// Instantiates a new instance of the Validation class. The Validation class
        /// can now be used to perform validation tasks throughout the rest of your code.
        /// /// </summary>
        public Validation validate = new Validation();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for StepClass.
        /// </summary>
        public RecipeClass()
        {
            StepListIn = new List<StepClass>();
            IngredientListIn = new List<IngredientClass>();
            RecipeList = new List<RecipeClass>();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// 
        /// </summary>
        /// <param name="clockTimerClass"></param>
        /// <returns></returns>
        public string GetRecipeName(ClockTimerClass clockTimerClass)
        {
            bool valid;
            string userInput;
            string name = string.Empty;

            Console.Write("\nPlease enter the recipe name as text (e.g. 'Cake'):   \t");
            userInput = Console.ReadLine();

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
                    userInput = Console.ReadLine();
                    clockTimerClass.ChangeBack();
                }
            } while (!valid);

            return name;
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
