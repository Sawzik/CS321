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
        public abstract void Insert(T val); //Inserts a value of type T into the tree.
        public abstract bool Contains(T val); //Returns true if a value is in the tree.
        public abstract void InOrder(); //Prints the tree an inorder traversal.
        public abstract void PreOrder(); //Prints the tree in a preorder traversal.
        public abstract void PostOrder(); //Prints the tree in a postorder traversal.
        public abstract T FindMin(); //Returns the minimum of type T.
        public abstract T FindMax(); //Returns the maximum of type T.
        public abstract bool IsEmpty(); //Returns true if the tree is empty.
        public abstract bool Remove(T input); //Removes a node from the tree. Returns true if a node was removed.
        public abstract int Count(); //Returns the number of nodes in the tree.
        public abstract int Depth(); //Returns the depth of the deepest node in the tree.    
        public abstract int TheoreticalMinDepth(); //returns the theoretical minimum depth of the deepest node.        
    }

}

