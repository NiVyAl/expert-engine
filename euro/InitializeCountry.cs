using System;
namespace euro
{

	public class InitializeCountry // class for calculating the coordinates of each city in cointries
	{
		private int x1, x2, y1, y2;
		private int xLength, yLength;

		private int[,][] allCities;
		public  int[,][] AllCities
		{
			get { return allCities; }
		}

		private string countryName;
		public  string CountryName
		{
			get { return countryName; }
		}

		public InitializeCountry(string countryName, int x1, int y1, int x2, int y2)
		{
			this.countryName = countryName;
			this.x1 = x1;
			this.x2 = x2;
			this.y1 = y1;
			this.y2 = y2;

			xLength = Math.Abs(x2 - x1) + 1;
			yLength = Math.Abs(y2 - y1) + 1;

			allCities = computAllCities();
		}

		public int[,][] computAllCities() // return coordinates of all cities in country
		{
			int[,][] a = new int[xLength, yLength] [];
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
	}
}
