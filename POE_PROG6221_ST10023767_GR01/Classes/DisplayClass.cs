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
        /// </summary>
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
        public DisplayClass() { }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method displays the ingredients of a selected recipe. 
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass</param>
        /// <param name="selectedRecipe">The name of the selected recipe</param>
        /// <param name="ingredientTuples">The list of ingredient tuples</param>
        /// <param name="TotalCaloriesList">The list of total calories for each recipe</param>
        /// <param name="recipeIndex">The index of the selected recipe</param>
        public void PrintIngredients(ClockTimerClass clockTimerClass, string selectedRecipe,
            List<(string, double, string, double, string, double, double, string)> ingredientTuples, List<double> TotalCaloriesList,
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

            Console.WriteLine("\r\nThe total number of calories is: " + Convert.ToString(TotalCaloriesList[recipeIndex])
                + "\r\n\tRange: "+ CalorieRangeInformation(TotalCaloriesList[recipeIndex]));
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method displays the calorie range information
        /// </summary>
        /// <param name="totalNumberOfCalories">The total calories of the selected recipe</param>
        /// <returns></returns>
        private string CalorieRangeInformation(double totalNumberOfCalories)
        {
            string information;

            switch (totalNumberOfCalories)
            {
                case double n when (n > 0 && n <= 100):
                    information = "Between 0 and 100 calories: A gust of wind would burn more calories than this!\r\n" +
                                "\tBut hey, every little bit counts, right? So keep those legs moving!";
                    break;
                case double n when (n > 100 && n <= 300):
                    information = "Between 100 and 300: You're in the snacking zone!\r\n" +
                                "\tThis range is perfect for satisfying your cravings without going overboard.";
                    break;
                case double n when (n > 300 && n <= 500):
                    information = "Between 300 and 500: Your taste buds are in for a sizzling adventure!\r\n" +
                                "\tThis range offers you a delicious dishes while still keeping your calorie count in check.\r\n" +
                                "\tBon appétit!";
                    break;
                case double n when (n > 500 && n <= 800):
                    information = "Between 500 and 800: You're burning it up!\r\n" +
                                "\tThis range is for those who enjoy their fair share of physical activities, keep burning those calories!";
                    break;
                case double n when (n > 800 && n <= 1200):
                    information = "Between 800 and 1200: Your body is a well-oiled machine!\r\n" +
                                "\tThis range is ideal for those who engage in intense workouts and need extra fuel to power through.\r\n" +
                                "\tYou're like a calorie-burning furnace!";
                    break;
                case double n when (n > 1200 && n <= 1500):
                    information = "Between 1200 and 1500: You've got a hearty appetite!\r\n" +
                                "\tIn this range, you can savor a variety of delicious meals while still maintaining a balanced intake.\r\n" +
                                "\tIt's time to explore the flavors of the world!";
                    break;
                case double n when (n > 1500 && n <= 2000):
                    information = "Between 1500 and 2000: You're the reigning food champion!\r\n" +
                                "\tThis range allows you to enjoy a wide range of tasty treats.";
                    break;
                default:
                    information = "More than 2000: Prepare for an explosion of energy!\r\n" +
                                "\tThis range offers you the opportunity to embrace your inner foodie to the fullest.\r\n" +
                                "\tRemember, with great food comes great enjoyment!";
                    break;
            }

            return information;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method displays the steps of a recipe. Changes the console background and foreground colors to highlight 
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
            
            DisplayLine();

            Console.WriteLine();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method displays a thank you message and sets the console color to black with white text. Also calls 
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
            validate.GetUserInput();

            Environment.Exit(0);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method is used to display a block of white spaces on the console at a specific position.
        /// If the argument "length" is equal to "welcome", it sets the cursor position to the end of the welcome 
        /// message. Otherwise, it calculates the end line of the console output and sets the cursor position to 
        /// the beginning of the "farewell" message, then displays a block of 60 white spaces to overwrite any previous
        /// text in that line.
        /// </summary>
        /// <param name="length">The length of the recipe name</param>
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
        /// Method displays a horizontal line made of Unicode character "\u2500". The length of the line is 100 
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
        /// Method displays the names of the recipes in the given list. Displays the recipe names with corresponding 
        /// numbers, followed by an option to go back. 
        /// </summary>
        /// <param name="recipeNames">The list of recipe names</param>
        public void PrintRecipeNames(List<string> recipeNames, ClockTimerClass clockTimerClass)
        {
            clockTimerClass.ChangeBack();

            for (int i = 0; i < recipeNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {recipeNames[i]}");
            }
            Console.WriteLine($"{recipeNames.Count + 1}. Back");
            Console.Write(">");
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method displays the information of a spesific food group that is selected by the user
        /// </summary>
        /// <param name="recipeIndex">The food group index/number selected by the user</param>
        public void DisplayFoodGroupInformation(int recipeIndex)
        {
            DisplayLine();

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
                        "\r\nThis group includes foods that are high in protein, fiber, and various nutrients." +
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

            DisplayLine();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method displays the food group options using the FoodGroupList
        /// </summary>
        public void DisplayFoodGroupOptions(List<string> FoodGroupList)
        {
            for (int i = 0; i < FoodGroupList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {FoodGroupList[i]}");
            }
            Console.WriteLine($"{FoodGroupList.Count + 1}. Back");
            Console.Write(">");
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method displays the scaling factor options
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass.</param>
        public void DisplayScalingFactorOptions(ClockTimerClass clockTimerClass)
        {
            clockTimerClass.ChangeBackColor(clockTimerClass.selectedTextBackgroundColor);
            clockTimerClass.ChangeForeColor(clockTimerClass.selectedForeColor);

            Console.Write("\r\nWhat scaling factor would you like to use? Please enter a number to indicate the desired factor: ");

            clockTimerClass.ChangeBack();
            Console.Write("\n1. Half\n2. Double\n3. Triple\n4. Reset\n>");
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
