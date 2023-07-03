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
    public class StepClassP3
    {
        /// <summary>
        /// Holds the number of steps 
        /// </summary>
        public int NumOfSteps { get; set; }

        /// <summary>
        /// Holds the step
        /// </summary>
        public string Step { get; set; }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for StepClass.
        /// </summary>
        public StepClassP3() { }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Constructor for StepClass that initializes the object with the provided step and number of steps.
        /// </summary>
        public StepClassP3(string step, int numOfSteps)
        {
            this.Step = step;
            this.NumOfSteps = numOfSteps;
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//
