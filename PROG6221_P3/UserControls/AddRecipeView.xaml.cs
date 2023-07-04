#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 3
#endregion

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
using Microsoft.VisualBasic;
using System.Xml.Linq;
using System.Windows.Markup;

namespace PROG6221_P3.UserControls
{
    /// <summary>
    /// Delegate for notifying the user about the calories of a recipe.
    /// </summary>
    /// <param name="recipeName">The name of the recipe.</param>
    public delegate void RecipeNotificationDelegate(double calories);

    /// <summary>
    /// Interaction logic for AddRecipeView.xaml
    /// </summary>
    public partial class AddRecipeView : UserControl
    {
        /// <summary>
        /// Holds the list of ingredients for the recipe.
        /// </summary>
        public List<IngredientClassP3> ingredientList = new List<IngredientClassP3>();

        /// <summary>
        /// Holds the list of steps for the recipe
        /// </summary>
        public List<StepClassP3> stepList = new List<StepClassP3>();

        /// <summary>
        /// Holds the list of the total calories for each recipe
        /// </summary>
        public List<double> TotalCalories = new List<double>();

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
        /// Event that is triggered when a recipe exceeds the calorie limit.
        /// </summary>
        public event RecipeNotificationDelegate RecipeExceedsCaloriesEvent;

        /// <summary>
        /// Holds the list of recipes.
        /// </summary>
        private List<RecipeClassP3> recipeList = new List<RecipeClassP3>();

        /// <summary>
        /// Holds the number of steps added 
        /// </summary>
        private int numberOfSteps = 0;

        /// <summary>
        /// Holds the number of ingredients added 
        /// </summary>
        private int numberOfIngredients = 0;

        /// <summary>
        /// Instantiates a new instance of the Validation class thats in the part 2 project. The Validation class 
        /// can now be used to perform validation tasks throughout the rest of the code.
        /// /// </summary>
        private POE_PROG6221_ST10023767_GR01.Validation validation = new POE_PROG6221_ST10023767_GR01.Validation();

        /// <summary>
        /// Instantiates a new instance of the Worker class.
        /// </summary>
        private WorkerClassP3 worker = new WorkerClassP3();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for AddRecipeView.
        /// </summary>
        public AddRecipeView()
        {
            InitializeComponent();

            DataContext = ServiceLocator.MainViewModel;

            RecipeNames = new List<string>();
            StepCollections = new List<List<string>>();
            IngredientCollections = new List<List<(string, double, string, double, string, double, double, string)>>();
            TotalCalories = new List<double>();

            // Set the DataContext of the DataGrid to the ingredients collection
            dgIngredients.ItemsSource = ingredientList;

            // Set the ItemsSource of the ListBox
            dgSteps.ItemsSource = stepList;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method gets the food group selected by the user out of the combo box
        /// </summary>
        private string GetFoodGroup()
        {
            string foodGroup = ((ComboBoxItem)cmbFoodGroup.SelectedItem).Content.ToString();

            // Return the foodGroup string 
            return foodGroup;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method gets the valid number of calories and if the calories is valid it will return the number 
        /// of calories as a double but if it is not, it will show an input dialog that will require valid input 
        /// in order to close 
        /// </summary>
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
                    userInput = worker.ShowInputDialog("Enter a valid number for calories, please use a comma if" +
                        " it is a decimal value:", "Calories");
                }

            } while (!valid1 || !valid2);

            double calories = Convert.ToDouble(userInput);

