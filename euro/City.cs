using System;
using System.Collections.Generic;
namespace euro
{
	/// <summary>
	///		class for each city
	/// </summary>
	public class City
	{
		private const int _initiallyCountCoins = 1000000;
		private const double _portionCoins = 0.001;

		private int _numberOfCountry;
		private int[] _coins; // city balance
		private int[] _givenCoins; // representative portion of coins
		private int _countUncompleteCountries;
		//private bool[] _neighborsCountries;

		public int[] Coins
		{
			get { return _coins; }
		}
		//public bool[] NeighborsCountries
		//{
		//	get { return _neighborsCountries; }
		//}
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
			//_neighborsCountries = new bool[numberOfCountry];
			_numberOfCountry = numberOfCountry;
			_countUncompleteCountries = _numberOfCountry - 1;

			for (int i = 0; i < _numberOfCountry; i++)
			{
				_coins[i] = 0;
				_givenCoins[i] = 0;
			}
			_coins[countryIndex] = _initiallyCountCoins;
		}

		/// <summary>
		///		called when a new day comes, the City through the GivenCoins interface shows which coins are given
		/// </summary>
		public void giveCoins()
		{
			for (int i = 0; i < _numberOfCountry; i++)
			{
				if (_coins[i] > 0)
					_givenCoins[i] = (int)(_coins[i] * _portionCoins);
			}

		}

		/// <summary>
		///		city take coins
		/// </summary>
		/// <param name="takenCoins"></param>
		public void takeCoins(int[] takenCoins) 
		{
			for (int i = 0; i < _numberOfCountry; i++)
			{
				int startDayCoins = _coins[i];
				_coins[i] -= _givenCoins[i]; // given coins are deducted
				_coins[i] += takenCoins[i]; // take coins
				if (startDayCoins == 0 && takenCoins[i] > 0) // if there were 0 coins and there are more
					_countUncompleteCountries -= 1; // the number of un complete cities decreases
					//_neighborsCountries[i] = true;
			}
		}
	}
}
