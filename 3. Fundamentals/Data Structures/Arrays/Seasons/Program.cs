using System;

namespace Seasons
{
    internal class Program
    {
        enum Season
        {
            Spring,
            Summer,
            Fall,
            Winter
        }
        static void Main(string[] args)
        {
            string[] seasons= { "Spring", "Summer", "Fall", "Winter" };

            Console.WriteLine(CreateDayDescriptionWithEnum(1, Season.Spring, 1337));
            Console.WriteLine(CreateDayDescriptionWithArray(1, 0, 1337));

            // to keep console open
            Console.ReadLine();

            string CreateDayDescriptionWithEnum(int day, Season season, int year)
            {
                return $"{day} day of {season} in the year {year}";
            }

            string CreateDayDescriptionWithArray(int day, int season, int year)
            {
                return $"{day} day of {seasons[season]} in the year {year}";
            }
        }
    }
}
