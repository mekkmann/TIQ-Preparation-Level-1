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

            // prints title
            Console.WriteLine("MONSTER MANUAL\n");
            // prints instruction
            Console.WriteLine("Enter a query to search monsters by name:");
            // stores query results
            List<MonsterType> queryResults = [];
            // stores query input
            string queryInput = string.Empty;

            // do at least once and then while there are no results in the query
            do
            {
                queryInput = Console.ReadLine();
                foreach (MonsterType monster in allMonsterTypes)
                {
                    if (monster.name.Contains(queryInput, StringComparison.CurrentCultureIgnoreCase))
                    {
                        queryResults.Add(monster);
                    }
                }
                if (queryResults.Count == 0)
                {
                    Console.WriteLine("\nNo monsters found. Try again:");
                }

            } while (queryResults.Count == 0);

            // if 1 result 
            if (queryResults.Count == 1)
            {
                //print sentence and monster data to console
                Console.WriteLine($"\nDisplaying information for {queryResults[0].name}\n");
                queryResults[0].PrintMonsterData();
            }
            // if more than 1 result
            else
            {
                // ask user which monster to look yup
                Console.WriteLine("\nWhich monster did you want to look up?:\n");

                // print all options
                for (int i = 0; i < queryResults.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {queryResults[i].name}");
                }

                // ask user for number
                Console.WriteLine("\nEnter Number:");
                // take number input
                string numberInput = Console.ReadLine();

                // parse number input and subtract 1 to get index of monster
                int numberInputAsIndex = int.Parse(numberInput) - 1;

                //print sentence and monster data to console
                Console.WriteLine($"\nDisplaying information for {queryResults[numberInputAsIndex].name}\n");
                queryResults[numberInputAsIndex].PrintMonsterData();
            }

            // to keep console open
            Console.ReadLine();
        }

        // functions to display all monster data
        static void DisplayAllMonsterData(List<MonsterType> list)
        {
            // for every element in list
            foreach (MonsterType monsterType in list)
            {
                // print monster data
                monsterType.PrintMonsterData();
                //spacing 
                Console.WriteLine();
            }
        }

        // function to generate all monster types from file
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
