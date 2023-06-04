#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 1 and 2
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace POE_PROG6221_ST10023767_GR01
{
    public class Validation
    {
        /// <summary>
        /// Enumeration called "measurements" which contains the standard units of measurement used in recipes such 
        /// as tsp, tbsp, oz, lb, cup, and gal. 
        /// </summary>
        enum Measurements
        {
            tsp, teaspoon, teaspoons, tbsp, tablespoon, tablespoons, oz,
            ounce, ounces, lb, pound, pounds, cup, cups, gal, gallon, gallons
        }

        /// <summary>
        /// Private string array is used to convert numeric quantities into their word form for better user 
        /// readability. Each element represents a word form of a single-digit number, from "one" to "nine".
        /// </summary>
        private readonly string[] ones = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

        /// <summary>
        /// Private string array stores the English names of numbers between 11 and 19 that have unique names 
        /// and do not follow the standard pattern of adding "-teen" to the end of the ones place value.
        /// </summary>
        private readonly string[] teens = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen",
            "seventeen", "eighteen", "nineteen" };

        /// <summary>
        /// Private string array stores the English names for the numbers 10, 20, 30, ..., 90.
        /// </summary>
        private readonly string[] tens = { "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy",
            "eighty", "ninety" };

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for Validation.
        /// </summary>
        public Validation()
        {

        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method is used to validate user input that should be "yes" or "no". It first checks whether the 
        /// user input is a valid string using the Validate_String method. Then it checks whether the input is
        /// either "yes" or "no", ignoring any leading/trailing spaces and case sensitivity.
        /// </summary>
        /// <param name="userInput">The user input string to be validated.</param>
        /// <returns>boolean True if the input is a yes or no, false otherwise.</returns>
        public bool Validate_Yes_Or_No(string userInput)
        {
            // Initialize variable
            bool valid;

            if (Validate_String(userInput) == true)
            {
                if ((userInput.Trim().ToUpper().Equals("YES")) || (userInput.Trim().ToUpper().Equals("NO")))
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                }
            }
            else
            {
                valid = false;
            }
            return valid;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method is used to validate a string input. It checks whether the input is not empty or null. It 
        /// returns true if the input is valid and false otherwise. It also catches any exceptions that may occur 
        /// during the validation process and displays an error message.
        /// </summary>
        /// <param name="userInput">The user input string to be validated.</param>
        /// <returns>boolean True if the input is a valid string, false otherwise.</returns>
        public bool Validate_String(string userInput)
        {
            // Initialize variable
            bool valid = false;

            try
            {
                if ((!userInput.Equals(string.Empty)) && (!userInput.Equals(null)))
                {
                    valid = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Line 111: " + ex.Message);
                valid = false;
            }
            return valid;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Validates if the user input is a valid integer. Returns true if the input is a valid integer,false 
        /// otherwise.
        /// </summary>
        /// <param name="userInput">The user input string to be validated.</param>
        /// <returns>boolean True if the input is a valid integer, false otherwise.</returns>
        public bool Validate_Integer(string userInput)
        {
            // Initialize variable
            bool valid ;
            int number;

            try
            {
                if (Int32.TryParse(userInput, out number) == true)
                {
                    valid = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Line 139: " + ex.Message);
                valid = false;//maby not needed 
            }
            finally
            {
                valid = Int32.TryParse(userInput, out number);
            }
            return valid;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method validates if a user input can be parsed as a float number. It returns true if the input 
        /// can be successfully parsed, false otherwise. It also catches any exception that may arise during the
        /// parsing process and outputs an error message to the console. The method uses the float.TryParse 
        /// method to parse the input as a float number.
        /// </summary>
        /// <param name="userInput">The user input string to be validated.</param>
        /// <returns>boolean True if the input is a valid float, false otherwise.</returns>
        public bool Validate_Float(string userInput)
        {
            // Initialize variable
            bool valid;
            float number;

            try
            {
                if (float.TryParse(userInput, out number) == true)
                {
                    valid = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Line 173: " + ex.Message);
                valid = false;
            }
            finally
            {
                valid = float.TryParse(userInput, out number);
            }
            return valid;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method converts the string number entered by the user to a numerical number, like "twenty six" to 26
        /// </summary>
        /// <param name="userInput">The user input as a string to be converted.</param>
        /// <returns>The numerical number</returns>
        public double Convert_Text_To_Corresponding_Numerical_Value(string userInput)
        {
            // Initialize variable
            double currentResult = 0.0f;
            int result = 0, bigMultiplierValue = 1, wholeNumber = 0, decimalNumber = 0 ;
            bool bigMultiplierIsActive = false, valid = false;
            bool minusFlag = false, halfFlag = false, decimalFlag = false;

            // Define the big scales numbers, e.g. "hundred" is 100, "thousand" is 1000, etc.
            var bigscales = new Dictionary<string, int>() 
            {
                { "hundred", 100 },
                { "hundreds", 100 },
                { "thousand", 1000 },
                { "million", 1000000 },
                { "billion", 1000000000 },
            };

            // Define words that indicate a negative value
            string[] minusWords = { "minus", "negative" };

            // Define the characters used to split up the input text
            var splitchars = new char[] { ' ', '-', ',', '.' };

            // Convert all words to lowercase for proper matching
            var lowercase = userInput.ToLower();
            var inputwords = lowercase.Split(splitchars, StringSplitOptions.RemoveEmptyEntries);
            double number;
            try
            {
                foreach (string curword in inputwords)
                {
                    // Check if the current word is a big multiplier word (e.g. hundred, thousand)
                    if (bigscales.ContainsKey(curword))
                    {
                        bigMultiplierValue *= bigscales[curword];
                        bigMultiplierIsActive = true;
                        valid = true;
                    }
                    else
                    {
                        // Multiply the current result by the previous word bigMultiplier
                        // and disable the big multiplier until next time
                        if (bigMultiplierIsActive)
                        {
                            result += (int)currentResult * bigMultiplierValue;
                            currentResult = 0;
                            bigMultiplierValue = 1; // reset the multiplier value
                            bigMultiplierIsActive = false; // turn it off until next time
                            valid = true;
                        }
                        else
                        {
                            valid = false;
                        }

                        // Translate the incoming text word to an integer
                        int n;
                        if ((n = Array.IndexOf(ones, curword) + 1) > 0)
                        {
                            if (decimalFlag == false)
                            {
                                currentResult += n;
                                valid = true;
                            }
                            else
                            {
                                wholeNumber = (int)currentResult;
                                decimalNumber += n;
                                valid = true;
                            }
                        }
                        else if ((n = Array.IndexOf(teens, curword) + 1) > 0)
                        {
                            if (decimalFlag == false)
                            {
                                currentResult += n + 10;
                                valid = true;
                            }
                            else
                            {
                                wholeNumber = (int)currentResult;
                                decimalNumber += n + 10;
                                valid = true;
                            }
                        }
                        else if ((n = Array.IndexOf(tens, curword) + 1) > 0)
                        {
                            if (decimalFlag == false)
                            {
                                currentResult += n * 10;
                                valid = true;
                            }
                            else
                            {
                                wholeNumber = (int)currentResult;
                                decimalNumber += n * 10;
                                valid = true;
                            }
                        }
                        // Allow for negative words (like "minus") 
                        else if (minusWords.Contains(curword))
                        {
                            minusFlag = true;
                            valid = true;
                        }
                        else
                        {
                            valid = false;
                        }
                        // Allow for half values
                        if (curword == "half")
                        {
                            halfFlag = true;
                            valid = true;
                        }
                        // Allow for decimal values 
                        if (curword == "point")
                        {
                            decimalFlag = true;
                            valid = true;
                        }
                        else
                        {
                            if (curword == "comma")
                            {
                                decimalFlag = true;
                                valid = true;
                            }
                        }
                    }
                }

                // Combine the results and the big multiplier value to get the final number
                if ((valid == true) && (decimalFlag == false))
                {
                    number = result + currentResult * bigMultiplierValue;
                }
                else
                {
                    if ((valid == true) && (decimalFlag == true))
                    {
                        string decimalValue = wholeNumber + "," + decimalNumber;
                        number = float.Parse(decimalValue);
                    }
                    else
                    {
                        number = -9999;
                    }
                }

                // If a minus word was found, make the number negative
                if (minusFlag)
                    number *= -1;

                // If a half word was found, add 0.5 to the number
                if (halfFlag)
                    number += 0.5;
            }
            catch (Exception ex)
            {
                // If an exception occurred, print the error message and set the number to 0
                Console.WriteLine("Line 352: " + ex.Message);
                number = -9999;
            }

            return number;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method converts the numerical number entered by the user to a string number, like 26 to "twenty six" 
        /// </summary>
        /// <param name="userInput">The user input as a double to be converted.</param>
        /// <returns>The string number</returns>
        public string Convert_Numerical_Value_To_Corresponding_Text(double userInput)
        {
            // Initialize variable
            string half = "and a half";
            string result = string.Empty;
            int wholeNumber;
            int decimalNumber;

            // Define the big scales we'll be using, e.g. 100 is "hundred" , 1000 is "thousand", etc.
            var largeValueToText = new Dictionary<int, string>() 
            {
                { 1000000000, "billion" }, 
                { 1000000, "million" }, 
                { 1000, "thousand" }, 
                { 100, "hundred" } 
            };

            // check if number is negative
            if (userInput < 0)
            {
                result += "minus ";
                userInput *= -1;
            }

            string input = Convert.ToString(userInput);
            
            // convert number to string and split by decimal point
            if (input.Contains("."))
            {
                string[] parts = input.Split('.');
                wholeNumber = int.Parse(parts[0]);
                decimalNumber = int.Parse(parts[1]);
            }
            else
            {
                if (input.Contains(","))
                {
                    string[] parts = input.Split(',');
                    wholeNumber = int.Parse(parts[0]);
                    decimalNumber = int.Parse(parts[1]);
                }
                else
                {
                    wholeNumber = Convert.ToInt32(userInput);
                    decimalNumber = 0;
                }
            }

            // convert whole number to text words
            if (wholeNumber == 0)
            {
                result += "zero";
            }
            else
            {
                foreach (int scale in largeValueToText.Keys)
                {
                    if (wholeNumber >= scale)
                    {
                        int quotient = (int)(wholeNumber / scale);
                        wholeNumber %= (int)scale;
                        result += Convert_Numerical_Value_To_Corresponding_Text(quotient) + " " + 
                            largeValueToText[scale];
                        if (wholeNumber > 0)
                        {
                            result += " ";
                        }
                    }
                }

                if (wholeNumber > 0)
                {
                    if (wholeNumber >= 11 && wholeNumber <= 19)
                    {
                        result += teens[wholeNumber - 11];

                    }
                    else if (wholeNumber % 10 == 0)
                    {
                        result += tens[wholeNumber / 10 - 1];
                    }
                    else
                    {
                        if (wholeNumber > 0 && wholeNumber < 10)
                        {
                            result += ones[wholeNumber - 1];
                        }
                        else
                        {
                            int over = wholeNumber % 10;
                            wholeNumber -= over;
                            result += tens[wholeNumber / 10 - 1] + " " + ones[over - 1];
                        }
                    }
                }
            }

            // add decimal part if not zero
            if ((decimalNumber > 0) && (decimalNumber != 5))
            {
                result += " point ";
                if (decimalNumber >= 11 && decimalNumber <= 19)
                {
                    result += teens[decimalNumber - 11];
                }
                else if (decimalNumber % 10 == 0)
                {
                    result += tens[decimalNumber / 10 - 1];
                }
                else
                {
                    if (decimalNumber > 0 && decimalNumber < 10)
                    {
                        result += ones[decimalNumber - 1];
                    }
                    else
                    {
                        int over = decimalNumber % 10;
                        decimalNumber -= over;
                        result += tens[decimalNumber / 10 - 1] + " " + ones[over - 1];
                    }
                }
            }

            // add "and a half" if decimal part is 0.5
            if (decimalNumber == 5)
            {
                result += $" {half}";
            }

            if (result == "zero and a half")
            {
                result = "half";
            }
            return result;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// This method validates if a user input is a valid unit of measurement type. It returns true if the 
        /// input is found in the enum, false otherwise. 
        /// </summary>
        /// <param name="userInput">The user input string to be validated.</param>
        /// <returns>boolean True if the input is a valid unit of measurement, false otherwise</returns>
        public bool Validate_Unit_Of_Measurement(string userInput)
        {
            bool valid = Enum.IsDefined(typeof(Measurements), userInput);
            return valid;
        }


        public bool ValidateUserInput(string userInput, int recipeCount, ClockTimerClass clockTimerClass)
        {
            if (int.TryParse(userInput, out int userChoice))
            {
                if (userChoice >= 1 && userChoice <= (recipeCount+1))
                {
                    return true;
                }
            }
            return false;
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//