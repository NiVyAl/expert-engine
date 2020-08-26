using System;
using System.Collections.Generic;
namespace euro
{
	public abstract class AbstractCity
	{

		public abstract void giveCoins();
		public abstract void takeCoins(Dictionary<string, int> takenCoins);
		public abstract Dictionary<string, int> GivenCoins { get; set; }
		public abstract Dictionary<string, int> Coins { get; set; }
		public abstract String Country { get; set; }
		public abstract bool IsComplete { get; set; }
	}

	public class City : AbstractCity
	{
		/* интерфейсы */
		public override bool IsComplete { get; set; } = false;
		private Dictionary<string, int> coins = new Dictionary<string, int>();
		public override Dictionary<string, int> Coins
		{
			get
			{
				return coins;
			}
			set
			{
				//coins = value;
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
				givenCons.Add(i, 0); // заполняю givenCoins чтобы там были все ключи
			}
			coins[country] = 1000000;
			this.country = country;
			//ConsoleWrite.Wr(coins);
			//Console.WriteLine("---------------");
		}


		public override void giveCoins() // какие монеты отдает город
		{
			foreach (var i in coins.Keys)
			{
				if (coins[i] > 0)
				{
					givenCons[i] = coins[i] / 1000;
				}
				
			}

		}

		public override void takeCoins(Dictionary<string, int> takenCoins) // получает 1000 монет. возвращает закончен ли город (есть ли все монеты) 
		{
			Dictionary<string, int> tempCollection = new Dictionary<string, int>(); // (что-то сделать получше!!!)
			foreach (var i in coins.Keys) // перекидываем всех в новую коллекцию
			{
				tempCollection.Add(i, coins[i]);
			}

			foreach (var i in tempCollection.Keys)
			{
				coins[i] = coins[i] - GivenCoins[i]; // вычитаю монеты
				if (takenCoins.ContainsKey(i))
				{
					coins[i] = coins[i] + takenCoins[i]; // прибавляю полученные монеты
				}
			}


			foreach (var i in coins.Keys) // проверяю есть ли монеты каждой страны
			{
				if (coins[i] == 0)
				{
					IsComplete = false;
					break;
				} else
				{
					IsComplete = true;
				}
			}
			
		}
	}
}
