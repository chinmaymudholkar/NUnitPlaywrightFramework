namespace NUnitPlaywrightFramework.Libs
{
    internal static class Wrappers
    {
        //Create a method to get the current date and time
        public static string GetCurrentDateTime(string format= "yyyyMMddHHmmss")
        {
            return DateTime.Now.ToString(format);
        }

        public static string CleanString(string input)
        {
            char[] charsToRemove =
                        [
                ' ', ':', '/', '\\', '.', ',', ';', '(', ')', '[', ']', '{', '}', '!', '@', '#', '$', '%', '^', '&', '*', '+', '=', '?', '>', '<', '|', '~', '`', '\'', '\"'
                        ];

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
