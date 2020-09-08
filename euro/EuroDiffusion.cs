using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace euro
{
	public class EuroDiffusion
	{
		private int _numberOfCountry; // number of countries in each casey
		private Country[] _countries;
		private int _XMaxCoordinate; 
		private int _YMaxCoordinate;
		private int _caseNumber = 1;
		private City[,] _allCity;

		public EuroDiffusion(string address)
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
							Console.WriteLine("Can't parse number of country");
							break;
						}
						if (_numberOfCountry == 0)
							break;

						_countries = new Country[_numberOfCountry];
						_XMaxCoordinate = 0; // to determine the dimension of the array of cities (AbstractCity[,] _allCity)
						_YMaxCoordinate = 0; //

						initializeCountries(sr); // comput coordinates of cities of each country

						if (_numberOfCountry > 1) // calculation starts if the number of countries is> 1 (if only 1 country, it complete in 0 days)
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


		/// <summary>
		///		return country name from file
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
        private string returnCountry(string line)
		{
            string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return words[0];
        }

		/// <summary>
		///		return coordinates of country from file
		/// </summary>
		/// <param name="line"></param>
		/// <returns></returns>
        private int[] returnCoordinates(string line)
        {
			int coordinatesCount = 4;
			string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int[] coordinates = new int[coordinatesCount];
			for (int i = 1; i <= coordinatesCount; i++)
			{
				if (int.TryParse(words[i], out coordinates[i - 1]) == false)
				{
					throw new Exception("Can't parse coordinates");
				}
			}

            return coordinates;
        }

		/// <summary>
		///		write output in console
		/// </summary>
		private void writeOutput()
		{
			Console.WriteLine($"Case Number {_caseNumber}");

			var q =
				from t in _countries 
				orderby t 
				select t;

			var result = q.ToList();
			foreach (Country i in result)
			{
				Console.WriteLine($"	{i.CountryName}: {i.Days}");
			}
		}

		/// <summary>
		///		initialize each countries
		/// </summary>
		/// <param name="sr"></param>
		private void initializeCountries(StreamReader sr)
		{
			for (int i = 0; i < _numberOfCountry; i++)
			{
				string line = sr.ReadLine();
				string country = returnCountry(line);
				int[] coordinates = returnCoordinates(line);
				_countries[i] = new Country(country, x1: coordinates[0], y1: coordinates[1], x2: coordinates[2], y2: coordinates[3]);

				if (coordinates[2] > _XMaxCoordinate)
					_XMaxCoordinate = coordinates[2];
				if (coordinates[3] > _YMaxCoordinate)
					_YMaxCoordinate = coordinates[3];
			}
		}

		/// <summary>
		///		Comput days for complete each country
		/// </summary>
		private void daysComput()
		{
			/* Initialization of each city */
			int xLength = _XMaxCoordinate + 1;
			int yLength = _YMaxCoordinate + 1;
			_allCity = new City[xLength, yLength]; // an array of instances of the City class is created for each city, the position of the city in the array = the coordinates of the city
			for (int k = 0; k < _numberOfCountry; k++)
			{
				for (int i = 0; i < _countries[k].XCoordinates.GetLength(0); i++)
				{
					int x = _countries[k].XCoordinates[i];
					int y = _countries[k].YCoordinates[i];
					_allCity[x, y] = new City(countryIndex: k, _numberOfCountry);
				}
			}
			/* */

			/* cycle of passing days */
			int days = 0;
			int numberUncompleteCountries = _numberOfCountry;
			while (numberUncompleteCountries > 0)
			{
				days++;
				for (int i = 1; i < xLength; i++) // comput a portion of coins for transported to each neighbor of the city
				{
					for (int j = 1; j < yLength; j++)
					{
						if (_allCity[i, j] != null)
							_allCity[i, j].giveCoins();
					}
				}

				for (int i = 1; i < xLength; i++) // cities get coins
				{
					for (int j = 1; j < yLength; j++)
					{
						if (_allCity[i, j] != null)
						{
							if (i - 1 >= 1) // checking for the have a neighboring city to the left
							{
								takeCoins(i, j, -1, 0);
							}

							if (j - 1 >= 1) // checking for the have a neighboring city to the bottom
							{
								takeCoins(i, j, 0, -1);
							}

							if (i + 1 < _allCity.GetLength(0)) // checking for the have a neighboring city to the right
							{
								takeCoins(i, j, +1, 0);
							}

							if (j + 1 < _allCity.GetLength(1)) // checking for the have a neighboring city to the top
							{
								takeCoins(i, j, 0, +1);
							}
						}
					}
				}

				/* check for completion of each country */
				for (int k = 0; k < _numberOfCountry; k++)
				{
					if (_countries[k].Days == 0)
					{
						for (int i = 0; i < _countries[k].XCoordinates.GetLength(0); i++)
						{
							int x = _countries[k].XCoordinates[i];
							int y = _countries[k].YCoordinates[i];
							if (_allCity[x, y].IsComplete == false) // if city isn't complete
							{
								_countries[k].Days = 0;
								break;
							}
							else
								_countries[k].Days = days;
						}
						if (_countries[k].Days > 0)
							numberUncompleteCountries--;
					}
				}
				/* */
			}
		}

		private void takeCoins(int x, int y, int neighborIndexX, int neighborIndexY)
		{
			if (_allCity[x + neighborIndexX, y + neighborIndexY] != null)
				_allCity[x, y].takeCoins(_allCity[x + neighborIndexX, y + neighborIndexY].GivenCoins);
		}
	}
}
