using System;
using System.Collections.Generic;
namespace euro
{
	public class City // class for each city
	{
		private const int _initiallyCountCoins = 1000000;
		private const double _portionCoins = 0.001;

		private int _numberOfCountry;
		private int[] _coins; // city balance
		private int[] _givenCoins; // representative portion of coins
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


		public void giveCoins() // called when a new day comes, the City through the GivenCoins interface shows which coins are given
		{
			for (int i = 0; i < _numberOfCountry; i++)
			{
				if (_coins[i] > 0)
					_givenCoins[i] = (int)(_coins[i] * _portionCoins);
			}

		}

		public void takeCoins(int[] takenCoins) // city take coins
		{
			for (int i = 0; i < _numberOfCountry; i++)
			{
				int startDayCoins = _coins[i];
				_coins[i] -= _givenCoins[i]; // given coins are deducted
				_coins[i] += takenCoins[i]; // take coins
				if (startDayCoins == 0 && takenCoins[i] > 0) // if there were 0 coins and there are more
					_countUncompleteCountries -= 1; // the number of un complete cities decreases
			}
		}
	}
}
