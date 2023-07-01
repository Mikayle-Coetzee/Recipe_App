using POE_PROG6221_ST10023767_GR01;
using POE_PROG6221_ST10023767_GR01.Classes;
using PROG6221_P3.Classes;
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
        public List<IngredientClassP3> ingredientList = new List<IngredientClassP3>();
        public List<StepClassP3> stepList = new List<StepClassP3>();
        public List<POE_PROG6221_ST10023767_GR01.RecipeClass> recipes = new List<POE_PROG6221_ST10023767_GR01.RecipeClass>();

        public List<string> recipeList = new List<string>();
        private int numberOfSteps = 0;
        private int numberOfIngredients = 0;

        POE_PROG6221_ST10023767_GR01.Validation validation = new POE_PROG6221_ST10023767_GR01.Validation();

        public AddRecipeView()
        {
            InitializeComponent();
            // Set the DataContext of the DataGrid to the ingredients collection
            dgIngredients.ItemsSource = ingredientList;

            // Set the ItemsSource and DisplayMemberPath of the ListBox
            dgSteps.ItemsSource = stepList;
        }

        private void WriteIngredientsToList()
        {
            int number = this.numberOfIngredients;

            ingredientList = new List<IngredientClassP3>();


            for (int i = 0; i < number; i++)
            {
                var newIngredient = new IngredientClassP3
                {
                    Name = GetIngredientName(),
                    Quantity = GetIngredientQuantity(),
                    Unit = GetIngredientUnit(),
                    IngredientCalories = GetIngredientCalories(),
                    FoodGroup = GetFoodGroup(),
                    NumOfIngredients = number
                };

                ingredientList.Add(newIngredient);
            }
        }


        private string GetFoodGroup()
        {
            // Initialize variables
            bool valid1, valid2;

            string foodGroup = ((ComboBoxItem)cmbFoodGroup.SelectedItem).Content.ToString();

            // Validate the user's input
            do
            {
                valid1 = validation.Validate_String(foodGroup);
                valid2 = validation.Validate_Food_Group(foodGroup);//this must be checked if the user enter it in the message box, not the combo box

                if (!valid1 || !valid2)
                {
                    //pop up message and have them enter in the pop up message 
                    //Unit = new InputBinding from message
                }
            } while (!valid1 || !valid2);

            // Return the string of the entered ingredient unit
            return foodGroup;
        }



        private double GetIngredientCalories()
        {
            // Initialize variables
            bool valid1, valid2;

            string userInput = txtCalories.Text;


            do
            {
                valid1 = validation.Validate_String(userInput);
                valid2 = validation.Validate_Float(userInput);

                if (!valid1 || !valid2)
                {
                    //pop up message and have them enter in the pop up message 
                    //calories = new InputBinding from message
                }

            } while (!valid1 || !valid2);

            double calories = Convert.ToDouble(userInput);

            return calories;
        }


        public double GetIngredientQuantity()
        {
            // Initialize variables
            bool valid1, valid2;

            string userInput = txtQuantity.Text;


            do
            {
                valid1 = validation.Validate_String(userInput);
                valid2 = validation.Validate_Float(userInput);

                if (!valid1 || !valid2)
                {
                    //pop up message and have them enter in the pop up message 
                    //Quantity = new InputBinding from message
                }

            } while (!valid1 || !valid2);


            double quantity = Convert.ToDouble(userInput);

            return quantity;
        }

        private String GetIngredientUnit()
        {
            // Initialize variables
            bool valid1, valid2;

            string unit = ((ComboBoxItem)cmdUnits.SelectedItem).Content.ToString();

            // Validate the user's input
            do
            {
                valid1 = validation.Validate_String(unit);
                valid2 = validation.Validate_Unit_Of_Measurement(unit);//this must be checked if the user enter it in the message box, not the combo box

                if (!valid1 || !valid2)
                {
                    //pop up message and have them enter in the pop up message 
                    //Unit = new InputBinding from message
                }
            } while (!valid1 || !valid2);

            // Return the string of the entered ingredient unit
            return unit;
        }
        private string GetIngredientName()
        {
            string name = txtIngredientName.Text;
            bool valid;
            do
            {
                valid = validation.Validate_String(name);
                if (!valid)
                {
                    //pop up message and have them enter in the pop up message 
                    //Name = new InputBinding from message
                }

            } while (!valid);

            return name;
        }





        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            numberOfIngredients++;

            IngredientClassP3 newIngredient = new IngredientClassP3
            {
                Name = GetIngredientName(),
                Quantity = GetIngredientQuantity(),
                Unit = GetIngredientUnit(),
                IngredientCalories = GetIngredientCalories(),
                FoodGroup = GetFoodGroup(),
                NumOfIngredients = numberOfIngredients
            };

            ingredientList.Add(newIngredient);

            dgIngredients.ItemsSource = ingredientList;

            // Refresh the DataGrid to reflect the changes
            dgIngredients.Items.Refresh();

            // Select the newly added item in the DataGrid
            dgIngredients.SelectedItem = newIngredient;
            dgIngredients.ScrollIntoView(newIngredient);

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
            WriteStepsToList();
            WriteIngredientsToList();

            txtRecipeName.Text = string.Empty;
            ingredientList.Clear();
            dgIngredients.DataContext = ingredientList;
        }

        private void btnAddStep_Click(object sender, RoutedEventArgs e)
        {
            numberOfSteps++;

            StepClassP3 newStep = new StepClassP3
            {
                NumOfSteps = numberOfSteps,
                Step = GetStep()
            };

            stepList.Add(newStep);

            dgSteps.ItemsSource = stepList;

            // Refresh the DataGrid to reflect the changes
            dgSteps.Items.Refresh();

            // Select the newly added item in the DataGrid
            dgSteps.SelectedItem = newStep;
            dgSteps.ScrollIntoView(newStep);

            txtStep.Text = string.Empty;
        }

        private void WriteStepsToList()
        {
            // Initialize variables
            int number = this.numberOfSteps;

            stepList = new List<StepClassP3>();

            for (int i = 0; i < number; i++)
            {
                StepClassP3 newStep = new StepClassP3
                {
                    NumOfSteps = number,
                    Step = GetStep()
                };

                stepList.Add(newStep);
            }
        }

        private string GetStep()
        {
            string step = txtStep.Text;
            bool valid;
            do
            {
                valid = validation.Validate_String(step);
                if (!valid)
                {
                    //pop up message and have them enter in the pop up message 
                    //Name = new InputBinding from message
                }

            } while (!valid);

            return step;
        }
    }
}
