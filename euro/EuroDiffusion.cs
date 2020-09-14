using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace euro
{
	public class EuroDiffusion
	{
		private int _numberOfCountry; // number of countries in each case
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
		/// <returns>country name</returns>
		private string returnCountry(string line)
		{
            string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return words[0];
        }

		/// <summary>
		///		return coordinates of country from file
		/// </summary>
		/// <param name="line"></param>
		/// <returns>int[] coordinates</returns>
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
				_countries[i] = new Country(country, x1: coordinates[0], y1: coordinates[1], x2: coordinates[2], y2: coordinates[3], _numberOfCountry);

				/* find max x and y coordinates (for initialization array of city) */
				if (coordinates[2] > _XMaxCoordinate)
					_XMaxCoordinate = coordinates[2];
				if (coordinates[3] > _YMaxCoordinate)
					_YMaxCoordinate = coordinates[3];
				/* */
			}
			checkCorrectData();
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
				for (int i = 0; i < _countries[k].Coordinates.GetLength(0); i++)
				{
					int x = _countries[k].Coordinates[i].X;
					int y = _countries[k].Coordinates[i].Y;
					if (_allCity[x, y] != null)
						throw new Exception($"Wrong coordinates (country stay at another country), case number: {_caseNumber}, country name: {_countries[k].CountryName}");
					_allCity[x, y] = new City(countryIndex: k, _numberOfCountry);
				}
			}
			/* */

			/* cycle of passing days */
			int days = 1;
			int numberUncompleteCountries = _numberOfCountry;
			while (numberUncompleteCountries > 0)
			{
				/* comput a portion of coins for transported to each neighbor of the city */
				for (int i = 1; i < xLength; i++)
				{
					for (int j = 1; j < yLength; j++)
					{
						if (_allCity[i, j] != null)
							_allCity[i, j].giveCoins();
					}
				}

				/* cities get coins */
				for (int i = 1; i < xLength; i++)
				{
					for (int j = 1; j < yLength; j++)
					{
						if (_allCity[i, j] != null)
						{
							int[,] neighbors = new int[4, 2]
							{
								{ i - 1, j }, // left neighbor
								{ i + 1, j }, // right neighbor
								{ i, j + 1 }, // top neighbor
								{ i, j - 1 }  // bottom neighbor
							};
							for (int l = 0; l < 4; l++) // city get coins from neighbors
							{
								takeCoins(i, j, neighbors[l, 0], neighbors[l, 1]);
							}
						}
					}
				}

				if (days == 1) // at the first day, check data correct
				{
					checkCorrectData();
				}
				/* check for completion of each country */
				for (int k = 0; k < _numberOfCountry; k++)
				{
					if (_countries[k].Days == 0)
					{
						for (int i = 0; i < _countries[k].Coordinates.GetLength(0); i++)
						{
							int x = _countries[k].Coordinates[i].X;
							int y = _countries[k].Coordinates[i].Y;
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
				days++;
				/* */
			}
		}

		/// <summary>
		///		city get coins from neighbor
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="neighborIndexX"></param>
		/// <param name="neighborIndexY"></param>
		private void takeCoins(int x, int y, int neighborIndexX, int neighborIndexY)
		{
			if ((neighborIndexX >= 1 && neighborIndexY >= 1 && neighborIndexX < _allCity.GetLength(0) && neighborIndexY < _allCity.GetLength(1)) == false) // checking the correctness of coordinates 
				return;
			if (_allCity[neighborIndexX, neighborIndexY] != null) // check availability of neighbor
				_allCity[x, y].takeCoins(_allCity[neighborIndexX, neighborIndexY].GivenCoins);
		}

		/// <summary>
		///		check is countries border each other
		/// </summary>
		private void checkCorrectData()
		{
			/* define the neighbors of each country */
			for (int i = 0; i < _numberOfCountry; i++)
			{
				for (int j = 0; j < _numberOfCountry; j++)
				{
					if (_countries[i].Neighbors[j] == true || i == j)
						continue;

					for (int m = 0; m < 2; m++)
					{
						if (_countries[i].GivenCoordinates[m] - 1 == _countries[j].GivenCoordinates[m+2] || _countries[i].GivenCoordinates[m + 2] + 1 == _countries[j].GivenCoordinates[m]) // check right/left neighbors
						{
							int n = m;
							if (m == 1)
								n = m - 2;
							if ((_countries[j].GivenCoordinates[n+1] >= _countries[i].GivenCoordinates[n + 1] && _countries[j].GivenCoordinates[n + 1] <= _countries[i].GivenCoordinates[n + 3]) || (_countries[j].GivenCoordinates[n + 3] >= _countries[i].GivenCoordinates[n + 1] && _countries[j].GivenCoordinates[n + 3] <= _countries[i].GivenCoordinates[n + 3])) // check top/bottom neighbors
							{
								_countries[i].Neighbors[j] = true;
								_countries[j].Neighbors[i] = true;
								break;
							}
						}
					}
				}
			}
			/* */

			/* define if all countries are connected to each other */
			bool[] correctCountries = new bool[_numberOfCountry];
			bool[] queueCheck = new bool[_numberOfCountry];
			queueCheck[0] = true;

			bool isCanRun = true;
			while (isCanRun)
			{
				isCanRun = false;
				for (int k = 0; k < _numberOfCountry; k++)
				{
					if (queueCheck[k] == true && correctCountries[k] == false)
					{
						isCanRun = true;
						queueCheck[k] = false;
						correctCountries[k] = true;

						for (int j = 0; j < _numberOfCountry; j++)
						{
							if (_countries[k].Neighbors[j])
							{
								queueCheck[j] = true;
							}
						}
					}
				}
			}

			for (int k = 0; k < _numberOfCountry; k++)
			{
				if (correctCountries[k] == false)
					throw new Exception($"Wrong coordinates, cities don't border (caseNumber: {_caseNumber})");
			}
			/* */
		}
	}
}
