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
                        //Border Corners
                        if (x == 0 && y == 0 ||
                            x == width - 1 && y == height - 1 ||
                            x == 0 && y == height - 1 ||
                            x == width - 1 && y == 0
                            )
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("+");
                            continue;
                        }
                        //Vertical Borders
                        if (x == 0 && y != 0 || x == width - 1 && y != height - 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("|");
                            continue;
                        }
                        //Horizontal Borders
                        if (y == 0 && x != 0 || y == height - 1 && x != width - 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("-");
                            continue;
                        }

                        //Forest
                        if (x <= (width - 1) / 4 && random.NextDouble() < 0.5)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("T");
                            continue;
                        }

                        // no conditions met
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
