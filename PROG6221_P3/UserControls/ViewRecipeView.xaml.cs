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
using System.Collections.ObjectModel;
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
        /// <summary>
        /// Gets or sets a list of recipes
        /// </summary>
        public List<RecipeClassP3> RecipeList { get; set; }

        /// <summary>
        /// Holds the currently selected recipe
        /// </summary>
        private RecipeClassP3 selectedRecipe;

        /// <summary>
        /// Holds the list of filtered recipes
        /// </summary>
        private List<RecipeClassP3> filteredRecipes;

        /// <summary>
        /// Instantiates a new instance of the Worker class.
        /// </summary>
        private WorkerClassP3 worker = new WorkerClassP3();

        /// <summary>
        /// Holds the list of recipes sorted by recipe name
        /// </summary>
        private List<RecipeClassP3> sortedRecipe = new List<RecipeClassP3>();

        /// <summary>
        /// Instantiates a new instance of the Validation class thats in the part 2 project. The Validation class 
        /// can now be used to perform validation tasks throughout the rest of the code.
        /// /// </summary>
        private POE_PROG6221_ST10023767_GR01.Validation validation = new POE_PROG6221_ST10023767_GR01.Validation();

        //・♫-------------------------------------------------------------------------------------------------♫・//

        // I apologize, Sir, for any shortcomings in the code. I devoted my utmost effort within the given time
        // constraints to make it functional. However, I acknowledge that there is still faults and room for
        // improvement.         

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for ViewRecipeView.
        /// </summary>
        public ViewRecipeView()
        {
            InitializeComponent();

            //DataContext = ServiceLocator.MainViewModel;

            //cmbRecipeNames.ItemsSource = (DataContext as MainViewModel).Recipies;
            cmbRecipeNames.DisplayMemberPath = "RecipeName";

            // Initialize the filteredRecipes list
            filteredRecipes = ServiceLocator.MainViewModel.Recipies.ToList();

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method is called when the selection in the recipe names combo box is changed. 
        /// It updates the selected recipe, refreshes the data grid and step list, and calculates
        /// the total calories.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRecipe = cmbRecipeNames.SelectedItem as RecipeClassP3;
            dgIngredients.ItemsSource = null;
            dgIngredients.Items.Refresh();

            lstRecipeSteps.ItemsSource = null;
            lstRecipeSteps.Items.Refresh();

            UpdateStepList();
            UpdateIngredientDataGrid();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method updates the list of steps based on the selected recipe..
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
        /// This method updates the data grid with the ingredients of the selected recipe.
        /// </summary>
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

        private void lstRecipeSteps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method updates the visibility and content of various controls based on the selected filter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        //lblRecipeName.Visibility = Visibility.Hidden;
                        //cmbRecipeNames.Visibility = Visibility.Hidden;
                        break;
                    case "A food group that must be in the recipe":
                        lblFilter.Content = "Food group";
                        lblFilter.Visibility = Visibility.Visible;
                        cmbFoodGroupFilter.Visibility = Visibility.Visible;
                        txtIngredientFilterName.Visibility = Visibility.Hidden;
                        txtCaloriesFilterMax.Visibility = Visibility.Hidden;
                        //lblRecipeName.Visibility = Visibility.Hidden;
                        //cmbRecipeNames.Visibility = Visibility.Hidden;
                        break;
                    case "The maximum number of calories":
                        lblFilter.Content = "Max calories: ";
                        lblFilter.Visibility = Visibility.Visible;
                        txtCaloriesFilterMax.Visibility = Visibility.Visible;
                        cmbFoodGroupFilter.Visibility = Visibility.Hidden;
                        txtIngredientFilterName.Visibility = Visibility.Hidden;
                        //lblRecipeName.Visibility = Visibility.Hidden;
                        //cmbRecipeNames.Visibility = Visibility.Hidden;
                        break;
                    case "Nothing"://normal display
                        lblFilter.Content = string.Empty;
                        lblFilter.Visibility = Visibility.Hidden;
                        txtCaloriesFilterMax.Visibility = Visibility.Hidden;
                        cmbFoodGroupFilter.Visibility = Visibility.Hidden;
                        txtIngredientFilterName.Visibility = Visibility.Hidden;
                        btnFilter.Visibility = Visibility.Hidden;
                        cmbRecipeNames.ItemsSource = ServiceLocator.MainViewModel.Recipies;
                        //lblRecipeName.Visibility = Visibility.Visible;
                        //cmbRecipeNames.Visibility = Visibility.Visible;
                        break;
                    default:
                        lblFilter.Content = string.Empty;
                        lblFilter.Visibility = Visibility.Hidden;
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
        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This button will filter the recipes and update the combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RecipeList = sortedRecipe;

            if (RecipeList == null)
            {
                // Handle the case when RecipeList is null
                return;
            }

            List<RecipeClassP3> filteredRecipes = RecipeList;

            if (txtIngredientFilterName.Visibility == Visibility.Visible && !string.IsNullOrEmpty(txtIngredientFilterName.Text))
            {
                string selectedIngredient = txtIngredientFilterName.Text.Trim();
                filteredRecipes = filteredRecipes.Where(r => r.IngredientListIn.Any(i => i.Name.Contains(selectedIngredient))).ToList();
            }

            if (txtCaloriesFilterMax.Visibility == Visibility.Visible && double.TryParse(txtCaloriesFilterMax.Text, out double selectedMaxCalories))
            {
                filteredRecipes = filteredRecipes?.Where(r => r.TotalCalories != null && r.TotalCalories.Any(c => c <= selectedMaxCalories)).ToList();
            }

            if (cmbFoodGroupFilter.Visibility == Visibility.Visible && cmbFoodGroupFilter.SelectedItem is ComboBoxItem selectedGroupItem)
            {
                string selectedGroup = selectedGroupItem.Content.ToString();
                filteredRecipes = filteredRecipes?.Where(r => r.IngredientListIn.Any(i => i.FoodGroup == selectedGroup)).ToList();
            }

            cmbRecipeNames.ItemsSource = filteredRecipes;
            cmbRecipeNames.DisplayMemberPath = "RecipeName";

            cmbRecipeNames.SelectedItem = null;
            cmbRecipeNames.Visibility = Visibility.Visible;
            lblRecipeName.Visibility = Visibility.Visible;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFoodGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && ServiceLocator.MainViewModel.Recipies != null && ServiceLocator.MainViewModel.Recipies.Any())
            {
                string text = comboBox.Text;

                var recipes = ServiceLocator.MainViewModel.Recipies.Where(x => x.IngredientListIn.Any(y => y.FoodGroup == text));

                cmbRecipeNames.ItemsSource = recipes;
                sortedRecipe = recipes.ToList();
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method sorts the recipes alphabetically and updates the combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            DataContext = ServiceLocator.MainViewModel;

            ServiceLocator.MainViewModel.Recipies =
                new ObservableCollection<RecipeClassP3>(ServiceLocator.MainViewModel.Recipies.OrderBy(r => r.RecipeName));

            //cmbRecipeNames.ItemsSource = sortedRecipe;
            cmbRecipeNames.DisplayMemberPath = "RecipeName";
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method is called when the text in the ingredient filter textbox is changed.
        /// It filters the recipes based on the ingredient name and updates the combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtIngredientFilterName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string text = textBox.Text;
                var recipes = ServiceLocator.MainViewModel.Recipies.Where(x => x.IngredientListIn.Any(z => z.Name.ToLowerInvariant().StartsWith(text.ToLowerInvariant())));
                cmbRecipeNames.ItemsSource = recipes;
                sortedRecipe = recipes.ToList();

            }

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method is called when the text in the maximum calories filter textbox is changed.
        /// It filters the recipes based on the maximum calories and updates the combo box.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtCaloriesFilterMax_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null)
            {
                string text = textBox.Text;
                if (validation.Validate_Float(text) == true)
                {
                    double selectedMaxCalories = Convert.ToDouble(text);
                    var recipes = ServiceLocator.MainViewModel.Recipies.Where(r => r.TotalCalories != null && 
                    r.TotalCalories.Any(c => c <= selectedMaxCalories)).ToList();
                    cmbRecipeNames.ItemsSource = recipes;

                }
            }
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
