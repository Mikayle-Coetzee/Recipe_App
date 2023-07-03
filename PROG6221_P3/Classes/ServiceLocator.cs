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

namespace PROG6221_P3.Classes
{
    /// <summary>
    /// Static class to provide a centralized location for accessing the MainViewModel instance
    /// </summary>
    public static class ServiceLocator
    {
        /// <summary>
        /// Private static field to hold the MainViewModel instance
        /// </summary>
        private static MainViewModel _mainViewModel;

        /// <summary>
        /// Public static property to access the MainViewModel instance
        /// </summary>
        public static MainViewModel MainViewModel
        {
            get
            {
                //If _mainViewModel is null, create a new instance and assign it to _mainViewModel
                if (_mainViewModel == null)
                    _mainViewModel = new MainViewModel();
                // Return the MainViewModel instance
                return _mainViewModel;
            }
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
