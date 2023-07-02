﻿using PROG6221_P3.Classes;
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
        public DeleteRecipeView()
        {
            InitializeComponent();
            DataContext = ServiceLocator.MainViewModel;

            cmbRecipeNames.ItemsSource = (DataContext as MainViewModel).Recipies;
            cmbRecipeNames.DisplayMemberPath = "RecipeName";
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///
        }
    }
}
