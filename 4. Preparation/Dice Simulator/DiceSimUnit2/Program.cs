namespace DiceSimUnit2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MakeDiceRoll("d6");
            MakeDiceRoll("2d4");
            MakeDiceRoll("d8+12");
            MakeDiceRoll("d8-12");
            MakeDiceRoll("2d4-1");
            MakeDiceRoll("2d4+1");

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
        static int DiceRoll(string diceNotation)
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
