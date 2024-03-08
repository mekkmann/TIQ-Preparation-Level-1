namespace Regex
{
    using System.Text.RegularExpressions;
    internal class Program
    {
        static void Main(string[] args)
        {
            // initiate a string with the path to a file
            string pathToFile = "MonsterManual.txt";

            // read all lines of a file and store them in an array of string
            string[] readFile = File.ReadAllLines(pathToFile);

            // a list of strings to store monster names
            List<string> nameList = [];

            // a list of bool to store if a monster can fly or not
            List<bool> canFly = [];

            // a list of bools to store if a monster needs 10 or more dice rolls for their hp
            List<bool> tenPlusDiceRollsForHP = [];

            // counter to keep track of if we're in a specific monsters block or are at the next
            int monsterLineCounter = 0;
            foreach (var line in readFile)
            {
                //name
                if (monsterLineCounter == 0)
                {
                    nameList.Add(line);
                }

                //hitpoints
                if (monsterLineCounter == 2)
                {
                    tenPlusDiceRollsForHP.Add(Regex.IsMatch(line, @"\(\d{2}d"));

                }

                //can fly
                if (monsterLineCounter == 4)
                {
                    canFly.Add(line.Contains("fly"));
                }

                //check if at the end of a monster
                if (string.IsNullOrWhiteSpace(line))
                {
                    monsterLineCounter = 0;
                }
                else
                {
                    monsterLineCounter++;
                }
            }
            // print monster name and ability to fly to console
            Console.WriteLine("Monsters in the manual are:");
            for (var i = 0; i < nameList.Count; i++)
            {
                Console.WriteLine($"{nameList[i]} - can fly: {canFly[i]}");
            }

            //print monster name and if they require 10 or more dice rolls
            Console.WriteLine("Monsters in the manual are:");
            for (var i = 0; i < nameList.Count; i++)
            {
                Console.WriteLine($"{nameList[i]} - 10+ dice rolls: {tenPlusDiceRollsForHP[i]}");
            }

            //to keep console open
            Console.ReadLine();
        }
    }
}
