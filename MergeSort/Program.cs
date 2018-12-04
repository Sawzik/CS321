using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] SIZES = {4, 8, 64, 256, 1024, 4096, 16384, 65536, 262144, 1048576, 4194304, 16777216, 67108864, 88888888 };
            int[] smallerSizes = { 2, 4, 8, 16, 32, 64 };
            int[] test = { 1024, 8192 };
            int[] normalSizes = { 8, 64, 256, 1024, 4096, 1634, 65536, 262144, 1048576 };

            Console.WriteLine("Threads:\t\tArray Size\t\tTime to completion (milliseconds)");

            foreach (int size in normalSizes)
            {
                Random rand = new Random();
                int[] data = new int[size];

                for (int i = 0; i != size; i++)
                    data[i] = rand.Next(Int32.MaxValue);

                ListMerger listMerger = new ListMerger(data.ToList());
                Merger arrayMerger = new Merger(data);
                ThreadedMerger threadedMerger = new ThreadedMerger(data, 8);
                StaticThreadedMerger staticThreadedMerger = new StaticThreadedMerger(data);
                ParallelMerger parallelMerger = new ParallelMerger(data);

                // Array version is better likely because it is much more cache optimized.
                long arrayOffset = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                int[] array = arrayMerger.Sort();
                arrayOffset = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - arrayOffset;
                Console.WriteLine("Single\t\t\t{0}\t\t\t" + arrayOffset.ToString(), size);

                if (size < 500000) //starts to get waaaay to long to wait for
                {
                    // List is slower but still not that bad. Also probably using an inefficient algorithm here.
                    long listOffset = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    List<int> list = listMerger.Sort();
                    listOffset = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - listOffset;
                    Console.WriteLine("Single(list)\t\t{0}\t\t\t" + listOffset.ToString(), size);
                }
                else
                    Console.WriteLine("Single(list)\t\t{0}\t\t\tAbove 30 seconds", size);

                // Making so many threads is really expensive, and doesnt speed anything up at all.
                if (size < 5000) //crashes aboce 4096 elements in debug mode
                {
                    long threadedOffset = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    int[] threaded = threadedMerger.Sort();
                    threadedOffset = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - threadedOffset;
                    Console.WriteLine("Threaded\t\t{0}\t\t\t" + threadedOffset.ToString(), size); // will only happen if there is no exception
                }
                else
                    Console.WriteLine("Threaded\t\t{0}\t\t\tCrash", size);

                // Making 8 threads at the start might be the most efficient.
                long staticThreadedOffset = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                int[] staticThreaded = staticThreadedMerger.Sort();
                staticThreadedOffset = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - staticThreadedOffset;
                Console.WriteLine("8 threads\t\t{0}\t\t\t" + staticThreadedOffset.ToString(), size);

                // Letting Parallel.ForEach is already optimized and creates threads respective to work. Is the fastest method
                long parallelOffset = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                int[] parallel = parallelMerger.Sort();
                parallelOffset = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - parallelOffset;
                Console.WriteLine("parallel\t\t{0}\t\t\t" + parallelOffset.ToString(), size);

                Console.WriteLine();

                //for (int i = 0; i < staticThreaded.Length; i++)
                //    Console.Write("{0}\t at: {1}\n", staticThreaded[i], i);

                //List<int> isOrdered = parallel.OrderBy(x => x).ToList(); //checks if the list is in order
                //Console.WriteLine(isOrdered.SequenceEqual(parallel).ToString());

                //int temp = 0;
                //int index = 0;
                //foreach (int i in staticThreaded)
                //{
                //    if (temp > i)
                //        Console.Write("{1}, {0} at {2}\n", i, temp, index++);
                //    temp = i;
                //}

                //Console.WriteLine();
                //Console.WriteLine("Single\t\t\t{0}\t\t\t" + arrayOffset.ToString(), size);
                //Console.WriteLine("Single(list)\t\t{0}\t\t\t" + listOffset.ToString(), size);
                //Console.WriteLine("Threaded\t\t{0}\t\t\t" + threadedOffset.ToString(), size);
                //Console.WriteLine("8 threads\t\t{0}\t\t\t" + staticThreadedOffset.ToString(), size);
                //Console.WriteLine("parallel\t\t{0}\t\t\t" + parallelOffset.ToString(), size);
                //Console.WriteLine();
            }

            Console.ReadKey();

        }
    }
}
