using System;
using System.Linq;
using System.IO;

namespace FilesMissions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string playerPath = "player-name.txt";
            string playerName = String.Empty;

            string backersPath = "backers.txt";
            string[] backers = null;

            
            if (File.Exists(playerPath))
            {
                playerName = File.ReadAllText(playerPath);
            }

            if (string.IsNullOrWhiteSpace(playerName))
            {
                Console.Write(
                    "Welcome to your biggest adventure yet!\n\n" +
                    "What is your name, traveler?\n" +
                    "> "
                    );
                string nameInput = Console.ReadLine();
                Console.WriteLine($"\n\nNice to meet you, {nameInput}!");
                File.WriteAllText(playerPath, nameInput);
                using (var writer = new StreamWriter(backersPath))
                {
                    writer.WriteLine(nameInput);
                }

            }
            else
            {
                Console.WriteLine($"Welcome back, {playerName}, let's continue!");

                if (File.Exists(backersPath))
                {
                    backers = File.ReadAllLines(backersPath);
                }

                if (backers.Contains(playerName))
                {
                    Console.WriteLine("You successfully enter Dr. Fred's secret laboratory and are greeted with a warm welcome for backing the game's Kickstarter!");
                } else
                {
                    Console.WriteLine("Unfortunately I cannot let you into Dr. Fred's secret laboratory.");
                }
                
            }

            // to keep the console open;
            Console.ReadLine();
        }
    }
}
