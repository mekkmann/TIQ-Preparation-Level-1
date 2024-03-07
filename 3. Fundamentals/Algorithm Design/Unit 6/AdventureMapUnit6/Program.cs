using System.Numerics;

namespace AdventureMap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map(75, 20, "Adventure Map");
            //Map(60, 20, "Adventure Map");
            //Map(40, 20, "Adventure Map");
            //Map(20, 20, "Adventure Map");
            //Map(20, 10, "Adventure Map");
            //Map(30, 10, "Adventure Map");

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
                List<int> riverStart = GenerateCurve(height, startOfLastQuarter, 0.4f);
                List<int> wallStart = GenerateCurve(height, endOfFirstQuarter + 1, 0.1f);
                List<int> horizontalPathY = GenerateHorizontalPathY(height, width, riverStart, wallStart);
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

                        //Title
                        if (x == titleStart.X && y == titleStart.Y)
                        {
                            Console.Write(title);
                            x += title.Length - 1;
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
                            DrawCurve(riverStart, y, ConsoleColor.Blue);
                            continue;
                        }

                        //Turrets
                        if (y == horizontalPathY[x] - 1 || y == horizontalPathY[x] + 1)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;

                            if (x == wallStart[horizontalPathY[x]])
                            {
                                Console.Write("[");
                                continue;
                            }

                            if (x == wallStart[horizontalPathY[x]] + 1)
                            {
                                Console.Write("]");
                                continue;
                            }
                        }

                        //Wall
                        if (x >= wallStart[y] && x < wallStart[y] + 2)
                        {
                            DrawCurve(wallStart, y, ConsoleColor.DarkGray);
                            continue;
                        }



                        //Forest
                        if (x <= endOfFirstQuarter)
                        {
                            bool writeSymbol = false;
                            double randomDouble = random.NextDouble();
                            /*
                             * These next 3 if-statments check where we are on the map and 
                             * if we're close to the left boarder, the chance of spawning a tree is high
                             * and the further we get to the right, the chance of spawning decreases
                             */
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

                            // this part is actually writing the symbol (tree)
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


            }
            static Vector2 GenerateTitleStartPosition(int width, string title, int yStart = 1)
            {
                int x = (width - title.Length) / 2;
                return new Vector2(x, yStart);
            }

            static void DrawCurve(List<int> curve, int position, ConsoleColor consoleColor)
            {
                Console.ForegroundColor = consoleColor;

                //checks if the curve is going left, right or straight
                int direction = curve[position + 1] - curve[position];

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
            }

            static List<int> GenerateCurve(int height, int startPositionX, float curveChance)
            {
                Random random = new();
                List<int> curveValues = [startPositionX];

                int currentCurveStart = startPositionX;
                // for the height of the map
                for (int i = 0; i < height; i++)
                {
                    double curveChanceDouble = random.NextDouble();
                    // if the random double is less than curveChance, choose a random direction
                    if (curveChanceDouble < curveChance)
                    {
                        int direction = random.Next(1, 3);
                        if (direction == 2 && currentCurveStart > startPositionX)
                        {
                            currentCurveStart--;
                        }
                        if (direction == 1)
                        {
                            currentCurveStart++;
                        }
                    }
                    curveValues.Add(currentCurveStart);
                }

                return curveValues;
            }

            static List<int> GenerateHorizontalPathY(int height, int width, List<int> riverStart, List<int> wallStart)
            {
                Random random = new();
                List<int> horizontalPathY = [];

                int currentPathStepY = height / 2;

                //for the width of the map
                for (int x = 0; x < width; x++)
                {
                    horizontalPathY.Add(currentPathStepY);

                    // if on river
                    if (x >= riverStart[currentPathStepY] - 2 && x <= riverStart[currentPathStepY] + 6)
                    {
                        continue;
                    }

                    // if on wall
                    if (x >= wallStart[currentPathStepY] - 1 && x <= wallStart[currentPathStepY] + 2)
                    {
                        continue;
                    }

                    // move path
                    int direction = random.Next(7);
                    if (direction == 0 && currentPathStepY > 1) currentPathStepY--;
                    if (direction == 1 && currentPathStepY < height - 2) currentPathStepY++;

                }

                return horizontalPathY;
            }

            static int GenerateRoadIntersectionY(int width, List<int> riverStart, List<int> horizontalPathY)
            {
                int roadIntersectionX = 0;
                // for the width of the map
                for (int x = 0; x < width; x++)
                {
                    // if x is 5 away from the river
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
