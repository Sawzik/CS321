//Isaac Schultz 11583435

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BSTtree
{
    class BSTtree<T> : BinTree<T>
        where T : System.IComparable<T>, new()
    {
        private BSTnode<T> root = null; //creates an empty root node to start the tree.
        private int nodes = 0;        

        //recursively calls itself until it finds where to add a new node. increases the height each time
        private int Insert(ref BSTnode<T> input, ref BSTnode<T> node)
        {
            if (node == null)
            {
                nodes++; //keeps track of the number of nodes
                node = input;
            }
            else if (input < node)
                node.SetHeight(Insert(ref input, ref node.left) + 1);
            else if (input == node) //if data is already in the tree
                return -1;
            else
                node.SetHeight(Insert(ref input, ref node.right) + 1);          

            return node.Height();
        }

        private BSTnode<T> RemoveRoot(ref BSTnode<T> node)
        {
            BSTnode<T> newNode = null;

            if (node.left != null && node.right != null) //two children
            {
                newNode = node;

                ref BSTnode<T> min = ref FindMin(ref node.right);
                node.Equals(min); //finds the minimum node on the right subtree and copies its data into this node
                Remove(ref node, ref min); //deletes the node that was brought up

                return newNode;
            }
            else if (node.left == null) //one right child
            {
                newNode = node.right;
            }
            else if (node.right == null) //one left child
                newNode = node.left;

            node = null; //no children
            nodes--;

            return newNode;
        }

        private void Remove(ref BSTnode<T> rmData, ref BSTnode<T> node)
        {
            if (node == null) //data not found
                return;
            if (rmData == node) //data found
            {
                if (node.left == null && node.right == null) //no children                
                    node = null; //relying on the garbage collector to delete memory once there are no more references.                
                else if (node.left == null) //one right child
                    node = node.right;
                else if (node.right == null) //one left child
                    node = node.left;
                else //two children
                {
                    BSTnode<T> min = FindMin(ref node.right);
                    node.Equals(min); //finds the minimum node on the right subtree and copies its data into this node
                    Remove(ref node, ref min); //deletes the node that was brought up
                }
                nodes--;
            }
            else if (node < rmData)
                Remove(ref rmData, ref node.right); //traverses right subtree
            else
                Remove(ref rmData, ref node.left); //traverses left subtree
        }

        private ref BSTnode<T> FindMin(ref BSTnode<T> node)
        {
            if (node.left == null) //reached the leftmost node
                return ref node;
            return ref FindMin(ref node.left);
        }

        private ref BSTnode<T> FindMax(ref BSTnode<T> node)
        {
            if (node.right == null) //reached the leftmost node
                return ref node;
            return ref FindMax(ref node.right);
        }

        private BSTnode<T> Find(ref BSTnode<T> input, BSTnode<T> node)
        {
            if (node == null)
                return null;

            if (input == node)
                return node;

            if (input < node)
                return Find(ref input, node.left);

            return Find(ref input, node.right);
        }

        private bool Contains(ref BSTnode<T> input, BSTnode<T> node)
        {
            if (Find(ref input, node) == null)
                return false;
            return true;
        }

        void MakeEmpty(BSTnode<T> node)
        {
            if (node != null)
            {
                MakeEmpty(node.left);
                MakeEmpty(node.right);
                node = null;
            }
        }

        private void InOrder(ref BSTnode<T> node)
        {
            if (node != null)
            {
                InOrder(ref node.left);
                Console.Write(node.ToString() + " ");
                InOrder(ref node.right);
            }
        }

        private void PreOrder(ref BSTnode<T> node)
        {
            if (node != null)
            {
                Console.Write(node.ToString() + " ");
                PreOrder(ref node.left);
                PreOrder(ref node.right);
            }
        }

        private void PostOrder(ref BSTnode<T> node)
        {
            if (node != null)
            {
                PostOrder(ref node.left);
                PostOrder(ref node.right);
                Console.Write(node.ToString() + " ");
            }
        }

        private int Depth(ref BSTnode<T> node)
        {
            if (node == null)
                return 0;            
            int leftDepth = Depth(ref node.left);
            int rightDepth = Depth(ref node.right);
            if (leftDepth > rightDepth)
                return leftDepth + 1;   
            return rightDepth + 1;                           
        }

        ~BSTtree()
        {
            MakeEmpty(root);
        }

        public T FindMin()
        {
            return FindMin(ref root).Value();
        }

        public T FindMax()
        {
            return FindMax(ref root).Value();
        }

        public T Find(T input)
        {
            BSTnode<T> inputAsNode = new BSTnode<T>(input);
            return Find(ref inputAsNode, root).Value();
        }

        public bool IsEmpty()
        {
            return root == null;
        }

        public void PrintTree()
        {
            InOrder(ref root);
        }

        public override void InOrder()
        {
            InOrder(ref root);        
        }

        public override void PreOrder()
        {
            PreOrder(ref root);
        }

        public override void PostOrder()
        {
            PostOrder(ref root);
        }

        public override void Insert(T input) 
        {
            BSTnode<T> inputAsNode = new BSTnode<T>(input);
            Insert(ref inputAsNode, ref root);
        }

        public override bool Contains(T input)
        {
            BSTnode<T> inputAsNode = new BSTnode<T>(input);
            return Contains(ref inputAsNode, root);
        }

        public void Remove(T input)
        {
            BSTnode<T> inputAsNode = new BSTnode<T>(input);
            if (root == inputAsNode)            
                root = RemoveRoot(ref root);
            else
                Remove(ref inputAsNode, ref root);
        }

        public int Count()
        {
            return this.nodes;
        }

        public int Depth()
        {
            if (nodes > 0)
                return Depth(ref root) - 1; //depth returned will always be larger by 1            
            return 0;
        }

        public int TheoreticalMinDepth()
        {
            if (nodes > 0)
                return (int)Math.Log(nodes, 2); //Formula for best case binary tree is log base 2 of n in a perfectly balanced tree.
            return 0;
        }

    }
}
