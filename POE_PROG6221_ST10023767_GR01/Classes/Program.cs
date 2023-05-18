#region
// Mikayle Coetzee
// ST10023767
// PROG6221 POE 2023
// Part 1 and 2 
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Media;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using POE_PROG6221_ST10023767_GR01.Classes;

namespace POE_PROG6221_ST10023767_GR01
{
    internal class Program
    {
        /// <summary>
        /// Entry point of the console application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            MusicClass musicClass = new MusicClass();
            musicClass.AudioOptions();
        }
    }
}//★---♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫---★・。。END OF FILE 。。・★---♫ ♬:;;;:♬ ♫:;;;: ♫ ♬:;;;:♬ ♫:;;;: ♫---★//