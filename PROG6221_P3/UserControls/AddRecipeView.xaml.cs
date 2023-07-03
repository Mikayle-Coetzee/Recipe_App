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
{/// <summary>
 /// Delegate for notifying the user about the calories of a recipe.
 /// </summary>
 /// <param name="recipeName">The name of the recipe.</param>
    public delegate void RecipeNotificationDelegate(double calories);
    /// <summary>
    /// Interaction logic for AddRecipeView.xaml
    /// </summary>
    public partial class AddRecipeView : UserControl
    {
        public List<IngredientClassP3> ingredientList = new List<IngredientClassP3>();
        public List<StepClassP3> stepList = new List<StepClassP3>();
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

        private int numberOfSteps = 0;
        private int numberOfIngredients = 0;

        POE_PROG6221_ST10023767_GR01.Validation validation = new POE_PROG6221_ST10023767_GR01.Validation();

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

            // Set the ItemsSource and DisplayMemberPath of the ListBox
            dgSteps.ItemsSource = stepList;
        }

        private void WriteIngredientsToList()
        {
            int number = this.numberOfIngredients;

        }


        private string GetFoodGroup()
        {
            string foodGroup = ((ComboBoxItem)cmbFoodGroup.SelectedItem).Content.ToString();

            // Return the foodGroup string 
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
                    userInput = ShowInputDialog("Enter a valid number for calories, please use a comma if it is a decimal value:", "Calories");
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
                    userInput = ShowInputDialog("Enter a valid number for quantities, please use a comma if it is a decimal value:", "Quantities");
                }

            } while (!valid1 || !valid2);


            double quantity = Convert.ToDouble(userInput);

            return quantity;
        }

        private String GetIngredientUnit()
        {
            string unit = ((ComboBoxItem)cmdUnits.SelectedItem).Content.ToString();

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
                    name = ShowInputDialog("Enter a valid ingredient name: ", "Name");

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
                    step = ShowInputDialog("Enter a valid step:", "Step");

                }

            } while (!valid);

            return step;
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipe();

            this.numberOfIngredients = 0;
            this.numberOfSteps = 0;
            txtRecipeName.Text = string.Empty;
            ingredientList.Clear();
            dgIngredients.DataContext = ingredientList;
            stepList.Clear();
            dgSteps.DataContext = stepList;

            dgIngredients.Items.Refresh();
            dgSteps.Items.Refresh();
        }

        private string GetRecipeName()
        {
            string name = txtRecipeName.Text;
            bool valid;
            do
            {
                valid = validation.Validate_String(name);
                if (!valid)
                {
                    name = ShowInputDialog("Enter a valid recipe name:", "Name");
                }

            } while (!valid);

            return name;
        }

        public void AddRecipe()
        {
            string newRecipeName = GetRecipeName();

            // Write the steps and ingredients to the lists
            WriteStepsToList();
            WriteIngredientsToList();

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
                ShowNotificationBox($"The recipe exceeds 300 calories. Total calories are {calories}", "Recipe Notification");
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
        public List<double> CalculateTotalCalories(List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections)
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


        public void StoreRecipes(List<List<string>> stepCollections,
            List<List<(string, double, string, double, string, double, double, string)>> ingredientCollections)
        {
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
                    //StoreRecipes(stepCollections, ingredientCollections);
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

        private string ShowInputDialog(string message, string title)
        {
            var inputBox = new TextBox();
            inputBox.FontSize = 18;
            inputBox.FontFamily = new FontFamily("Segoe Print");
            inputBox.Foreground = Brushes.White;
            inputBox.BorderBrush = Brushes.White;
            inputBox.BorderThickness = new Thickness(1);
            inputBox.Background = Brushes.Black;

            var window = new Window()
            {
                Title = title,
                Width = 400,
                Height = 200,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.SingleBorderWindow,
                Background = Brushes.Black,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow
            };

            var okButton = new Button()
            {
                Content = "OK",
                Width = double.NaN,  // Stretch button width
                FontSize = 18,
                FontFamily = new FontFamily("Segoe Print"),
                Foreground = Brushes.Black,
                Background = Brushes.White
            };

            okButton.Click += (sender, e) =>
            {
                window.DialogResult = true;
                window.Close();
            };

            var buttonPanel = new StackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            buttonPanel.Children.Add(okButton);

            window.Content = new StackPanel()
            {
                Children =
        {
            new TextBlock() { Text = message, FontSize = 18, FontFamily = new FontFamily("Segoe Print"), Foreground = Brushes.White },
            inputBox,
            buttonPanel
        }
            };

            window.SizeToContent = SizeToContent.WidthAndHeight;

            window.ShowDialog();

            return window.DialogResult == true ? inputBox.Text : "Exit";
        }


        private void ShowNotificationBox(string message, string title)
        {
            var window = new Window()
            {
                Title = title,
                Width = 400,
                Height = 200,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = WindowStyle.SingleBorderWindow,
                Background = Brushes.Black,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow
            };

            var okButton = new Button()
            {
                Content = "OK",
                Width = double.NaN,  // Stretch button width
                FontSize = 18,
                FontFamily = new FontFamily("Segoe Print"),
                Foreground = Brushes.Black,
                Background = Brushes.White
            };

            okButton.Click += (sender, e) =>
            {
                window.Close();
            };

            var buttonPanel = new StackPanel()
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            buttonPanel.Children.Add(okButton);

            window.Content = new StackPanel()
            {
                Children =
        {
            new TextBlock() { Text = message, FontSize = 18, FontFamily = new FontFamily("Segoe Print"), Foreground = Brushes.White },
            buttonPanel
        }
            };

            window.SizeToContent = SizeToContent.WidthAndHeight;

            window.ShowDialog();
        }


    }
}
