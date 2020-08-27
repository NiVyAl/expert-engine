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
		public abstract bool IsComplete { get; set; }
	}

	public class City : AbstractCity
	{
		/* интерфейсы */
		private Dictionary<string, int> coins = new Dictionary<string, int>();
		private Dictionary<string, int> givenCons = new Dictionary<string, int>();

		public override bool IsComplete { get; set; } = false;

		public override Dictionary<string, int> Coins
		{
			get
			{
				return coins;
			}
			set
			{
			}
		}
		public override Dictionary<string, int> GivenCoins
		{
			get
			{
				return givenCons;
			}
			set
			{
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
			//ConsoleWrite.Wr(coins);
			//Console.WriteLine("---------------");
		}


		public override void giveCoins() // вызывается когда наступает новый день, Город через интерфейс GivenCoins показывает какие монеты отдает
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
			foreach (var i in GivenCoins.Keys) // прохожу по GivenCoins, тк у него те же ключи, что и у coins
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
