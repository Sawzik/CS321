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
            int[] SIZES = { 8, 64, 256, 1024, 4096, 16384, 65536 };

            Console.WriteLine("Threads:\tArray Size\tTime to completion (milliseconds)");

            foreach (int size in SIZES)
            {
                Random rand = new Random();
                List<int> data = new List<int>();

                for (int i = 0; i != size; i++)
                    data.Add(rand.Next(Int32.MaxValue));

                //List<int> threadedData = data.ToList(); //copies the data into another list

                Merger merger = new Merger(data);
                ThreadedMerger threadedMerger = new ThreadedMerger(data);

                long offset = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                List<int> list = merger.Sort();
                offset = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - offset;

                foreach (int i in list)
                    Console.Write(", {0}", i);

                Console.WriteLine("Single\t\t{0} \t\t" + offset.ToString(), size);
            }
            Console.ReadKey();
        }
    }
}
