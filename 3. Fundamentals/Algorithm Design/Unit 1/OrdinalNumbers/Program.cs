namespace OrdinalNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(OrdinalNumber(1));
            Console.WriteLine(OrdinalNumber(2));
            Console.WriteLine(OrdinalNumber(3));
            Console.WriteLine(OrdinalNumber(4));
            Console.WriteLine(OrdinalNumber(10));
            Console.WriteLine(OrdinalNumber(11));
            Console.WriteLine(OrdinalNumber(12));
            Console.WriteLine(OrdinalNumber(13));
            Console.WriteLine(OrdinalNumber(21));
            Console.WriteLine(OrdinalNumber(101));
            Console.WriteLine(OrdinalNumber(111));
            Console.WriteLine(OrdinalNumber(121));

            // to keep console open
            Console.ReadLine();

            static string OrdinalNumber(int number)
            {
                var lastDigit = number % 10;
                if (number > 10)
                {
                    var secondToLastDigit = (number / 10) % 10;

                    if (secondToLastDigit == 1)
                    {
                        return $"{number}th";
                    }
                }
                if (lastDigit == 1)
                {
                    return $"{number}st";
                }
                if (lastDigit == 2)
                {
                    return $"{number}nd";
                }
                if (lastDigit == 3)
                {
                    return $"{number}rd";
                }

                return $"{number}th";
            }
        }
    }
}
