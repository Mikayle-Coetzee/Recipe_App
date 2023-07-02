using POE_PROG6221_ST10023767_GR01;
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
    /// Interaction logic for ViewRecipeView.xaml
    /// </summary>
    public partial class ViewRecipeView : UserControl
    {
        public List<RecipeClassP3> RecipeList { get; set; }

        public ViewRecipeView()
        {
            InitializeComponent();


            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            
            // Handle ComboBox selection change
            string selectedRecipeName = cmbRecipeNames.SelectedItem as string;
            if (selectedRecipeName != null)
            {
                ////Find the selected recipe by name
                ////RecipeClassP3 selectedRecipe = RecipeList.FirstOrDefault(r => r.Name == selectedRecipeName);
                ////if (selectedRecipe != null)
                ////{
                ////    Display recipe ingredients and steps
                ////    lstRecipeSteps.ItemsSource = selectedRecipe.steps;
                ////}
            }
        }

        private void lstRecipeSteps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void cmbFoodGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            RecipeList = GetRecipes();

            // Update ComboBox with recipe names
            cmbRecipeNames.ItemsSource = RecipeList;
        }

        private List<RecipeClassP3> GetRecipes()
        {
            List<RecipeClassP3> recipes = new List<RecipeClassP3>();

            foreach (RecipeClassP3 recipe in RecipeList)
            {
                recipes.Add(recipe);
            }

            return recipes;
        }

    }
}
