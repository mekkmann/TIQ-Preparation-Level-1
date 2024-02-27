using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstGame4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            char artillery = '/';
            char tank = 'T';
            char ground = '_';
            int tankDistance = random.Next(40, 70 + 1);

            string battlefieldToTank = $"{ground}{artillery}".PadRight(tankDistance - 1, ground);
            string battlefieldWithTank = $"{battlefieldToTank}{tank}".PadRight(80, ground);

            Console.WriteLine(
                "DANGER! A tank is approaching our position. Your artillery unit is our only hope!\n\n"
                );

            Console.WriteLine("What is your name, commander?\n");
            Console.Write("Enter name: ");
            string chosenName = Console.ReadLine();

            bool tankIsAlive = true;
            while (tankIsAlive)
            {
                Console.Clear();
                Console.WriteLine(
                "Here is the map of the battlefield:\n\n" +
                $"{battlefieldWithTank}\n\n"
                );
                if (tankDistance == 3)
                {
                    break;
                }
                Console.WriteLine($"Aim your shot, {chosenName}!");
                Console.Write("Enter distance: ");

                string chosenDistance = Console.ReadLine();
                int shotDistance = Int32.Parse(chosenDistance);

                string drawExplosion = "  ".PadRight(shotDistance - 1, ' ') + "*";
                Console.WriteLine(drawExplosion);

                if (shotDistance > tankDistance)
                {
                    Console.WriteLine("Alas, the shell flies past the tank.");
                }
                else if (shotDistance < tankDistance)
                {
                    Console.WriteLine("Oh no, your shot was too short.");
                }
                else if (shotDistance == tankDistance)
                {
                    Console.WriteLine("TAKE THAT NAZIS! Your aim is legendary and the tank is destroyed!");
                    tankIsAlive = false;
                    break;
                }
                int tankAdvancement = random.Next(1, 15 + 1);
                tankDistance -= tankAdvancement;
                if (tankDistance < 3) 
                { 
                    tankDistance = 3; 
                }
                battlefieldToTank = $"{ground}{artillery}".PadRight(tankDistance - 1, ground);
                battlefieldWithTank = $"{battlefieldToTank}{tank}".PadRight(80, ground);
            }

            if (tankIsAlive)
            {
                Console.WriteLine($"Enemy has reached the base, godspeed {chosenName}.\n\n");
            }

            Console.WriteLine("Press any key to continue...");
            // to keep console open
            Console.ReadLine();
        }
    }
}
