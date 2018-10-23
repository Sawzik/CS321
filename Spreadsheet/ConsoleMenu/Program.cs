using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CptS321;
using System.Text.RegularExpressions;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            ExpTree tree = new ExpTree("100-10-20"); //default starting value of our Expression tree.
            int number = 0;

            do
            {
                Console.WriteLine("Menu: (current expression: {0})", tree.ToString());
                Console.WriteLine("\t1 = Enter a new expression");
                Console.WriteLine("\t2 = Set a variable value");
                Console.WriteLine("\t3 = Evaluate tree");
                Console.WriteLine("\t4 = Quit");
                bool isInt = int.TryParse(Console.ReadLine(), out number); //gets user input and stres it in number if it is an integer.
                if (isInt)
                {
                    switch (number)
                    {
                        case 1:
                            Console.Write("Enter new expression: ");
                            string userInput = Console.ReadLine();
                            tree = new ExpTree(userInput);
                            break;
                        case 2:
                            Console.Write("Enter variable name: ");
                            string userInputVariable = Console.ReadLine();
                            Console.Write("Enter variable value: ");
                            string userInputValue = Console.ReadLine();
                            //tree.SetVar(userInputVariable, userInputValue);
                            break;
                        case 3:
                            Console.WriteLine(tree.Eval());
                            break;
                    }
                }
            } while (number != 4);
        }
    }
}
