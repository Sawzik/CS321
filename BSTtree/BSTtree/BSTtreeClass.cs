using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSTtree
{
    class BSTtree
    {

        private BSTnode root = null; //creates an empty root node to start the tree.
        private int nodes = 0;
        private int depth = 0;

        //recursively calls itself until it finds where to add a new node. increases the height each time
        private int Insert(int input, ref BSTnode node)
        {
            if (node == null)
            {
                nodes++; //keeps track of the number of nodes
                node = new BSTnode(input);
            }
            else if (input < node.data)
                node.SetHeight(Insert(input, ref node.left) + 1);
            else if (input == node.data) //if data is already in the tree
                return -1;
            else
                node.SetHeight(Insert(input, ref node.right) + 1);

            if (node.height > depth) //keeps track of the deepest node
                depth = node.height;

            return node.height;
        }

        private BSTnode RemoveRoot(ref BSTnode node)
        {
            BSTnode newNode = null;

            if (node.left != null && node.right != null) //two children
            {
                newNode = node;

                BSTnode min = FindMin(node.right);
                node.data = min.data; //finds the minimum node on the right subtree and copies its data into this node
                Remove(node.data, ref min); //deletes the node that was brought up

                return newNode;
            }
            else if (node.left == null) //one right child
                newNode = node.right;
            else if (node.right == null) //one left child
                newNode = node.left;

            node = null; //no children

            return newNode;
        }

        private void Remove(int rmData, ref BSTnode node)
        {
            if (node == null) //data not found
                return;
            if (rmData == node.data) //data found
            {
                if (node.left == null && node.right == null) //no children
                    node = null; //relying on the garbage collector to delete memory once there are no more references.
                else if (node.left == null) //one right child
                    node = node.right;
                else if (node.right == null) //one left child
                    node = node.left;
                else //two children
                {
                    BSTnode min = FindMin(node.right);
                    node.data = min.data; //finds the minimum node on the right subtree and copies its data into this node
                    Remove(node.data, ref min); //deletes the node that was brought up
                }
            }
            else if (node.data < rmData)
                Remove(rmData, ref node.left); //traverses left subtree
            else
                Remove(rmData, ref node.right); //traverses right subtree
        }

        private BSTnode FindMin(BSTnode node)
        {
            if (node.left == null) //reached the leftmost node
                return node;
            return FindMin(node.left);
        }

        private BSTnode FindMax(BSTnode node)
        {
            if (node.left == null) //reached the leftmost node
                return node;
            return FindMax(node.right);
        }

        private BSTnode Find(int input, BSTnode node)
        {
            if (node == null)
                return null;

            if (input == node.data)
                return node;

            if (input < node.data)
                return Find(input, node.left);

            return Find(input, node.right);
        }

        private bool Contains(int input, BSTnode node)
        {
            if (Find(input, node) == null)
                return false;
            return true;
        }

        void MakeEmpty(BSTnode node)
        {
            if (node != null)
            {
                MakeEmpty(node.left);
                MakeEmpty(node.right);
                node = null;
            }
        }

        private void PrintTree(ref BSTnode node)
        {
            if (node != null)
            {
                PrintTree(ref node.left);
                Console.Write(node.data + " ");
                PrintTree(ref node.right);
            }
        }

        ~BSTtree()
        {
            MakeEmpty(root);
        }

        public int FindMin()
        {
          return FindMin(root).data;
        }

        public int FindMax()
        {
            return FindMax(root).data;
        }

        public int Find(int input)
        {
            return Find(input, root).data;
        }

        public bool Contains(int input)
        {
            return Contains(input, root);
        }

        public bool IsEmpty()
        {
            return root == null;
        }

        public void PrintTree()
        {
            PrintTree(ref root);
        }

        public void Insert(int input)
        {
            Insert(input, ref root);
        }

        public void Remove(int input)
        {
            if (root.data == input)
                root = RemoveRoot(ref root);
            else
                Remove(input, ref root);
        }

        public int Count()
        {
            return this.nodes;
        }

        public int Depth()
        {
            return this.depth;
        }

        public int TheoreticalMinDepth()
        {
            return (int)Math.Log(nodes, 2); //Formula for best case binary tree is log base 2 of n in a perfectly balanced tree.
        }

    }
}
