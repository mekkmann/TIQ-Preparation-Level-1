namespace MonsterManual
{
    // defining a class
    // (primary constructor)
    internal class MonsterType(string name, string description, string alignment, string hpRoll)
    {
        //variables
        //name
        string name = name;
        //description
        string description = description;
        //alignment
        string alignment = alignment;
        //dice notation for hp roll (or just hp in rare cases)
        string hpRoll = hpRoll;

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
