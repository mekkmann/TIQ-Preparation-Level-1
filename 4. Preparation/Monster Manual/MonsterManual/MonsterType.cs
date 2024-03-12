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
        public string Name = name;
        //description
        public string Description = description;
        //alignment
        public string Alignment = alignment;
        //dice notation for hp roll (or just hp in rare cases)
        public string HpRoll = hpRoll;
        //armor class
        public int ArmorClass = armorClass;
        //armor type
        public ArmorTypeId ArmorType = armorType;

        public void PrintMonsterData(Dictionary<ArmorTypeId, ArmorType> armorTypes)
        {
            //prints name
            Console.WriteLine($"Name: {name}");
            //prints description
            Console.WriteLine($"Description: {description}");
            //prints alignment
            Console.WriteLine($"Alignment: {alignment}");
            //prints hp roll
            Console.WriteLine($"Hit points roll: {hpRoll}");
            Console.WriteLine($"Armor class: {armorClass}");

            // if the monsters armor types is of a specific category
            if (armorTypes.TryGetValue(armorType, out ArmorType value))
            {
                //prints armor type
                Console.WriteLine($"Armor type: {value.DisplayName}");
                // prints armor category
                Console.WriteLine($"Armor category: {value.Category}");
                // prints armor weight
                Console.WriteLine($"Armor weight: {value.Weight} lb");
            }
            else
            {
                //prints armor type
                Console.WriteLine($"Armor type: {armorType}");
            }
        }
    }
}
