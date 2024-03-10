namespace MonsterManual
{
    // armor category enum
    enum ArmorCategory
    {
        Light,
        Medium,
        Heavy
    }

    // class definition
    internal class ArmorType(string displayName, ArmorCategory category, int weight)
    {
        // display name
        public string DisplayName = displayName;
        // armor category
        public ArmorCategory Category = category;
        // weight
        public int Weight = weight;
    }
}
