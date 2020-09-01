using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace euro
{
	public class EuroDiffusionClass
	{
		private InitializeCountryClass[] _countries; // координаты всех городов каждой страны
		private int _numberOfCountry; // количество стран
		private string[] _countriesNames; // список названий всех стран
		private int[] _countriesDays; // количество дней для заполнения каждой страны
		private int _XMaxCoordinate; 
		private int _YMaxCoordinate;
		private int _caseNumber = 1;

		public EuroDiffusionClass(string address)
	    {
			try
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
						bool isCanParse = int.TryParse(line, out _numberOfCountry);
						if (!isCanParse)
						{
							Console.WriteLine("Неверные данные");
							break;
						}
						if (_numberOfCountry == 0)
							break;

						_countries = new InitializeCountryClass[_numberOfCountry];
						_countriesNames = new string[_numberOfCountry];
						_countriesDays = new int[_numberOfCountry];
						_XMaxCoordinate = 0; // для определения размерности массива городов (AbstractCity[,] AllCity)
						_YMaxCoordinate = 0; //

						initializeCountries(sr); // определяются координаты городов каждой страны

						if (_numberOfCountry > 1) // расчет начинается, если количество стран > 1
							daysComput(); 
						writeOutput();
						_caseNumber++;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
            
        }



        private string returnCountry(string line)
		{
            string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return words[0];
        }


        private int[] returnCoordinates(string line)
        {
			int coordinatesCount = 4;
			string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int[] coordinates = new int[coordinatesCount];
			for (int i = 1; i <= coordinatesCount; i++)
			{
				int.TryParse(words[i], out coordinates[i-1]);
			}

            return coordinates;
        }


		private void writeOutput()
		{
			Console.WriteLine($"Case Number {_caseNumber}");

			CityDays[] cities = new CityDays[_numberOfCountry];
			for (int i = 0; i < _numberOfCountry; i++)
			{
				cities[i] = new CityDays { Name = _countriesNames[i], Days = _countriesDays[i] };
			}

			var q =
				from t in cities 
				orderby t 
				select t;

			var result = q.ToList();
			foreach (CityDays i in result)
			{
				Console.WriteLine($"	{i.Name}: {i.Days}");
			}
		}


		private void initializeCountries(StreamReader sr)
		{
			for (int i = 0; i < _numberOfCountry; i++)
			{
				string line = sr.ReadLine();
				string country = returnCountry(line);
				int[] coordinates = returnCoordinates(line);
				_countries[i] = new InitializeCountryClass(country, x1: coordinates[0], y1: coordinates[1], x2: coordinates[2], y2: coordinates[3]);
				_countriesNames[i] = country; //  названия стран

				if (coordinates[2] > _XMaxCoordinate)
					_XMaxCoordinate = coordinates[2];
				if (coordinates[3] > _YMaxCoordinate)
					_YMaxCoordinate = coordinates[3];

				_countriesDays[i] = 0; // количество дней для заполнения каждой страны первоначально = 0
			}
		}


		private void daysComput()
		{
			/* Инициализация каждого города */
			int xLength = _XMaxCoordinate + 1;
			int yLength = _YMaxCoordinate + 1;
			City[,] AllCity = new City[xLength, yLength]; // создается массив экземпляров класса City для каждого города, положение города в массиве = координатам города
			for (int k = 0; k < _numberOfCountry; k++)
			{
				for (int i = 0; i < _countries[k].AllCities.GetLength(0); i++)
				{
					for (int j = 0; j < _countries[k].AllCities.GetLength(1); j++)
					{
						int a = _countries[k].AllCities[i, j][0];
						int b = _countries[k].AllCities[i, j][1];
						AllCity[a, b] = new City(countryIndex: k, _numberOfCountry);
					}
				}
			}
			/* */

			/* цикл по дням */
			int days = 0;

			while (days >= 0)
			{
				days++;

				for (int i = 1; i < xLength; i++) // высчитывается порция монет для выдачи городом
				{
					for (int j = 1; j < yLength; j++)
					{
						if (AllCity[i, j] != null)
							AllCity[i, j].giveCoins();
					}
				}

				for (int i = 1; i < xLength; i++) // города получают монеты
				{
					for (int j = 1; j < yLength; j++)
					{
						if (AllCity[i, j] != null)
						{
							if (i - 1 >= 1) // проверка наличия соседнего города слева
							{
								if (AllCity[i - 1, j] != null)
									AllCity[i, j].takeCoins(AllCity[i - 1, j].GivenCoins);
							}

							if (j - 1 >= 1) // проверка наличия соседнего города снизу
							{
								if (AllCity[i, j - 1] != null)
									AllCity[i, j].takeCoins(AllCity[i, j - 1].GivenCoins);
							}

							if (AllCity.GetLength(0) > i + 1) // проверка наличия соседнего города справа
							{
								if (AllCity[i + 1, j] != null)
									AllCity[i, j].takeCoins(AllCity[i + 1, j].GivenCoins);
							}

							if (AllCity.GetLength(1) > j + 1) // проверка наличия соседнего города сверху
							{
								if (AllCity[i, j + 1] != null)
									AllCity[i, j].takeCoins(AllCity[i, j + 1].GivenCoins);
							}
						}
					}
				}

				bool isComplete = false;
				/* проверка на завершение каждой страны */
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
								if (AllCity[a, b].IsComplete == false) // если город не закончен
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
