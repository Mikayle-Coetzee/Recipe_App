#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 3
#endregion

using POE_PROG6221_ST10023767_GR01;
using PROG6221_P3.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ViewRecipeView.xaml
    /// </summary>
    public partial class ViewRecipeView : UserControl
    {
       public List<RecipeClassP3> RecipeList { get; set; }
        private RecipeClassP3 selectedRecipe;
        private List<RecipeClassP3> filteredRecipes; 
        private WorkerClassP3 worker = new WorkerClassP3();
        private List<RecipeClassP3> sortedRecipe = new List<RecipeClassP3>();
        private double numTotalCalories = 0;

        public ViewRecipeView()
        {
            InitializeComponent();

            DataContext = ServiceLocator.MainViewModel;

            cmbRecipeNames.ItemsSource = (DataContext as MainViewModel).Recipies;
            cmbRecipeNames.DisplayMemberPath = "RecipeName";

            // Initialize the filteredRecipes list
            filteredRecipes = new List<RecipeClassP3>((DataContext as MainViewModel).Recipies);
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRecipe = cmbRecipeNames.SelectedItem as RecipeClassP3;
            dgIngredients.ItemsSource = null;
            dgIngredients.Items.Refresh();

            lstRecipeSteps.ItemsSource = null;
            lstRecipeSteps.Items.Refresh();

            UpdateStepList();
            UpdateIngredientDataGrid();
            List<IngredientClassP3> ingredients;

            ingredients = selectedRecipe.IngredientListIn;
            numTotalCalories = ingredients.Sum(ingredient => ingredient.IngredientCalories);
            txtTotalCalories.Text = "Total Calories: " + numTotalCalories.ToString();
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

        private void UpdateIngredientDataGrid()
        {
            if (selectedRecipe == null)
                return;

            List<IngredientClassP3> ingredients;

            ingredients = selectedRecipe.IngredientListIn
                .Select(ingredient => new IngredientClassP3
                {
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity,
                    Unit = ingredient.Unit,
                    IngredientCalories = ingredient.IngredientCalories,
                    FoodGroup = ingredient.FoodGroup
                })
                .ToList();
            dgIngredients.ItemsSource = ingredients;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string confirm = worker.ShowInputDialog("Enter 'yes' to confirm that you want to delete the selected recipe.", "Confirm Delete");

            if (confirm.ToLower().Equals("yes"))
            {
                DataContext = ServiceLocator.MainViewModel;

                RecipeClassP3 selectedRecipe = cmbRecipeNames.SelectedItem as RecipeClassP3;
                if (selectedRecipe != null)
                {
                    // Delete the recipe from the MainViewModel
                    ServiceLocator.MainViewModel.Recipies.Remove(selectedRecipe);
                    filteredRecipes.Remove(selectedRecipe); // Remove from the filtered list as well

                    // Clear the selection in the ComboBox
                    cmbRecipeNames.SelectedItem = null;
                }

                cmbRecipeNames.ItemsSource = (DataContext as MainViewModel).Recipies;
                cmbRecipeNames.DisplayMemberPath = "RecipeName";
            }
            else
            {
                worker.ShowNotificationBox("Request to delete the selected recipe was canceled", "Cancel Delete Request");
            }
        }

        private void lstRecipeSteps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

             btnFilter.Visibility = Visibility.Visible;
            

            if (cmbFilter.SelectedItem is ComboBoxItem selectedFilter)
            {
                string filter = selectedFilter.Content.ToString();
                switch (filter)
                {
                    case "The name of an ingredient that must be in the recipe":
                        lblFilter.Content = "Ingredient name: ";
                        lblFilter.Visibility = Visibility.Visible;
                        txtIngredientFilterName.Visibility = Visibility.Visible;
                        cmbFoodGroupFilter.Visibility = Visibility.Hidden;
                        txtCaloriesFilterMax.Visibility = Visibility.Hidden;
                        lblRecipeName.Visibility = Visibility.Hidden;
                        cmbRecipeNames.Visibility = Visibility.Hidden;
                        break;
                    case "A food group that must be in the recipe":
                        lblFilter.Content = "Food group";
                        lblFilter.Visibility = Visibility.Visible;
                        cmbFoodGroupFilter.Visibility = Visibility.Visible;
                        txtIngredientFilterName.Visibility = Visibility.Hidden;
                        txtCaloriesFilterMax.Visibility = Visibility.Hidden;
                        lblRecipeName.Visibility = Visibility.Hidden;
                        cmbRecipeNames.Visibility = Visibility.Hidden;
                        break;
                    case "The maximum number of calories":
                        lblFilter.Content = "Max calories: ";
                        lblFilter.Visibility = Visibility.Visible;
                        txtCaloriesFilterMax.Visibility = Visibility.Visible;
                        cmbFoodGroupFilter.Visibility = Visibility.Hidden;
                        txtIngredientFilterName.Visibility = Visibility.Hidden;
                        lblRecipeName.Visibility = Visibility.Hidden;
                        cmbRecipeNames.Visibility = Visibility.Hidden;
                        break;
                    case "Nothing"://normal display
                        lblFilter.Content = string.Empty;
                        lblFilter.Visibility = Visibility.Hidden;
                        txtCaloriesFilterMax.Visibility = Visibility.Hidden;
                        cmbFoodGroupFilter.Visibility = Visibility.Hidden;
                        txtIngredientFilterName.Visibility = Visibility.Hidden;
                        btnFilter.Visibility = Visibility.Hidden;
                        lblRecipeName.Visibility = Visibility.Visible;
                        cmbRecipeNames.Visibility = Visibility.Visible;
                        break;
                    default:
                        lblFilter.Content=string.Empty;
                        lblFilter.Visibility= Visibility.Hidden;
                        txtCaloriesFilterMax.Visibility = Visibility.Hidden;
                        cmbFoodGroupFilter.Visibility = Visibility.Hidden;
                        txtIngredientFilterName.Visibility = Visibility.Hidden;
                        btnFilter.Visibility = Visibility.Hidden;
                        break;
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
         
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            lblRecipeName.Visibility=Visibility.Visible;
            cmbRecipeNames.Visibility=Visibility.Visible;

            if (RecipeList != null)
            {
                string ingredientName = txtIngredientFilterName.Text;

                List<RecipeClassP3> recipesWithIngredient = RecipeList.Where(recipe => recipe.IngredientListIn.Any(ingredient => ingredient.Name == ingredientName)).ToList();

                if (recipesWithIngredient.Count > 0)
                {
                    // Display the recipes that contain the specified ingredient
                    dgIngredients.ItemsSource = recipesWithIngredient;
                }
                else
                {
                    // No recipes contain the specified ingredient
                    dgIngredients.ItemsSource = null;
                }
            }
        }

        private void cmbFoodGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            DataContext = ServiceLocator.MainViewModel;

            sortedRecipe = (DataContext as MainViewModel).Recipies.OrderBy(r => r.RecipeName).ToList();

            cmbRecipeNames.ItemsSource = sortedRecipe;
            cmbRecipeNames.DisplayMemberPath = "RecipeName";
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
