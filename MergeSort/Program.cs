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
            Random rand = new Random();
            Merge merger;
            int[] data = new int[1024];
            for (int i = 0; i != 1024; i++)
                data[i] = rand.Next(Int32.MaxValue);
            merger.mergeSort(data)
        }
    }
}
