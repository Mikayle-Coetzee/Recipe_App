#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 3
#endregion

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
    /// Interaction logic for InfoView.xaml
    /// </summary>
    public partial class InfoView : UserControl
    {
        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for InfoView.
        /// </summary>
        public InfoView()
        {
            InitializeComponent();
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This handles the selection change event of the cmbInfo ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbInfo.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Content.ToString() == "Calories")
                {
                    // Show information related to calories
                    FoodGroupComboBox.Visibility = Visibility.Collapsed;
                    txtFoodGroup.Visibility = Visibility.Collapsed;
                    txtInfo.Text = "Calories are a measure of energy. In the context of nutrition, calories refer to the energy content of food and drinks that our bodies can utilize for various physiological functions and activities. Example: Let's consider an example of a medium-sized apple. It contains approximately 52 calories. When we consume this apple, our bodies extract and utilize the energy stored in those calories. This energy is then used for essential functions such as maintaining body temperature, powering physical activities, and supporting bodily processes like digestion and cellular metabolism. It's important to note that the calorie content of foods can vary based on factors such as serving size, cooking methods, and specific varieties.";
                    infoImage.Source = new BitmapImage(new Uri("/Images/InfoCalories.jpg", UriKind.Relative));
                }
                else
                {
                    if (selectedItem.Content.ToString() == "Food Group")
                    {
                        // Show information related to food groups
                        txtInfo.Text = string.Empty;
                        infoImage.Source = new BitmapImage(new Uri("", UriKind.Relative));
                        FoodGroupComboBox.Visibility = Visibility.Visible;
                        txtFoodGroup.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        // Hide food group controls
                        FoodGroupComboBox.Visibility = Visibility.Collapsed;
                        txtFoodGroup.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This handles the selection change event of the FoodGroupComboBox ComboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FoodGroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FoodGroupComboBox.SelectedItem is ComboBoxItem selectedFoodGroup)
            {
                string foodGroup = selectedFoodGroup.Content.ToString();
                switch (foodGroup)
                {
                    case "Starchy foods":
                        txtInfo.Text = "These are foods rich in carbohydrates and provide energy. They typically include grains, cereals, potatoes, and root vegetables. For example, rice, bread, pasta, and sweet potatoes are considered starchy foods.";
                        infoImage.Source = new BitmapImage(new Uri("/Images/InfoStarchyFoods.jpg", UriKind.Relative));
                        break;
                    case "Vegetables and fruits":
                        txtInfo.Text = "This food group consists of plant-based foods that are rich in vitamins, minerals, and dietary fiber. It includes a wide variety of vegetables (leafy greens, cruciferous vegetables, etc.) and fruits (apples, bananas, berries, etc.). For instance, spinach, broccoli, oranges, and strawberries are part of this group.";
                        infoImage.Source = new BitmapImage(new Uri("/Images/InfoVegtablesAndFruits.jpg", UriKind.Relative));
                        break;
                    case "Dry beans, peas, lentils and soya":
                        txtInfo.Text = "This group includes foods that are high in protein, fiber, and various nutrients. Examples of dry beans include kidney beans, black beans, and chickpeas. Lentils and peas, such as green lentils and split peas, also fall into this category. Soybeans are used to make soy products like tofu and soy milk.";
                        infoImage.Source = new BitmapImage(new Uri("/Images/InfoDryBeansPeasLentilsAndSoya.jpg", UriKind.Relative));
                        break;
                    case "Chicken, fish, meat and eggs":
                        txtInfo.Text = "This food group comprises animal-based protein sources. It includes poultry (chicken, turkey), fish (salmon, tuna), red meat (beef, pork), and eggs. These foods are rich in protein, vitamins, and minerals and serve as important sources of nutrition.";
                        infoImage.Source = new BitmapImage(new Uri("/Images/InfoChickenFishMeatAndEggs.jpg", UriKind.Relative));
                        break;
                    case "Milk and dairy products":
                        txtInfo.Text = "This group encompasses milk and various dairy products derived from it, such as cheese, yogurt, and butter. These foods are excellent sources of calcium, protein, and vitamins like vitamin D. Examples include cow's milk, cheddar cheese, and Greek yogurt.";
                        infoImage.Source = new BitmapImage(new Uri("/Images/InfoMilkAndDairyProducts.jpg", UriKind.Relative));
                        break;
                    case "Fats and oil":
                        txtInfo.Text = "This food group includes fats and oils that are consumed in moderation. It comprises sources of healthy fats like vegetable oils (olive oil, canola oil), nuts, seeds, and avocados. While fats are high in calories, they play essential roles in providing energy and carrying fat-soluble vitamins.";
                        infoImage.Source = new BitmapImage(new Uri("/Images/InfoFatsAndOil.jpg", UriKind.Relative));
                        break;
                    case "Water":
                        txtInfo.Text = "Water is not a food group but an essential component of a healthy diet. It is crucial for hydration and the proper functioning of the body. Drinking sufficient water helps maintain bodily functions, supports digestion, and aids in temperature regulation.";
                        infoImage.Source = new BitmapImage(new Uri("/Images/InfoWater.jpg", UriKind.Relative));
                        break;
                    default:
                        txtInfo.Text = string.Empty;
                        infoImage.Source = new BitmapImage(new Uri("/Images/AccountImage.jpg", UriKind.Relative));
                        break;
                }
            }
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
