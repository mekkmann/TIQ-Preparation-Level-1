namespace Bowling
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // initiate a new random
            Random random = new Random();
            // set totalFrames to a random number between 1-10
            int totalFrames = random.Next(1, 11);

            //DrawFrames(totalFrames);
            //totalFrames = random.Next(1, 11);
            totalFrames = 5;
            DrawFrames(totalFrames);

            Console.ReadLine();
        }

        static void DrawFrames(int frames)
        {
            //path to emptyFrame ascii art
            string pathToEmptyFrame = "EmptyFrame.txt";
            //path to closingFrame ascii art
            string pathToClosingFrame = "FrameClosing.txt";

            // read file and store all lines in an array
            string[] emptyFrameLines = File.ReadAllLines(pathToEmptyFrame);
            string[] closingFrameLines = File.ReadAllLines(pathToClosingFrame);

            //for each line in emptyFrameLines
            for (var i = 0; i < emptyFrameLines.Length; i++)
            {
                // for every totalFrame
                for (int j = 0; j < frames; j++)
                {
                    //if i equals 1 (where we write the score)
                    if (i == 1)
                    {
                        // roll bowling ball and get results oh rolls
                        int[] rolls = Roll();
                        // translate results to symbols
                        string[] rollSymbols = TranslateRollToSymbols(rolls);
                        // if strike, length will be 1
                        if (rolls.Length == 1)
                        {
                            // print line with symbols
                            Console.Write($"| |{rollSymbols[0]}| ");
                        }
                        // if not a strike, length will be over 1
                        else
                        {
                            // print line with symbols
                            Console.Write($"| |{rollSymbols[0]}|{rollSymbols[1]}");
                        }
                    }
                    // if we shouldn't write the score
                    else
                    {
                        // print the rest of the frame
                        Console.Write(emptyFrameLines[i]);
                    }

                    // if at the end of loop
                    if (j == frames - 1)
                    {
                        // print closing part of the empty frame
                        Console.Write(closingFrameLines[i]);
                    }
                }
                //spacing
                Console.WriteLine();
            }
        }

        static int[] Roll()
        {
            Random random = new Random();

            int totalPins = 10;
            int firstRoll = random.Next(0, totalPins + 1);
            if (firstRoll == 10)
            {
                return [10];
            }
            int secondRoll = random.Next(0, totalPins + 1 - firstRoll);

            return [firstRoll, secondRoll];
        }

        static string[] TranslateRollToSymbols(int[] roll)
        {
            if (roll.Length == 1)
            {
                return ["X"];
            }

            string symbol1 = roll[0].ToString();
            string symbol2 = roll[1].ToString();

            switch (roll[0])
            {
                case 0:
                    symbol1 = "-";
                    break;
            }

            if (roll[1] == 0)
            {
                symbol2 = "-";
            }
            else if (roll[1] == 10 && roll[0] == 0 || roll[1] + roll[0] == 10)
            {
                symbol2 = "/";
            }

            return [symbol1, symbol2];
        }

    }
}
