using System;
using System.Collections;

namespace euro
{
    internal class CityDays : IComparable
    {
        public string Name { get; set; }
        public int Days { get; set; }

		public int CompareTo(object obj)
		{
            int res;
            if (obj == null) return 1;

            CityDays otherCityDays = obj as CityDays;
            if (otherCityDays == null)
                throw new ArgumentException("Object is not a Temperature");

            res = this.Days.CompareTo(otherCityDays.Days);  // сортировка по количеству дней

            if (res == 0)
                res = this.Name.CompareTo(otherCityDays.Name);
            return res;

        }
	}
}