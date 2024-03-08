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

            // a list of strings to store the name of a monster that can fly 10-49 feet per turn
            List<string> nameOfSlowFlyers = [];

            // a list of strings to store names and specific alignment of monsters
            List<string> namesAndSpecificAlignment = [];

            // counter to keep track of if we're in a specific monsters block or are at the next
            int monsterLineCounter = 0;

            // keeps track of the current monsters name
            string currentName = string.Empty;

            // goes through the read file line by line
            foreach (var line in readFile)
            {
                //name
                if (monsterLineCounter == 0)
                {
                    // add name to nameList
                    nameList.Add(line);
                    // set the current monster name
                    currentName = line;
                }

                //alignment
                if (monsterLineCounter == 1)
                {
                    string splitLine = line.Split(", ")[1];
                    Match match = Regex.Match(splitLine, @"(chaotic|lawful|neutral) ?(good|neutral|evil)?");

                    if (match.Success)
                    {
                        namesAndSpecificAlignment.Add($"{currentName} ({splitLine})");
                    }
                }

                //hitpoints
                if (monsterLineCounter == 2)
                {
                    // if the monster takes 10+ rolls for Hp, add to tenPlusDiceRollsForHP
                    tenPlusDiceRollsForHP.Add(Regex.IsMatch(line, @"\(\d{2}d"));

                }

                //flying
                if (monsterLineCounter == 4)
                {
                    // if the monster can fly, add to canFly
                    canFly.Add(line.Contains("fly"));

                    // if the monster can fly 10-49 feet, add to nameOfSlowFlyers
                    if (Regex.IsMatch(line, @"fly [1-4]\d "))
                    {
                        nameOfSlowFlyers.Add(currentName);
                    }
                }

                //check if at the end of a monster
                if (string.IsNullOrWhiteSpace(line))
                {
                    // if line is null or whitespace, we're at a new monster
                    monsterLineCounter = 0;
                }
                else
                {
                    // anything else we increment to the next line of the same monster
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

            //print monsters that are slow flyers
            Console.WriteLine("Monsters that can fly 10-49 feet per turn:");
            foreach (var name in nameOfSlowFlyers)
            {
                Console.WriteLine(name);
            }

            //print monsters with specific alignments
            Console.WriteLine("Monsters with a specific alignment:");
            foreach (var monster in namesAndSpecificAlignment)
            {
                Console.WriteLine(monster);
            }

            //to keep console open
            Console.ReadLine();
        }
    }
}
