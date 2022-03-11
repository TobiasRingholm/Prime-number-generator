using System.Diagnostics;

namespace CompulsoryAssignment
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new PrimeNumberGenerator());
        }

        public static void TaskThird()
        {
            ComparePerformanceAndCheckIfListsAreEqual(1, 1000000);
            ComparePerformanceAndCheckIfListsAreEqual(1, 10000000);
            ComparePerformanceAndCheckIfListsAreEqual(10000000, 20000000);
            ComparePerformanceAndCheckIfListsAreEqual(100000000, 200000000);
        }

        public static void ComparePerformanceAndCheckIfListsAreEqual(long start, long end)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Restart();
            var list1 = GetPrimesParallel(start, end);
            stopwatch.Stop();

            Console.WriteLine("Parallel: " + stopwatch.Elapsed);

            stopwatch.Restart();
            var list2 = GetPrimesSequential(start, end);
            stopwatch.Stop();
            Console.WriteLine("Sequential: " + stopwatch.Elapsed);

            Console.WriteLine(Enumerable.SequenceEqual(list1, list2));
        }

        //Parallel Function
        public static List<long> GetPrimesParallel(long first, long last)
        {
            var primesParallelList = new List<long>();
            object myLock = new();
            Parallel.For(first, last + 1, i =>
            {
                if (i != 1)
                {
                    Boolean isPrime = true;
                    for (long factor = 2; factor <= i / 2; factor++)
                    {
                        if (i % factor == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime)
                    {
                        lock (myLock)
                        {
                            primesParallelList.Add(i);
                        }
                    }
                }
            });
            primesParallelList.Sort();
            return primesParallelList;
        }

        //Sequential Function
        public static List<long> GetPrimesSequential(long first, long last)
        {
            var primesSequentialList = new List<long>();
            while (first <= last)
            {
                if (first != 1)
                {
                    Boolean isPrime = true;
                    for (long factor = 2; factor <= first / 2; factor++)
                    {
                        if (first % factor == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }
                    if (isPrime)
                    {
                        primesSequentialList.Add(first);
                    }
                }
                first++;
            }
            return primesSequentialList;

        }
    }
}