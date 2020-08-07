using System;

namespace euro
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = @"C:\SomeDir\hta.txt";
            //City Paris = new City("Paris", countries);
            //Paris.

            string[] countries = new string[3] { "Netherlands", "France", "Germany" };
            InitializeCountryClass Netherlands = new InitializeCountryClass("Netherlands", 1, 1, 2, 2);
			InitializeCountryClass France = new InitializeCountryClass("France", 3, 1, 6, 3);
			InitializeCountryClass Germany = new InitializeCountryClass("Germany", 1, 4, 4, 6);
            Netherlands.allCities();
            France.allCities();
            Germany.allCities();

            AbstractCity[,] NCity = new AbstractCity[Netherlands.allCities().GetLength(0), Netherlands.allCities().GetLength(1)];
            for (int i = 0; i < Netherlands.allCities().GetLength(0); i++)
            {
                for (int j = 0; j < Netherlands.allCities().GetLength(1); j++)
                {
					NCity[i, j] = new City("Netherlands", countries);
				}
            }

            for (int i = 0; i < Netherlands.allCities().GetLength(0); i++)
            {
                for (int j = 0; j < Netherlands.allCities().GetLength(1); j++)
                {
                    NCity[i, j].giveCoins();
                    Console.WriteLine(NCity[i, j].GivenCons);
                }
            }
        }
    }
}
