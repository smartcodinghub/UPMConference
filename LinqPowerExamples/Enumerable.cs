using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LinqPowerExamples
{
    public static class CustomEnumerable
    {
        /// <summary>
        /// This methods generates a range of Dates.
        /// </summary>
        /// <param name="start">The start date, inclusive</param>
        /// <param name="end">The end date, exclusive</param>
        /// <param name="step">The increase rate or step.</param>
        /// <returns></returns>
        public static IEnumerable<DateTime> Range(DateTime start, DateTime end, TimeSpan? step = null)
        {
            step = step ?? TimeSpan.FromDays(1);

            while(start < end)
            {
                yield return start;
                start = start.Add(step.Value);
            }
        }

        /// <summary>
        /// This methods generates a range of Dates.
        /// </summary>
        /// <param name="start">The start date, inclusive</param>
        /// <param name="end">The end date, exclusive</param>
        /// <param name="step">The increase rate or step.</param>
        /// <returns></returns>
        public static IEnumerable<FileInfo> GetAllFiles(DirectoryInfo root)
        {
            foreach(var file in root.GetFiles()) yield return file;

            foreach(var dir in root.GetDirectories())
            {
                foreach(var file in GetAllFiles(dir)) yield return file;
            }
        }

        /// <summary>
        /// This methods generates a big range from 0
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<int> EndlessFor(int to)
        {
            for(int i = 0; i < to; i++) yield return i;
        }

        /// <summary>
        /// This methods generates a big range from 0
        /// </summary>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IEnumerable<int> EndlessList(int to)
        {
            var list = new List<int>();
            for(int i = 0; i < to; i++) list.Add(i);
            return list;
        }

        /// <summary>
        /// This methods generates endless prime numbers
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<int> EndlessPrimes()
        {
            Func<int, bool> isPrime = n =>
            {
                if(n % 2 == 0) return false;

                int max = (int)Math.Sqrt(n);
                for(int i = 3; i <= max; i += 2)
                {
                    if((n % i) == 0) return false;
                }
                return true;
            };

            yield return 2;

            for(int i = 3; ; i++)
            {
                if(isPrime(i)) yield return i;
            }
        }

        /// <summary>
        /// This methods generates endless fib numbers
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<int> EndlessFibonacci()
        {
            yield return 0;
            yield return 1;

            int l1 = 0, l2 = 1, next = 0;

            for(int i = 2; ; i++)
            {
                next = l1 + l2;
                l1 = l2;
                l2 = next;
                yield return next;
            }
        }

        /// <summary>
        /// This methods generates n fib numbers
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<int> Fibonacci(int n)
        {
            List<int> fib = new List<int>(n) { 0 };

            if(n >= 1)
                fib.Add(1);

            for(int i = 2; i <= n; i++) fib.Add(fib[i - 1] + fib[i - 2]);

            return fib;
        }

        public static void LazyExecutionExample()
        {
            var list = Enumerable.Range(0, 20)
                .Select(i => { Console.WriteLine(i); return i; })
                .Take(10)
                .Select(i => i * 2)
                .Take(5)
                .ToList();

        }

        /// <summary>
        /// expand = new List<string>() { "A5", "B10", "C", "D2" };
        /// 
        /// Cadena con la letra repetida tantas veces como diga su número, sin número significa una sola vez
        /// Devolver un string AAAAABBBBBBBBBBCDD
        /// </summary>
        /// <param name="expand"></param>
        /// <returns></returns>
        public static string ExpandLettersString()
        {
            string expand = "A5B10CD2";

            return expand.Aggregate(new List<Tuple>(), (seed, current) =>
            {
                if(char.IsDigit(current))
                {
                    seed.Last().Count += current;
                }
                else
                {
                    if(seed.LastOrDefault()?.Count == "") seed.Last().Count = "1";
                    seed.Add(new Tuple() { Letter = current, Count = "" });
                }

                return seed;
            })
            .Aggregate(new StringBuilder(),
                (seed, next) => seed.Append(next.Letter, int.Parse(next.Count)))
            .ToString();
        }

        public static string ExpandLettersYield()
        {
            string expand = "A5B10CD2";

            return ExpandLettersYield(expand)
                .Aggregate(new StringBuilder(),
                    (seed, next) => seed.Append(next.Letter, int.Parse(next.Count)))
                .ToString();
        }

        public static IEnumerable<Tuple> ExpandLettersYield(string expand)
        {
            Tuple current = null;

            foreach(var c in expand)
            {
                if(char.IsDigit(c))
                {
                    current.Count += c;
                }
                else
                {
                    if(current != null)
                    {
                        if(current.Count == "") current.Count = "1";
                        yield return current;
                    }

                    current = new Tuple() { Letter = c, Count = "" };
                }
            }

            yield return current;
        }
    }

    public class Tuple
    {
        public char Letter { get; set; }
        public string Count { get; set; }
    }
}
