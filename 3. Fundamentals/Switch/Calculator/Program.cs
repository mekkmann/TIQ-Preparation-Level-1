using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Set the price: ");
            List<string> priceInput = Console.ReadLine().Split(' ').ToList();
            float price = 0;
            if (priceInput.Count < 3)
            {
                price = float.Parse(priceInput.First());
            
            }
            else
            {
                float float1 = float.Parse(priceInput.First());
                priceInput.Remove(priceInput.First());

                float float2 = float.Parse(priceInput.Last());
                priceInput.Remove(priceInput.Last());

                string mathOperator = string.Join(" ", priceInput);


                switch (mathOperator)
                {
                    case "+":
                    case "plus":
                        price = float1 + float2;
                        break;
                    case "-":
                    case "minus":
                        price = float1 - float2;
                        break;
                    case "*":
                    case "times":
                        price = float1 * float2;
                        break;
                    case "/":
                    case "divided by":
                        price = float1 / float2;
                        break;
                }

            }
            Console.WriteLine($"The price was set to: {price}");



            // to keep the console open
            Console.ReadLine();
        }
    }
}
