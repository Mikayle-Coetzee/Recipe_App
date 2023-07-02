using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6221_P3.Classes
{
    public class MainViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// ObservableCollection to hold the recipes
        /// </summary>
        public ObservableCollection<RecipeClassP3> Recipies { get; set; }

        public MainViewModel()
        {
            // Initialize the Recipes collection as a new instance of ObservableCollection
            Recipies = new ObservableCollection<RecipeClassP3>();
        }

        /// <summary>
        /// Method to add a recipe to the Recipes collection
        /// </summary>
        /// <param name="recipe"></param>
        public void Add(RecipeClassP3 recipe)
        {
            Recipies.Clear();

            // Add the recipe to the Recipes collection
            Recipies.Add(recipe);

            // Notify subscribers that the Recipes property has changed
            OnPropertyChanged("Recipies");
        }

        /// <summary>
        /// INotifyPropertyChanged event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method to raise the PropertyChanged event
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            // Invoke the PropertyChanged event with the updated property name
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
