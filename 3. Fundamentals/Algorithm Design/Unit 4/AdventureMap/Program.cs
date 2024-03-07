using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace AdventureMap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map(75, 20, "Adventure Map");
            Map(60, 20, "Adventure Map");
            Map(40, 20, "Adventure Map");
            Map(20, 20, "Adventure Map");
            Map(20, 10, "Adventure Map");

            // to keep console open
            Console.ReadLine();
            static void Map(int width, int height, string title)
            {
                Random random = new();

                //Helper Variables
                int endOfFirstQuarter = width / 4;
                int startOfLastQuarter = endOfFirstQuarter * 3 - 1;
                List<string> treeSymbols = ["T", "@", "%", ")", "("];
                Vector2 titleStart = GenerateTitleStartPosition(width, title);
                List<int> riverStart = GenerateRiverStart(height, startOfLastQuarter);
                List<int> horizontalPathY = GenerateHorizontalPathY(height, width, riverStart);
                int roadIntersectionY = GenerateRoadIntersectionY(width, riverStart, horizontalPathY);

                //Draw the map
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

                        //Bridge Railings
                        if ((y == horizontalPathY[x] - 1 || y == horizontalPathY[x] + 1) && x > riverStart[horizontalPathY[x]] - 3 && x < riverStart[horizontalPathY[x]] + 5)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("=");
                            continue;
                        }

                        //HorizontalPath
                        if (y == horizontalPathY[x])
                        {

                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("#");
                            continue;
                        }

                        //RiverPath
                        int riverPathX = riverStart[y] - 5;
                        if (y > roadIntersectionY && x == riverPathX)
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write("#");
                            continue;
                        }

                        //River
                        if (x >= riverStart[y] && x < riverStart[y] + 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;

                            int direction = riverStart[y + 1] - riverStart[y];

                            if (direction == -1)
                            {
                                Console.Write("/");
                            }
                            else if (direction == 1)
                            {
                                Console.Write("\\");
                            }
                            else
                            {
                                Console.Write("|");
                            }

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
                            if (x <= endOfFirstQuarter / 3 * 2 && randomDouble < 0.5f)
                            {
                                writeSymbol = true;

                            }
                            if (x <= endOfFirstQuarter / 3 && randomDouble < 0.9f)
                            {
                                writeSymbol = true;
                            }
                            if (writeSymbol)
                            {
                                string randomTreeSymbol = treeSymbols[random.Next(treeSymbols.Count)];
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(randomTreeSymbol);
                                continue;
                            }
                        }

                        // no conditions met, draw ground
                        Console.Write(" ");
                    }
                    //end of row
                    Console.WriteLine();
                }

                static Vector2 GenerateTitleStartPosition(int width, string title, int yStart = 1)
                {
                    int x = (width - title.Length) / 2;
                    return new Vector2(x, yStart);
                }

                static List<int> GenerateRiverStart(int height, int startOfLastQuarter)
                {
                    Random random = new();
                    List<int> riverStart = [];

                    int currentRiverStart = startOfLastQuarter;

                    for (int y = 0; y < height; y++)
                    {
                        riverStart.Add(currentRiverStart);
                        int direction = random.Next(10);
                        if (direction == 0 && currentRiverStart > startOfLastQuarter)
                        {
                            currentRiverStart--;
                        }
                        if (direction == 2)
                        { 
                            currentRiverStart++;
                        }
                    }

                    return riverStart;
                }

                static List<int> GenerateHorizontalPathY(int height, int width, List<int> riverStart)
                {
                    Random random = new();
                    List<int> horizontalPathY = [];

                    int currentPathStepY = height / 2;

                    for (int x = 0; x < width; x++)
                    {
                        // if within map bounds and NOT on river 
                        if ((currentPathStepY - 1 >= 2 || currentPathStepY <= height - 2) && (x < riverStart[currentPathStepY] - 2 || x > riverStart[currentPathStepY] + 6))
                        {
                            int direction = random.Next(6);
                            if (direction == 0)
                            {
                                currentPathStepY--;
                            }
                            if (direction == 2)
                            {
                                currentPathStepY++;
                            }

                            //safeguards
                            if (currentPathStepY > height - 2)
                            {
                                currentPathStepY -= 2;
                            }
                            if (currentPathStepY < 2)
                            {
                                currentPathStepY = 2;
                            }

                        }

                        horizontalPathY.Add(currentPathStepY);
                    }

                    return horizontalPathY;
                }

                static int GenerateRoadIntersectionY(int width, List<int> riverStart, List<int> horizontalPathY)
                {
                    int roadIntersectionX = 0;
                    for (int x = 0; x < width; x++)
                    {
                        // if 5 away from river
                        if (x > riverStart[horizontalPathY[x]] - 5)
                        {
                            roadIntersectionX = x;
                            break;
                        }
                    }

                    int roadIntersectionY = horizontalPathY[roadIntersectionX];

                    return roadIntersectionY;
                }
            }
        }
    }
}
