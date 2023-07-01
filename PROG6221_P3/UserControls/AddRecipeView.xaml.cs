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
        public List<POE_PROG6221_ST10023767_GR01.IngredientClass> ingredients = new List<POE_PROG6221_ST10023767_GR01.IngredientClass>();
        public List<POE_PROG6221_ST10023767_GR01.StepClass> steps = new List<POE_PROG6221_ST10023767_GR01.StepClass>();
        public List<POE_PROG6221_ST10023767_GR01.RecipeClass> recipes = new List<POE_PROG6221_ST10023767_GR01.RecipeClass>();

        public List<string> recipeList = new List<string>();


        public List<string> stepList = new List<string>(); 
        private int numberOfSteps = 0;

        public AddRecipeView()
        {
            InitializeComponent();
            // Set the DataContext of the DataGrid to the ingredients collection
            dgIngredients.ItemsSource = ingredients;

            // Set the ItemsSource of the ListBox to the steps list
            lstSteps.ItemsSource = steps;
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            int numberOfIngredients = 0;

            // Get the values entered by the user
            string ingredientName = txtIngredientName.Text;
            string quantity = txtQuantity.Text;
            double doubleQuantity = 0.0f;
            string unit = ((ComboBoxItem)cmdUnits.SelectedItem).Content.ToString();
            string calories = txtCalories.Text;
            string foodGroup = ((ComboBoxItem)cmbFoodGroup.SelectedItem).Content.ToString();
            double doubleCalories = 0.0f;
            POE_PROG6221_ST10023767_GR01.Validation validation = new POE_PROG6221_ST10023767_GR01.Validation();

            if (validation.Validate_Float(quantity) == true && validation.Validate_Float(calories) == true && validation.Validate_String(ingredientName) == true)
            {
                doubleQuantity = Convert.ToDouble(quantity);
                doubleCalories = Convert.ToDouble(calories);
                numberOfIngredients++;
            }
            else
            {
                //errormessage
                //return
            }



            // Create a new Ingredient object with the entered values
            POE_PROG6221_ST10023767_GR01.IngredientClass newIngredient = new POE_PROG6221_ST10023767_GR01.IngredientClass
            {
                Name = ingredientName,
                Quantity = doubleQuantity,
                Unit = unit,
                IngredientCalories = doubleCalories,
                FoodGroup = foodGroup,
                NumOfIngredients = numberOfIngredients
            };

            // Add the new ingredient to the ingredients collection
            ingredients.Add(newIngredient);

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
            string recipeName = txtRecipeName.Text;

            if (!string.IsNullOrWhiteSpace(recipeName))
            {
                
                recipeList.Add(recipeName);
                POE_PROG6221_ST10023767_GR01.RecipeClass newRecipe = new POE_PROG6221_ST10023767_GR01.RecipeClass
                {
                    IngredientListIn = ingredients,
                    StepListIn = steps,
                    RecipeName = recipeName,
                   // RecipeList = recipeList
                };
                recipes.Add(newRecipe);

                if (lstSteps.ItemsSource != null)
                {
                    foreach (string step in lstSteps.ItemsSource)
                    {
                        POE_PROG6221_ST10023767_GR01.StepClass newStep = new POE_PROG6221_ST10023767_GR01.StepClass
                        {
                            Step = step,
                            NumOfSteps = numberOfSteps
                        };
                        steps.Add(newStep);

                        stepList.Add(step);
                    }
                }
            }



            txtRecipeName.Text = string.Empty;
            ingredients.Clear();
            dgIngredients.DataContext = ingredients;
            lstSteps.ItemsSource = null;
            lstSteps.Items.Clear();


            lstSteps.ItemsSource = recipeList;
        }

        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {
            string step = txtStep.Text;
            // Check if the step is not empty
            if (!string.IsNullOrWhiteSpace(step))
            {
                stepList.Add(step);
                lstSteps.ItemsSource = stepList;

                // Clear the input field
                txtStep.Text = string.Empty;
            }
            
            numberOfSteps++;
            
        }
    }
}
