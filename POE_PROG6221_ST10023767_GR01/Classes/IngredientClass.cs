#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 1 and 2 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace POE_PROG6221_ST10023767_GR01
{
    public class IngredientClass
    {
        /// <summary>
        /// Holds the name of the ingredient 
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Holds the quantity of the ingredient in string format e.g. 1 = 'one'
        /// </summary>
        public double Quantity { get; set; } = (double)0.0;

        /// <summary>
        /// Holds the unit of measurement
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// Holds the number of ingredients 
        /// </summary>
        public int NumOfIngredients { get; set; } = 0;

        /// <summary>
        /// Holds the number of calories
        /// </summary>
        public double IngredientCalories { get; set; }

        /// <summary>
        /// Holds the selected food group
        /// </summary>
        public string FoodGroup { get; set; } = string.Empty;

        /// <summary>
        /// Holds the list of food groups.
        /// </summary>
        public List<string> FoodGroupList { get; set; } = new List<string>
        {
            "Starchy foods",
            "Vegetables and fruits",
            "Dry beans, peas, lentils and soya",
            "Chicken, fish, meat and eggs",
            "Milk and dairy products",
            "Fats and oil",
            "Water"
        };

        /// <summary>
        /// Instantiates a new instance of the Validation class. The Validation class can now be used to 
        /// perform validation tasks throughout the rest of the code.
        /// /// </summary>
        public Validation validate = new Validation();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Initializes a new instance of the IngredientClass with the specified properties.
        /// </summary>
        /// <param name="name">The name of the ingredient.</param>
        /// <param name="quantity">The quantity of the ingredient.</param>
        /// <param name="unit">The unit of measurement for the ingredient.</param>
        /// <param name="numOfIngredients">The number of ingredients.</param>
        /// <param name="calories">The calorie content of the ingredient.</param>
        /// <param name="foodgroup">The food group of the ingredient.</param>
        public IngredientClass(string name, double quantity, string unit, int numOfIngredients, double calories, string foodgroup)
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
        /// Default constructor for IngredientClass.
        /// </summary>
        public IngredientClass()
        {
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method prompts the user to enter ingredient data and returns an IngredientClass object with the 
        /// data entered. The method first calls GetNumOfIngredients to get the number of ingredients to be 
        /// entered. It then calls GetIngredientName, GetIngredientQuantity, GetIngredientUnit, GetIngredientCalories,
        /// and the GetFoodGroup to get the name, quantity, unit, calories and food group of each ingredient. It 
        /// creates and returns a new IngredientClass object with the data entered.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass</param>
        /// <returns>a new IngredientClass object with the data entered</returns>
        public IngredientClass GetIngredientData(ClockTimerClass clockTimerClass)
        {
            int numOfIngredients = GetNumOfIngredients(clockTimerClass);
            string name = GetIngredientName(clockTimerClass);
            double quantity = GetIngredientQuantity(clockTimerClass);
            string unit = GetIngredientUnit(clockTimerClass);
            double calories = GetIngredientCalories(clockTimerClass);
            string foodGroup = GetFoodGroup(clockTimerClass);

            return new IngredientClass(name, quantity, unit, numOfIngredients, calories, foodGroup);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method retrieves the number of ingredients from the user input as a whole number. It first 
        /// initializes the necessary variables and then prompts the user to enter the number of ingredients. 
        /// It then checks whether the input is a valid integer using the Validate_Integer method. If the input
        /// is not valid, it prompts the user to re-enter the number of ingredients. The method continues to loop
        /// until a valid input is received. The method then returns the valid number of ingredients as an integer.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass/param>
        /// <returns>The valid number of ingredients as an integer</returns>
        public int GetNumOfIngredients(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            bool valid = false;
            string userInput;
            int number = 0;

            // Prompt the user to enter the number of ingredients as a whole number
            Console.Write("\r\nPlease enter the number of ingredients as a whole number: ");
            userInput = Console.ReadLine();
            // Validate the user's input
            do
            {
                try
                {
                    valid = validate.Validate_Integer(userInput);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Line 121: " + e.Message);
                }

                if (valid)
                {
                    number = Convert.ToInt32(userInput);
                }
                // If the input is invalid, prompt the user to re-enter the ingredient unit and change the console
                // color to indicate an error
                else
                {
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nPlease re-enter the number of ingredients as a whole number: ");
                    userInput = Console.ReadLine();
                    clockTimerClass.ChangeBack();
                }
            } while (!valid);

            // Return the integer of the entered number of ingredients 
            return number;
        }
        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to enter a text string representing the name of an ingredient, validates the 
        /// input to ensure it is a valid string, and returns the input as a string.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass</param>
        /// <returns>The string representing the entered ingredient name</returns>
        public string GetIngredientName(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            bool valid;
            string userInput;
            string name = string.Empty;

            // Prompt the user to enter the ingredient name as text
            Console.Write("Please enter the ingredient name as text (e.g. 'Sugar'):   \t");
            userInput = Console.ReadLine();

            // Validate the user's input
            do
            {
                valid = validate.Validate_String(userInput);

                if (valid)
                {
                    name = userInput;
                }
                // If the input is invalid, prompt the user to re-enter the ingredient unit and change the console
                // color to indicate an error
                else
                {
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nPlease re-enter the ingredient name as text (e.g. 'Sugar'):   \t");
                    userInput = Console.ReadLine();
                    clockTimerClass.ChangeBack();
                }
            } while (!valid);

            // Return the string of the entered ingredient name
            return name;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to enter an ingredient unit as text (e.g. "cup") and validates the input to
        /// ensure it is a valid string and a valid unit of measurement. It returns the validated unit as a string.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass</param>
        /// <returns>The string representing the entered ingredient unit</returns>
        public String GetIngredientUnit(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            bool valid1;
            bool valid2;
            string userInput;
            string unit = string.Empty;

            // Prompt the user to enter the ingredient unit as text
            Console.Write("Please enter the ingredient unit as text (e.g. 'cup'):   \t");
            userInput = Console.ReadLine();

            // Validate the user's input
            do
            {
                valid1 = validate.Validate_String(userInput);
                valid2 = validate.Validate_Unit_Of_Measurement(userInput);

                if (valid1 && valid2)
                {
                    unit = userInput;
                }
                // If the input is invalid, prompt the user to re-enter the ingredient unit and change the console
                // color to indicate an error
                else
                {
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nPlease re-enter the ingredient unit as text (e.g. 'cup'):   \t");
                    userInput = Console.ReadLine();
                    clockTimerClass.ChangeBack();
                }
            } while (!valid1 || !valid2);

            // Return the string of the entered ingredient unit
            return unit;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to enter an ingredient quantity as text (e.g. "one") and converts it to 
        /// a numerical value. Or if the user selected that they would like to enter the quantity as a numerical 
        /// value it would prompt the user to enter the ingreident quantity as a numerical value (e.g. 1)
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass</param>
        /// <returns>The numerical value of the entered ingredient quantity.</returns>
        public double GetIngredientQuantity(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            bool valid;
            string userInput;
            double quantity = -9999;
            int userChoice;

            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.Write("\r\nPlease select an option by entering its corresponding number: ");
            clockTimerClass.ChangeBack();
            Console.Write("\r\n1. Enter the ingredient quantity as text (e.g. 'one')" +
                "\r\n2. Enter the ingredient quantity as a numerical number (e.g. 1)\r\n>");
            userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out userChoice) || userChoice < 1 || userChoice > 2)
            {
                clockTimerClass.ChangeToErrorColor();
                Console.Write("\r\nPlease select an option by entering its corresponding number: ");
                clockTimerClass.ChangeBack();
                Console.Write("\r\n1. Enter the ingredient quantity as text (e.g. 'one')" +
                    "\r\n2. Enter the ingredient quantity as a numerical number (e.g. 1)\r\n>");
                userInput = Console.ReadLine();
            }

            // Validate the user's input
            if (userChoice == 1)
            {
                clockTimerClass.ChangeBackColor(clockTimerClass.selectedColor);
                clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
                Console.Write("\r\nPlease enter the ingredient quantity as text (e.g. 'one'): \t");
                userInput = Console.ReadLine();
                clockTimerClass.ChangeBack();
                do
                {

                    // If the input is invalid, prompt the user to re-enter the ingredient quantity and change the
                    // console color to indicate an error
                    if (!validate.Validate_String(userInput) || validate.Validate_Float(userInput))
                    {
                        valid = false;
                        clockTimerClass.ChangeToErrorColor();
                        Console.Write("\r\nPlease re-enter the ingredient quantity as text (e.g. 'one'): \t");
                        userInput = Console.ReadLine();
                        clockTimerClass.ChangeBack();
                    }
                    else
                    {
                        quantity = Math.Round(validate.Convert_Text_To_Corresponding_Numerical_Value(userInput), 2);
                        if (quantity == -9999)
                        {
                            valid = false;
                            clockTimerClass.ChangeToErrorColor();
                            Console.Write("\r\nPlease re-enter the ingredient quantity as text (e.g. 'one'): \t");
                            userInput = Console.ReadLine();
                            clockTimerClass.ChangeBack();
                        }
                        else
                        {
                            valid = true;
                        }
                    }
                } while (!valid);
            }
            else
            {
                clockTimerClass.ChangeBackColor(clockTimerClass.selectedColor);
                clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
                Console.Write("\r\nPlease enter the ingredient quantity as a numerical number (e.g. 1): \t");
                userInput = Console.ReadLine();
                clockTimerClass.ChangeBack();
                do
                {


                    // If the input is invalid, prompt the user to re-enter the ingredient quantity and change
                    // the console color to indicate an error
                    if (!validate.Validate_String(userInput) || !validate.Validate_Float(userInput))
                    {
                        valid = false;
                        clockTimerClass.ChangeToErrorColor();
                        Console.Write("\r\nPlease re-enter the ingredient quantity as a numerical number " +
                            "(e.g. 1): \t");
                        userInput = Console.ReadLine();
                        clockTimerClass.ChangeBack();
                    }
                    else
                    {

                        quantity = Math.Round(Convert.ToDouble(userInput), 2);
                        if (quantity == -9999)
                        {
                            valid = false;
                            clockTimerClass.ChangeToErrorColor();
                            Console.Write("\r\nPlease re-enter the ingredient quantity as a numerical " +
                                "number (e.g. 1): \t");
                            userInput = Console.ReadLine();
                            clockTimerClass.ChangeBack();
                        }
                        else
                        {
                            valid = true;
                        }
                    }
                } while (!valid);
            }

            // Return the numerical value of the entered ingredient quantity
            return quantity;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to enter the number of calories for an ingredient and returns the numerical
        /// value.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        /// <returns>The numerical value of the entered ingredient calories.</returns>
        public double GetIngredientCalories(ClockTimerClass clockTimerClass)
        {
            bool valid;
            string userInput;
            double calories = 0.0;
            int userChoice;

            do
            {
                clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
                clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
                Console.Write("\r\nPlease select an option by entering its corresponding number: ");
                clockTimerClass.ChangeBack();
                Console.Write("\r\n1. Enter the number of calories as a numerical number (e.g. 25)" +
                    "\r\n2. More information about calories\r\n>");
                userInput = Console.ReadLine();

                while (!int.TryParse(userInput, out userChoice) || userChoice < 1 || userChoice > 2)
                {
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nPlease select an option by entering its corresponding number: ");
                    clockTimerClass.ChangeBack();
                    Console.Write("\r\n1. Enter the number of calories as a numerical number (e.g. 25)" +
                        "\r\n2. More information about calories\r\n>");
                    userInput = Console.ReadLine();
                }

                // Validate the user's input
                if (userChoice == 1)
                {
                    clockTimerClass.ChangeBackColor(clockTimerClass.selectedColor);
                    clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
                    Console.Write("\r\nPlease enter the number of calories as a numerical number (e.g. 25): \t");
                    userInput = Console.ReadLine();
                    clockTimerClass.ChangeBack();
                    do
                    {
                        // If the input is invalid, prompt the user to re-enter the ingredient quantity and change
                        // the console color to indicate an error
                        if (!validate.Validate_String(userInput) || !validate.Validate_Float(userInput))
                        {
                            valid = false;
                            clockTimerClass.ChangeToErrorColor();
                            Console.Write("\r\nPlease enter the number of calories as a numerical number (e.g. 25): \t");
                            userInput = Console.ReadLine();
                            clockTimerClass.ChangeBack();
                        }
                        else
                        {
                            calories = Convert.ToDouble(userInput);
                            valid = true;
                        }
                    } while (!valid);
                }
                else
                {
                    valid = false;
                    Console.WriteLine("\r\nCalories:" +
                        "\r\nCalories are a measure of energy. In the context of nutrition, calories refer to the energy " +
                        "\r\ncontent of food and drinks that our bodies can utilize for various physiological functions " +
                        "\r\nand activities." +
                        "\r\n\r\nExample:" +
                        "\r\nLet's consider an example of a medium-sized apple. It contains approximately 52 calories." +
                        "\r\nWhen we consume this apple, our bodies extract and utilize the energy stored in those calories." +
                        "\r\nThis energy is then used for essential functions such as maintaining body temperature, powering " +
                        "\r\nphysical activities, and supporting bodily processes like digestion and cellular metabolism." +
                        "\r\n\r\nIt's important to note that the calorie content of foods can vary based on factors such as serving size," +
                        "\r\ncooking methods, and specific varieties.");
                }
            } while (!valid);

            // Return the numerical value of the entered ingredient calories
            return calories;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to select a food group from the list and returns the selected food group 
        /// as a string.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        /// <returns>The selected food group as a string</returns>
        public string GetFoodGroup(ClockTimerClass clockTimerClass)
        {
            string userInput, foodGroup = string.Empty;
            bool valid = false;

            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine("\r\nPlease select a food group by entering its corresponding number: ");
            clockTimerClass.ChangeBack();

            do
            {
                for (int i = 0; i < FoodGroupList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {FoodGroupList[i]}");
                }
                Console.WriteLine($"{FoodGroupList.Count + 1}. More information on a specific food group.");
                Console.Write(">");

                userInput = Console.ReadLine();
                if (int.TryParse(userInput, out int userChoice))
                {
                    if (userChoice >= 1 && userChoice <= FoodGroupList.Count)
                    {
                        int foodGroupIndex = userChoice - 1;

                        if (foodGroupIndex >= 0 && foodGroupIndex < FoodGroupList.Count)
                        {
                            valid = true;
                            foodGroup = FoodGroupList[foodGroupIndex];
                        }
                    }
                    else if (userChoice == FoodGroupList.Count + 1)
                    {
                        FoodGroupInformation(clockTimerClass);
                    }
                }

                if (!valid)
                {
                    clockTimerClass.ChangeToErrorColor();
                    clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
                    clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
                    Console.WriteLine("\r\nPlease select a food group by entering its corresponding number: ");
                    clockTimerClass.ChangeBack();
                }
            } while (!valid);

            return foodGroup;
        }
        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method displays information about a specific food group based on the users selection.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        public void FoodGroupInformation(ClockTimerClass clockTimerClass)
        {
            string userInput;
            bool valid;

            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);
            Console.WriteLine("\r\nPlease select a food group to view by entering its corresponding number: ");
            clockTimerClass.ChangeBack();

            for (int i = 0; i < FoodGroupList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {FoodGroupList[i]}");
            }
            Console.WriteLine($"{FoodGroupList.Count + 1}. Back");
            Console.Write(">");

            userInput = Console.ReadLine();

            do
            {
                if (int.TryParse(userInput, out int userChoice))
                {
                    if (userChoice >= 1 && userChoice <= FoodGroupList.Count)
                    {
                        int recipeIndex = userChoice - 1;

                        if (recipeIndex >= 0 && recipeIndex < FoodGroupList.Count)
                        {
                            valid = true;
                            switch (recipeIndex)
                            {
                                case 0:
                                    Console.WriteLine("\r\nStarchy foods:" +
                                        "\r\nThese are foods rich in carbohydrates and provide energy." +
                                        "\r\nThey typically include grains, cereals, potatoes, and root vegetables." +
                                        "\r\nFor example, rice, bread, pasta, and sweet potatoes are considered starchy foods.");
                                    break;
                                case 1:
                                    Console.WriteLine("\r\nVegetables and fruits:" +
                                        "\r\nThis food group consists of plant-based foods that are rich in vitamins, minerals, and dietary fiber." +
                                        "\r\nIt includes a wide variety of vegetables (leafy greens, cruciferous vegetables, etc.) and " +
                                        "\r\nfruits (apples, bananas, berries, etc.)." +
                                        "\r\nFor instance, spinach, broccoli, oranges, and strawberries are part of this group.");
                                    break;
                                case 2:
                                    Console.WriteLine("\r\nDry beans, peas, lentils, and soya:" +
                                        "\r\nThis group includes legumes that are high in protein, fiber, and various nutrients." +
                                        "\r\nExamples of dry beans include kidney beans, black beans, and chickpeas. Lentils and peas," +
                                        "\r\nsuch as green lentils and split peas, also fall into this category. Soybeans are used to make" +
                                        "\r\nsoy products like tofu and soy milk.");
                                    break;
                                case 3:
                                    Console.WriteLine("\r\nChicken, fish, meat, and eggs: " +
                                        "\r\nThis food group comprises animal-based protein sources." +
                                        "\r\nIt includes poultry (chicken, turkey), fish (salmon, tuna), red meat (beef, pork), and eggs. " +
                                        "\r\nThese foods are rich in protein, vitamins, and minerals and serve as important sources of nutrition.");
                                    break;
                                case 4:
                                    Console.WriteLine("\r\nMilk and dairy products: " +
                                        "\r\nThis group encompasses milk and various dairy products derived from it, such as cheese, yogurt, and butter." +
                                        "\r\nThese foods are excellent sources of calcium, protein, and vitamins like vitamin D. " +
                                        "\r\nExamples include cow's milk, cheddar cheese, and Greek yogurt.");
                                    break;
                                case 5:
                                    Console.WriteLine("\r\nFats and oil: " +
                                        "\r\nThis food group includes fats and oils that are consumed in moderation. " +
                                        "\r\nIt comprises sources of healthy fats like vegetable oils (olive oil, canola oil), nuts, seeds, and avocados." +
                                        "\r\n While fats are high in calories, they play essential roles in providing energy and carrying fat-soluble vitamins.");
                                    break;
                                default:
                                    Console.WriteLine("\r\nWater:" +
                                        "\r\nWater is not a food group but an essential component of a healthy diet." +
                                        "\r\nIt is crucial for hydration and the proper functioning of the body." +
                                        "\r\n Drinking sufficient water helps maintain bodily functions, supports digestion, and aids in temperature regulation.");
                                    break;
                            }
                        }
                        else
                        {
                            valid = false;
                            clockTimerClass.ChangeToErrorColor();
                            Console.WriteLine("\r\nPlease select a food group to view by entering its corresponding number: ");
                            clockTimerClass.ChangeBack();

                            for (int i = 0; i < FoodGroupList.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {FoodGroupList[i]}");
                            }
                            Console.WriteLine($"{FoodGroupList.Count + 1}. Back");
                            Console.Write(">");

                            userInput = Console.ReadLine();
                        }
                    }
                    else
                    {
                        if (userChoice == FoodGroupList.Count + 1)
                        {
                            valid = true;
                        }
                        else
                        {
                            valid = false;
                            clockTimerClass.ChangeToErrorColor();
                            Console.WriteLine("\r\nPlease select a food group by entering its corresponding number: ");
                            clockTimerClass.ChangeBack();

                            for (int i = 0; i < FoodGroupList.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {FoodGroupList[i]}");
                            }
                            Console.WriteLine($"{FoodGroupList.Count + 1}. Back");
                            Console.Write(">");

                            userInput = Console.ReadLine();
                        }
                    }
                }
                else
                {
                    valid = false;
                    clockTimerClass.ChangeToErrorColor();
                    Console.WriteLine("\r\nPlease select a food group by entering its corresponding number: ");
                    clockTimerClass.ChangeBack();

                    for (int i = 0; i < FoodGroupList.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {FoodGroupList[i]}");
                    }
                    Console.WriteLine($"{FoodGroupList.Count + 1}. Back");
                    Console.Write(">");

                    userInput = Console.ReadLine();
                }
            } while (valid == false);
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//