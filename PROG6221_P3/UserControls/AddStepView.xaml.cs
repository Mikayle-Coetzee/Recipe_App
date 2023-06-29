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
    /// Interaction logic for AddStepView.xaml
    /// </summary>
    public partial class AddStepView : UserControl
    {
        //private RecipeClass recipeManager;
        //private ObservableCollection<StepClass> recipeSteps;

        public AddStepView()
        {
            InitializeComponent();
            ////////recipeManager = new RecipeClass();
            ////////recipeSteps = new ObservableCollection<StepClass>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //////////////RecipeClass recipe = new RecipeClass
            //////////////{
            //////////////    //Name = txtRecipeName.Text,
            //////////////    Steps = new ObservableCollection<StepClass>(recipeSteps)
            //////////////};

            //////////////recipeManager.AddRecipe(recipe);

            // Optionally, you can navigate to the ViewRecipeView or update the UI accordingly
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string stepText = txtStep.Text;

            //////////////StepClass newStep = new StepClass
            //////////////{
            //////////////    StepText = stepText,
            //////////////    IsCompleted = false
            //////////////};

            //////////////recipeSteps.Add(newStep);

            // Optionally, you can clear the text box after adding the step
            txtStep.Text = string.Empty;
        }
    }
}
