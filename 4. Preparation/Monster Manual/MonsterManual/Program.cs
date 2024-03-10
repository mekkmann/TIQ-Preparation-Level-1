using System.Text.RegularExpressions;

namespace MonsterManual
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // path to MonsterManual.txt
            string monsterManualPath = "MonsterManual.txt";
            // read and store all lines from file
            string[] monsterManualLines = File.ReadAllLines(monsterManualPath);

            //list to store all monster types and generate monster types
            List<MonsterType> allMonsterTypes = GenerateMonstersFromFile(monsterManualLines);

            // for every element in list
            foreach (MonsterType monsterType in allMonsterTypes)
            {
                // print monster data
                monsterType.PrintMonsterData();
                //spacing 
                Console.WriteLine();
            }
            // to keep console open
            Console.ReadLine();
        }

        static List<MonsterType> GenerateMonstersFromFile(string[] readFile)
        {

            // store temp variables for a monster type
            string currentName = string.Empty;
            string currentDescription = string.Empty;
            string currentAlignment = string.Empty;
            string currentHpRoll = string.Empty;

            // list of monster types
            List<MonsterType> monsterTypes = [];

            // counter to keep track of if we're in a specific monster types block or at the next
            int monsterLineCounter = 0;
            // track previousLine
            string previousLine = string.Empty;
            // goes through the file, line by line
            foreach (string line in readFile)
            {
                // increment to the next line of the same monster
                monsterLineCounter++;

                //name
                if (monsterLineCounter == 1)
                {
                    // pattern for name
                    string namePattern = @"^(.*)$";
                    // getting matches for the line
                    Match match = Regex.Match(line, namePattern);
                    // set current name to value that's matched
                    currentName = match.Groups[1].Value;
                }

                //description and alignment
                if (monsterLineCounter == 2)
                {
                    //pattern for description and alignment
                    string descriptionAndAlignmentPattern = @"^(.*), (.*)$";
                    // getting matches for the line
                    Match match = Regex.Match(line, descriptionAndAlignmentPattern);
                    // set current description to value that's matched
                    currentDescription = match.Groups[1].Value;
                    // set current alignment to value that's matched
                    currentAlignment = match.Groups[2].Value;
                }

                //hitpoints
                if (monsterLineCounter == 3)
                {
                    // pattern for hitpoints roll
                    string hpRollPattern = @"(\d+) (\(.*\))?";
                    //getting matches for the line
                    Match match = Regex.Match(line, hpRollPattern);
                    // check if dice notation exists and if it does, use that
                    if (!string.IsNullOrWhiteSpace(match.Groups[2].Value))
                    {
                        // set to hp roll
                        currentHpRoll = match.Groups[2].Value.Trim(['(', ')']);
                    }
                    // if dice notation doesn't exist
                    else
                    {
                        // set to hp
                        currentHpRoll = match.Groups[1].Value;
                    }
                }
                //check if at the end of a monster
                if (line.Length == 0)
                {
                    // reset counter to 0
                    monsterLineCounter = 0;

                    // set previous line
                    previousLine = line;

                    // create and add monster to list
                    monsterTypes.Add(
                        new MonsterType(
                            currentName,
                            currentDescription,
                            currentAlignment,
                            currentHpRoll)
                        );

                    // reset temp variables
                    currentName = string.Empty;
                    currentDescription = string.Empty;
                    currentAlignment = string.Empty;
                    currentHpRoll = string.Empty;
                }

            }
            // return list
            return monsterTypes;
        }
    }
}
