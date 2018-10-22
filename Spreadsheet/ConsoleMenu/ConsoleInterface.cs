using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CptS321;

namespace ConsoleMenu
{
    class ConsoleInterface
    {
        ExpTree tree = new ExpTree("A1-B1-C1"); //default starting value of our Expression tree.

        public int GetUserInput()
        {
            DisplayMenu();
            return Int32.Parse(Console.ReadLine()); //turns string of input into an integer
        }

        private void DisplayMenu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("\t1 = Enter a new expression");
            Console.WriteLine("\t2 = Set a variable value");
            Console.WriteLine("\t3 = Evaluate tree");
            Console.WriteLine("\t4 = Quit");
        }
    }
}
