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
    
    class BSTtreeInterface //test for the interface class. add more to this.
    {

    }

    class BSTtree : BSTtreeInterface //actual implementation of the Binary Tree, put this in another file.w
    {
        private class BSTnode
        {
            public BSTnode left = null;
            public BSTnode right = null;
            public int data = -1; //picked -1 arbritarily as being no data as its outside of the user input range.
            public int height = 0; //variable that stores the current height of the node


            //initializes class members from contrsuctor arguments
            BSTnode(int input, BSTnode inputLeft = null, BSTnode inputRight = null)
            {
                data = input;
                left = inputLeft;
                right = inputRight;
            }

            void setHeight(int inputHeight)
            {
                if (height < inputHeight)
                    height = inputHeight;
            }
        }

        private BSTnode root = null; //creates an empty root node to start the tree.

        private int insert(int input, BSTnode node)
            {
            if (node == null)
                node = new BSTnode(input); //trying to make a recursive function to insert into the tree.
                return node.height;
            }
    }
}