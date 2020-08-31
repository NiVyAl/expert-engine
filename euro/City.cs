using System;
using System.Collections.Generic;
namespace euro
{
	public class City
	{
		private int _initiallyCountCoins = 1000000;
		private double _portionCoins = 0.001;

		private int _numberOfCountry;
		private int[] _coins;
		private int[] _givenCoins;

		public int[] Coins
		{
			get { return _coins; }
		}
		public int[] GivenCoins
		{
			get { return _givenCoins; }
		}

		public City(int countryIndex, int numberOfCountry)
		{
			_coins = new int[numberOfCountry];
			_givenCoins = new int[numberOfCountry];
			_numberOfCountry = numberOfCountry;

			for (int i = 0; i < _numberOfCountry; i++)
			{
				_coins[i] = 0;
				_givenCoins[i] = 0;
			}
			_coins[countryIndex] = _initiallyCountCoins;
		}


		public void giveCoins() // вызывается когда наступает новый день, Город через интерфейс GivenCoins показывает какие монеты отдает
		{
			for (int i = 0; i < _numberOfCountry; i++)
			{
				if (_coins[i] > 0)
					_givenCoins[i] = (int)(_coins[i] * _portionCoins); // !!!!! НОРМ?
			}

		}

		public void takeCoins(int[] takenCoins) // получает 1000 монет. возвращает закончен ли город (есть ли все монеты) 
		{
			for (int i = 0; i < _numberOfCountry; i++)
			{
				_coins[i] -= _coins[i] - _givenCoins[i]; // вычитаю монеты, отданые монеты
				_coins[i] += takenCoins[i]; // получаю монеты
			}
		}

		public bool isComplete() // проверяю есть ли монеты каждой страны
		{
			for (int i = 0; i < _numberOfCountry; i++)
			{
				if (_coins[i] == 0)
					return false;
			}
			return true;
		}
	}
}
