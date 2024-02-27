using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CombatSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            List<string> characters = new List<string> { "The Chosen Undead", "The Bearer of the Curse", "The Ashen One", "The Tarnished" };

            Console.WriteLine($"Fighters {String.Join(", ", characters)} descend into the dungeon.\n");
            SimulateCombat(characters, "orc", DiceRoll(2, 8, 6), 10);
            if (characters.Count > 0 ) SimulateCombat(characters, "azer", DiceRoll(6, 8, 12), 18);
            if (characters.Count > 0) SimulateCombat(characters, "troll", DiceRoll(8, 10, 40), 16);
            if (characters.Count > 0) Console.WriteLine($"After three grueling battles, the heroes {String.Join(", ", characters)} return from the dungeons to live another day.");

            // to keep console open
            Console.ReadLine();

            int DiceRoll(int numberOfRolls, int diceSides, int fixedBonus = 0)
            {
                int total = fixedBonus;
                for (int i = 0; i < numberOfRolls; i++)
                {
                    total += random.Next(1, diceSides + 1);
                }
                return total;
            }

            void SimulateCombat(List<string> characterNames, string monsterName, int monsterHP, int savingThrowDC)
            {

                int monstHP = monsterHP;

                Console.WriteLine($"Watch out, {monsterName} with {monstHP} HP appears!\n\n");
                do
                {
                    foreach (var characterName in characterNames)
                    {
                        int totalDamage = DiceRoll(2, 6);
                        monstHP -= totalDamage;
                        if (monstHP <= 0)
                        {
                            monstHP = 0;
                        }

                        Console.WriteLine($"{characterName} hits the {monsterName} for {totalDamage} damage. The {monsterName} has {monstHP} HP left.");
                        if (monstHP == 0)
                        {
                            break;
                        }
                    }

                    if (monstHP > 0)
                    {
                        string chosenTarget = characterNames[random.Next(characterNames.Count)];
                        Console.WriteLine($"\n\nThe {monsterName} attacks {chosenTarget}");
                        int savingThrow = DiceRoll(1, 20, 3);
                        if (savingThrow >= savingThrowDC)
                        {
                            Console.WriteLine($"{chosenTarget} rolls a {savingThrow} and is saved from the attack.\n\n");
                        }
                        else
                        {
                            Console.WriteLine($"{chosenTarget} rolls a {savingThrow} and fails to be saved. {chosenTarget} is killed.\n\n");
                            characterNames.Remove(chosenTarget);
                        }
                    }
                } while (monstHP > 0 && characterNames.Count > 0);

                if (monstHP <= 0)
                {
                    Console.WriteLine($"The {monsterName} collapses and the heroes celebrate their victory!\n");

                }
                else
                {
                    Console.WriteLine($"The party has failed and the {monsterName} continues to attack unsuspecting adventurers.");
                }
                characters = characterNames;
            }
        }
    }
}
