using System;
namespace euro
{
	public class InitializeCountryClass
	{
		private string countryName;
		private int x1, x2, y1, y2;
		private int xLength, yLength, cityCount;
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
		}

		public int allCities()
		{
			Console.WriteLine(countryName);
			//int [] [,] a = new int[2][xLength, yLength];
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < cityCount; j++)
				{
					
				}
			}
			return 0;
		}
	}
}
