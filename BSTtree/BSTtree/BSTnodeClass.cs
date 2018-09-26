using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTtree
{
    class BSTnode<T> : ICloneable, IComparable<BSTnode<T>>
        where T : System.IComparable<T>
    {
        public BSTnode<T> left = null;
        public BSTnode<T> right = null;
        private T data;
        private int height = 0; //variable that stores the current height of the node in the tree

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

        public int CompareTo(BSTnode<T> other)
        {
            if (data.CompareTo(other.data) < 0) //less than
                return -1;
            else if (data.CompareTo(other.data) == 0) //equal to
                return 0;
            return 1; //greater than
        }

        public static bool operator ==(BSTnode<T> LeftNode, BSTnode<T> rightNode)
        {
            return LeftNode.data.CompareTo(rightNode.data) == 0;
        }
        public static bool operator !=(BSTnode<T> LeftNode, BSTnode<T> rightNode)
        {
            return LeftNode.data.CompareTo(rightNode.data) != 0;
        }
        public static bool operator >=(BSTnode<T> LeftNode, BSTnode<T> rightNode)
        {
            return LeftNode.data.CompareTo(rightNode.data) >= 0;
        }
        public static bool operator <=(BSTnode<T> LeftNode, BSTnode<T> rightNode)
        {
            return LeftNode.data.CompareTo(rightNode.data) <= 0;
        }
        public static bool operator >(BSTnode<T> LeftNode, BSTnode<T> rightNode)
        {
            return LeftNode.data.CompareTo(rightNode.data) > 0;
        }
        public static bool operator <(BSTnode<T> LeftNode, BSTnode<T> rightNode)
        {
            return LeftNode.data.CompareTo(rightNode.data) < 0;
        }

        public T Equals(BSTnode<T> LeftNode, BSTnode<T> rightNode)
        {
            return LeftNode.data = rightNode.data;
        }

        public override String ToString()
        {
            return data.ToString();
        }

        public T Value()
        {
            return data;
        }

        public int Height()
        {
            return height;
        }

        public void SetHeight(int inputHeight)
        {
            if (height < inputHeight)
                height = inputHeight;
        }

        public void ResetHeight(int inputHeight)
        {
            height = inputHeight;
        }
    }
}
