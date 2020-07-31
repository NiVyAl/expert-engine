using System;

namespace euro
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\SomeDir\hta.txt";
            string[] countries = new string[3] { "France", "Spain", "Portugal" };
            City Paris = new City("Paris", countries);
            //Paris.

			InitializeCountryClass Netherlands = new InitializeCountryClass("Netherlands", 1, 3, 2, 4);
            Netherlands.allCities();
        }
    }
}
