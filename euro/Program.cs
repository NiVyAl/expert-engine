using System;

namespace euro
{
    class Program
    {
		static void Main(string[] args)
		{
			int result = Environment.TickCount;
			EuroDiffusion fileText = new EuroDiffusion(@"input/input.in");
			Console.WriteLine($"{Environment.TickCount - result}ms");
		}
	}
}
