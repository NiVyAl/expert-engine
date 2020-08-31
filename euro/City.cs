using System;
using System.Collections.Generic;
namespace euro
{
	public class City
	{
		private int _initiallyCountCoins = 1000000;
		private double _portionCoins = 0.001;
		private Dictionary<string, int> _coins = new Dictionary<string, int>();
		private Dictionary<string, int> _givenCons = new Dictionary<string, int>();

		public  Dictionary<string, int> Coins
		{
			get { return _coins; }
		}
		public  Dictionary<string, int> GivenCoins
		{
			get { return _givenCons; }
		}

		public City(string country, string[] countries)
		{
			foreach(string i in countries)
			{
				_coins.Add(i, 0);
				_givenCons.Add(i, 0); // заполняю givenCoins чтобы там были все ключи
			}
			_coins[country] = _initiallyCountCoins;
		}


		public void giveCoins() // вызывается когда наступает новый день, Город через интерфейс GivenCoins показывает какие монеты отдает
		{
			foreach (var i in _coins.Keys)
			{
				if (_coins[i] > 0)
					_givenCons[i] = (int)(_coins[i] * _portionCoins); // !!!!! НОРМ?
			}

		}

		public void takeCoins(Dictionary<string, int> takenCoins) // получает 1000 монет. возвращает закончен ли город (есть ли все монеты) 
		{
			foreach (var i in GivenCoins.Keys) // прохожу по GivenCoins, тк у него те же ключи, что и у _coins
			{
				_coins[i] = _coins[i] - GivenCoins[i]; // вычитаю монеты
				if (takenCoins.ContainsKey(i))
				{
					int a = _coins[i];
					_coins[i] += takenCoins[i]; // прибавляю полученные монеты
					//if ((a == 0) && (_coins[i] > 0))
					//{
					//	// ДОБАВЬ!!! переменная сколько осталось нулевых стран
					//}
				}
			}
		}

		public bool isComplete() // проверяю есть ли монеты каждой страны
		{
			foreach (var i in _coins.Keys) 
			{
				if (_coins[i] == 0)
					return false;
			}
			return true;
		}
	}
}
