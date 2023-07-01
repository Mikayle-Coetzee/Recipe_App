using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221_P3.Classes
{
    public class IngredientClassP3
    {
        /// <summary>
        /// Holds the name of the ingredient 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Holds the quantity of the ingredient.
        /// </summary>
        public double Quantity { get; set; }

        /// <summary>
        /// Holds the unit of measurement
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Holds the number of ingredients 
        /// </summary>
        public int NumOfIngredients { get; set; }

        /// <summary>
        /// Holds the number of calories
        /// </summary>
        public double IngredientCalories { get; set; }

        /// <summary>
        /// Holds the selected food group
        /// </summary>
        public string FoodGroup { get; set; }


        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Initializes a new instance of the IngredientClassP3 with the specified properties.
        /// </summary>
        /// <param name="name">The name of the ingredient.</param>
        /// <param name="quantity">The quantity of the ingredient.</param>
        /// <param name="unit">The unit of measurement for the ingredient.</param>
        /// <param name="numOfIngredients">The number of ingredients.</param>
        /// <param name="calories">The calorie content of the ingredient.</param>
        /// <param name="foodgroup">The food group of the ingredient.</param>
        public IngredientClassP3(string name, double quantity, string unit, int numOfIngredients, double calories, string foodgroup)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.Unit = unit;
            this.NumOfIngredients = numOfIngredients;
            this.IngredientCalories = calories;
            this.FoodGroup = foodgroup;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for IngredientClassP3.
        /// </summary>
        public IngredientClassP3() { }

    }
}
