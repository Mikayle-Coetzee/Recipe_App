#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 3
#endregion

using PROG6221_P3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for ScaleRecipeView.xaml
    /// </summary>
    public partial class ScaleRecipeView : UserControl
    {
        /// <summary>
        /// Holds the currently selected recipe from the cmbRecipeName ComboBox. 
        /// </summary>
        private RecipeClassP3 selectedRecipe;

        /// <summary>
        /// Holds the total number of calories calculated for the scaled ingredients. 
        /// </summary>
        private double numTotalCalories = 0;

        /// <summary>
        /// Holds the list of recipes sorted by recipe name
        /// </summary>
        private List<RecipeClassP3> sortedRecipe = new List<RecipeClassP3>();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for ScaleRecipeView.
        /// </summary>
        public ScaleRecipeView()
        {
            InitializeComponent();

            // Set the data context to the MainViewModel
            DataContext = ServiceLocator.MainViewModel;

            // Populate the cmbRecipeName ComboBox with recipe names from the MainViewModel
            cmbRecipeName.ItemsSource = (DataContext as MainViewModel).Recipies;
            cmbRecipeName.DisplayMemberPath = "RecipeName";
        }


        private void lstRecipeSteps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method updates the selectedRecipe with the currently selected recipe and clears the 
        /// ingredient and step lists.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRecipeName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected recipe from the cmbRecipeName ComboBox
            selectedRecipe = cmbRecipeName.SelectedItem as RecipeClassP3;

            // Clear and refresh the ingredient and step lists
            dgIngredients.ItemsSource = null;
            dgIngredients.Items.Refresh();

            lstRecipeSteps.ItemsSource = null;
            lstRecipeSteps.Items.Refresh();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method calls two methods, UpdateIngredientDataGrid and UpdateStepList, to update the displayed 
        /// ingredients and steps based on the selected scaling factor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFactor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateIngredientDataGrid();
            UpdateStepList();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method that updates the dgIngredients DataGrid based on the selected recipe and scaling factor.
        /// It calculates the scaled quantities of ingredients and displays it.
        /// </summary>
        private void UpdateIngredientDataGrid()
        {
            if (selectedRecipe == null)
                return;

            string selectedFactor = ((ComboBoxItem)cmbFactor.SelectedItem).Content.ToString();

            if (selectedFactor == null)
                return;

            List<IngredientClassP3> scaledIngredients;


            if (selectedFactor == "Reset")
            {
                // Reset the ingredients to its original quantities
                scaledIngredients = selectedRecipe.IngredientListIn;
                numTotalCalories = scaledIngredients.Sum(ingredient => ingredient.IngredientCalories);
                txtTotalCalories.Text = "Total Calories: " + numTotalCalories.ToString();
            }
            else
            {
                // Calculate the scaled quantities of ingredients based on the selected scaling factor
                double factor = 1.0;

                if (selectedFactor == "Half")
                    factor = 0.5;
                else if (selectedFactor == "Double")
                    factor = 2.0;
                else if (selectedFactor == "Triple")
                    factor = 3.0;

                scaledIngredients = selectedRecipe.IngredientListIn
                    .Select(ingredient => new IngredientClassP3
                    {
                        Name = ingredient.Name,
                        Quantity = GetUnit(ingredient.Unit, ingredient.Quantity, factor).Item2,
                        Unit = GetUnit(ingredient.Unit, ingredient.Quantity, factor).Item1,
                        IngredientCalories = Math.Round(ingredient.IngredientCalories * factor, 2),
                        FoodGroup = ingredient.FoodGroup
                    })
                    .ToList();

                numTotalCalories = scaledIngredients.Sum(ingredient => ingredient.IngredientCalories);
                txtTotalCalories.Text = "Total Calories: " + numTotalCalories.ToString();
            }

            // Update the ingredient data grid with the scaled ingredients
            dgIngredients.ItemsSource = scaledIngredients;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method calculates the scaled unit and quantity of an ingredient based on the selected scaling factor.
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="quantity"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        private (string, double) GetUnit(string unit, double quantity, double factor)
        {
            double quantityFactor = Math.Round(quantity * factor, 2);
            for (int j = 0; j < 2; j++)
            {
                switch (unit.ToLower())
                {
                    case "teaspoon":
                    case "teaspoons":
                        if (quantityFactor >= 3)
                        {
                            quantityFactor = Math.Round((quantityFactor / 3), 2);
                            if (quantityFactor <= 1 && quantityFactor > 0)
                            {
                                unit = "Tablespoon";
                            }
                            else
                            {
                                unit = "Tablespoons";
                            }
                        }
                        else if (quantityFactor > (double)0.0)
                        {
                            if (quantityFactor <= 1)
                            {
                                unit = "Teaspoon";
                            }
                            else
                            {
                                if (quantityFactor == 3)
                                {
                                    quantityFactor = Math.Round((quantityFactor / 3), 2);

                                    unit = "Tablespoon";
                                }
                                else
                                {
                                    unit = "Teaspoons";
                                }
                            }
                        }
                        continue;

                    case "tablespoon":
                    case "tablespoons":
                        if (quantityFactor >= 16)
                        {
                            quantityFactor = Math.Round((quantityFactor / 16), 2);
                            if (quantityFactor <= 1 && quantityFactor > 0)
                            {
                                unit = "Cup";
                            }
                            else
                            {
                                unit = "Cups";
                            }
                        }
                        else if (quantityFactor <= 1)
                        {
                            unit = "Tablespoon";
                        }
                        else
                        {
                            unit = "Tablespoons";
                        }

                        continue;

                    case "ounce":
                    case "ounces":
                        if (quantityFactor >= 16)
                        {
                            quantityFactor = Math.Round((quantityFactor / 16), 2);
                            unit = "Pounds";
                        }
                        else if (quantityFactor > (double)0.0)
                        {
                            if (quantityFactor <= 1 && quantityFactor > 0)
                            {
                                unit = "Ounce";
                            }
                            else
                            {
                                unit = "Ounces";
                            }
                        }

                        continue;

                    case "pound":
                    case "pounds":
                        if (quantityFactor <= 1 && quantityFactor > 0)
                        {
                            if (quantityFactor == 1)
                            {
                                unit = "Pound";
                            }
                            else
                            {
                                unit = "Pounds";
                            }
                        }
                        else
                        {
                            quantityFactor = Math.Round((quantityFactor * 16), 2);
                            if (quantityFactor <= 1 && quantityFactor > 0)
                            {
                                unit = "Ounce";
                            }
                            else
                            {
                                unit = "Ounces";
                            }
                        }
                        continue;

                    case "cup":
                    case "cups":
                        if (quantityFactor >= 16)
                        {
                            quantityFactor = Math.Round((quantityFactor / 16), 2);
                            unit = "Gallons";
                        }
                        else if (quantityFactor >= (double)0.0)
                        {
                            if (quantityFactor <= 1 && quantityFactor > 0)
                            {
                                unit = "Cup";
                            }
                            else
                            {
                                unit = "Cups";
                            }
                        }

                        continue;

                    case "gallon":
                    case "gallons":
                        if (quantityFactor >= 1 && quantityFactor > 0)
                        {
                            if (quantityFactor == 1)
                            {
                                unit = "Gallon";
                            }
                            else
                            {
                                unit = "Gallons";
                            }
                        }
                        else
                        {
                            quantityFactor = Math.Round((quantityFactor * 16), 2);
                            if (quantityFactor <= 1 && quantityFactor > 0)
                            {
                                unit = "Cup";
                            }
                            else
                            {
                                unit = "Cups";
                            }
                        }
                        continue;
                    default:
                        break;
                }
            }
            return (unit, quantityFactor);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method updates the lstRecipeSteps ListBox with the steps of the selected recipe.
        /// </summary>
        private void UpdateStepList()
        {
            if (selectedRecipe == null)
                return;

            List<StepClassP3> steps = selectedRecipe.StepListIn
                .Select((step, index) => new StepClassP3
                {
                    NumOfSteps = index + 1,
                    Step = step.Step
                })
                .ToList();

            lstRecipeSteps.ItemsSource = steps;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method sorts the recipes in the cmbRecipeName ComboBox alphabetically and updates the data context.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            DataContext = ServiceLocator.MainViewModel;

            sortedRecipe = (DataContext as MainViewModel).Recipies.OrderBy(r => r.RecipeName).ToList();

            cmbRecipeName.ItemsSource = sortedRecipe;
            cmbRecipeName.DisplayMemberPath = "RecipeName";
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
