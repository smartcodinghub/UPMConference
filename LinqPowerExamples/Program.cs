using System;
using System.Linq;

namespace LinqPowerExamples
{
    public class Program
    {
        public static void Main()
        {
            //Composition.ComposeLinq();

            //foreach(var item in CustomEnumerable.EndlessFor(int.MaxValue))
            //{
            //    Console.WriteLine(item);
            //}
            //CustomEnumerable.EndlessList(int.MaxValue);

            //foreach(var prime in CustomEnumerable.EndlessPrimes().Take(20))
            //{
            //    Console.WriteLine(prime);
            //}

            foreach(var fib in CustomEnumerable.Fibonacci(200))
            {
                Console.WriteLine(fib);
            }

            //CustomEnumerable.LazyExecutionExample();
            Console.ReadLine();
        }
    }
}
