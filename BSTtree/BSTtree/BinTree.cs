//Isaac Schultz 11583435

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTtree
{
    public abstract class BinTree<T>
        where T : System.IComparable<T>, new()
    {
        public abstract void Insert(T val); //Inserts a alue of type T into the tree
        public abstract bool Contains(T val); //returns true if a value is in the tree
        public abstract void InOrder(); //prints the tree an inorder traversal
        public abstract void PreOrder(); //prints the tree in a preorder traversal
        public abstract void PostOrder(); //prints the tree in a postorder traversal
    }
}
