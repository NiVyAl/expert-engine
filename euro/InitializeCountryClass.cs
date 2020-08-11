using System;
namespace euro
{
	public class InitializeCountryClass
	{
		private string countryName;
		private int x1, x2, y1, y2;
		private int xLength, yLength, cityCount;

		private int[,][] allCities;
		public int[,][] AllCities
		{
			get
			{
				return allCities;
			}
			private set
			{
				allCities = value;
			}
		}

		public InitializeCountryClass(string countryName, int x1, int y1, int x2, int y2)
		{
			this.countryName = countryName;
			this.x1 = x1;
			this.x2 = x2;
			this.y1 = y1;
			this.y2 = y2;

			xLength = Math.Abs(x2 - x1) + 1;
			yLength = Math.Abs(y2 - y1) + 1;
			cityCount = xLength * yLength;

			allCities = computAllCities();
		}

		public int[,][] computAllCities() // возвращает координаты всех городов
		{
			//Console.WriteLine(countryName);
			int[,][] a = new int[xLength, yLength] [];
			for (int i = 0; i < xLength; i++)
			{
				for (int j = 0; j < yLength; j++)
				{
					a[i, j] = new int[2];
					a[i, j][0] = x1 + i; // x
					a[i, j][1] = y1 + j; // y

					//Console.WriteLine($"{a[i, j][0]} {a[i, j][1]}");
				}
				//Console.WriteLine();
			}
			return a;
		}
	}
}
