namespace Regex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // initiate a string with the path to a file
            string pathToFile = "MonsterManual.txt";

            // read all lines of a file and store them in an array of string
            string[] readFile = File.ReadAllLines(pathToFile);

            for (int i = 0; i < readFile.Length - 1; i++)
            {
                if (i % 7 == 0)
                {
                    Console.WriteLine(readFile[i]);
                }
            }

            //to keep console open
            Console.ReadLine();
        }
    }
}
