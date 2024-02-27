using System;

namespace FirstGame2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Mission 1
            //Random random = new Random();

            //int firstThrow = random.Next(1, 7);
            //int secondThrow = random.Next(1, 7);
            //int thirdThrow = random.Next(1, 7);

            //Console.WriteLine(
            //    $"First dice throw is {firstThrow}\n" +
            //    $"Second dice throw is {secondThrow}\n" +
            //    $"Third dice throw is {thirdThrow}"
            //    );

            // Mission 2
            //Random random = new Random();

            //int firstThrow = random.Next(1, 7);
            //int secondThrow = random.Next(1, 7);
            //int thirdThrow = random.Next(1, 7);

            //int totalScore = (firstThrow + secondThrow + thirdThrow * 3) * 2;
            //Console.WriteLine(
            //    $"You roll: {firstThrow}, {secondThrow}, {thirdThrow}\n" +
            //    $"The total score is: {totalScore}"
            //    );

            // Mission 3
            //Random random = new Random();

            //int totalPins = 10;
            //int firstRoll = random.Next(0, totalPins + 1);
            //int secondRoll = random.Next(0, totalPins + 1 - firstRoll);
            //Console.WriteLine(
            //    $"First roll: {firstRoll}\n" +
            //    $"Second Roll: {secondRoll}"
            //    );

            // Mission 4
            //Random random = new Random();

            //int minShots = 10;
            //int maxShots = 20;
            //int shots = random.Next(minShots, maxShots + 1);
            //int hits = random.Next(0, shots + 1);
            //double accuracy = (double)hits / shots * 100;

            //Console.WriteLine(
            //    $"Total shots: {shots}\n" +
            //    $"Hits made: {hits}\n" +
            //    $"Hit accuracy: {accuracy}%");

            // Mission 5
            float lightyearsToAC = 4.365f;
            float parsecInLightyears = 3.26f;

            float moreAccurate = lightyearsToAC / parsecInLightyears;

            Console.WriteLine(moreAccurate);


            // to keep console open
            Console.ReadLine();
        }
    }
}
