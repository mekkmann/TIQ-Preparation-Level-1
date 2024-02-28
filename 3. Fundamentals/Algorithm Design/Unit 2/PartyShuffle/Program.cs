using System;

namespace PartyShuffle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> participants = ["Mary", "Jane", "Wiz", "Snoop", "MGK"];

            Console.WriteLine($"Signed up participants: {string.Join(", ", participants)}\n\n");

            Console.WriteLine("Generating starting order..\n");
            List<string> shuffledList = ShuffleList(participants);

            Console.WriteLine($"Starting order: {string.Join(", ", shuffledList)}");
            shuffledList = ShuffleList(participants);
            Console.WriteLine($"Starting order: {string.Join(", ", shuffledList)}");
            shuffledList = ShuffleList(participants);
            Console.WriteLine($"Starting order: {string.Join(", ", shuffledList)}");

            // to keep console open
            Console.ReadLine();

            static List<string> ShuffleList(List<string> items)
            {
                Random random = new();
                List<string> itemsCopy = items;
                int copyCount = itemsCopy.Count;

                for (int i = copyCount - 1; i > 0; i--)
                {
                    int j = random.Next(0, copyCount--);
                    (itemsCopy[j], itemsCopy[i]) = (itemsCopy[i], itemsCopy[j]);
                }

                return itemsCopy;
            }
        }
    }
}
