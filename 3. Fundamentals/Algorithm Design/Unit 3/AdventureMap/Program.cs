using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace AdventureMap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map(60, 20, "Adventure Map");

            // to keep console open
            Console.ReadLine();
            static void Map(int width, int height, string title)
            {
                Random random = new();

                //Helper Variables
                Vector2 titleStart = GenerateTitleStartPosition(width, title);
                Vector2 horizontalPathStart = GenerateRandomHorizontalPathStart(height);
                int endOfFirstQuarter = width / 4;
                int startOfLastQuarter = endOfFirstQuarter * 3 - 1;
                List<string> treeSymbols = ["T", "@", "%", ")", "("];

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        //Border
                        bool verticalBorder = x == 0 || x == width - 1;
                        bool horizontalBorder = y == 0 || y == height - 1;

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        
                        if (verticalBorder && horizontalBorder)
                        {
                            Console.Write("+");
                            continue;
                        }
                        if (verticalBorder)
                        {
                            Console.Write("|");
                            continue;
                        }
                        if (horizontalBorder)
                        {
                            Console.Write("-");
                            continue;
                        }

                        //Title
                        if (x == titleStart.X && y == titleStart.Y)
                        {
                            Console.Write(title);
                            x += title.Length - 1;
                            continue;
                        }

                        //Forest
                        if (x <= endOfFirstQuarter)
                        {
                            bool writeSymbol = false;
                            double randomDouble = random.NextDouble();
                            if (x >= endOfFirstQuarter / 3 * 2 && randomDouble < 0.3f)
                            {
                                writeSymbol = true;

                            }
                            if (x <= endOfFirstQuarter/3 * 2 && randomDouble < 0.5f)
                            {
                                writeSymbol = true;

                            }
                            if (x <= endOfFirstQuarter/3 && randomDouble < 0.9f)
                            {
                                writeSymbol = true;
                            }
                            if (writeSymbol)
                            {
                                string randomTreeSymbol = treeSymbols[random.Next(treeSymbols.Count)];
                                Console.ForegroundColor= ConsoleColor.Green;
                                Console.Write(randomTreeSymbol);
                                continue;
                            }
                        }

                        // no conditions met, draw ground
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.Write("l");
                    }
                    //end of row
                    Console.WriteLine();
                }

                static Vector2 GenerateTitleStartPosition(int width, string title, int yStart = 1)
                {
                    int titleLength = title.Length;
                    int x = (width - titleLength) / 2;
                    return new Vector2(x, yStart);
                }

                static Vector2 GenerateRandomHorizontalPathStart(int height)
                {
                    Random random = new();
                    int y = random.Next(2, height - 2);
                    return new Vector2(1, y);
                }
            }
        }
    }
}
