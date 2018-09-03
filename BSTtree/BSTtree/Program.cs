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
            Console.WriteLine("test input by typing numbers. no error detection");

            string input = Console.ReadLine(); //reading input from user. Gets the whole line
            string[] parsedInput = input.Split(' ');

            Console.WriteLine(input);

            foreach (string s in parsedInput) 
            {  
                Console.WriteLine(s);
                tree.Insert(Int32.Parse(s));
            }

            tree.PrintTree();

            Console.ReadKey(); 
        }
    }
}