using System;
using System.Collections;
namespace euro
{
	/// <summary>
	///		inintialize for each country, include: completion days, coordinates of cities, country name  
	/// </summary>
	public class Country : IComparable
	{
		public int X1 { get; private set; }
		public int Y1 { get; private set; }
		public int X2 { get; private set; }
		public int Y2 { get; private set; }

		private int _xLength, _yLength, _cityCount;
		private Coordinate[] _coordinates;

		/// <summary>
		///		store coordinates of all cities in country
		/// </summary>
		public Coordinate[] Coordinates
		{
			get { return _coordinates; }
		}

		public bool[] Neighbors;
		//public bool[] Neighbors
		//{
		//	get { return _neighbors}
		//	set { value = _neighbors}
		//}

		public string CountryName { get; set; }
		public int Days { get; set; }

		public Country(string countryName, int x1, int y1, int x2, int y2, int numberOfCountry)
		{
			this.CountryName = countryName;
			X1 = x1;
			Y1 = y1;
			X2 = x2;
			Y2 = y2;
			_xLength = Math.Abs(x2 - x1) + 1;
			_yLength = Math.Abs(y2 - y1) + 1;
			_cityCount = _xLength * _yLength;
			_coordinates = new Coordinate[_cityCount];
			Neighbors = new bool[numberOfCountry];

			int coordinateIndex = 0;
			for (int i = 0; i < _xLength; i++)
			{
				for (int j = 0; j < _yLength; j++)
				{
					_coordinates[coordinateIndex] = new Coordinate();
					_coordinates[coordinateIndex].X = x1 + i;
					_coordinates[coordinateIndex].Y = y1 + j;
					coordinateIndex++;
				}
			}

		}

		public int CompareTo(object obj)
		{
			int res;
			if (obj == null) return 1;

			Country otherCityDays = obj as Country;
			if (otherCityDays == null)
				throw new ArgumentException("Object is not a Temperature");

			res = this.Days.CompareTo(otherCityDays.Days);  // sort by number of days

			if (res == 0) // or if number is same
				res = this.CountryName.CompareTo(otherCityDays.CountryName); // sort alphabetically
			return res;

		}
	}
}
