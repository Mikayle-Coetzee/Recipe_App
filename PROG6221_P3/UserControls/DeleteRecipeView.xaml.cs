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
        WorkerClassP3 worker = new WorkerClassP3 ();
        private RecipeClassP3 selectedRecipe;
        private List<RecipeClassP3> sortedRecipe = new List<RecipeClassP3> ();

        public DeleteRecipeView()
        {
            InitializeComponent();
            DataContext = ServiceLocator.MainViewModel;

            cmbRecipeNames.ItemsSource = (DataContext as MainViewModel).Recipies;
            cmbRecipeNames.DisplayMemberPath = "RecipeName";

            lstRecipeNames.ItemsSource = (DataContext as MainViewModel).Recipies;
            lstRecipeNames.DisplayMemberPath = "RecipeName";
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
            string confirm = worker.ShowInputDialog("Enter 'yes' to confirm that you want to delete the selected recipe.","Confirm Delete");
            
            if (confirm.ToLower().Equals("yes"))
            {
            DataContext = ServiceLocator.MainViewModel;

                if (cmbRecipeNames.SelectedItem is RecipeClassP3 selectedRecipe)
                {
                    // Delete the recipe from the MainViewModel
                    ServiceLocator.MainViewModel.Recipies.Remove(selectedRecipe);

                    // Clear the selection in the ComboBox
                    cmbRecipeNames.SelectedItem = null;
                }

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

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            DataContext = ServiceLocator.MainViewModel;

            sortedRecipe = (DataContext as MainViewModel).Recipies.OrderBy(r => r.RecipeName).ToList();

            cmbRecipeNames.ItemsSource = sortedRecipe;
            cmbRecipeNames.DisplayMemberPath = "RecipeName";

            lstRecipeNames.ItemsSource = sortedRecipe;
            lstRecipeNames.DisplayMemberPath = "RecipeName"; 
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
