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
    /// Interaction logic for DeleteRecipeView.xaml
    /// </summary>
    public partial class DeleteRecipeView : UserControl
    {
        /// <summary>
        /// Instantiates a new instance of the Worker class.
        /// </summary>
        private WorkerClassP3 worker = new WorkerClassP3 ();

        /// <summary>
        /// Store the currently selected recipe
        /// </summary>
        private RecipeClassP3 selectedRecipe;

        /// <summary>
        /// Store a sorted list of recipes
        /// </summary>
        private List<RecipeClassP3> sortedRecipe = new List<RecipeClassP3> ();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for DeleteRecipeView.
        /// </summary>
        public DeleteRecipeView()
        {
            InitializeComponent();
            DataContext = ServiceLocator.MainViewModel;

            // Set up the ComboBox and ListBox data sources
            cmbRecipeNames.ItemsSource = (DataContext as MainViewModel).Recipies;
            cmbRecipeNames.DisplayMemberPath = "RecipeName";

            lstRecipeNames.ItemsSource = (DataContext as MainViewModel).Recipies;
            lstRecipeNames.DisplayMemberPath = "RecipeName";
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This handles the selection change event of the ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the selected recipe
            selectedRecipe = cmbRecipeNames.SelectedItem as RecipeClassP3;

            // Refresh the DataGrid to reflect the changes
            dgIngredients.ItemsSource = null;
            dgIngredients.Items.Refresh();

            lstRecipeSteps.ItemsSource = null;
            lstRecipeSteps.Items.Refresh();

            UpdateStepList();
            UpdateIngredientDataGrid();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method updates the list of steps for the selected recipe
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
        /// This method updates the data grid with the list of ingredients for the selected recipe
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

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This button will delete the selected recipe if the user confirms that they would like to delete the 
        /// recipe 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // asks for confirmation before deleting the selected recipe
            string confirm = worker.ShowInputDialog("Enter 'yes' to confirm that you want to delete the selected " +
                "recipe.","Confirm Delete");
            
            if (confirm.ToLower().Equals("yes"))
            {
                // Remove the selected recipe from the MainViewModel
                DataContext = ServiceLocator.MainViewModel;

                if (cmbRecipeNames.SelectedItem is RecipeClassP3 selectedRecipe)
                {
                    // Delete the recipe from the MainViewModel
                    ServiceLocator.MainViewModel.Recipies.Remove(selectedRecipe);

                    // Clear the selection in the ComboBox
                    cmbRecipeNames.SelectedItem = null;
                }

                // Update the ComboBox and ListBox with the sorted recipe list
                sortedRecipe = (DataContext as MainViewModel).Recipies.OrderBy(r => r.RecipeName).ToList();

                cmbRecipeNames.ItemsSource = sortedRecipe;
                cmbRecipeNames.DisplayMemberPath = "RecipeName";

                lstRecipeNames.ItemsSource = sortedRecipe;
                lstRecipeNames.DisplayMemberPath = "RecipeName"; ;
            }
            else
            {
                worker.ShowNotificationBox("Request to delete the selected recipe was canceled", "Cancel Delet Request"); 
            }  
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This button will sort the recipes in alphabetical order and update the comboboxes 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            DataContext = ServiceLocator.MainViewModel;

            // Sort the recipe list
            sortedRecipe = (DataContext as MainViewModel).Recipies.OrderBy(r => r.RecipeName).ToList();

            // Update the ComboBox and ListBox data sources
            cmbRecipeNames.ItemsSource = sortedRecipe;
            cmbRecipeNames.DisplayMemberPath = "RecipeName";

            lstRecipeNames.ItemsSource = sortedRecipe;
            lstRecipeNames.DisplayMemberPath = "RecipeName"; 
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
