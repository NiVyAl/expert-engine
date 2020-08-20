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
                while ((line = sr.ReadLine()) != null)
                {
                    int numberOfCountry;
                    int.TryParse(line, out numberOfCountry);
                    if (numberOfCountry == 0)
					{
                        break;
					}
                    Console.WriteLine($"{numberOfCountry}:");

                    for (int i = 0; i < numberOfCountry; i++)
					{
                        line = sr.ReadLine();
                        Console.WriteLine(line);
                    }
                }
            }
        }
}
}
