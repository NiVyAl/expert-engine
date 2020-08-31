using System;
using System.IO;
using System.Threading.Tasks;
namespace euro
{
	public class EuroDiffusionClass
	{
		private InitializeCountryClass[] _countries; // хранятся координаты всех городов каждой страны
		private int _numberOfCountry; // количество стран
		private string[] _countriesNames; // список названий всех стран
		private int[] _countriesDays; // массив сколько дней заполнялась каждая страна 
		private int _XMaxCoordinate; 
		private int _YMaxCoordinate;
		private int _caseNumber = 1;

		public EuroDiffusionClass(string address)
	    {
            using (StreamReader sr = new StreamReader(address, System.Text.Encoding.Default))
            {
				Console.WriteLine("");
				Console.WriteLine("");
				Console.WriteLine("");
				Console.WriteLine("start programm -----------------");
				string line;
                while ((line = sr.ReadLine()) != null)
                {
					_numberOfCountry = 0;
					int.TryParse(line, out _numberOfCountry);
                    if (_numberOfCountry == 0)
                        break;

					_countries = new InitializeCountryClass[_numberOfCountry]; 
					_countriesNames = new string[_numberOfCountry]; 
					_countriesDays = new int[_numberOfCountry]; 
					_XMaxCoordinate = 0; // для определения размерности массива городов (AbstractCity[,] AllCity)
					_YMaxCoordinate = 0; // 

					initializeCountries(line, sr); // Заполняю страны из файла
					if (_numberOfCountry > 1) // если всего одна страна, то она заполняется за 0 дней
						daysComput();
					writeOutput();
					_caseNumber++;
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


		private void writeOutput()
		{
			Console.WriteLine($"Case Number {_caseNumber}");
			for (int i = 0; i < _numberOfCountry; i++)
			{
				Console.WriteLine($"	{_countriesNames[i]}: {_countriesDays[i]}");
			}
		}


		private void initializeCountries(string line, StreamReader sr)
		{
			for (int i = 0; i < _numberOfCountry; i++)
			{
				line = sr.ReadLine();
				string country = returnCountry(line);
				int[] coordinates = returnCoordinates(line);
				_countries[i] = new InitializeCountryClass(country, x1: coordinates[0], y1: coordinates[1], x2: coordinates[2], y2: coordinates[3]);
				_countriesNames[i] = country; // массив из названия стран

				if (coordinates[2] > _XMaxCoordinate)
					_XMaxCoordinate = coordinates[2];
				if (coordinates[3] > _YMaxCoordinate)
					_YMaxCoordinate = coordinates[3];

				_countriesDays[i] = 0; // в начале дни за сколько завершилась каждая страна = 0
			}
		}


		private void daysComput()
		{
			/* Заполнение Городами */
			City[,] AllCity = new City[_XMaxCoordinate + 1, _YMaxCoordinate + 1]; // массив с городами (сколько монет в каждом городе), положение города в массиве = координатам города
			for (int k = 0; k < _numberOfCountry; k++)
			{
				for (int i = 0; i < _countries[k].AllCities.GetLength(0); i++)
				{
					for (int j = 0; j < _countries[k].AllCities.GetLength(1); j++)
					{
						int a = _countries[k].AllCities[i, j][0];
						int b = _countries[k].AllCities[i, j][1];
						AllCity[a, b] = new City(_countries[k].CountryName, _countriesNames);
					}
				}
			}
			/* */

			/* Проход дней */
			int days = 0;

			while (days >= 0) // бесконечный цикл
			{
				days++;

				for (int i = 1; i < AllCity.GetLength(0); i++) // все показывают какие монеты отдают
				{
					for (int j = 1; j < AllCity.GetLength(1); j++)
					{
						if (AllCity[i, j] != null)
							AllCity[i, j].giveCoins();
					}
				}

				for (int i = 1; i < AllCity.GetLength(0); i++) // города получают монеты
				{
					for (int j = 1; j < AllCity.GetLength(1); j++)
					{
						if (AllCity[i, j] != null)
						{
							if (i - 1 >= 1) // меньше единицы нет координат
							{
								if (AllCity[i - 1, j] != null)
									AllCity[i, j].takeCoins(AllCity[i - 1, j].GivenCoins);
							}

							if (j - 1 >= 1) // меньше единицы нет координат
							{
								if (AllCity[i, j - 1] != null)
									AllCity[i, j].takeCoins(AllCity[i, j - 1].GivenCoins);
							}

							if (AllCity.GetLength(0) > i + 1) // чтобы не вылезти из размерности массива
							{
								if (AllCity[i + 1, j] != null)
									AllCity[i, j].takeCoins(AllCity[i + 1, j].GivenCoins);
							}

							if (AllCity.GetLength(1) > j + 1) // чтобы не вылезти из размерности массива
							{
								if (AllCity[i, j + 1] != null)
									AllCity[i, j].takeCoins(AllCity[i, j + 1].GivenCoins);
							}
						}
					}
				}

				bool isComplete = false;
				/* проверка на завершение страны */
				for (int k = 0; k < _numberOfCountry; k++)
				{
					if (_countriesDays[k] == 0)
					{
						bool isCanCheck = true;
						for (int i = 0; i < _countries[k].AllCities.GetLength(0) && isCanCheck; i++)
						{
							for (int j = 0; j < _countries[k].AllCities.GetLength(1); j++)
							{
								int a = _countries[k].AllCities[i, j][0];
								int b = _countries[k].AllCities[i, j][1];
								if (AllCity[a, b].isComplete() == false) // если город не закончен
								{
									_countriesDays[k] = 0;
									isCanCheck = false;
									break;
								}
								else
								{
									_countriesDays[k] = days;
								}
							}
						}
					}
				}
				/* */

				/* проверка на завершение всех стран */
				for (int i = 0; i < _numberOfCountry; i++)
				{
					if (_countriesDays[i] == 0)
					{
						isComplete = false;
						break;
					}
					else
						isComplete = true;
				}
				if (isComplete == true)
					break;
				/* */
			}
		}
	}
}
