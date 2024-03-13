namespace LoopsPractice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int part1Value = 5;

            //1.1
            DrawLine(part1Value);
            //spacing
            Console.WriteLine("\n");
            //1.2
            DrawSquare(part1Value);
            //spacing
            Console.WriteLine("\n");
            //1.3
            DrawRightTriangle(part1Value);
            //spacing
            Console.WriteLine("\n");
            //1.4
            DrawParallelogram(part1Value);
            //spacing
            Console.WriteLine("\n");

            // to keep console open
            Console.ReadLine();
        }

        // PART 1 FUNCTIONS
        /// <summary>
        /// Draws a line of a specified length
        /// </summary>
        static void DrawLine(int length)
        {
            for (int i = 0; i < length; i++)
            {
                Console.Write("#");
            }
        }
        /// <summary>
        /// Draws a square with a specified side length
        /// </summary>
        static void DrawSquare(int side)
        {
            for (int i = 0; i <= side; i++)
            {
                for (int j = 0; j < side; j++)
                {
                    Console.Write("#");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Draw a right triangle with legs size n
        /// </summary>
        static void DrawRightTriangle(int longestLeg)
        {
            for (int i = 0; i <= longestLeg; i++)
            {
                int counter = 0;
                while (counter < i)
                {
                    Console.Write("#");
                    counter++;
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Draw a parallelogram with height n.
        /// </summary>
        static void DrawParallelogram(int height)
        {
            for (int i = height; i < 0; i--)
            {


            }
        }
    }
}
