using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitPlaywrightFramework.Libs
{
    internal class Wrappers
    {
        //Create a method to get the current date and time
        public string GetCurrentDateTime()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        public string CleanString(string input)
        {
            char[] charsToRemove = new char[]
                        {
                ' ', ':', '/', '\\', '.', ',', ';', '(', ')', '[', ']', '{', '}', '!', '@', '#', '$', '%', '^', '&', '*', '+', '=', '?', '>', '<', '|', '~', '`', '\'', '\"', '‘', '’', '“', '”'
                        };

            // Loop through the input string and remove all these characters
            foreach (char c in charsToRemove)
            {
                input = input.Replace(c.ToString(), string.Empty);
            }

            // Return the cleaned string
            return input;
        }
    }
}
