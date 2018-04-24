using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
    }
}
