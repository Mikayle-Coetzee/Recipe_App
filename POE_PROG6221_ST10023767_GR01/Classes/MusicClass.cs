#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 1 
#endregion

using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POE_PROG6221_ST10023767_GR01.Classes
{
    public class MusicClass
    {
        /// <summary>
        ///  Creates an instance of the ClockTimerClass, which provides functionality for
        ///  keeping track of time and firing events at specified intervals. The 'readonly' 
        ///  keyword is used to ensure that the clockTimerClass object cannot be reassigned 
        ///  after it has been initialized.
        /// </summary>
        private readonly ClockTimerClass clockTimerClass = new ClockTimerClass();

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Default constructor for MusicClass.
        /// </summary>
        public MusicClass() 
        {
        
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method plays the given audio file in a new thread and updates the console color using the
        /// ClockTimerClass.
        /// </summary>
        /// <param name="Audio"></param>
        private void Play(string Audio)
        {
            if (Audio != null)
            {
                // Start playing the audio file in a new thread
                new Thread(() => PlayAudioFile(Audio)).Start();
            }
            
            clockTimerClass.ChangeColor();

            // Continue executing the code
            WorkerClass worker = new WorkerClass(clockTimerClass);
            worker.Application(clockTimerClass);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method provides options for the user to select a song to play. It prompts the 
        /// user to enter a number corresponding to a song from a list of available songs.
        /// It validates the user's input and ensures that only valid numbers are accepted. 
        /// If an invalid number is entered, it prompts the user to re-enter a valid number.
        /// Once a valid number is entered, the corresponding song is played using the "Play()" method.
        /// The method also sets the console color and size, and starts the timer using the "ClockTimerClass" 
        /// object.
        /// </summary>
        public void AudioOptions()
        {
            ConsoleAppSize();
            clockTimerClass.StartTimer();

            // Initialize variables
            string audio = string.Empty;
            string userInput;
            int selection;
            bool valid = false;

            clockTimerClass.ChangeBackColor(ConsoleColor.Cyan);
            clockTimerClass.ChangeForeColor(ConsoleColor.Black);
            Console.WriteLine("Please select a song to play by entering its corresponding number: ");
            clockTimerClass.ChangeBackColor(ConsoleColor.Black);
            clockTimerClass.ChangeForeColor(ConsoleColor.White);
            Console.Write("1.  Bon Jovi - It's My Life\r\n2.  Bryan Adams - Summer of 69\r\n" +
                "3.  Cat Stevens - Morning Has Broken\r\n4.  Demi Lee Moore - Coat Of Many Colors\r\n" +
                "5.  Dire Straits - Walk Of Life\r\n6.  Kenny Loggins - Footloose\r\n" +
                "7.  Nightbirde – It’s Okay\r\n8.  Bellamy Brothers - Old Hippie\r\n" +
                "9.  Bellamy Brothers - We Dared The Lightning\r\n10. Smokie - Living next door to Alice\r\n" +
                "11. Sia - Madilyn Paige - The Greatest\r\n12. Piano background song\r\n13. Nothing\r\n> ");
            userInput = Console.ReadLine();

            do
            {
                if ((Int32.TryParse(userInput, out selection) == true))
                {
                    selection = Convert.ToInt32(userInput);
                    if (selection > 0 && selection < 14)
                    {
                        valid = true;
                    }
                }
                else
                {
                    valid = false;
                }
                switch (userInput)
                {
                    case "1":
                        audio = "Bon Jovi_It is My Life.mp3";
                        break;
                    case "2":
                        audio = "Bryan Adams_Summer of 69.mp3";
                        break;
                    case "3":
                        audio = "Cat Stevens_Morning Has Broken.mp3";
                        break;
                    case "4":
                        audio = "Demi Lee Moore_Coat Of Many Colors.mp3";
                        break;
                    case "5":
                        audio = "Dire Straits_Walk Of Life.mp3";
                        break;
                    case "6":
                        audio = "Kenny Loggins_Footloose.mp3";
                        break;
                    case "7":
                        audio = "Nightbirde_It is Okay.mp3";
                        break;
                    case "8":
                        audio = "Old Hippie_Bellamy Brothers.mp3";
                        break;
                    case "9":
                        audio = "The Bellamy Brothers_We Dared The Lightning.mp3";
                        break;
                    case "10":
                        audio = "Smokie_Living next door to Alice.mp3";
                        break;
                    case "11":
                        audio = "The Greatest_Sia_Madilyn Paige.mp3";
                        break;
                    case "12":
                        audio = "Through The Years_Kenny Rogers_Piano Cover.mp3";
                        break;
                    case "13":
                        audio = null;
                        break;
                    default:
                        clockTimerClass.ChangeToErrorColor();
                        Console.WriteLine("\r\nPlease re-select a song to play by entering its corresponding " +
                            "number: ");
                        clockTimerClass.ChangeBackColor(ConsoleColor.Black);
                        clockTimerClass.ChangeForeColor(ConsoleColor.White);
                        Console.Write("1.  Bon Jovi - It's My Life\r\n2.  Bryan Adams - Summer of 69\r\n" +
                            "3.  Cat Stevens - Morning Has Broken\r\n4.  Demi Lee Moore - Coat Of Many Colors\r\n" +
                            "5.  Dire Straits - Walk Of Life\r\n6.  Kenny Loggins - Footloose\r\n" +
                            "7.  Nightbirde – It’s Okay\r\n8.  Bellamy Brothers - Old Hippie\r\n" +
                            "9.  Bellamy Brothers - We Dared The Lightning\r\n10. Smokie - Living next door to Alice\r\n" +
                            "11. Sia - Madilyn Paige - The Greatest\r\n12. Piano background song\r\n13. Nothing\r\n> ");
                        userInput = Console.ReadLine();
                        break;
                }
            } while (valid == false);
            Play(audio);
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Sets the console window size and buffer height for the application.
        /// </summary>
        private void ConsoleAppSize()
        {
            // Set the window width to 100 characters
            int windowWidth = 100;
            // Set the window height to 40 characters
            int windowHeight = 40;
            // Set the window buffer height to 9999 lines
            int bufferHeight = 9999;

            Console.SetWindowSize(windowWidth, windowHeight);
            Console.BufferHeight = bufferHeight;
        }

        //・♫-------------------------------------------------------------------------------------------------♫・//
        /// <summary>
        /// Method is used to play an audio file in the console application using the NAudio library. It takes 
        /// a file name as input and uses the Mp3FileReader class to read the audio file from the "Songs" directory.
        /// It then uses the WaveOutEvent class to initialize the audio output device and play the audio file.
        /// The method waits until the audio playback state is no longer playing before exiting.
        /// If the audio file is not found, it prints an error message and returns.
        /// </summary>
        /// <param name="fileName"></param>
        private void PlayAudioFile(string fileName)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            Directory.SetCurrentDirectory(projectDirectory);

            string audioFilePath = Path.Combine("Songs", fileName);

            string currentDirectory = Directory.GetCurrentDirectory();

            if (!File.Exists(audioFilePath))
            {
                Console.WriteLine($"Audio file not found: {audioFilePath}");
                return;
            }

            using (var audioFileReader = new Mp3FileReader(audioFilePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFileReader);
                outputDevice.Play();

                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//