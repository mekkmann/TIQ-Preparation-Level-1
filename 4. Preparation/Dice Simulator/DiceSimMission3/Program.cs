using System.Text.RegularExpressions;

namespace DiceSimMission3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MakeDiceRoll("1d6");
            MakeDiceRoll("2d8");
            MakeDiceRoll("3d6+8");
            MakeDiceRoll("1d4+4");

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

        // function that checks the standard dice notation with a Regex
        // and then runs the DiceRoll function above
        static int DiceRoll(string diceNotation)
        {
            // int initiated to 0 to store the result of the roll
            int result = 0;
            // a regex match for standard dice notation
            Match match = Regex.Match(diceNotation, @"^(\d)d(\d)\+?(\d)?$");

            // if the match is successful
            if (match.Success)
            {
                // if the dice notation has a fixed bonus
                if (!string.IsNullOrWhiteSpace(match.Groups[3].Value))
                {
                    // make the dice roll and set result
                    result = DiceRoll(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value));
                }
                else //if the dice notation is without a fixed bonus
                {
                    // make the dice roll and set result
                    result = DiceRoll(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value));
                }
            }

            // returns result
            return result;
        }

        //function that does a diceroll 10 times with a diceNotation
        static void MakeDiceRoll(string diceNotation)
        {
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
    }
}
