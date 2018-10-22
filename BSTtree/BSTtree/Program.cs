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
            //BSTtree<int> BST = new BSTtree<int>();
            //AVLtree<int> AVL = new AVLtree<int>();
            //Console.WriteLine("Enter a collection of numbers between 0 and 100. No error detection");
            //string input = Console.ReadLine(); //reading input from user. Gets the whole line
            //string[] parsedInput = input.Split(' ');
            //foreach (string s in parsedInput) //inertion into tree
            //{
            //    AVL.Insert(Int32.Parse(s));
            //    BST.Insert(Int32.Parse(s));
            //}
            //Console.Write("\n\nBSTTree Contents (horizontal inorder traversal):\n\n");
            //BST.HorizontalOrder(7);
            //Console.Write("\n\nBSTTree Contents (inorder traversal): ");
            //BST.InOrder();
            //Console.Write("\n\nBSTTree Contents (preorder traversal): ");
            //BST.PreOrder();
            //Console.Write("\n\nBSTTree Contents (postorder traversal): ");
            //BST.PostOrder();
            //Console.WriteLine("\n\nBSTTree statistics:\n\tNumber of nodes: " + BST.Count());
            //Console.WriteLine("\tNumber of levels in BST: " + BST.Depth());
            //Console.WriteLine("\tTheoretical Minimum Depth of BST: " + BST.TheoreticalMinDepth());
            //Console.WriteLine("\tContains '2' in BST: " + BST.Contains(2));
            //Console.WriteLine("\tContains '13' in BST: " + BST.Contains(13));
            //Console.Write("\n\nAVLTree Contents (horizontal inorder traversal):\n\n");
            //AVL.HorizontalOrder(7);
            //Console.Write("\n\nAVLTree Contents (inorder traversal): ");
            //AVL.InOrder();
            //Console.Write("\n\nAVLTree Contents (preorder traversal): ");
            //AVL.PreOrder();
            //Console.Write("\n\nAVLTree Contents (postorder traversal): ");
            //AVL.PostOrder();
            //Console.WriteLine("\n\nAVLTree statistics:\n\tNumber of nodes: " + AVL.Count());
            //Console.WriteLine("\tNumber of levels in AVL: " + AVL.Depth());
            //Console.WriteLine("\tTheoretical Minimum Depth of AVL: " + AVL.TheoreticalMinDepth());
            //Console.WriteLine("\tContains '2' in AVL: " + AVL.Contains(2));
            //Console.WriteLine("\tContains '13' in AVL: " + AVL.Contains(13));
            //Console.ReadKey(); //pause
            #endregion

            //shows off the efficiency as the number of nodes gets really really big
            #region random number test case
            Random rand = new Random();
            AVLtree<int> data = new AVLtree<int>();
            data.Insert(2);
            data.Insert(13);
            for (int i = 0; i != 5536; i++) //inputs 2^10 random numbers
                data.Insert(rand.Next(99999));
            //Console.WriteLine();
            //data.HorizontalOrder(7); //prints a sideways tree. makes it very very nice to see what is going on.
            Console.Write("\n\nTree Contents (inorder traversal): ");
            data.InOrder();
            //Console.Write("\n\nTree Contents (preorder traversal): ");
            //data.PreOrder();
            //Console.Write("\n\nTree Contents (postorder traversal): ");
            //data.PostOrder();
            Console.WriteLine("\n\nTree statistics:\n\tNumber of nodes: " + data.Count());
            Console.WriteLine("\tNumber of levels: " + data.Depth());
            Console.WriteLine("\tTheoretical Minimum Depth: " + data.TheoreticalMinDepth());
            Console.WriteLine("\tContains '2': " + data.Contains(2));
            Console.WriteLine("\tContains '13': " + data.Contains(13));
            for (int i = 0; i != 128; i++) //removes 2^7 random numbers
                data.Remove(rand.Next(999));
            data.Remove(2);
            data.Remove(13);
            //Console.WriteLine();
            //data.HorizontalOrder(7); //prints a sideways tree. makes it very very nice to see what is going on.
            Console.Write("\n\nTree Contents (inorder traversal): ");
            data.InOrder();
            //Console.Write("\n\nTree Contents (preorder traversal): ");
            //data.PreOrder();
            //Console.Write("\n\nTree Contents (postorder traversal): ");
            //data.PostOrder();
            Console.WriteLine("\n\nTree statistics:\n\tNumber of nodes: " + data.Count());
            Console.WriteLine("\tNumber of levels: " + data.Depth());
            Console.WriteLine("\tTheoretical Minimum Depth: " + data.TheoreticalMinDepth());
            Console.WriteLine("\tContains '2': " + data.Contains(2));
            Console.WriteLine("\tContains '13': " + data.Contains(13));
            #endregion

            Console.ReadKey(); //pause
        }
    }
}