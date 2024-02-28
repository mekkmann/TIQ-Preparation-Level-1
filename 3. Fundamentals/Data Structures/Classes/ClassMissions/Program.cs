using System.ComponentModel;

namespace ClassMissions
{
    internal class Program
    {
        class Location(string name, string description)
        {
            public string Name = name;
            public string Description = description;
            public List<Location> Neighbors = [];

            public void GreetTravelers()
            {
                Console.WriteLine($"Welcome to {Name}, {Description}.\n");
            }

            public void ShowDestinations()
            {
                Console.WriteLine("Possible destinations are:");
                for ( var i = 0; i < Neighbors.Count; i++ )
                {
                    Console.WriteLine( $"{i + 1}. {Neighbors[i].Name}" );
                }
                Console.WriteLine();
            }

            public Location TravelToNeighbor()
            {
                Console.WriteLine("Where would you like to travel? (Choose a number)");
                string choiceInput = Console.ReadLine();
                int choice = int.Parse(choiceInput);
                Console.WriteLine();
                return Neighbors[choice - 1];
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("MISSION: BOSS LEVEL");
            Location winterfell = new("Winterfell", "the capital of the Kingdom of the North");
            Location kingsLanding = new("King's Landing", "the capital, and largest city, of the Seven Kingdoms");
            Location riverrun = new("Riverrun", "a large castle located in the central-western part of the Riverlands");
            Location theTrident = new("The Trident", "one of the largest and most well-known rivers on the continent of Westeros");
            Location pyke = new("Pyke", "the stronghold and seat of House Greyjoy");
            Location highgarden = new("Highgarden", "the regional capital of the Reach");

            ConnectLocations(winterfell, theTrident);
            ConnectLocations(winterfell, pyke);

            ConnectLocations(kingsLanding, theTrident);
            ConnectLocations(kingsLanding, riverrun);
            ConnectLocations(kingsLanding, highgarden);

            ConnectLocations(riverrun, theTrident);
            ConnectLocations(riverrun, kingsLanding);
            ConnectLocations(riverrun, pyke);
            ConnectLocations(riverrun, highgarden);

            ConnectLocations(theTrident, kingsLanding);
            ConnectLocations(theTrident, winterfell);
            ConnectLocations(theTrident, riverrun);

            ConnectLocations(pyke, winterfell);
            ConnectLocations(pyke, riverrun);
            ConnectLocations(pyke, highgarden);

            ConnectLocations(highgarden, kingsLanding);
            ConnectLocations(highgarden, riverrun);
            ConnectLocations(highgarden, pyke);
            
            Location currentLocation = winterfell;
            do
            {
                currentLocation.GreetTravelers();
                currentLocation.ShowDestinations();
                currentLocation = currentLocation.TravelToNeighbor();
            } while (true);


            static void ConnectLocations(Location a, Location b)
            {
                if(!a.Neighbors.Contains(b))
                {
                    a.Neighbors.Add(b);
                }
                if (!b.Neighbors.Contains(a))
                {
                    b.Neighbors.Add(a);
                }
            }
        }
    }
}
