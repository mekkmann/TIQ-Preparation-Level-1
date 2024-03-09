using System.Text.RegularExpressions;

namespace DiceSimUnit4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //// Should throw dice
            //MakeDiceRoll("2d6");

            //// should throw special exceptions
            //MakeDiceRoll("34");
            //MakeDiceRoll("-12");
            //MakeDiceRoll("ad6");
            //MakeDiceRoll("-3d6");
            //MakeDiceRoll("0d6");
            //MakeDiceRoll("d+");
            //MakeDiceRoll("2d-4");
            //MakeDiceRoll("2d2.5");
            //MakeDiceRoll("2d$");


            //// Should display dice notations and amount of rolls
            //DiceNotationsAndRollsInText("To use the magic potion of Dragon Breath, first roll d8. If you roll 2 or higher, you manage to open the potion. Now roll 5d4+5 to see how many seconds the spell will last. Finally, the damage of the flames will be 2d6 per second.");

            Simulate();
        }
        static void Simulate()
        {
            Console.WriteLine("DICE SIMULATOR\n");
            bool isStandardNotation = false;
            string? notationInput = string.Empty;
            Console.WriteLine("Enter desired roll in standard dice notation:");
            while (!isStandardNotation)
            {
                notationInput = Console.ReadLine();
                Console.WriteLine();
                isStandardNotation = IsStandardDiceNotation(notationInput);
                if (!isStandardNotation)
                {
                    Console.WriteLine("You did not use standard dice notation. Try again:");
                }
            }

            Console.WriteLine($"You rolled {DiceRoll(notationInput)}.");
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
                // generate random number between 1 - diceSides
                int randomNumber = random.Next(1, diceSides + 1);
                // add the randomNumber to the total
                total += randomNumber;
                // print result of every roll
                Console.WriteLine($"{OrdinalNumber(i + 1)} roll is: {randomNumber}");
            }
            Console.WriteLine();

            // return the result of the roll
            return total;
        }

        // function that splits a string and checks the dice notation
        // and then runs the DiceRoll function above
        static int DiceRoll(string diceNotation)
        {
            Console.WriteLine("Simulating ...\n");
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
            Match match = Regex.Match(text, @"^(-)?([\w{1,2}])?(\w)?(.{1,3})?(\d{1,2})?$");

            // if the regex matches 
            if (match.Success)
            {
                // try performing code and if successful, skip and if exception is thrown go to catch
                // checks if the notation is completely wrong
                try
                {
                    // if text doesn't contain 'd' AND '-' is in string OR if text doesn't contain 'd'
                    if (!text.Contains('d') && match.Groups[1].Value == "-" || !text.Contains('d'))
                    {
                        // throw a new exception with an error message
                        throw new ArgumentException($"Can't throw {text} ... Roll description is not in standard dice notation.");
                    }
                }
                // catch the exception
                catch (ArgumentException ae)
                {
                    // print the error message
                    Console.WriteLine(ae.Message);
                    // return false
                    return false;
                }

                // try performing code and if successful, skip and if exception is thrown go to catch
                // check if dice sides is an integer or negative
                try
                {
                    // check if parsing is possible
                    if (int.TryParse(match.Groups[4].Value, out int diceSides))
                    {
                        // if the value contains '-'
                        if (match.Groups[4].Value.Contains('-'))
                        {
                            // throw a new exception with an error message
                            throw new ArgumentException($"Can't throw {text} ... Dice sides ({match.Groups[4].Value[0]}) is not an integer");
                        }
                        // if diceSides equals 0
                        if (diceSides == 0)
                        {
                            // throw a new exception with an error message
                            throw new ArgumentException($"Can't throw {text} ... Dice sides ({diceSides}) needs to be positive");
                        }
                    }
                    else // if where dice sides should be is not an int
                    {
                        // check that these two groups aren't null, empty or whitespace
                        if (!string.IsNullOrWhiteSpace(match.Groups[5].Value) && !string.IsNullOrWhiteSpace(match.Groups[7].Value))
                        {
                            // throw a new exception with an error message
                            throw new ArgumentException($"Can't throw {text} ... Dice sides ({match.Groups[5].Value + match.Groups[7].Value}) is not an integer");
                        }
                        // check that this group isn't null, empty or whitespace
                        if (!string.IsNullOrWhiteSpace(match.Groups[4].Value))
                        {
                            // throw a new exception with an error message
                            throw new ArgumentException($"Can't throw {text} ... Dice sides ({match.Groups[4]}) is not an integer");
                        }
                    }
                }
                // catch the exception
                catch (ArgumentException ae)
                {
                    // print the error message
                    Console.WriteLine(ae.Message);
                    // return false
                    return false;
                }

                // try performing code and if successful, skip and if exception is thrown go to catch
                // check if number of rolls is an integer or negative
                try
                {
                    // try to parse
                    if (int.TryParse(match.Groups[2].Value, out int numberOfRolls))
                    {
                        // if any of these are true
                        if (match.Groups[1].Value == "-" || numberOfRolls == 0)
                        {
                            // throw a new exception with an error message
                            throw new ArgumentException($"Can't throw {text} ... Number of rolls ({match.Groups[1].Value + numberOfRolls}) needs to be positive");
                        }
                    }
                    else // if where number of rolls should be is not an int
                    {
                        // throw a new exception with an error message
                        throw new ArgumentException($"Can't throw {text} ... Number of rolls ({match.Groups[2].Value}) is not an integer");
                    }
                }
                // catch the exception
                catch (ArgumentException ae)
                {
                    // print the error message
                    Console.WriteLine(ae.Message);
                    // return false
                    return false;
                }


            }
            //return true
            return true;
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

        // function that returns ordinal number from int
        static string OrdinalNumber(int number)
        {
            //if number is over 10
            if (number > 10)
            {
                // get the second to last digit by dividing number by 10 and then taking that value and use modulo 10
                var secondToLastDigit = (number / 10) % 10;

                // if the second to last digit is 1
                if (secondToLastDigit == 1)
                {
                    // return Xth
                    return $"{number}th";
                }
            }
            // get the last digit of the number with modulo 10
            var lastDigit = number % 10;

            // if last digit of number is 1
            if (lastDigit == 1)
            {
                //return Xst
                return $"{number}st";
            }
            // if last digit of number is 2
            if (lastDigit == 2)
            {
                //return Xnd
                return $"{number}nd";
            }
            // if last digit of number is 3
            if (lastDigit == 3)
            {
                // return Xrd
                return $"{number}rd";
            }
            // if no conditions are met return Xth
            return $"{number}th";
        }
    }
}
