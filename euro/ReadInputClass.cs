using System;
using System.IO;
using System.Threading.Tasks;
namespace euro
{
	public class ReadInputClass
	{
        public ReadInputClass(string address)
	    {
            using (StreamReader sr = new StreamReader(address, System.Text.Encoding.Default))
            {
                string line;
                int caseNumber = 1;
                while ((line = sr.ReadLine()) != null)
                {
                    int numberOfCountry;
                    int.TryParse(line, out numberOfCountry);
                    if (numberOfCountry == 0)
					{
                        break;
					}

                    AbsractInitializeCountryClass[] Countries = new AbsractInitializeCountryClass[numberOfCountry];
                    Console.WriteLine($"{numberOfCountry}:");
                    string[] countriesNames = new string[numberOfCountry]; // список всех стран
                    for (int i = 0; i < numberOfCountry; i++)
					{
                        line = sr.ReadLine();
                        string country = returnCountry(line);
                        int[] coordinates = returnCoordinates(line);
                        Countries[i] = new InitializeCountryClass(country, coordinates[0], coordinates[1], coordinates[2], coordinates[3]);
                        countriesNames[i] = country;
                    }

                    /* Заполнение Городами */
                    AbstractCity[,] AllCity = new AbstractCity[100, 100];
                    for (int k = 0; k < numberOfCountry; k++)
					{
                        for (int i = 0; i < Countries[k].AllCities.GetLength(0); i++)
                        {
                            for (int j = 0; j < Countries[k].AllCities.GetLength(1); j++)
                            {
                                int a = Countries[k].AllCities[i, j][0];
                                int b = Countries[k].AllCities[i, j][1];
                                AllCity[a, b] = new City(Countries[k].CountryName, countriesNames);
                            }
                        }
                    }
                    /* */
                        
                        
                    caseNumber++;
                }
            }
        }



        private string returnCountry(string line)
		{
            string[] words = line.Split(new char[] { ' ' });
            return words[0];
        }

        private int[] returnCoordinates(string line)
        {
            string[] words = line.Split(new char[] { ' ' });
            int[] coordinates = new int[4];
			int.TryParse(words[1], out coordinates[0]);
			int.TryParse(words[2], out coordinates[1]);
			int.TryParse(words[3], out coordinates[2]);
			int.TryParse(words[4], out coordinates[3]);

            return coordinates;

        }
    }
}
