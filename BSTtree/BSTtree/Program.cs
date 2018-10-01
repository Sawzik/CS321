//Isaac Schultz 11583435
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
            #region Homework Required input
            //BSTtree<int> tree = new BSTtree<int>();
            //Console.WriteLine("Enter a collection of numbers between 0 and 100. No error detection");
            //string input = Console.ReadLine(); //reading input from user. Gets the whole line
            //string[] parsedInput = input.Split(' ');
            //foreach (string s in parsedInput) //inertion into tree
            //    tree.Insert(Int32.Parse(s));

            //Console.Write("\n\nTree Contents (inorder traversal): ");
            //tree.InOrder();
            //Console.Write("\n\nTree Contents (preorder traversal): ");
            //tree.PreOrder();
            //Console.Write("\n\nTree Contents (postorder traversal): ");
            //tree.PostOrder();
            //Console.WriteLine("\n\nTree statistics:\n\tNumber of nodes: " + tree.Count());
            //Console.WriteLine("\tNumber of levels: " + tree.Depth());
            //Console.WriteLine("\tTheoretical Minimum Depth: " + tree.TheoreticalMinDepth());
            //Console.WriteLine("\tContains '2': " + tree.Contains(2));
            //Console.WriteLine("\tContains '13': " + tree.Contains(13));
            #endregion

            #region random number test case
            Random rand = new Random();
            BSTtree<int> data = new BSTtree<int>();
            for (int i = 0; i != 512; i++)
                data.Insert(rand.Next(999));
            Console.WriteLine();
            data.HorizontalOrder(2); //prints a sideways tree. makes it very very nice to see what is going on.
            Console.Write("\n\nTree Contents (inorder traversal): ");
            data.InOrder();
            Console.Write("\n\nTree Contents (preorder traversal): ");
            data.PreOrder();
            Console.Write("\n\nTree Contents (postorder traversal): ");
            data.PostOrder();
            Console.WriteLine("\n\nTree statistics:\n\tNumber of nodes: " + data.Count());
            Console.WriteLine("\tNumber of levels: " + data.Depth());
            Console.WriteLine("\tTheoretical Minimum Depth: " + data.TheoreticalMinDepth());
            Console.WriteLine("\tContains '2': " + data.Contains(2));
            Console.WriteLine("\tContains '13': " + data.Contains(13));
            #endregion

            //Testing of other tree functions
            //tree.Insert(2);
            //tree.Insert(5);
            //tree.Insert(7);
            //tree.Insert(3);
            //tree.Insert(10);
            //tree.Insert(9);
            //tree.Insert(15);
            //tree.Insert(4);

            //Console.Write("\n\nTree Contents: ");
            //tree.PrintTree();
            //Console.WriteLine("\nTree statistics:\n\tNumber of nodes: " + tree.Count());
            //Console.WriteLine("\tNumber of levels: " + tree.Depth());
            //Console.WriteLine("\tTheoretical Minimum Depth: " + tree.TheoreticalMinDepth());
            //Console.WriteLine("\tContains '2': " + tree.Contains(2));

            //tree.Remove(3);
            //tree.Remove(15);
            //tree.Remove(4);
            //tree.Remove(2);
            //tree.Remove(5);
            //tree.Remove(10);
            //tree.Remove(9);
            //tree.Remove(7);

            //Console.Write("\n\nTree Contents: ");
            //tree.PrintTree();
            //Console.WriteLine("\nTree statistics:\n\tNumber of nodes: " + tree.Count());
            //Console.WriteLine("\tNumber of levels: " + tree.Depth());
            //Console.WriteLine("\tTheoretical Minimum Depth: " + tree.TheoreticalMinDepth());
            //Console.WriteLine("\tContains '2': " + tree.Contains(2));

            Console.ReadKey(); //pause
        }
    }
}