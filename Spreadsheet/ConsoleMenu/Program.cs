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
            ExpTree tree = new ExpTree("A+B+3"); //default starting value of our Expression tree.
            tree.ShuntingYard("A+B+3");
            int number = 0;

            tree.SetVar("A", 2); //setting some example values to the variables
            tree.SetVar("B", 5);
            do
            {
                Console.WriteLine("Menu: (current expression: {0})", tree.ToString()); //menu
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
                            tree = new ExpTree(userInput); //makes a new tree with the new expression
                            break;
                        case 2:
                            Console.Write("Enter variable name: ");
                            string userInputVariable = Console.ReadLine();
                            Console.Write("Enter variable value: ");
                            string userInputValue = Console.ReadLine();
                            double parsedDouble; //used to store the value of the double
                            bool isDouble = double.TryParse(userInputValue, out parsedDouble); //stores the user input as a double if it is a double
                            if (isDouble) //if the value inputted was a double
                                tree.SetVar(userInputVariable, parsedDouble); //set the variable to that double
                            break;
                        case 3:
                            Console.WriteLine(tree.Eval());
                            break;
                    }
                }
            } while (number != 4); //runs until 4 is typed in and then falls out to main.
        }
    }
}
