using System;
using System.Collections;
namespace euro
{
	/// <summary>
	///		inintialize for each country, include: completion days, coordinates of cities, country name  
	/// </summary>
	public class Country : IComparable
	{
		private int x1, x2, y1, y2;
		private int xLength, yLength, cityCount;

		private Coordinate[] coordinates;
		public Coordinate[] Coordinates
		{
			get { return coordinates; }
		}

		public string CountryName { get; set; }
		public int Days { get; set; }

		public Country(string countryName, int x1, int y1, int x2, int y2)
		{
			this.CountryName = countryName;
			this.x1 = x1;
			this.x2 = x2;
			this.y1 = y1;
			this.y2 = y2;

			xLength = Math.Abs(x2 - x1) + 1;
			yLength = Math.Abs(y2 - y1) + 1;
			cityCount = xLength * yLength;
			coordinates = new Coordinate[cityCount];

			int coordinateIndex = 0;
			for (int i = 0; i < xLength; i++)
			{
				for (int j = 0; j < yLength; j++)
				{
					coordinates[coordinateIndex] = new Coordinate();
					coordinates[coordinateIndex].X = x1 + i;
					coordinates[coordinateIndex].Y = y1 + j;
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
