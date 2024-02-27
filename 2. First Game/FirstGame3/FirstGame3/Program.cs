using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Mission 1
            //Random random = new Random();

            //int maxPins = 10;

            //int firstThrow = random.Next(0, maxPins + 1);
            //int secondThrow;

            //if (firstThrow == 10)
            //{
            //    Console.WriteLine(
            //        $"First roll: X\n" +
            //        $"Knocked pins: 10"
            //        );
            //} else
            //{
            //    secondThrow = random.Next(0, maxPins + 1 - firstThrow);

            //    string firstThrowSymbol;
            //    if (firstThrow == 0)
            //    {
            //        firstThrowSymbol = "-";
            //    }else
            //    {
            //        firstThrowSymbol = firstThrow.ToString();
            //    }

            //    string secondThrowSymbol;
            //    if (secondThrow == 10 || firstThrow + secondThrow == 10)
            //    {
            //        secondThrowSymbol = "/";
            //    }else if (secondThrow == 0)
            //    {
            //        secondThrowSymbol = "-";
            //    }else
            //    {
            //        secondThrowSymbol = secondThrow.ToString();
            //    }

            //    Console.WriteLine(
            //        $"First roll: {firstThrowSymbol}\n" +
            //        $"Second roll: {secondThrowSymbol}\n" +
            //        $"Knocked pins: {firstThrow + secondThrow}"
            //        );
            //}

            //// Mission 2
            //Random random = new Random();
            //int warriorHP = random.Next(0, 100 + 1);

            //Console.WriteLine(
            //    $"Warrior HP: {warriorHP}\n" +
            //    $"The Regenerate spell is cast!"
            //    );
            //while ( warriorHP < 50 )
            //{
            //    warriorHP += 10;
            //    Console.WriteLine($"Warrior HP: {warriorHP}");
            //}
            //Console.WriteLine("The Regenerate spell is complete.");

            //// Mission 3
            //Random random = new Random();
            //int totalScore = 0;
            //int currentRoll = -1;
            //while (currentRoll != 6) 
            //{
            //    currentRoll = random.Next(1, 7);
            //    totalScore += currentRoll;
            //    Console.WriteLine($"The player rolls: {currentRoll}");

            //}
            //Console.WriteLine($"Total score: {totalScore}");

            // Mission 4
            Random random = new Random();

            int characterStrength = 0;
            for(int i = 0; i < 3; i++)
            {
                characterStrength += random.Next(1, 7);
            }
            Console.WriteLine($"A character with strength {characterStrength} was created.");

            int cubeHP = 0;
            for(int i = 0; i < 8; i++) 
            {
                cubeHP += random.Next(1, 11);
            }
            cubeHP += 40;
            Console.WriteLine($"A gelatinous cube with {cubeHP} HP appears!");

            int cubeArmyHP = cubeHP;
            for(int i = 0; i < 100; i++)
            {
                for (int y = 0; y < 8; y++)
                {
                    cubeArmyHP += random.Next(1, 11);
                }
                cubeArmyHP += 40;
            }
            Console.WriteLine($"Dear gods, an army of 100 cubes descends upon us with a total of {cubeArmyHP} HP. We are doomed!");

            // to keep console open
            Console.ReadLine();
        }
    }
}
