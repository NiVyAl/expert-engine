using System;
using System.Collections.Generic;
namespace euro
{
	public abstract class AbstractCity
	{

		public abstract void giveCoins();
		public abstract bool takeCoins(Dictionary<string, int> takenCoins);
		public abstract Dictionary<string, int> GivenCoins { get; set; }
		public abstract String Country { get; set; }
	}

	public class City : AbstractCity
	{
		/* интерфейсы */
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
		public override Dictionary<string, int> GivenCoins
		{
			get
			{
				return givenCons;
			}
			set
			{
				//givenCons = value;
			}
		}

		private string country;
		public override string Country
		{
			get
			{
				return country;
			}
			set
			{
				//givenCons = value;
			}
		}
		/* */

		public City(string country, string[] countries)
		{
			foreach(string i in countries)
			{
				coins.Add(i, 0);
			}
			coins[country] = 1000000;
			this.country = country;
		}


		public override void giveCoins() // какие монеты отдает город
		{
			foreach (var i in coins.Keys)
			{
				if (coins[i] > 0)
				{
					givenCons.Add(i, coins[i] / 100);
				}
				
			}

		}

		public override bool takeCoins(Dictionary<string, int> takenCoins) // получает 1000 монет. возвращает закончен ли город (есть ли все монеты) 
		{
			foreach (var i in coins.Keys) 
			{
				coins[i] = coins[i] - coins[i] / 100; // вычитаю монеты
				coins[i] = coins[i] + takenCoins[i]; // прибавляю полученные монеты
			}

			foreach (var i in coins.Keys) // проверяю есть ли монеты каждой страны
			{
				if (coins[i] == 0)
				{
					return false;
				}
			}

			return true;
			
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
