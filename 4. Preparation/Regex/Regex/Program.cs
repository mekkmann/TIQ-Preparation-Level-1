namespace Regex
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //// MISSION 1 START
            //// initiate a string with the path to a file
            //string pathToFile = "MonsterManual.txt";

            //// read all lines of a file and store them in an array of string
            //string[] readFile = File.ReadAllLines(pathToFile);

            //for (int i = 0; i < readFile.Length - 1; i++)
            //{
            //    if (i % 7 == 0)
            //    {
            //        Console.WriteLine(readFile[i]);
            //    }
            //}

            ////to keep console open
            //Console.ReadLine();
            //// MISSION 1 END


            // MISSION 2 START
            // initiate a string with the path to a file
            string pathToFile = "MonsterManual.txt";

            // read all lines of a file and store them in an array of string
            string[] readFile = File.ReadAllLines(pathToFile);

            // a list of strings to store monster names
            List<string> nameList = [];

            // a list of bool to store if a monster can fly or not
            List<bool> canFly = [];

            // counter to keep track of if we're in a specific monsters block or are at the next
            int monsterLineCounter = 0;
            foreach (var line in readFile)
            {
                //name
                if (monsterLineCounter == 0)
                {
                    nameList.Add(line);
                }

                //can fly
                if (monsterLineCounter == 4)
                {
                    canFly.Add(line.Contains("fly"));
                }

                //check if at the end of a monster
                if (string.IsNullOrWhiteSpace(line))
                {
                    monsterLineCounter = 0;
                }
                else
                {
                    monsterLineCounter++;
                }
            }
            // print monster name and ability to fly to console
            Console.WriteLine("Monster in the manual are:");
            for (var i = 0; i < nameList.Count; i++)
            {
                Console.WriteLine($"{nameList[i]} - can fly: {canFly[i]}");
            }

            //to keep console open
            Console.ReadLine();
            // MISSION 2 END
        }
    }
}
