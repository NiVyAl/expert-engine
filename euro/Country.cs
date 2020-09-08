using System;
using System.Collections;
namespace euro
{
	public class Country : IComparable
	{
		private int x1, x2, y1, y2;
		private int xLength, yLength, cityCount;

		private int[] xCoordinates;
		private int[] yCoordinates;

		public int[] XCoordinates
		{
			get { return xCoordinates; }
		}
		public int[] YCoordinates
		{
			get { return yCoordinates; }
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

			xCoordinates = new int[cityCount];
			yCoordinates = new int[cityCount];

			int coordinateIndex = 0;
			for (int i = 0; i < xLength; i++)
			{
				for (int j = 0; j < yLength; j++)
				{
					xCoordinates[coordinateIndex] = x1 + i;
					yCoordinates[coordinateIndex] = y1 + j;
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
