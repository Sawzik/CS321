using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTtree
{
    class BSTnode<T> : ICloneable//, IComparable<BSTnode<T>>
    {
        public BSTnode<T> left = null;
        public BSTnode<T> right = null;
        private T data = new; //picked -1 arbritarily as being no data as its outside of the user input range.
        public int height = 0; //variable that stores the current height of the node in the tree

        public BSTnode(T input)
        {
            data = input;
            left = null;
            right = null;
        }

        public BSTnode(T input, BSTnode<T> inputLeft, BSTnode<T> inputRight)
        {
            data = input;
            left = inputLeft;
            right = inputRight;
        }

        public object Clone()
        {
            BSTnode<T> clonedLeft = null;
            BSTnode<T> clonedRight = null;
            if (this.left != null) //Recusively calling clone on each child for a deep copy.
                clonedLeft = this.left.Clone() as BSTnode<T>; //clone takes an object class, telling it that its taking a BSTnode which is inherits from object 
            if (this.right != null)
                clonedRight = this.right.Clone() as BSTnode<T>;
            return new BSTnode<T>(this.data, clonedLeft, clonedRight);
        }

        //public int CompareTo(BSTnode<T> other)
        //{
        //    if (this.data < other.data)
        //        return -1;
        //    else if (this.data == other.data)
        //        return 0;
        //    return 1;
        //}

        public override operator ==(BSTnode<T> LeftNode, BSTnode<T> rightNode)
            {
                return CompareTo
            }

        public void SetHeight(int inputHeight)
        {
            if (height < inputHeight)
                height = inputHeight;
        }
    }
}
