namespace MonsterManual
{
    // armor type enum
    enum ArmorTypeId
    {
        Unspecified,
        Natural,
        Leather,
        StuddedLeather,
        Hide,
        ChainShirt,
        ChainMail,
        ScaleMail,
        Plate,
        Other
    }

    // defining a class
    // (primary constructor)
    internal class MonsterType(string name, string description, string alignment, string hpRoll, int armorClass, ArmorTypeId armorType = ArmorTypeId.Unspecified)
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
        //armor class
        public int armorClass = armorClass;
        //armor type
        public ArmorTypeId armorType = armorType;

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
            //prints armor class
            Console.WriteLine($"Armor class: {armorClass}");
            //prints armor type
            Console.WriteLine($"Armor type: {armorType}");
        }
    }
}
