using POE_PROG6221_ST10023767_GR01;
using POE_PROG6221_ST10023767_GR01.Classes;
using PROG6221_P3.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PROG6221_P3.UserControls
{
    /// <summary>
    /// Interaction logic for AddRecipeView.xaml
    /// </summary>
    public partial class AddRecipeView : UserControl
    {

        // Create a collection to hold the ingredients
        public List<IngredientClassP3> ingredientList = new List<IngredientClassP3>();
        public List<StepClassP3> stepList = new List<StepClassP3>();

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
        private List<RecipeClassP3> recipeList = new List<RecipeClassP3>();

        private int numberOfSteps = 0;
        private int numberOfIngredients = 0;

        POE_PROG6221_ST10023767_GR01.Validation validation = new POE_PROG6221_ST10023767_GR01.Validation();

        public AddRecipeView()
        {
            InitializeComponent();
            // Set the DataContext of the DataGrid to the ingredients collection
            dgIngredients.ItemsSource = ingredientList;

            // Set the ItemsSource and DisplayMemberPath of the ListBox
            dgSteps.ItemsSource = stepList;
        }

        private void WriteIngredientsToList()
        {
            int number = this.numberOfIngredients;
            


            //ingredientList = new List<IngredientClassP3>();


            //for (int i = 0; i < number; i++)
            //{
            //    var newIngredient = new IngredientClassP3
            //    {
            //        Name = GetIngredientName(),
            //        Quantity = GetIngredientQuantity(),
            //        Unit = GetIngredientUnit(),
            //        IngredientCalories = GetIngredientCalories(),
            //        FoodGroup = GetFoodGroup(),
            //        NumOfIngredients = number
            //    };

            //    ingredientList.Add(newIngredient);
            //}
        }


        private string GetFoodGroup()
        {
            // Initialize variables
            bool valid1, valid2;

            string foodGroup = ((ComboBoxItem)cmbFoodGroup.SelectedItem).Content.ToString();

            // Validate the user's input
            do
            {
                valid1 = validation.Validate_String(foodGroup);
                valid2 = validation.Validate_Food_Group(foodGroup);//this must be checked if the user enter it in the message box, not the combo box

                if (!valid1 || !valid2)
                {
                    //pop up message and have them enter in the pop up message 
                    //Unit = new InputBinding from message
                }
            } while (!valid1 || !valid2);

            // Return the string of the entered ingredient unit
            return foodGroup;
        }



        private double GetIngredientCalories()
        {
            // Initialize variables
            bool valid1, valid2;

            string userInput = txtCalories.Text;


            do
            {
                valid1 = validation.Validate_String(userInput);
                valid2 = validation.Validate_Float(userInput);

                if (!valid1 || !valid2)
                {
                    //pop up message and have them enter in the pop up message 
                    //calories = new InputBinding from message
                }

            } while (!valid1 || !valid2);

            double calories = Convert.ToDouble(userInput);

            return calories;
        }


        public double GetIngredientQuantity()
        {
            // Initialize variables
            bool valid1, valid2;

            string userInput = txtQuantity.Text;


            do
            {
                valid1 = validation.Validate_String(userInput);
                valid2 = validation.Validate_Float(userInput);

                if (!valid1 || !valid2)
                {
                    //pop up message and have them enter in the pop up message 
                    //Quantity = new InputBinding from message
                }

            } while (!valid1 || !valid2);


            double quantity = Convert.ToDouble(userInput);

            return quantity;
        }

        private String GetIngredientUnit()
        {
            // Initialize variables
            bool valid1, valid2;

            string unit = ((ComboBoxItem)cmdUnits.SelectedItem).Content.ToString();

            // Validate the user's input
            do
            {
                valid1 = validation.Validate_String(unit);
                valid2 = validation.Validate_Unit_Of_Measurement(unit);//this must be checked if the user enter it in the message box, not the combo box

                if (!valid1 || !valid2)
                {
                    //pop up message and have them enter in the pop up message 
                    //Unit = new InputBinding from message
                }
            } while (!valid1 || !valid2);

            // Return the string of the entered ingredient unit
            return unit;
        }
        private string GetIngredientName()
        {
            string name = txtIngredientName.Text;
            bool valid;
            do
            {
                valid = validation.Validate_String(name);
                if (!valid)
                {
                    //pop up message and have them enter in the pop up message 
                    //Name = new InputBinding from message
                }

            } while (!valid);

            return name;
        }





        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            numberOfIngredients++;

            IngredientClassP3 newIngredient = new IngredientClassP3
            {
                Name = GetIngredientName(),
                Quantity = GetIngredientQuantity(),
                Unit = GetIngredientUnit(),
                IngredientCalories = GetIngredientCalories(),
                FoodGroup = GetFoodGroup(),
                NumOfIngredients = numberOfIngredients
            };

            ingredientList.Add(newIngredient);

            dgIngredients.ItemsSource = ingredientList;

            // Refresh the DataGrid to reflect the changes
            dgIngredients.Items.Refresh();

            // Select the newly added item in the DataGrid
            dgIngredients.SelectedItem = newIngredient;
            dgIngredients.ScrollIntoView(newIngredient);

            // Clear the input fields
            txtIngredientName.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            cmdUnits.SelectedIndex = 0;
            txtCalories.Text = string.Empty;
            cmbFoodGroup.SelectedIndex = 0;
        }



        private void cmbFoodGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAddSteps_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {
            numberOfSteps++;

            StepClassP3 newStep = new StepClassP3
            {
                NumOfSteps = numberOfSteps,
                Step = GetStep()
            };

            stepList.Add(newStep);

            dgSteps.ItemsSource = stepList;

            // Refresh the DataGrid to reflect the changes
            dgSteps.Items.Refresh();

            // Select the newly added item in the DataGrid
            dgSteps.SelectedItem = newStep;
            dgSteps.ScrollIntoView(newStep);

            txtStep.Text = string.Empty;
        }

        private void WriteStepsToList()
        {
            // Initialize variables
            int number = this.numberOfSteps;

            //stepList = new List<StepClassP3>();

            //for (int i = 0; i < number; i++)
            //{
            //    StepClassP3 newStep = new StepClassP3
            //    {
            //        NumOfSteps = number,
            //        Step = GetStep()
            //    };

            //    stepList.Add(newStep);
            //}
        }

        private string GetStep()
        {
            string step = txtStep.Text;
            bool valid;
            do
            {
                valid = validation.Validate_String(step);
                if (!valid)
                {
                    //pop up message and have them enter in the pop up message 
                    //Name = new InputBinding from message
                }

            } while (!valid);

            return step;
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {

            RecipeNames = new List<string>();
            StepCollections = new List<List<string>>();
            IngredientCollections = new List<List<(string, double, string, double, string, double, double, string)>>();


            AddRecipe();

            txtRecipeName.Text = string.Empty;
            ingredientList.Clear();
            dgIngredients.DataContext = ingredientList;
            stepList.Clear();
            dgSteps.DataContext = stepList;
        }

        public void AddRecipe()
        {
            string newRecipeName = txtRecipeName.Text;

            // Write the steps and ingredients to the lists
            WriteStepsToList();
            WriteIngredientsToList();

            // Update the recipe data
            RecipeNames.Add(newRecipeName);

            StepCollections.Add(stepList.Select(step => step.Step).ToList());
            IngredientCollections.Add(ingredientList.Select(ingredient =>
                (ingredient.Name, ingredient.Quantity, ingredient.Unit, ingredient.IngredientCalories,
                ingredient.FoodGroup, ingredient.Quantity, ingredient.IngredientCalories, ingredient.Unit)).ToList());

            //TotalCaloriesList = CalculateTotalCalories(IngredientCollections);

            //double totalCalories = TotalCaloriesList.LastOrDefault(); // Get the total calories of the last added recipe

            // Handle the event
            //HandleRecipeExceedsCaloriesEvent(totalCalories);

            StoreRecipes(StepCollections, IngredientCollections);
        }

        public void StoreRecipes(List<List<string>> stepCollections,
            List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections)
        {
            //RecipeNames = GetRecipeNames();
            //recipeList.Clear();

            for (int i = 0; i < RecipeNames.Count; i++)
            {
                RecipeClassP3 recipe = new RecipeClassP3
                {
                    RecipeName = RecipeNames[i]
                };

                // Check if the stepCollections and ingredientCollections have elements at the current index
                if (i < stepCollections.Count && i < ingredientCollections.Count)
                {
                    List<string> steps = stepCollections[i];
                    List<(string, double, string, double, string, double, double, string)> ingredientTuples = ingredientCollections[i];

                    // Add steps to the recipe
                    foreach (var step in steps)
                    {
                        StepClassP3 stepObj = new StepClassP3
                        {
                            Step = step
                        };
                        recipe.StepListIn.Add(stepObj);
                    }

                    // Add ingredients to the recipe
                    foreach ((string name, double quantity, string unit, double calories, string foodgroup, double originalQuantity,
                        double originalCalories, string originalUnit) in ingredientTuples)
                    {
                        IngredientClassP3 ingredientObj = new IngredientClassP3
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
                    //pop up error message box
                    StoreRecipes(stepCollections, ingredientCollections);
                }

                // Add the recipe to the list
                recipeList.Add(recipe);
            }

            RecipeClassP3 recipeClassP3 = new RecipeClassP3()
            {
                RecipeList = recipeList
            };
        }
    }
}
