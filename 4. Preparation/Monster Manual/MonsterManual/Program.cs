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

            // path to ArmorTypes.txt
            string armorTypesPath = "ArmorTypes.txt";
            // read and store all lines from file
            string[] armorTypeLines = File.ReadAllLines(armorTypesPath);

            //dictionary to store all armor types fore easy access and generate them
            Dictionary<ArmorTypeId, ArmorType> armorTypes = GenerateAllArmorTypesFromFile(armorTypeLines);

            // prints title
            Console.WriteLine("MONSTER MANUAL\n");

            // prints instruction to choose type of query
            Console.WriteLine("Do you want to search by (n)ame or (a)rmor type?:\n");
            // store query options
            ConsoleKeyInfo consoleKeyInput = Console.ReadKey(true);

            // stores query results
            List<MonsterType> queryResults = [];
            // stores query input
            string queryInput = string.Empty;
            // do at least once and then while there are no results in the query
            do
            {
                // if they chose to search by name
                if (consoleKeyInput.Key == ConsoleKey.N)
                {
                    // prints instruction
                    Console.WriteLine("Enter a query to search monsters by name:");
                    // input to search by
                    queryInput = Console.ReadLine();
                    // for every monster 
                    foreach (MonsterType monster in allMonsterTypes)
                    {
                        // if name contains query
                        if (monster.Name.Contains(queryInput, StringComparison.CurrentCultureIgnoreCase))
                        {
                            // add monster to query results
                            queryResults.Add(monster);
                        }
                    }
                }
                // if they chose to search by armor type
                if (consoleKeyInput.Key == ConsoleKey.A)
                {
                    // prints instruction
                    Console.WriteLine("\nWhich armor type do you want to display?\n");
                    // stores all values in armor type enum
                    ArmorTypeId[] armorTypeValues = Enum.GetValues<ArmorTypeId>();
                    // display all possible choices
                    for (var i = 0; i < armorTypeValues.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}: {armorTypeValues[i]}");
                    }

                    // armorToSearchByInput 
                    int armorIndex = int.Parse(Console.ReadLine()) - 1;
                    // chosen armor type 
                    ArmorTypeId chosenArmorType = armorTypeValues[armorIndex];

                    // for each monster in list
                    foreach (MonsterType monster in allMonsterTypes)
                    {
                        // if the armor type is the armor type we chose to look for
                        if (monster.ArmorType == chosenArmorType)
                        {
                            // add monster to query results
                            queryResults.Add(monster);
                        }
                    }
                }
                // if no results, run loop again
                if (queryResults.Count == 0)
                {
                    Console.WriteLine("\nNo monsters found. Try again.");
                }


            } while (queryResults.Count == 0);

            // if 1 result 
            if (queryResults.Count == 1)
            {
                //print sentence and monster data to console
                Console.WriteLine($"\nDisplaying information for {queryResults[0].Name}\n");
                queryResults[0].PrintMonsterData(armorTypes);
                //PrintMonsterData(queryResults[0], armorTypes);

            }
            // if more than 1 result
            else
            {
                // ask user which monster to look yup
                Console.WriteLine("\nWhich monster did you want to look up?:\n");

                // print all options
                for (int i = 0; i < queryResults.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {queryResults[i].Name}");
                }

                // ask user for number
                Console.WriteLine("\nEnter Number:");
                // take number input
                string numberInput = Console.ReadLine();

                // parse number input and subtract 1 to get index of monster
                int numberInputAsIndex = int.Parse(numberInput) - 1;

                //print sentence and monster data to console
                Console.WriteLine($"\nDisplaying information for {queryResults[numberInputAsIndex].Name}\n");
                queryResults[numberInputAsIndex].PrintMonsterData(armorTypes);
            }

            // to keep console open
            Console.ReadLine();
        }

        // function to generate all armor types from file
        static Dictionary<ArmorTypeId, ArmorType> GenerateAllArmorTypesFromFile(string[] readFile)
        {
            // dictionary
            Dictionary<ArmorTypeId, ArmorType> dict = [];

            // goes through file, line by line
            foreach (string line in readFile)
            {
                //temp variables
                string displayName = string.Empty;
                ArmorCategory armorCategory = ArmorCategory.Light;
                int weight = 0;
                ArmorTypeId key = ArmorTypeId.Unspecified;
                // splits string by ',' to access comma separated values
                string[] lineParts = line.Split(',');
                // for every comma separated value per line
                for (int i = 0; i < lineParts.Length; i++)
                {
                    // if index 0
                    if (i == 0)
                    {
                        // set key to armortypeid based on string
                        switch (lineParts[i])
                        {
                            case "Leather":
                                key = ArmorTypeId.Leather;
                                break;
                            case "StuddedLeather":
                                key = ArmorTypeId.StuddedLeather;
                                break;
                            case "Hide":
                                key = ArmorTypeId.Hide;
                                break;
                            case "ChainShirt":
                                key = ArmorTypeId.ChainShirt;
                                break;
                            case "ChainMail":
                                key = ArmorTypeId.ChainMail;
                                break;
                            case "ScaleMail":
                                key = ArmorTypeId.ScaleMail;
                                break;
                            case "Plate":
                                key = ArmorTypeId.Plate;
                                break;
                        }
                    }
                    // if index 1 set display name
                    if (i == 1)
                    {
                        displayName = lineParts[i];
                    }
                    // if index 2 set armor category based on string
                    if (i == 2)
                    {
                        switch (lineParts[i])
                        {
                            case "Light":
                                armorCategory = ArmorCategory.Light;
                                break;
                            case "Medium":
                                armorCategory = ArmorCategory.Medium;
                                break;
                            case "Heavy":
                                armorCategory = ArmorCategory.Heavy;
                                break;
                        }
                    }
                    // if index 3 set weight
                    if (i == 3)
                    {
                        weight = int.Parse(lineParts[i]);
                    }
                }

                // add Key and Value to dictionary, create ArmorType
                dict.Add(key, new ArmorType(displayName, armorCategory, weight));
            }
            //return dict
            return dict;
        }
        // function to generate all monster types from file
        static List<MonsterType> GenerateMonstersFromFile(string[] readFile)
        {

            // store temp variables for a monster type
            string currentName = string.Empty;
            string currentDescription = string.Empty;
            string currentAlignment = string.Empty;
            string currentHpRoll = string.Empty;
            string currentArmorClass = string.Empty;
            ArmorTypeId currentArmorType = ArmorTypeId.Unspecified;

            // list of monster types
            List<MonsterType> monsterTypes = [];

            // counter to keep track of if we're in a specific monster types block or at the next
            int monsterLineCounter = 0;
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

                //armor class and armor type
                if (monsterLineCounter == 4)
                {
                    // pattern for armor class and armor type
                    string armorPattern = @"(\d+) (\(.*\))?";
                    //getting matches for the line
                    Match match = Regex.Match(line, armorPattern);
                    // check if armor type exists
                    if (!string.IsNullOrWhiteSpace(match.Groups[2].Value))
                    {
                        // get the armor type as string and trim parentheses
                        string armorTypeString = match.Groups[2].Value.Trim(['(', ')']);
                        // get the first part of armor type
                        string[] armorTypeParts = armorTypeString.Split(',');

                        // set armor type
                        switch (armorTypeParts[0].ToLower())
                        {
                            // to natural armor
                            case "natural armor":
                                currentArmorType = ArmorTypeId.Natural;
                                break;
                            // to leather armor
                            case "leather":
                                currentArmorType = ArmorTypeId.Leather;
                                break;
                            // to studded leather
                            case "studded leather":
                                currentArmorType = ArmorTypeId.StuddedLeather;
                                break;
                            // to hide
                            case "hide":
                                currentArmorType = ArmorTypeId.Hide;
                                break;
                            // to chain shirt
                            case "chain shirt":
                                currentArmorType = ArmorTypeId.ChainShirt;
                                break;
                            // to chain mail
                            case "chain mail":
                                currentArmorType = ArmorTypeId.ChainMail;
                                break;
                            // to scale mail
                            case "scale mail":
                                currentArmorType = ArmorTypeId.ScaleMail;
                                break;
                            // to plate
                            case "plate":
                                currentArmorType = ArmorTypeId.Plate;
                                break;
                            // to other if no cases are met
                            default:
                                currentArmorType = ArmorTypeId.Other;
                                break;
                        }
                    }
                    // otherwise set it to unspecified
                    else
                    {
                        currentArmorType = ArmorTypeId.Unspecified;
                    }

                    // set current armor class to value
                    currentArmorClass = match.Groups[1].Value;
                }

                //check if at the end of a monster
                if (line.Length == 0)
                {
                    // reset counter to 0
                    monsterLineCounter = 0;

                    // create and add monster type to list
                    // creating the monster with the temp variables from before
                    monsterTypes.Add(
                        new MonsterType(
                            currentName,
                            currentDescription,
                            currentAlignment,
                            currentHpRoll,
                            int.Parse(currentArmorClass),
                            currentArmorType)
                        );
                    // reset temp variables
                    currentName = string.Empty;
                    currentDescription = string.Empty;
                    currentAlignment = string.Empty;
                    currentHpRoll = string.Empty;
                    currentArmorClass = string.Empty;
                    currentArmorType = ArmorTypeId.Unspecified;
                }

            }
            // return list
            return monsterTypes;
        }
    }
}
