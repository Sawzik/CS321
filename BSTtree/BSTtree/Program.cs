using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTtree
{
    class Program
    {
        static void Main(string[] args) //Main of the program
        {
            Console.WriteLine("test input by typing numbers. no error detection");

            string input = Console.ReadLine(); //reading input from user. Gets the whole line

            string[] parsedInput = input.Split(' ');

            Console.WriteLine(input);

            foreach (string s in parsedInput) //use this loop to add each element into the tree.
            {
                Console.WriteLine(s); //writes to the console one line at a time.
            }

            Console.ReadKey(); //pausing at the end of the program
        }
    }
    
    class BSTtreeInterface
    {

    }
}
