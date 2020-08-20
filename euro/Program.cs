using System;

namespace euro
{
    class Program
    {
		static void Main(string[] args)
		{
			ReadInputClass fileText = new ReadInputClass(@"input/input.in");

			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("");
			Console.WriteLine("start programm -----------------");
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
				}
			}
			for (int i = 0; i < Spain.AllCities.GetLength(0); i++)
			{
				for (int j = 0; j < Spain.AllCities.GetLength(1); j++)
				{
					int a = Spain.AllCities[i, j][0];
					int b = Spain.AllCities[i, j][1];
					AllCity[a, b] = new City("Spain", countries);
				}
			}
			for (int i = 0; i < Portugal.AllCities.GetLength(0); i++)
			{
				for (int j = 0; j < Portugal.AllCities.GetLength(1); j++)
				{
					int a = Portugal.AllCities[i, j][0];
					int b = Portugal.AllCities[i, j][1];
					AllCity[a, b] = new City("Portugal", countries);
				}
			}
			/*  */

			/* Проход дней */
			int days = 0;
			int FranceDays = 0;
			int SpainDays = 0;
			int PortugalDays = 0;
			while (days >=0)
			{
				if (PortugalDays != 0 && FranceDays != 0 && SpainDays != 0)
				{
					break;
				}
				days++;
				for (int i = 0; i < AllCity.GetLength(0); i++) // все показывают какие монеты отдают
				{
					for (int j = 0; j < AllCity.GetLength(1); j++)
					{
						if (AllCity[i, j] != null)
						{
							AllCity[i, j].giveCoins();
						}
					}
				}

				for (int i = 0; i < AllCity.GetLength(0); i++) // города получают монеты
				{
					for (int j = 0; j < AllCity.GetLength(1); j++)
					{
						if (AllCity[i, j] != null)
						{
							if (i != 0) // меньше нуля нет координат
							{
								if (AllCity[i - 1, j] != null)
								{
									AllCity[i, j].takeCoins(AllCity[i - 1, j].GivenCoins);
								}
							}

							if (j != 0) // меньше нуля нет координат
							{
								if (AllCity[i, j - 1] != null)
								{
									AllCity[i, j].takeCoins(AllCity[i, j - 1].GivenCoins);
								}
							}

							if (AllCity[i + 1, j] != null)
							{
								AllCity[i, j].takeCoins(AllCity[i + 1, j].GivenCoins);
							}

							if (AllCity[i, j + 1] != null)
							{
								AllCity[i, j].takeCoins(AllCity[i, j + 1].GivenCoins);
							}

						}
					}
				}


				/* проверка на завершение страны */
		
				if (FranceDays == 0)
				{
					int tempFrance = 0; // УДАЛИТЬ (не нужно)
					bool isCanCheckF = true;
					for (int i = 0; i < France.AllCities.GetLength(0) && isCanCheckF; i++)
					{
						for (int j = 0; j < France.AllCities.GetLength(1); j++)
						{
							int a = France.AllCities[i, j][0];
							int b = France.AllCities[i, j][1];
							if (AllCity[a, b].IsComplete == false) // если город не закончен
							{
								tempFrance = 0;
								isCanCheckF = false;
								break;
							}
							else
							{
								tempFrance = days;
							}
						}
					}
					FranceDays = tempFrance;
				}

				if (SpainDays == 0)
				{
					bool isCanCheckS = true;
					int tempSpain = 0; // УДАЛИТЬ (не нужно)
					for (int i = 0; i < Spain.AllCities.GetLength(0) && isCanCheckS; i++)
					{
						for (int j = 0; j < Spain.AllCities.GetLength(1); j++)
						{
							int a = Spain.AllCities[i, j][0];
							int b = Spain.AllCities[i, j][1];
							if (AllCity[a, b].IsComplete == false)
							{
								tempSpain = 0;
								isCanCheckS = false;
								break;
							}
							else
							{
								tempSpain = days;
							}
						}
					}
					SpainDays = tempSpain;
				}

				if (PortugalDays == 0)
				{
					bool isCanCheckP = true;
					int tempPortugal = 0; // УДАЛИТЬ (не нужно)
					for (int i = 0; i < Portugal.AllCities.GetLength(0) && isCanCheckP; i++)
					{
						for (int j = 0; j < Portugal.AllCities.GetLength(1); j++)
						{
							int a = Portugal.AllCities[i, j][0];
							int b = Portugal.AllCities[i, j][1];
							if (AllCity[a, b].IsComplete == false)
							{
								tempPortugal = 0;
								isCanCheckP = false;
								break;
							}
							else
							{
								tempPortugal = days;
							}
						}
					}
					PortugalDays = tempPortugal;
				}
				/* */

			}
			/* */


			/* Вывожу сколько в каком городе монет */
			/*
			for (int i = 0; i < AllCity.GetLength(0); i++)
			{
				for (int j = 0; j < AllCity.GetLength(1); j++)
				{
					if (AllCity[i, j] != null)
					{
						AllCity[i, j].giveCoins();
						Console.WriteLine($"{AllCity[i, j].Country} {i}, {j}");
						ConsoleWrite.Wr(AllCity[i, j].Coins);
						//Console.WriteLine(AllCity[i, j].IsComplete);
						//Console.WriteLine("");
						//ConsoleWrite.Wr(AllCity[i, j].GivenCoins);
						Console.WriteLine("");
					}
				}
			}

			Console.WriteLine($"Spain: {SpainDays}");
			Console.WriteLine($"Portugal: {PortugalDays}");
			Console.WriteLine($"France: {FranceDays}");
			*/
			/* */
		}
	}
}
