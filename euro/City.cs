using System;
using System.Collections.Generic;
namespace euro
{
	public class City
	{
		private const int _initiallyCountCoins = 1000000;
		private const double _portionCoins = 0.001;

		private int _numberOfCountry;
		private int[] _coins;
		private int[] _givenCoins;
		private int _countUncompleteCountries;

		public int[] Coins
		{
			get { return _coins; }
		}
		public int[] GivenCoins
		{
			get { return _givenCoins; }
		}
		public bool IsComplete
		{
			get { return (_countUncompleteCountries == 0); }
		}

		public City(int countryIndex, int numberOfCountry)
		{
			_coins = new int[numberOfCountry];
			_givenCoins = new int[numberOfCountry];
			_numberOfCountry = numberOfCountry;
			_countUncompleteCountries = _numberOfCountry - 1;

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
					_givenCoins[i] = (int)(_coins[i] * _portionCoins);
			}

		}

		public void takeCoins(int[] takenCoins) // получает 1000 монет. возвращает закончен ли город (есть ли все монеты) 
		{
			for (int i = 0; i < _numberOfCountry; i++)
			{
				int startDayCoins = _coins[i];
				_coins[i] -= _givenCoins[i]; // вычитаю отданные монеты
				_coins[i] += takenCoins[i]; // получаю монеты
				if (startDayCoins == 0 && takenCoins[i] > 0)
					_countUncompleteCountries -= 1;
			}
		}
	}
}
