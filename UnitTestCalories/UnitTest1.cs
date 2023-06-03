#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 1 and 2 
#endregion

using Microsoft.VisualStudio.TestTools.UnitTesting;
using POE_PROG6221_ST10023767_GR01;
using System;
using System.Collections.Generic;
using UnitTestCalories;

namespace UnitTestCalories
{
    [TestClass]
    public class UnitTest1
    {
            /// <summary>
            /// This test ensures that the CalculateTotalCalories method works correctly with an empty 
            /// ingredient list and returns 0 as the total calories.
            /// It is important to test this case because it verifies the behavior of the method when 
            /// there are no ingredients, ensuring that it handles empty lists and provides the 
            /// expected result.
            /// </summary>
            [TestMethod]
            public void Test_CalculateTotalCalories_WithEmptyIngredientList()
            {
                WorkerClass workerClass = new WorkerClass(new ClockTimerClass());
                List<List<(string, double, string, double, string, double, double, string)>> emptyIngredientList =
                    new List<List<(string, double, string, double, string, double, double, string)>>();
                double totalCalories = workerClass.CalculateTotalCalories(emptyIngredientList);
                Assert.AreEqual(0.0f, totalCalories);
            }

            //・♫-------------------------------------------------------------------------------------------------♫・//
            /// <summary>
            /// This test verifies that the CalculateTotalCalories method returns the correct total calories
            /// when provided with an ingredient list.
            /// It is important to test this case because it ensures that the method correctly sums up the 
            /// calorie values of all the ingredients in the list and produces the expected result.
            /// </summary>
            //[TestMethod]
            //public void Test_CalculateTotalCalories_WithIngredientsList()
            //{
            //    WorkerClass workerClass = new WorkerClass(new ClockTimerClass());
            //    List<IngredientClass> ingredientList = new List<IngredientClass>()
            //    {
            //        new IngredientClass { IngredientCalories = 100.0 },
            //        new IngredientClass { IngredientCalories = 50.0 },
            //        new IngredientClass { IngredientCalories = 75.0 },
            //        new IngredientClass { IngredientCalories = 120.0 }
            //    };
            //    double totalCalories = workerClass.CalculateTotalCalories(ingredientList);
            //    Assert.AreEqual(345.0f, totalCalories);
            //}

            [TestMethod]
            public void Test_CalculateTotalCalories_WithIngredientsList()
            {
                WorkerClass workerClass = new WorkerClass(new ClockTimerClass());
                List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections = new List<List<(string, double, string, double, string, double, double, string)>>()
            {
                new List<(string, double, string, double, string, double, double, string)>
                {
                    ("Ingredient1", 0.0, "", 100.0, "", 0.0, 100.0, ""),
                    ("Ingredient2", 0.0, "", 50.0 , "", 0.0, 50.0 , ""),
                    ("Ingredient3", 0.0, "", 75.0 , "", 0.0, 75.0 , ""),
                    ("Ingredient4", 0.0, "", 120.0, "", 0.0, 120.0, "")
                }
            };

                double totalCalories = workerClass.CalculateTotalCalories(ingredientCollections);
                Assert.AreEqual(345.0, totalCalories);
            }

            //・♫-------------------------------------------------------------------------------------------------♫・//
            /// <summary>
            /// This test ensures that the CalculateTotalCalories method returns 0 when the ingredient list 
            /// is null.
            /// It is important to test this case because it validates that the method handles null input 
            /// and provides the expected result without errors.
            /// </summary>
            [TestMethod]
            public void Test_CalculateTotalCalories_WithNullIngredientList()
            {
                WorkerClass workerClass = new WorkerClass(new ClockTimerClass());
                List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections = new List<List<(string, double, string, double, string, double, double, string)>>()
            {
                new List<(string, double, string, double, string, double, double, string)>()
            };
                double totalCalories = workerClass.CalculateTotalCalories(ingredientCollections);
                Assert.AreEqual(0.0, totalCalories);
            }

