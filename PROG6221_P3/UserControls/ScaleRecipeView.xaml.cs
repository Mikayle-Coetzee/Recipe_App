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
    /// Interaction logic for ScaleRecipeView.xaml
    /// </summary>
    public partial class ScaleRecipeView : UserControl
    {
        public ScaleRecipeView()
        {
            InitializeComponent();
        }

        private void lstRecipeSteps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ////////////
        }

        private void cmbFoodGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///////////
        }
    }
}
