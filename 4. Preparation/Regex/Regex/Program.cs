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

            // array of string to keep to be able to index alignment to names in namesByAlignment
            string[] firstAlignment = ["lawful", "neutral", "chaotic"];
            // array of string to keep to be able to index alignment to names in namesByAlignment
            string[] secondAlignment = ["good", "neutral", "evil"];

            // 2d array of lists to track specific alignment of monsters
            // -- index will correspond to the firstAlignment and SecondAlignment above ^
            List<string>[,] namesByAlignment = new List<string>[3, 3];
            // populating the namesByAlignment array with empty lists
            for (int axis1 = 0; axis1 < 3; axis1++)
            {
                for (int axis2 = 0; axis2 < 3; axis2++)
                {
                    namesByAlignment[axis1, axis2] = new List<string>();
                }
            }

            // list of strings to keep track of unaligned monsters
            List<string> namesOfUnaligned = [];
            // list of strings to keep track of monsters with any alignment
            List<string> namesOfAnyAlignment = [];
            // list of strings to keep track of special case alignment monsters
            List<string> namesOfSpecialCases = [];
            // to keep track of the special case alignments
            List<string> specialCases = [];

            // counter to keep track of if we're in a specific monsters block or are at the next
            int monsterLineCounter = 0;

            // keeps track of the current monsters name
            string currentName = string.Empty;

            // goes through the read file line by line
            foreach (var line in readFile)
            {
                // increment to the next line of the same monster
                monsterLineCounter++;

                //name
                if (monsterLineCounter == 1)
                {
                    // add name to nameList
                    nameList.Add(line);
                    // set the current monster name
                    currentName = line;
                }

                //alignment
                if (monsterLineCounter == 2)
                {
                    string splitLine = line.Split(", ")[1];
                    Match match = Regex.Match(splitLine, @"(chaotic|lawful|neutral) (good|neutral|evil)");

                    if (match.Success)
                    {
                        namesAndSpecificAlignment.Add($"{currentName} ({splitLine})");
                        int firstAlignmentIndex = Array.IndexOf(firstAlignment, match.Groups[1].Value);
                        int secondAlignmentIndex = Array.IndexOf(secondAlignment, match.Groups[2].Value);
                        namesByAlignment[firstAlignmentIndex, secondAlignmentIndex].Add(currentName);
                        continue;
                    }

                    if (line.Contains("neutral"))
                    {
                        namesByAlignment[1, 1].Add(currentName);
                        continue;
                    }

                    if (line.Contains("unaligned"))
                    {
                        namesOfUnaligned.Add(currentName);
                        continue;
                    }

                    if (line.Contains("any alignment"))
                    {
                        namesOfAnyAlignment.Add(currentName);
                        continue;
                    }

                    namesOfSpecialCases.Add(currentName);
                    string[] parts = line.Split(", ");
                    specialCases.Add(parts[1]);
                }

                //hitpoints
                if (monsterLineCounter == 3)
                {
                    // if the monster takes 10+ rolls for Hp, add to tenPlusDiceRollsForHP
                    tenPlusDiceRollsForHP.Add(Regex.IsMatch(line, @"\(\d{2}d"));

                }

                //flying
                if (monsterLineCounter == 5)
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

            // prints all specific alignments and the monster with those alignments
            for (int secondAlignmentIndex = 0; secondAlignmentIndex < 3; secondAlignmentIndex++)
            {
                for (int firstAlignmentIndex = 0; firstAlignmentIndex < 3; firstAlignmentIndex++)
                {
                    if (firstAlignmentIndex == 1 && secondAlignmentIndex == 1)
                    {
                        Console.WriteLine($"\nMonsters with alignment true neutral are:");
                    }
                    else
                    {
                        Console.WriteLine($"\nMonsters with alignment {firstAlignment[firstAlignmentIndex]} {secondAlignment[secondAlignmentIndex]} are:");
                    }

                    foreach (string name in namesByAlignment[firstAlignmentIndex, secondAlignmentIndex])
                    {
                        Console.WriteLine(name);
                    }
                    Console.WriteLine();
                }
            }
            // prints unaligned monster
            Console.WriteLine($"\nUnaligned monsters are:");
            foreach (string name in namesOfUnaligned)
            {
                Console.WriteLine(name);
            }
            // prints monsters of any alignment
            Console.WriteLine($"\nMonsters which can be of any alignment are:");
            foreach (string name in namesOfAnyAlignment)
            {
                Console.WriteLine(name);
            }
            // prints monster with special case alignments
            Console.WriteLine($"\nMonsters with special cases are:");
            for (int i = 0; i < namesOfSpecialCases.Count; i++)
            {
                Console.WriteLine($"{namesOfSpecialCases[i]} ({specialCases[i]})");
            }

            //to keep console open
            Console.ReadLine();
        }
    }
}
