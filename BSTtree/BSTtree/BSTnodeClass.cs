using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTtree
{
    class BSTnode : ICloneable, IComparable<BSTnode>
    {
        public BSTnode left = null;
        public BSTnode right = null;
        public int data = -1; //picked -1 arbritarily as being no data as its outside of the user input range.
        public int height = 0; //variable that stores the current height of the node in the tree

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
            if (this.data < other.data)
                return -1;
            else if (this.data == other.data)
                return 0;
            return 1;
        }

        public void SetHeight(int inputHeight)
        {
            if (height < inputHeight)
                height = inputHeight;
        }
    }
}
