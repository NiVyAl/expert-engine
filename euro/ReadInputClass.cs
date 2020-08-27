using System;
using System.IO;
using System.Threading.Tasks;
namespace euro
{
	public class ReadInputClass
	{
        public ReadInputClass(string address)
	    {
            using (StreamReader sr = new StreamReader(address, System.Text.Encoding.Default))
            {
				Console.WriteLine("");
				Console.WriteLine("");
				Console.WriteLine("");
				Console.WriteLine("start programm -----------------");
				string line;
                int caseNumber = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    int numberOfCountry;
                    int.TryParse(line, out numberOfCountry);
                    if (numberOfCountry == 0)
					{
                        break;
					}


					AbsractInitializeCountryClass[] Countries = new AbsractInitializeCountryClass[numberOfCountry]; // хранятся координаты всех городов каждой страны
					string[] countriesNames = new string[numberOfCountry]; // список всех стран
					int[] countriesDays = new int[numberOfCountry]; // массив сколько дней заполнялась каждая страна
					int XMaxCoordinate = 0; // для определения размерности массива городов (AbstractCity[,] AllCity)
					int YMaxCoordinate = 0; // 

					for (int i = 0; i < numberOfCountry; i++)
					{
                        line = sr.ReadLine();
                        string country = returnCountry(line);
                        int[] coordinates = returnCoordinates(line);
						Countries[i] = new InitializeCountryClass(country, x1: coordinates[0], y1: coordinates[1], x2: coordinates[2], y2: coordinates[3]);
						countriesNames[i] = country; // массив из названия стран

						if (coordinates[2] > XMaxCoordinate)
						{
							XMaxCoordinate = coordinates[2];
						}
						if (coordinates[3] > YMaxCoordinate)
						{
							YMaxCoordinate = coordinates[3];
						}
					}

					if (numberOfCountry == 1) // если всего одна страна, то она заполняется за 0 дней
					{
						countriesDays[0] = 0;
					} else
					{
						/* Заполнение Городами */
						AbstractCity[,] AllCity = new AbstractCity[XMaxCoordinate+1, YMaxCoordinate+1]; // массив с городами (сколько монет в каждом городе), положение города в массиве = координатам города
						for (int k = 0; k < numberOfCountry; k++)
						{
							for (int i = 0; i < Countries[k].AllCities.GetLength(0); i++)
							{
								for (int j = 0; j < Countries[k].AllCities.GetLength(1); j++)
								{
									int a = Countries[k].AllCities[i, j][0];
									int b = Countries[k].AllCities[i, j][1];
									AllCity[a, b] = new City(Countries[k].CountryName, countriesNames);
								}
							}
						}
						/* */

						/* Проход дней */
						int days = 0;
						for (int i = 0; i < numberOfCountry; i++)
						{
							countriesDays[i] = 0; //  заполняем нулями
						}

						while (days >= 0) // бесконечный цикл
						{
							/* проверка на завершение всех стран */
							bool isComplete = false;
							for (int i = 0; i < numberOfCountry; i++)
							{
								if (countriesDays[i] == 0)
								{
									break;
								}
								else
								{
									isComplete = true;
								}
							}
							if (isComplete == true)
							{
								break;
							}
							/* */

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

										if (AllCity.GetLength(0) > i+1)
										{
											if (AllCity[i + 1, j] != null)
											{
												AllCity[i, j].takeCoins(AllCity[i + 1, j].GivenCoins);
											}
										}

										if (AllCity.GetLength(1) > j + 1)
										{
											if (AllCity[i, j + 1] != null)
											{
												AllCity[i, j].takeCoins(AllCity[i, j + 1].GivenCoins);
											}
										}
									}
								}
							}

							/* проверка на завершение страны */
							for (int k = 0; k < numberOfCountry; k++)
							{
								if (countriesDays[k] == 0)
								{
									int tempFrance = 0; // УДАЛИТЬ (не нужно)
									bool isCanCheckF = true;
									for (int i = 0; i < Countries[k].AllCities.GetLength(0) && isCanCheckF; i++)
									{
										for (int j = 0; j < Countries[k].AllCities.GetLength(1); j++)
										{
											int a = Countries[k].AllCities[i, j][0];
											int b = Countries[k].AllCities[i, j][1];
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
									countriesDays[k] = tempFrance;
								}
							}
							/* */
						}

					}
					/* */
					
					/* Вывожу сколько в каком городе монет */
					Console.WriteLine($"Case Number {caseNumber}");
					for (int i = 0; i < numberOfCountry; i++)
					{
						Console.WriteLine($"	{countriesNames[i]}: {countriesDays[i]}");
					}
					/* */

					caseNumber++;
                }
            }
        }



        private string returnCountry(string line)
		{
            string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return words[0];
        }

        private int[] returnCoordinates(string line)
        {
            string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int[] coordinates = new int[4];
			int.TryParse(words[1], out coordinates[0]);
			int.TryParse(words[2], out coordinates[1]);
			int.TryParse(words[3], out coordinates[2]);
			int.TryParse(words[4], out coordinates[3]);

            return coordinates;

        }
    }
}
