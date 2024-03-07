namespace CityGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DrawCity(20, 30);

            // to keep console open
            Console.ReadLine();

            static void GenerateRoad(bool[,] roads, int startX, int startY, int direction)
            {
                int width = roads.GetLength(0);
                int height = roads.GetLength(1);

                while (startX >= 0 && startX < width && startY >= 0 && startY < height)
                {
                    roads[startX, startY] = true;

                    if (direction == 0) startX++;
                    if (direction == 1) startY++;
                    if (direction == 2) startX--;
                    if (direction == 3) startY--;
                }
            }

            static void GenerateIntersections(bool[,] roads, int x, int y)
            {
                Random random = new();

                for (int i = 0; i < 4; i++)
                {
                    if (random.NextDouble() < 0.7)
                    {
                        GenerateRoad(roads, x, y, i);
                    }
                }
            }

            static void DrawCity(int width, int height)
            {
                var roads = new bool[width, height];

                Random random = new();

                for (int i = 0; i < 4; i++)
                {
                    GenerateIntersections(roads, random.Next(width), random.Next(height));
                }

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (i == 0 || j == 0 || i == height - 1 || j == width - 1)
                        {
                            continue;
                        }
                        if (roads[j, i])
                        {
                            bool up = roads[j, i - 1];
                            bool down = roads[j, i + 1];
                            bool left = roads[j - 1, i];
                            bool right = roads[j + 1, i];


                            if (up && down && left && right)
                            {
                                Console.Write("╬");
                                continue;
                            }
                            if (up && down && left)
                            {
                                Console.Write("╣");
                                continue;
                            }
                            if (up && down && right)
                            {
                                Console.Write("╠");
                                continue;
                            }
                            if (up && left && right)
                            {
                                Console.Write("╩");
                                continue;
                            }
                            if (down && left && right)
                            {
                                Console.Write("╦");
                                continue;
                            }
                            if (up && left)
                            {
                                Console.Write("╝");
                                continue;
                            }
                            if (down && left)
                            {
                                Console.Write("╗");
                                continue;
                            }
                            if (up && right)
                            {
                                Console.Write("╚");
                                continue;
                            }
                            if (down && right)
                            {
                                Console.Write("╔");
                                continue;
                            }
                            if (up || down)
                            {
                                Console.Write("║");
                                continue;
                            }
                            if (left || right)
                            {
                                Console.Write("═");
                                continue;
                            }
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
