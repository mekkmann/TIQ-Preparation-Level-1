using System.Text.RegularExpressions;

namespace RegexUnit4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //// instantiate a new HttpClient
            //HttpClient httpClient = new();

            //// get the HTML of the website
            //string htmlCode = httpClient.GetStringAsync(@"https://store.steampowered.com/app/275850/No_Mans_Sky/").Result;

            //// check if there's a regex match
            //Match match = Regex.Match(htmlCode, @"game_review_summary .*"">(.*?)<");

            //// if there's a match, print the title and rating
            //if (match.Success)
            //{
            //    Console.WriteLine($"The rating of the game No Man's Sky is {match.Groups[1].Value}");
            //}

            // instantiate new HttpClient
            HttpClient httpClient = new HttpClient();

            // array of string to store the urls to some games
            string[] urls =
            {
                "https://store.steampowered.com/app/275850/No_Mans_Sky/",
                "https://store.steampowered.com/app/427520/Factorio/",
                "https://store.steampowered.com/app/305660/Infect_and_Destroy/"
            };

            foreach (string url in urls)
            {
                // get the html of the website
                string htmlCode = httpClient.GetStringAsync(url).Result;

                // see if there's a regex match for the title
                Match match = Regex.Match(htmlCode, @"<title>(.*?) on Steam<\/title>");
                // if there's a match, print the title
                if (match.Success)
                {
                    Console.WriteLine($"\n{match.Groups[1].Value.ToUpper()}: ");
                }

                //recent reviews

                //see if there's a regex match for recent reviews
                match = Regex.Match(htmlCode, @"Recent Reviews.*\n.*\n.*game_review_summary.*?>(.*?)<");
                // if there's a match, print the recent rating
                if (match.Success)
                {
                    Console.WriteLine($"Recent reviews: {match.Groups[1].Value}");
                }

                //all reviews

                //see if there's a regex match for all-time reviews
                match = Regex.Match(htmlCode, @"All Reviews.*\n.*\n.*game_review_summary.*?>(.*?)<");
                // if there's a match, print the all-time rating
                if (match.Success)
                {
                    Console.WriteLine($"All reviews: {match.Groups[1].Value}");
                }
            }
            //to keep the console open
            Console.ReadLine();
        }
    }
}
