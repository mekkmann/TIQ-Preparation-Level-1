using System.Text.RegularExpressions;

namespace DiceSimUnit2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Should throw dice
            MakeDiceRoll("d6");
            MakeDiceRoll("2d4");
            MakeDiceRoll("d8+12");
            MakeDiceRoll("d8-12");
            MakeDiceRoll("2d4-1");
            MakeDiceRoll("2d4+1");

            // Should not throw dice
            MakeDiceRoll("34");
            MakeDiceRoll("ad");
            MakeDiceRoll("33d4*2");

            // Should display dice notations and amount of rolls
            DiceNotationsAndRollsInText("To use the magic potion of Dragon Breath, first roll d8. If you roll 2 or higher, you manage to open the potion. Now roll 5d4+5 to see how many seconds the spell will last. Finally, the damage of the flames will be 2d6 per second.");

            // to keep console open
            Console.ReadLine();
        }

        // function that calculates the total of a roll w/ or w/o the fixedBonus
        static int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus = 0)
        {
            // instantiate a Random for our calculations
            Random random = new();
            // set total to fixedBonus immediately, because it will always be added
            int total = fixedBonus;
            // for every roll
            for (int i = 0; i < numberOfRolls; i++)
            {
                // add a number from 1-diceSides to the total
                total += random.Next(1, diceSides + 1);
            }
            // return the result of the roll
            return total;
        }

        // function that splits a string and checks the dice notation
        // and then runs the DiceRoll function above
        static int? DiceRoll(string diceNotation)
        {
            // splits the diceNotation string based on delimiters
            string[] notationParts = diceNotation.Split(['d', '-', '+']);
            // if where the number of rolls should be is null or whitespace, default to 1 else get the number of rolls
            int numberOfRolls = string.IsNullOrWhiteSpace(notationParts[0]) ? 1 : int.Parse(notationParts[0]);
            // parsing the dice sides part of the string array
            int diceSides = int.Parse(notationParts[1]);

            // if the split notation contains more than 2 parts
            if (notationParts.Length > 2)
            {
                // parse the fixed bonus as a positive int
                int fixedBonus = int.Parse(notationParts[2]);
                // if the dicenotation contains '-'
                if (diceNotation.Contains('-'))
                {
                    // make the int negative
                    fixedBonus *= -1;
                }

                // return the result of diceroll including a fixed bonus
                return DiceRoll(numberOfRolls, diceSides, fixedBonus);
            }
            else
            {
                // return the result of diceroll without any bonus
                return DiceRoll(numberOfRolls, diceSides);
            }
        }

        //function that does a diceroll 10 times with a diceNotation
        static void MakeDiceRoll(string diceNotation)
        {
            // if not standard dice notation
            if (!IsStandardDiceNotation(diceNotation))
            {
                // write error message
                Console.WriteLine($"Can't throw {diceNotation}, it's not in standard dice notation.");
                // early return to stop dice from being thrown
                return;
            }
            // prints what diceNotation we're rolling
            Console.Write($"Throwing {diceNotation} ... ");
            for (int i = 0; i < 10; i++)
            {
                //simulating 10 rolls and prints the results
                Console.Write(" " + DiceRoll($"{diceNotation}"));
            }
            // prints new line for separation
            Console.WriteLine();
        }

        // function that checks if a string is in standard dice notation
        static bool IsStandardDiceNotation(string text)
        {
            // regex match with standard dice notation
            Match match = Regex.Match(text, @"^(\d{1,2})?d(\d{1,2})([+-])?(\d{1,2})?$");
            // if the regex matches
            if (match.Success)
            {
                // return true;
                return true;
            }
            // if the regex doesn't match, return false
            return false;
        }

        //function that checks how many rolls are in a dice notation
        static int RollsInDiceNotation(string diceNotation)
        {
            // regex match to check for dice rolls
            Match match = Regex.Match(diceNotation, @"(\d{0,2})?d\d");

            // if where the dice roll should be is null or whitespace return 1 else return parsed dice rolls
            return string.IsNullOrWhiteSpace(match.Groups[1].Value) ? 1 : int.Parse(match.Groups[1].Value);
        }

        // function that counts and prints amount of dice notations and total rolls in a text
        static void DiceNotationsAndRollsInText(string text)
        {
            // regex pattern 
            string regexPattern = @"(\d{1,2})?d(\d{1,2})([+-])?(\d{1,2})?";
            // regex match to check for dice notations
            Match match = Regex.Match(text, regexPattern);
            // regex count to count all dice notations
            int amountOfNotations = Regex.Count(text, regexPattern);

            // keep track of total dice rolls
            int totalRolls = 0;
            // for every match
            for (var i = 0; i < amountOfNotations; i++)
            {
                // check how many rolls are in a notation and add to totalRolls
                totalRolls += RollsInDiceNotation(match.Groups[0].Value);

                // set match to the next match
                match = match.NextMatch();
            }
            // print total amount of notations
            Console.WriteLine($"{amountOfNotations} standard dice notations present.");
            // print total amount of rolls
            Console.WriteLine($"The player will have to perform {totalRolls} rolls.");
        }
    }
}
