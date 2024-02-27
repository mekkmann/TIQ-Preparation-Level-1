using System;
using System.Runtime.InteropServices;

namespace FirstGame1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string playerName = "Mekkmann";
            Console.WriteLine($"Welcome to Dungeon World, {playerName}! " +
                $"We call upon you to oversee a team of adventurers in a series of " +
                $"battles that will decide the fate of the world itself!");

            string warriorName = "Hoarah Loux";
            string mageName = "Rogiera";

            Console.WriteLine("The party stared down the stone stairs into darkness. " +
                "\"We should've brought some torches with us,\" " +
                "remarked WARRIOR. MAGE turned around and replied, \"Worry not dear WARRIOR, let me shine some light for you,\" ".Replace("WARRIOR", warriorName).Replace("MAGE", mageName) +
                "as she cast a Continual light spell.");

            Console.WriteLine($"You are running the game on Windows: {RuntimeInformation.IsOSPlatform(OSPlatform.Windows)}");
            // to keep console from closing down immediately
            Console.ReadLine();
        }
    }
}
