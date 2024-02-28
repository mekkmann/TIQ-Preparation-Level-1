namespace OlympicGamesQuiz
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new();
            Dictionary<int, string> hostCountries = new(){ 
                { 1896, "Greece" },
                { 1900, "France" },
                { 1904, "United States" },
                { 1908, "United Kingdom" }
            };
            int year = 1896 + random.Next(hostCountries.Count) * 4;
            string country = hostCountries[year];

            Console.WriteLine("MISSION 1");
            Console.WriteLine($"Which country hosted the Summer Olympic Games in {year}?");
            string? inputCountry = Console.ReadLine();

            if ( inputCountry == country ) 
            {
                Console.WriteLine("\nCorrect!\n");
            } else
            {
                Console.WriteLine($"\nIncorrect. It was {country}\n");
            }


            Console.WriteLine("MISSION 2");
            SortedList<string, string> countries = new()
            {
                { "Sweden", "Stockholm" },
                { "Denmark", "Copenhagen" },
                { "Norway", "Oslo" },
                { "Finland", "Helsinki" }
            };
            int index = random.Next(countries.Count);
            string country2 = countries.Keys[index];
            string capital = countries[country2];
            Console.WriteLine($"What is the capital of {country2}");
            string? inputCapital = Console.ReadLine();

            if (inputCapital == capital)
            {
                Console.WriteLine("\nCorrect!\n");
            }
            else
            {
                Console.WriteLine($"\nIncorrect. It is {capital}\n");
            }


            Console.WriteLine("MISSION 3");
            Dictionary<string, int> rankings = new() { { "test", 1 } };
            Console.Write("Who won this round? ");
            string? winnerInput = Console.ReadLine();
            if ( winnerInput != null )
            {
                string lowercaseInput = winnerInput.ToLower();
                if (rankings.ContainsKey(lowercaseInput))
                {
                    rankings[lowercaseInput.ToLower()]++;
                } else
                {
                    rankings[lowercaseInput] = 1;
                }

            }

            Console.WriteLine("\nRANKINGS");
            foreach(var rank in rankings)
            {
                Console.WriteLine($"{rank.Key} {rank.Value}");
            }

            // to keep console open
            Console.ReadLine();
        }
    }
}
