using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddRecipeView.xaml
    /// </summary>
    public partial class AddRecipeView : UserControl
    {
        // Create a collection to hold the ingredients
        ////////////////private ObservableCollection<IngredientClass> ingredients = new ObservableCollection<IngredientClass>();

        public AddRecipeView()
        {
            InitializeComponent();
            // Set the DataContext of the DataGrid to the ingredients collection
            //////////////////dgIngredients.ItemsSource = ingredients;
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the values entered by the user
            string ingredientName = txtIngredientName.Text;
            string quantity = txtQuantity.Text;
            string unit = txtUnit.Text;
            string calories = txtCalories.Text;
            string foodGroup = ((ComboBoxItem)cmbFoodGroup.SelectedItem).Content.ToString();

            ////////////// Create a new Ingredient object with the entered values
            ////////////IngredientClass newIngredient = new IngredientClass
            ////////////{
            ////////////    Name = ingredientName,
            ////////////    Quantity = quantity,
            ////////////    Unit = unit,
            ////////////    Calories = calories,
            ////////////    FoodGroup = foodGroup
            ////////////};

            ////////////// Add the new ingredient to the ingredients collection
            ////////////ingredients.Add(newIngredient);

            // Clear the input fields
            txtIngredientName.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtUnit.Text = string.Empty;
            txtCalories.Text = string.Empty;
            cmbFoodGroup.SelectedIndex = 0;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Save the recipe
        }

        private void cmbFoodGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAddSteps_Click(object sender, RoutedEventArgs e)
        {
            ///redirect
            ///go to the add step view
            //////////////////////AddStepView addStepView = new AddStepView();
            //MainPageView.SetWelcomeMessage(username);

            //////////////////Window.GetWindow(this).Content = addStepView;
        }
    }
}
