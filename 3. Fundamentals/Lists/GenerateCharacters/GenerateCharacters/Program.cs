using System;
using System.Collections.Generic;
using System.Linq;

namespace GenerateCharacters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> abilityScores = new List<int>();

            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                List<int> temp = new List<int>();
                for (int j = 0; j < 4; j++)
                {
                    temp.Add(random.Next(1, 7));
                }
                string tempScores = String.Join(", ", temp);
                Console.Write($"You roll {tempScores}. ");
                temp.Remove(temp.Min());
                int totalTemp = temp.Sum();
                Console.Write($"The ability score is {totalTemp}\n");
                abilityScores.Add(totalTemp);
            }
            abilityScores.Sort();
            string scoresText = String.Join(", ", abilityScores);

            Console.WriteLine($"Your available ability scores are {scoresText}");

            // to keep console open
            Console.ReadLine();
        }
    }
}
