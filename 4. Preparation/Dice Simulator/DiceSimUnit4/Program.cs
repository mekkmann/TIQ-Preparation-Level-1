using System.Text.RegularExpressions;

namespace DiceSimUnit4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Simulate();
        }

        static void DrawArt(string path, ConsoleColor foregroundColor, string toBeReplaced = "", string replaceWith = "")
        {
            // set console foregroundcolor
            Console.ForegroundColor = foregroundColor;
            //read file and store all lines 
            string[] logoLines = File.ReadAllLines(path);
            // for each line in the string[]
            foreach (var line in logoLines)
            {
                // check if we have the strings needed to replace anything
                if (!string.IsNullOrWhiteSpace(toBeReplaced) && !string.IsNullOrWhiteSpace(replaceWith))
                {
                    // replace either toBeReplaced+1whitespace or just toBeraplaced.
                    // dependent on the length of replaceWith
                    // print line
                    Console.WriteLine(line.Replace(replaceWith.Length == 2 ? $"{toBeReplaced} " : toBeReplaced, replaceWith.ToString()));
                }
                // otherwise
                else
                {
                    //print line
                    Console.WriteLine(line);
                }
            }
            // stop execution for 0.5s to simulate it taking time
            Thread.Sleep(500);
            // spacing
            Console.WriteLine();
            // reset console foreground color back to normal
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        // function for running the program
        static void Simulate()
        {
            // draw the logo
            DrawArt("DiceLogo.txt", ConsoleColor.Magenta);
            // store if we've got good input
            bool isStandardNotation = false;
            // store actual input
            string? notationInput = string.Empty;
            // print message to user
            Console.WriteLine("Enter desired roll in standard dice notation:");
            // while we don't have good input
            while (!isStandardNotation)
            {
                // get new input
                notationInput = Console.ReadLine();
                //spacing
                Console.WriteLine();
                // try or throw exception and go to catch
                try
                {
                    // will throw exception if not standard
                    IsStandardDiceNotation(notationInput);
                    // will only be called if an exception is not thrown
                    // set to true, and exit while loop
                    isStandardNotation = true;
                }
                catch (ArgumentException ae) // catch exception
                {
                    // tell the player to try again and then starts over with the loop
                    Console.WriteLine($"{ae.Message} Try again:");
                }
            }

            // when the user has exited the loop, roll dice and display total result and each dice roll
            Console.WriteLine($"You rolled {DiceRoll(notationInput)}.\n");

            // do this loop at least once
            do
            {
                // print message to player asking for repeat, new roll or quit
                // key to press is signified by parentheses around letter
                Console.WriteLine("Do you want to (r)epeat, enter a (n)ew roll or (q)uit?");
                // read the players input
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                // if player pressed 'r'
                if (keyInfo.Key == ConsoleKey.R)
                {
                    //roll same dice notation as before and display total result and each dice roll
                    Console.WriteLine($"You rolled {DiceRoll(notationInput)}.\n");
                }
                /// if player pressed 'n'
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    // reset good input to false
                    isStandardNotation = false;
                    // reset actualinput to empty
                    notationInput = string.Empty;
                    // print message to user
                    Console.WriteLine("Enter desired roll in standard dice notation:");
                    // while we don't have good input
                    while (!isStandardNotation)
                    {
                        // get new input
                        notationInput = Console.ReadLine();
                        //spacing
                        Console.WriteLine();
                        // try or throw exception and go to catch
                        try
                        {
                            // will throw exception if not standard
                            IsStandardDiceNotation(notationInput);
                            // will only be called if an exception is not thrown
                            // set to true, and exit while loop
                            isStandardNotation = true;
                        }
                        catch (ArgumentException ae) // catch exception
                        {
                            // tell the player to try again and then starts over with the loop
                            Console.WriteLine($"{ae.Message} Try again:");
                        }
                    }

                    // when the user has exited the loop, roll dice and display total result and each dice roll
                    Console.WriteLine($"You rolled {DiceRoll(notationInput)}.\n");
                }
                // if any other key was pressed
                else
                {
                    // exit program
                    return;
                }
                // the return above cancels the do-while below 
            } while (true);
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
                if (diceSides == 4) // if dice is d4
                {
                    // call function to draw dice
                    DrawArt("D4.txt", ConsoleColor.Red, "X", randomNumber.ToString());
                }
                else if (diceSides == 6) // if dice is d6
                {
                    // call function to draw dice
                    DrawArt("D6.txt", ConsoleColor.Green, "X", randomNumber.ToString());
                }
                else // else
                {
                    // print normal text instead of dice
                    Console.WriteLine($"{OrdinalNumber(i + 1)} roll is: {randomNumber}");
                }
            }
            //spacing
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
            try
            {
                IsStandardDiceNotation(diceNotation);
            }
            catch (ArgumentException ae)
            {
                Console.WriteLine($"Can't throw {diceNotation} ... {ae.Message}");
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
        static void IsStandardDiceNotation(string text)
        {
            // split input by 'd'
            string[] textParts = text.Split('d');
            // if the length of textParts i equal to or less than 1
            if (textParts.Length <= 1)
            {
                // throw exception with description
                throw new ArgumentException($"Roll description is not in standard dice notation.");
            }

            // to store number of rolls
            int numberOfRolls;
            // try to execute some code and if we get an exception, go to catch
            try
            {
                // if the first element of textParts is longer than 0
                // if it is longer than 0, try to parse it (it will throw an exception if it can't)
                // and if it can parse set numberOfRolls to parsed value.
                // if it is not longer than 0, set numberOfRolls to 1
                numberOfRolls = textParts[0].Length > 0 ? int.Parse(textParts[0]) : 1;
            }
            catch // catch the exception
            {
                // throw exception with description
                throw new ArgumentException($"Number of rolls ({textParts[0]}) is not an integer.");
            }

            // if number of rolls is less than or equal to 0
            if (numberOfRolls <= 0)
            {
                // throw exception with description
                throw new ArgumentException($"Number of rolls ({textParts[0]}) has to be positive.");
            }

            // split element at textParts 1 (what comes after the d) by either '+' or '-'
            textParts = textParts[1].Split(['+', '-']);

            // to store dice sides
            int diceSides;
            // try to execute some code and if we get an exception, go to catch
            try
            {
                // if we can parse element at index 0, set diceSides to parsed value. Otherwise throw excption
                diceSides = int.Parse(textParts[0]);
            }
            catch // catch exception
            {
                // throw exception with description
                throw new ArgumentException($"Number of dice sides ({textParts[0]}) is not an integer.");
            }

            // if diceSides is less than or equal to 0
            if (diceSides <= 0)
            {
                // throw exception with description               
                throw new ArgumentException($"Number of sides ({textParts[0]}) has to be positive.");
            }

            // if textParts is longer than 1
            if (textParts.Length > 1)
            {
                // try to execute some code and if we get an exception, go to catch
                try
                {
                    // to store fixed bonus
                    // if we can parse element at index 1, discard parsed value. Otherwise throw excption
                    _ = int.Parse(textParts[1]);
                }
                catch // catch exception
                {
                    // throw exception with description
                    throw new ArgumentException($"Fixed bonus ({textParts[1]}) is not an integer.");
                }
            }
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
