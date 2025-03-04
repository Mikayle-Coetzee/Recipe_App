﻿#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 1 and 2 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE_PROG6221_ST10023767_GR01
{
    public class StepClass
    {
        /// <summary>
        /// Holds the number of steps 
        /// </summary>
        public int NumOfSteps { get; set; } = 0;
        
        /// <summary>
        /// Holds the step
        /// </summary>
        public string Step = string.Empty;

        /// <summary>
        /// Instantiates a new instance of the Validation class. The Validation class can now be used to 
        /// perform validation tasks throughout the rest of the code.
        /// /// </summary>
        public Validation validate = new Validation();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for StepClass.
        /// </summary>
        public StepClass() { }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Constructor for StepClass that initializes the object with the provided step and number of steps.
        /// </summary>
        public StepClass(string step, int numOfSteps)
        {
            this.Step = step;
            this.NumOfSteps = numOfSteps;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to enter a whole number representing the number of steps, validates the input, 
        /// and returns the value as an integer.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass</param>
        /// <returns>An integer value representing the number of steps entered by the user</returns>
        public int GetNumOfSteps(ClockTimerClass clockTimerClass)
        {
            // Initialize variables
            bool valid = false;
            string userInput = string.Empty;
            int number = 0;

            // Prompt the user to enter the number of steps
            Console.Write("\r\nPlease enter the number of steps as a whole number: ");
            userInput = validate.GetUserInput();

            // Validate the input using the Validate_Integer method
            do
            {
                try
                {
                    valid = validate.Validate_Integer(userInput);
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Line 81: " + e.Message);
                }

                // If the input is valid, convert it to an integer and return the value
                if (valid == true)
                {
                    number = Convert.ToInt32(userInput);
                }
                // If the input is not valid, display an error message and prompt the user to enter a new value
                else
                {
                    clockTimerClass.ChangeToErrorColor();
                    Console.Write("\r\nPlease re-enter the number of steps as a whole number: ");
                    userInput = validate.GetUserInput();
                    clockTimerClass.ChangeBack();
                }
            } while (valid == false);

            //return the number of steps 
            return number;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to enter a number of steps and a step description, creates a new StepClass 
        /// object with those values, and returns it.
        /// </summary>
        /// <param name="clockTimerClass">An instance of the ClockTimerClass</param>
        /// <returns>A new StepClass object with the entered step and number of steps</returns>
        public StepClass GetStepData(ClockTimerClass clockTimerClass)
        {
            // Get the number of steps from the user using the GetNumOfSteps method
            int numOfSteps = GetNumOfSteps(clockTimerClass);

            // Get the step description from the user using the GetStep method
            string step = GetStep();

            // Create and return a new StepClass object with the entered values
            return new StepClass(step,numOfSteps);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method prompts the user to enter a step description and returns the entered value as a string.
        /// </summary>
        /// <returns>The step description entered by the user</returns>
        public string GetStep()
        {
            // Get the step description from the user
            string userInput = validate.GetUserInput();

            // Initialize an empty string for the step
            string step = string.Empty;

            // If the user input is not empty or null, set the step variable to the user's input
            if (validate.Validate_String(userInput) == true)
            {
                step = userInput;
            }

            // Return the step
            return step;
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//