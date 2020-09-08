using System;
using System.Collections;
namespace euro
{
	public class Country : IComparable
	{
		private int x1, x2, y1, y2;
		private int xLength, yLength, cityCount;
		private int[,][] allCities;
		//private string countryName;
		public int[,][] AllCities
		{
			get { return allCities; }
		}

		//public string CountryName
		//{
		//	get { return countryName; }
		//}

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

			allCities = computAllCities();
		}

		public int[,][] computAllCities() // return coordinates of all cities in country
		{
			int[,][] a = new int[xLength, yLength][];
			for (int i = 0; i < xLength; i++)
			{
				for (int j = 0; j < yLength; j++)
				{
					a[i, j] = new int[2];
					a[i, j][0] = x1 + i; // x
					a[i, j][1] = y1 + j; // y
				}
			}
			return a;
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
