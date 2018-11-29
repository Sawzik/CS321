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
            int[] SIZES = {4, 8, 64, 256, 1024, 4096, 16384, 65536 };

            Console.WriteLine("Threads:\tArray Size\tTime to completion (milliseconds)");

            foreach (int size in SIZES)
            {
                Random rand = new Random();
                List<int> data = new List<int>();

                for (int i = 0; i != size; i++)
                    data.Add(rand.Next(Int32.MaxValue));

                //List<int> threadedData = data.ToList(); //copies the data into another list

                Merger merger = new Merger(data);
                ArrayMerger arrayMerger = new ArrayMerger(data.ToArray());
                ThreadedMerger threadedMerger = new ThreadedMerger(data);

                long offset1 = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                int[] list1 = arrayMerger.Sort();
                offset1 = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - offset1;

                long offset2 = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                List<int> list2 = merger.Sort();
                offset2 = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - offset2;


                //foreach (int i in list)
                //    Console.WriteLine(", {0}", i);

                Console.WriteLine("array\t\t{0} \t\t" + offset1.ToString(), size);
                Console.WriteLine("list\t\t{0} \t\t" + offset2.ToString(), size);
                Console.WriteLine();
            }
            Console.ReadKey();
        }
    }
}
