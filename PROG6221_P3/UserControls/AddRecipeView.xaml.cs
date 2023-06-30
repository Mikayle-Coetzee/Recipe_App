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
        private ObservableCollection<POE_PROG6221_ST10023767_GR01.IngredientClass> ingredients = new ObservableCollection<POE_PROG6221_ST10023767_GR01.IngredientClass>();

        public AddRecipeView()
        {
            InitializeComponent();
            // Set the DataContext of the DataGrid to the ingredients collection
            dgIngredients.ItemsSource = ingredients;
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the values entered by the user
            string ingredientName = txtIngredientName.Text;
            string quantity = txtQuantity.Text;
            double doubleQuantity = 0.0f;
            string unit = ((ComboBoxItem)cmdUnits.SelectedItem).Content.ToString();
            string calories = txtCalories.Text;
            string foodGroup = ((ComboBoxItem)cmbFoodGroup.SelectedItem).Content.ToString();
            double doubleCalories = 0.0f;
            POE_PROG6221_ST10023767_GR01.Validation validation = new POE_PROG6221_ST10023767_GR01.Validation();

            if(validation.Validate_Float(quantity) == true && validation.Validate_Float(calories) == true && validation.Validate_String(ingredientName) == true)
            {
                doubleQuantity = Convert.ToDouble(quantity);
                doubleCalories = Convert.ToDouble(calories);

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
                FoodGroup = foodGroup
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
            AddStepView addStepView = new AddStepView();
           // MainPageView.SetWelcomeMessage(username);

            Window.GetWindow(this).Content = addStepView;
        }
    }
}
