using System;

namespace IncreasingLevelDifficulty
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            int[] levels = new int[100];

            for (int i = 0; i < levels.Length; i++)
            {
                levels[i] = random.Next(1, 51);
            }

            Array.Sort(levels);

            Console.WriteLine(
                $"Number of monsters in levels: {string.Join(", ", levels)}"
                );

            // to keep console open
            Console.ReadLine();
        }
    }
}
