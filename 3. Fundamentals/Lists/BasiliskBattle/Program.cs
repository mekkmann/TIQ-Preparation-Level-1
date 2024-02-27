using System;
using System.Collections.Generic;

namespace BasiliskBattle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            List<string> characters = new List<string> { "The Chosen Undead", "The Bearer of the Curse", "The Ashen One", "The Tarnished"};

            int basiliskHP = 16;
            for (int i = 0; i < 8; i++)
            {
                basiliskHP += random.Next(1, 9);
            }

            Console.WriteLine(
                $"Fighters {String.Join(", ", characters)} descend into the dungeon.\n" +
                $"A basilisk with {basiliskHP} HP appears!\n\n"
                );

            do
            {
                foreach(var character in characters)
                {
                    int totalDamage = 0;
                    for (int i = 0; i < 2; i++) {
                        int damage = random.Next(1, 5);
                        totalDamage += damage;
                        basiliskHP -= damage;
                        if (basiliskHP <= 0)
                        {
                            basiliskHP = 0;
                        }
                    }
                    Console.WriteLine($"{character} hits the basilisk for {totalDamage} damage. Basilisk has {basiliskHP} HP left.");
                    if ( basiliskHP == 0 )
                    {
                        break;
                    }
                }

                if (basiliskHP > 0)
                {
                    string chosenTarget = characters[random.Next(characters.Count)];
                    Console.WriteLine($"\n\nThe basilisk uses Petrifying Gaze on {chosenTarget}");
                    int savingThrow = random.Next(1, 21) + 3;
                    if ( savingThrow >= 12)
                    {
                        Console.WriteLine($"{chosenTarget} rolls a {savingThrow} and is saved from the attack.\n\n");
                    } else
                    {
                        Console.WriteLine($"{chosenTarget} rolls a {savingThrow} and fails to be saved. {chosenTarget} is turned into stone.\n\n");
                        characters.Remove(chosenTarget);
                    }
                }
            } while (basiliskHP > 0 && characters.Count > 0);

            if (basiliskHP <= 0)
            {
                Console.WriteLine("The basilisk collapses and the heroes celebrate their victory!");
            } else
            {
                Console.WriteLine("The party has failed and the basilisk continues to turn unsuspecting adventurers to stone.");
            }

            // to keep console open
            Console.ReadLine();
        }
    }
}
