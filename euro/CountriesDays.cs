using System;
using System.Collections;

namespace euro
{
    internal class CountriesDays : IComparable // for sort countries
    {
        public string Name { get; set; }
        public int Days { get; set; }

		public int CompareTo(object obj)
		{
            int res;
            if (obj == null) return 1;

            CountriesDays otherCityDays = obj as CountriesDays;
            if (otherCityDays == null)
                throw new ArgumentException("Object is not a Temperature");

            res = this.Days.CompareTo(otherCityDays.Days);  // sort by number of days

            if (res == 0) // or if number is same
                res = this.Name.CompareTo(otherCityDays.Name); // sort alphabetically
            return res;

        }
	}
}