namespace Factorial
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Factorial(0));
            Console.WriteLine(Factorial(1));
            Console.WriteLine(Factorial(2));
            Console.WriteLine(Factorial(3));
            Console.WriteLine(Factorial(4));
            Console.WriteLine(Factorial(5));
            Console.WriteLine(Factorial(6));
            Console.WriteLine(Factorial(7));
            Console.WriteLine(Factorial(8));
            Console.WriteLine(Factorial(9));
            Console.WriteLine(Factorial(10));

            // to keep console open
            Console.ReadLine();

            static int Factorial(int n)
            {
                if (n == 0) return 1;

                return n * Factorial(n - 1);
            }
        }
    }
}
