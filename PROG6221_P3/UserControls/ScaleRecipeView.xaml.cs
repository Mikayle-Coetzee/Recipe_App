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
       // private POE_PROG6221_ST10023767_GR01.RecipeClass recipeClass = new POE_PROG6221_ST10023767_GR01.RecipeClass();
        private RecipeClassP3 selectedRecipe;
        private double numTotalCalories = 0 ;
        private List<RecipeClassP3> sortedRecipe = new List<RecipeClassP3>();

        public ScaleRecipeView()
        {
            InitializeComponent();

            DataContext = ServiceLocator.MainViewModel;

            cmbRecipeName.ItemsSource = (DataContext as MainViewModel).Recipies;
            cmbRecipeName.DisplayMemberPath = "RecipeName";
        }


        private void lstRecipeSteps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cmbRecipeName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRecipe = cmbRecipeName.SelectedItem as RecipeClassP3;
            dgIngredients.ItemsSource = null;
            dgIngredients.Items.Refresh();

            lstRecipeSteps.ItemsSource = null;
            lstRecipeSteps.Items.Refresh();
        }

        private void cmbFactor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateIngredientDataGrid();
            UpdateStepList();
        }


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
                scaledIngredients = selectedRecipe.IngredientListIn;
                numTotalCalories = scaledIngredients.Sum(ingredient => ingredient.IngredientCalories);
                txtTotalCalories.Text = "Total Calories: " + numTotalCalories.ToString();
            }
            else
            {
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
                        Unit = GetUnit(ingredient.Unit, ingredient.Quantity,factor).Item1,
                        IngredientCalories = Math.Round(ingredient.IngredientCalories * factor,2),
                        FoodGroup = ingredient.FoodGroup
                    })
                    .ToList();

                numTotalCalories = scaledIngredients.Sum(ingredient => ingredient.IngredientCalories);
                txtTotalCalories.Text = "Total Calories: " + numTotalCalories.ToString();
            }

            dgIngredients.ItemsSource = scaledIngredients;
        }

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
                            if(quantityFactor <= 1 && quantityFactor > 0)
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
                            if(quantityFactor <= 1 )
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
                        //else
                        //{
                        //    quantityFactor = Math.Round((quantityFactor * 48), 2);
                        //    unit = "Teaspoons";
                        //}
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
            return (unit,quantityFactor);
        }

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

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            DataContext = ServiceLocator.MainViewModel;

            sortedRecipe = (DataContext as MainViewModel).Recipies.OrderBy(r => r.RecipeName).ToList();

            cmbRecipeName.ItemsSource = sortedRecipe;
            cmbRecipeName.DisplayMemberPath = "RecipeName";
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
