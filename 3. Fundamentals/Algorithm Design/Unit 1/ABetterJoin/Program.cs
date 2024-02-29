namespace ABetterJoin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> characters = ["One", "Two", "Three", "Four"];

            Console.WriteLine($"The heroes in the party are: {JoinWithAnd(characters, false)}.");
            characters.RemoveAt(characters.Count - 1);
            Console.WriteLine($"The heroes in the party are: {JoinWithAnd(characters)}.");
            characters.RemoveAt(characters.Count - 1);
            Console.WriteLine($"The heroes in the party are: {JoinWithAnd(characters)}.");
            characters.RemoveAt(characters.Count - 1);
            Console.WriteLine($"The heroes in the party are: {JoinWithAnd(characters)}.");
            characters.RemoveAt(characters.Count - 1);
            Console.WriteLine($"The heroes in the party are: {JoinWithAnd(characters)}.");

            // to keep console open
            Console.ReadLine();

            static string JoinWithAnd(List<string> items, bool useSerialComma = true) 
            {
                int itemCount = items.Count;

                if (itemCount == 0) return "";
                if (itemCount == 1) return items.First();
                if (itemCount == 2) return string.Join(" and ", items);

                List<string> charactersCopy = new(items);

                if (useSerialComma)
                {
                    charactersCopy[itemCount - 1] = $"and {charactersCopy[itemCount - 1]}";
                } else
                {
                    charactersCopy[itemCount - 2] = $"{charactersCopy[itemCount - 2]} and {charactersCopy[itemCount - 1]}";
                    charactersCopy.RemoveAt(itemCount - 1);
                }

                return string.Join(", ", charactersCopy);
            }
        }
    }
}