            return calories;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method gets the valid ingredient quantity and if the quantity is valid it will return the ingredient 
        /// quantity as a double but if it is not, it will show an input dialog that will require valid input 
        /// in order to close 
        /// </summary>
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
                    userInput = worker.ShowInputDialog("Enter a valid number for quantities, please use a comma" +
                        " if it is a decimal value:", "Quantities");
                }

            } while (!valid1 || !valid2);


            double quantity = Convert.ToDouble(userInput);

            return quantity;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method gets the unit of measurement selected from the combobox
        /// </summary>
        private String GetIngredientUnit()
        {
            string unit = ((ComboBoxItem)cmdUnits.SelectedItem).Content.ToString();

            // Return the string of the entered ingredient unit
            return unit;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method gets and validates the ingredient name 
        /// </summary>
        private string GetIngredientName()
        {
            string name = txtIngredientName.Text;
            bool valid;
            do
            {
                valid = validation.Validate_String(name);
                if (!valid)
                {
                    name = worker.ShowInputDialog("Enter a valid ingredient name: ", "Name");
                }

            } while (!valid);

            return name;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This button will add a ingredient to the ingredient list, add the list to the datagrid and clear the 
        /// input fields 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This button will add a step to the step list, display the step list in the data grid and cleare the 
        /// input field 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            // Clear the input field
            txtStep.Text = string.Empty;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method will get the step description entered by the user
        /// </summary>
        /// <returns></returns>
        private string GetStep()
        {
            string step = txtStep.Text;
            bool valid;
            do
            {
                valid = validation.Validate_String(step);
                if (!valid)
                {
                    step = worker.ShowInputDialog("Enter a valid step:", "Step");
                }

            } while (!valid);

            return step;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This button will add the recipe.
        /// 
        /// The reason why I cleared the recipeList, recipeNames,  StepCollections and IngredientCollections before 
        /// a new recipe can be added is because when I added e.g. recipe 'Cake' it will populate the combobox with
        /// 'Cake' but when I enter another recipe, e.g. 'Coffee' it will populate the combobox with ('Cake','Cake',
        /// 'Coffee') and so on... 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            recipeList.Clear();
            RecipeNames.Clear();
            StepCollections.Clear();
            IngredientCollections.Clear();

            AddRecipe();

            worker.ShowNotificationBox("The '" + GetRecipeName() + "' recipe was added successfully.",
                "Added successfully");

            // Reset values 
            this.numberOfIngredients = 0;
            this.numberOfSteps = 0;

            // Clear the input fields
            txtRecipeName.Text = string.Empty;
            ingredientList.Clear();
            dgIngredients.DataContext = ingredientList;
            stepList.Clear();
            dgSteps.DataContext = stepList;

            // Refresh the DataGrid to reflect the changes
            dgIngredients.Items.Refresh();
            dgSteps.Items.Refresh();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method will get and validate the recipe name
        /// </summary>
        /// <returns></returns>
        private string GetRecipeName()
        {
            string name = txtRecipeName.Text;
            bool valid;
            do
            {
                valid = validation.Validate_String(name);
                if (!valid)
                {
                    name = worker.ShowInputDialog("Enter a valid recipe name:", "Name");
                }

            } while (!valid);

            return name;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method will add the recipe name to the recipe list, add all the ingredients of that recipe to
        /// ingredient collections and add the steps to step collections. It will then call the CalculateTotalCalories 
        /// method, event and the StoreRecipe method
        /// </summary>
        public void AddRecipe()
        {
            string newRecipeName = GetRecipeName();

            // Update the recipe data
            RecipeNames.Add(newRecipeName);

            StepCollections.Add(stepList.Select(step => step.Step).ToList());
            IngredientCollections.Add(ingredientList.Select(ingredient =>
                (ingredient.Name, ingredient.Quantity, ingredient.Unit, ingredient.IngredientCalories,
                ingredient.FoodGroup, ingredient.Quantity, ingredient.IngredientCalories, ingredient.Unit)).ToList());

            TotalCalories = CalculateTotalCalories(IngredientCollections);

            double totalCalories = TotalCalories.LastOrDefault(); // Get the total calories of the last added recipe

            // Handle the event
            HandleRecipeExceedsCaloriesEvent(totalCalories);

            StoreRecipes(StepCollections, IngredientCollections);

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Subscribes the event handler to the RecipeExceedsCaloriesEvent.
        /// </summary>
        private void SubscribeToRecipeExceedsCaloriesEvent()
        {
            RecipeExceedsCaloriesEvent += HandleRecipeExceedsCalories;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Handles the event when a recipe exceeds the calorie limit.
        /// </summary>
        /// <param name="calories">The calories of the recipe.</param>
        private void HandleRecipeExceedsCalories(double calories)
        {
            if (calories > 300)
            {
                worker.ShowNotificationBox($"The recipe exceeds 300 calories. Total calories are {calories}",
                    "Recipe Notification");
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Unsubscribes the event handler from the RecipeExceedsCaloriesEvent.
        /// </summary>
        private void UnsubscribeFromRecipeExceedsCaloriesEvent()
        {
            RecipeExceedsCaloriesEvent -= HandleRecipeExceedsCalories;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Notifies the user about a recipe.
        /// </summary>
        /// <param name="calories">The calories of the recipe.</param>
        protected virtual void NotifyUser(double calories)
        {
            if (calories > 300)
            {
                // Raise the event to notify the user
                RecipeExceedsCaloriesEvent?.Invoke(calories);
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Handles the event when a recipe exceeds the calorie limit by subscribing, notifying the user,
        /// and unsubscribing from the event.
        /// </summary>
        /// <param name="totalCalories">The total calories of the recipe.</param>
        private void HandleRecipeExceedsCaloriesEvent(double totalCalories)
        {
            SubscribeToRecipeExceedsCaloriesEvent(); // Subscribe to the event
            NotifyUser(totalCalories); // Notify the user
            UnsubscribeFromRecipeExceedsCaloriesEvent(); // Unsubscribe from the event
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Calculates the total calories of a list of ingredients.
        /// </summary>
        /// <param name="IngredientList">The list of ingredients.</param>
        /// <returns>The total calories.</returns>
        public List<double> CalculateTotalCalories(
            List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections)
        {
            if (ingredientCollections == null)
            {
                return new List<double>();
            }

            List<double> recipeCalories = new List<double>();

            foreach (var ingredientList in ingredientCollections)
            {
                double recipeTotalCalories = 0.0;

                foreach (var ingredient in ingredientList)
                {
                    // Ensure the ingredient's calorie value is non-negative
                    double ingredientCalories = Math.Max(ingredient.Item4, 0);
                    recipeTotalCalories += ingredientCalories;
                }

                recipeCalories.Add(recipeTotalCalories);
            }

            return recipeCalories;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method will store the recipe 
        /// </summary>
        /// <param name="stepCollections"></param>
        /// <param name="ingredientCollections"></param>
        public void StoreRecipes(List<List<string>> stepCollections,
            List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections)
        {
            for (int i = 0; i < RecipeNames.Count; i++)
            {
                RecipeClassP3 recipe = new RecipeClassP3
                {
                    RecipeName = RecipeNames[i]
                };


                List<string> steps = stepCollections[i];
                List<(string, double, string, double, string, double, double, string)> ingredientTuples = 
                    ingredientCollections[i];

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
                foreach ((string name, double quantity, string unit, double calories, string foodgroup, 
                    double originalQuantity, double originalCalories, string originalUnit) in ingredientTuples)
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

                // Add the recipe to the list
                recipeList.Add(recipe);

                // Attempt to retrieve the DataContext as an instance of MainViewModel
                var mvm = DataContext as MainViewModel;

                // If the DataContext is an instance of MainViewModel and not null, proceed to call the
                // Add method on the MainViewModel object, passing the recipe object as an argument
                mvm?.Add(recipe);
            }
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
