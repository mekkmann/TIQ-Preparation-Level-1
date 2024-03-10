namespace MonsterManual
{
    // defining a class
    // (primary constructor)
    internal class MonsterType(string name, string description, string alignment, string hpRoll)
    {
        //variables
        //name
        public string name = name;
        //description
        public string description = description;
        //alignment
        public string alignment = alignment;
        //dice notation for hp roll (or just hp in rare cases)
        public string hpRoll = hpRoll;

        // functions

        // prints monsterdata
        public void PrintMonsterData()
        {
            //prints name
            Console.WriteLine($"Name: {name}");
            //prints description
            Console.WriteLine($"Description: {description}");
            //prints alignment
            Console.WriteLine($"Alignment: {alignment}");
            //prints hp roll
            Console.WriteLine($"Hit points roll: {hpRoll}");
        }
    }
}
