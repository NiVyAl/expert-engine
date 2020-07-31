using System;
using System.Collections.Generic;
namespace euro
{
	public class City
	{
		private Dictionary<string, int> coins;
		public Dictionary<string, int> Coins
		{
			get
			{
				return coins;
			}
			private set
			{
				coins = value;
			}
		}

		private string country;
		private string[] countries;
		public City(string country, string[] countries)
		{
			this.country = country;
			this.countries = countries;
			coins[country] = 1000000;
		}

		public bool dayPassed(Dictionary<string, int> nearCitiesCountry) // nearCitiesCountry - каких стран города граничат с данным городом <"spain", 2> (два испанских города рядом)
		{
			foreach(var i in nearCitiesCountry.Keys)
			{
				if (i != country) // если соседний город не из нашей страны
				{
					coins[country] = coins[country] - 1000; // монет нашей страны становится меньше
					coins[i] = coins[i] + 1000; // монет соседней страны становится больше
				}
			}

			if (coins.Count == country.Length) // если в городе есть монеты всех стран
			{
				return true;
			} else
			{
				return false;
			}
		}
	}
}
