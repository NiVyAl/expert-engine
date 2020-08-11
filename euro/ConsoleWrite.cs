using System;
using System.Collections.Generic;
namespace euro
{
	public class ConsoleWrite
	{
		public ConsoleWrite()
		{
		}

		public static void  Wr(Dictionary<string, int> a)
		{
			//Console.WriteLine($"start----------");
			foreach (var i in a.Keys)
			{
				Console.WriteLine($"	{i}: {a[i]} ");
			}
			//Console.WriteLine($"end----------");
		}
	}
}
