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

            DrawEmptyFrames(totalFrames);
            totalFrames = random.Next(1, 11);
            DrawEmptyFrames(totalFrames);

            Console.ReadLine();
        }

        static void DrawEmptyFrames(int frames)
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
                    //print every line of empty frame
                    Console.Write(emptyFrameLines[i]);
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

    }
}
