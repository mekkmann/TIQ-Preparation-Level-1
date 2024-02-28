using System;

namespace TheMatrix
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new();
            var streams = new List<int>();
            var symbols = "!@#$%^&*()_+-=[];',.\\/~{}:|<>?";

            for (var i = 0; i < 10; i++) streams.Add(random.Next(0, 80));

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            while (true)
            {
                for (int x = 0; x < 80; x++)
                {
                    Console.Write(streams.Contains(x) ? symbols[random.Next(symbols.Length)] : ' ');
                }

                Console.WriteLine();
                Thread.Sleep(100);

                if (random.Next(3) == 0) streams.Add(random.Next(0, 80));
                if (random.Next(3) == 0) streams.RemoveAt(random.Next(streams.Count));
            }
        }
    }
}
