using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTtree
{
    class Program
    {
        static void Main(string[] args)
        {
            BSTtree tree = new BSTtree();

            Console.WriteLine("Enter a collection of numbers between 0 and 100. No error detection");
            string input = Console.ReadLine(); //reading input from user. Gets the whole line
            string[] parsedInput = input.Split(' ');

            foreach (string s in parsedInput) //inertion into tree
            {  
                //Console.Write(s + ", "); //debug
                tree.Insert(Int32.Parse(s));
            }

            Console.Write("\n\nTree Contents: ");
            tree.PrintTree();
            Console.WriteLine("\nTree statistics:\n\tNumber of nodes: " + tree.Count());
            Console.WriteLine("\tNumber of levels: " + tree.Depth());
            Console.WriteLine("\tTheoretical Minimum Depth: " + tree.TheoreticalMinDepth());

            Console.ReadKey(); //pause
        }
    }
}