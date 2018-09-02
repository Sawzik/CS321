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
    
    class BSTtree //actual implementation of the Binary Tree, put this in another file.w
    {
        private class BSTnode : ICloneable, IComparable<BSTnode>
        {
            public BSTnode left = null;
            public BSTnode right = null;
            public int data = -1; //picked -1 arbritarily as being no data as its outside of the user input range.
            public int height = 0; //variable that stores the current height of the node
            private int input;

            public BSTnode(int input)
            {
                data = input;
                left = null;
                right = null;
            }
 
            public BSTnode(int input, BSTnode inputLeft, BSTnode inputRight)
            {
                data = input;
                left = inputLeft;
                right = inputRight;
            }

            public object Clone()
            {
                BSTnode clonedLeft = null;
                BSTnode clonedRight = null;
                if (this.left != null) //Recusively calling clone on each child for a deep copy.
                    clonedLeft = this.left.Clone() as BSTnode; //clone takes an object class, telling it that its taking a BSTnode which is inherits from object 
                if (this.right != null)
                    clonedRight = this.right.Clone() as BSTnode;
                return new BSTnode(this.data, clonedLeft, clonedRight);
            }

            public int CompareTo(BSTnode other)
            {
                return this.data - other.data;
            }

            void setHeight(int inputHeight)
            {
                if (height < inputHeight)
                    height = inputHeight;
            }
        }

        private BSTnode root = null; //creates an empty root node to start the tree.

        private int Insert(int input, ref BSTnode node)
            {
            if (node == null)
                node = new BSTnode(input); //trying to make a recursive function to insert into the tree.
                return node.height;
            }
    }
}