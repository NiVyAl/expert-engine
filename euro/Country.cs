using System;
using System.Collections;
namespace euro
{
	/// <summary>
	///		inintialize for each country, include: completion days, coordinates of cities, country name  
	/// </summary>
	public class Country : IComparable
	{
		private int _xLength, _yLength, _cityCount;
		private Coordinates[] _coordinates;

		/// <summary>
		///		store coordinates of all cities in country
		/// </summary>
		public Coordinates[] Coordinates
		{
			get { return _coordinates; }
		}

		public string CountryName { get; set; }
		public int Days { get; set; }

		public Country(string countryName, int x1, int y1, int x2, int y2)
		{
			this.CountryName = countryName;

			_xLength = Math.Abs(x2 - x1) + 1;
			_yLength = Math.Abs(y2 - y1) + 1;
			_cityCount = _xLength * _yLength;
			_coordinates = new Coordinates[_cityCount];

			int coordinateIndex = 0;
			for (int i = 0; i < _xLength; i++)
			{
				for (int j = 0; j < _yLength; j++)
				{
					_coordinates[coordinateIndex] = new Coordinates();
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
