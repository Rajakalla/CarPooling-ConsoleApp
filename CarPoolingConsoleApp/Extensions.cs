using System;
using System.Globalization;

namespace CarPoolingConsoleApp
{
    internal static class Extensions
    {
        internal static String GetUserInputAsString()
        {
            String input = Console.ReadLine();
            while (input.Trim() == "")
            {
                Console.WriteLine("****Enter Valid input***");
                input = Console.ReadLine();
            }
            return input;
        }

        internal static int GetUserInputAsInt()
        {
            String input = Console.ReadLine();
            int value;
            while (!Int32.TryParse(input, out value))
            {
                Console.WriteLine("****Enter only numbers***");
                input = Console.ReadLine();
            }
            return value;
        }

        internal static DateTime GetUserInputAsDateTime()
        {

            String dateFormat = "dd/MM/yyyy HH:mm";
            DateTime dateTime;
            try
            {
                Console.WriteLine($"Format of date and time({dateFormat})");
                dateTime = DateTime.ParseExact(Console.ReadLine(), dateFormat, CultureInfo.InvariantCulture);
                return dateTime;
            }
            catch (FormatException)
            {
                Console.WriteLine("***Given value is not in the correct format.***");
                return GetUserInputAsDateTime();   
            }
        }
    }
}