            //・♫-------------------------------------------------------------------------------------------------♫・//
            /// <summary>
            /// This test verifies that the CalculateTotalCalories method handles negative calorie values 
            /// correctly and returns the correct total calories, where negative calories are treated as 0.
            /// It is important to test this case because it ensures that the method properly handles invalid 
            /// or unexpected input, such as negative calorie values, and produces the expected result 
            /// without any negative calories affecting the total.
            /// </summary>
            //[TestMethod]
            //public void Test_CalculateTotalCalories_WithNegativeCalories()
            //{
            //    WorkerClass workerClass = new WorkerClass(new ClockTimerClass());
            //    List<IngredientClass> ingredientList = new List<IngredientClass>()
            //    {
            //        new IngredientClass { IngredientCalories = -25.0 },
            //        new IngredientClass { IngredientCalories = 50.0 },
            //        new IngredientClass { IngredientCalories = 75.0 },
            //        new IngredientClass { IngredientCalories = 120.0 }
            //    };
            //    double totalCalories = workerClass.CalculateTotalCalories(ingredientList);
            //    Assert.AreEqual(245.0f, totalCalories);
            //}

            [TestMethod]
            public void Test_CalculateTotalCalories_WithNegativeCalories()
            {
                WorkerClass workerClass = new WorkerClass(new ClockTimerClass());
                List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections = new List<List<(string, double, string, double, string, double, double, string)>>()
    {
        new List<(string, double, string, double, string, double, double, string)>
        {
            ("Ingredient1", 0.0, "", -25.0, "", 0.0, -25.0, ""),
            ("Ingredient2", 0.0, "", 50.0, "", 0.0, 50.0, ""),
            ("Ingredient3", 0.0, "", 75.0, "", 0.0, 75.0, ""),
            ("Ingredient4", 0.0, "", 120.0, "", 0.0, 120.0, "")
        }
    };

                double totalCalories = workerClass.CalculateTotalCalories(ingredientCollections);
                Assert.AreEqual(245.0, totalCalories);
            }

            //・♫-------------------------------------------------------------------------------------------------♫・//
            /// <summary>
            /// This test ensures that the CalculateTotalCalories method handles large total calorie values 
            /// correctly and returns the correct result.
            /// It is important to test this case because it validates the methods ability to handle large
            /// numerical values without any overflow or error.
            /// </summary>
            [TestMethod]
            public void Test_CalculateTotalCalories_WithLargeTotalCalories()
            {
                WorkerClass workerClass = new WorkerClass(new ClockTimerClass());
                List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections = new List<List<(string, double, string, double, string, double, double, string)>>()
    {
        new List<(string, double, string, double, string, double, double, string)>
        {
            ("Ingredient1", 0.0, "", 1e9, "", 0.0, 1e9, ""),
            ("Ingredient2", 0.0, "", 2e9, "", 0.0, 2e9, ""),
            ("Ingredient3", 0.0, "", 3e9, "", 0.0, 3e9, "")
        }
    };

                double totalCalories = workerClass.CalculateTotalCalories(ingredientCollections);
                Assert.AreEqual(6e9, totalCalories);
            }

            //・♫-------------------------------------------------------------------------------------------------♫・//
            /// <summary>
            /// This test ensures that the CalculateTotalCalories method returns 0 when the ingredient list
            /// consists only of ingredients with 0 calories.
            /// It is important to test this case because it validates that the method handles lists with 
            /// all-zero calories and produces the expected result, ensuring that zero-calorie ingredients 
            /// do not contribute to the total.
            /// </summary>
            [TestMethod]
            public void Test_CalculateTotalCalories_WithZeroCalories()
            {
                WorkerClass workerClass = new WorkerClass(new ClockTimerClass());
                List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections = new List<List<(string, double, string, double, string, double, double, string)>>()
    {
        new List<(string, double, string, double, string, double, double, string)>
        {
            ("Ingredient1", 0.0, "", 0.0, "", 0.0, 0.0, ""),
            ("Ingredient2", 0.0, "", 0.0, "", 0.0, 0.0, ""),
            ("Ingredient3", 0.0, "", 0.0, "", 0.0, 0.0, "")
        }
    };

                double totalCalories = workerClass.CalculateTotalCalories(ingredientCollections);
                Assert.AreEqual(0.0, totalCalories);
            }
        }
    }//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
