using System;
using System.Collections.Generic;
namespace euro
{
	public class City
	{
		private Dictionary<string, int> coins = new Dictionary<string, int>();
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
		private Dictionary<string, int> givenCons = new Dictionary<string, int>();
		public Dictionary<string, int> GivenCons
		{
			get
			{
				return givenCons;
			}
			private set
			{
				givenCons = value;
			}
		}

		private string country;
		public City(string country)
		{
			this.country = country;
			coins.Add(country, 1000000);
		}

		public void giveCoins()
		{
			foreach (var i in coins.Keys)
			{
				givenCons[i] = coins[i] / 100; // возможно нужно сделать add
			}

		}

		public bool takeCoins(Dictionary<string, int> takenCoins) // возвращает закончен ли город (есть ли все монеты)
		{


			return false;
		}

		//public bool dayPassed(Dictionary<string, int> nearCitiesCountry) // nearCitiesCountry - каких стран города граничат с данным городом <"spain", 2> (два испанских города рядом)
		//{
		//	foreach(var i in nearCitiesCountry.Keys)
		//	{
		//		if (i != country) // если соседний город не из нашей страны
		//		{
		//			coins[country] = coins[country] - 1000; // монет нашей страны становится меньше
		//			coins[i] = coins[i] + 1000; // монет соседней страны становится больше
		//		}
		//	}

		//	if (coins.Count == country.Length) // если в городе есть монеты всех стран
		//	{
		//		return true;
		//	} else
		//	{
		//		return false;
		//	}
		//}
	}
}
