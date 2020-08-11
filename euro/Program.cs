using System;

namespace euro
{
    class Program
    {
        static void Main(string[] args)
        {
			string[] countries = new string[3] { "France", "Spain", "Portugal" };
            InitializeCountryClass France = new InitializeCountryClass("France", 1, 4, 4, 6);
            InitializeCountryClass Spain = new InitializeCountryClass("Spain", 3, 1, 6, 3);
            InitializeCountryClass Portugal = new InitializeCountryClass("Portugal", 1, 1, 2, 2);

			/* Заполнение Городами */
			AbstractCity[,] AllCity = new AbstractCity[100, 100];
			for (int i = 0; i < France.AllCities.GetLength(0); i++)
			{
				for (int j = 0; j < France.AllCities.GetLength(1); j++)
				{
					int a = France.AllCities[i, j][0];
					int b = France.AllCities[i, j][1];
					AllCity[a, b] = new City("France", countries);

					//AllCity[a, b].giveCoins();
					//Console.WriteLine($"a:{a}, b:{b},  {AllCity[a, b].GivenCons}");
					//Console.WriteLine(AllCity[i, j] == null);
				}
			}
			for (int i = 0; i < Spain.AllCities.GetLength(0); i++)
			{
				for (int j = 0; j < Spain.AllCities.GetLength(1); j++)
				{
					int a = Spain.AllCities[i, j][0];
					int b = Spain.AllCities[i, j][1];
					AllCity[a, b] = new City("Spain", countries);

					//AllCity[a, b].giveCoins();
					//Console.WriteLine($"a:{a}, b:{b},  {AllCity[a, b].GivenCons}");
					//Console.WriteLine(AllCity[i, j] == null);
				}
			}
			for (int i = 0; i < Portugal.AllCities.GetLength(0); i++)
			{
				for (int j = 0; j < Portugal.AllCities.GetLength(1); j++)
				{
					int a = Portugal.AllCities[i, j][0];
					int b = Portugal.AllCities[i, j][1];
					AllCity[a, b] = new City("Portugal", countries);

					//AllCity[a, b].giveCoins();
					//Console.WriteLine($"a:{a}, b:{b},  {AllCity[a, b].GivenCons}");
					//Console.WriteLine(AllCity[i, j] == null);
				}
			}
			/*  */

			/* вывожу сколько в каком городе монет*/
			for (int i = 0; i < AllCity.GetLength(0); i++)
			{
				for (int j = 0; j < AllCity.GetLength(1); j++)
				{
					if (AllCity[i, j] != null)
					{
						AllCity[i, j].giveCoins();
						Console.WriteLine(AllCity[i, j].Country);
						ConsoleWrite.Wr(AllCity[i, j].GivenCoins);
					}
				}
			}
			/* */



		}
    }
}
