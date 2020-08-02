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

			InitializeCountryClass Netherlands = new InitializeCountryClass("Netherlands", 1, 1, 2, 2);
			InitializeCountryClass France = new InitializeCountryClass("France", 3, 1, 6, 3);
			InitializeCountryClass Germany = new InitializeCountryClass("Germany", 1, 4, 4, 6);
            Netherlands.allCities();
            France.allCities();
            Germany.allCities();

            for (int i = 0; i < Netherlands.allCities().GetLength(0); i++)
            {
                for (int j = 0; j < Netherlands.allCities().GetLength(1); j++)
                {
                    
                }
            }
        }
    }
}
