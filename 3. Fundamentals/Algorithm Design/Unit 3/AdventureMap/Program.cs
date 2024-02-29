namespace AdventureMap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map(40, 20);

            // to keep console open
            Console.ReadLine();
            static void Map(int width, int height)
            {
                Random random = new();
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        //Border
                        bool verticalBorder = x == 0 || x == width - 1;
                        bool horizontalBorder = y == 0 || y == height - 1;
                        if (verticalBorder && horizontalBorder)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("+");
                            continue;
                        }
                        if (verticalBorder)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("|");
                            continue;
                        }
                        if (horizontalBorder)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("-");
                            continue;
                        }

                        //Forest
                        if (x <= width / 4 && random.NextDouble() < 0.5)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("T");
                            continue;
                        }

                        // no conditions met, draw ground
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("l");
                    }
                    //end of row
                    Console.WriteLine();
                }
            }
        }
    }
}
