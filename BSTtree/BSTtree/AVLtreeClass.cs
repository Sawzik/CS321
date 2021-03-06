﻿//Isaac Schultz 11583435
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BSTtree
{
    class AVLtree<T> : BinTree<T>
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

            Balance(ref node);
            return node.Height();
        }

        private void Balance(ref BSTnode<T> node)
        {
            int nodeBalance = node.BalanceFactor();
            if (nodeBalance > 1)
            {
                //left right
                if (node.left.BalanceFactor() < 0) //if left node has 1 right node
                {
                    RotateLR(ref node);
                    RotateRR(ref node);
                }
                //right right
                else //if left node has 1 left node                   
                {
                    RotateRR(ref node);
                }
            }
            else if (nodeBalance < -1)
            {
                //right left
                if (node.right.BalanceFactor() > 0) //if right node has 1 right node
                {
                    RotateRL(ref node);
                    RotateLL(ref node);;
                }
                //left left
                else //if right node has 1 left node
                {
                    RotateLL(ref node);
                }
            }
        }

        private void RotateLR(ref BSTnode<T> node)
        {
            BSTnode<T> temp = node.left;
            node.left = temp.right; //moves left-right node up
            temp.right = node.left.left; //replaces old left-right with old left-left
            node.left.left = temp; //replaces left-left with old left
        }
        private void RotateRR(ref BSTnode<T> node)
        {
            BSTnode<T> temp = node;
            node = temp.left;  // moves left node up
            temp.left = node.right; // replaces old left with old right
            node.right = temp; // replaces right with old top
        }
        private void RotateRL(ref BSTnode<T> node)
        {
            BSTnode<T> temp = node.right;
            node.right = temp.left; //moves right-left up
            temp.left = node.right.right; // replaces old right-left with old right-right
            node.right.right = temp; //replaces right-right with old right
        }
        private void RotateLL(ref BSTnode<T> node)
        {
            BSTnode<T> temp = node;
            node = temp.right; //moves right node up
            temp.right = node.left; // replaces old right with old left
            node.left = temp; //replaces left with old top
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

        private bool Remove(ref BSTnode<T> rmData, ref BSTnode<T> node)
        {
            if (node == null) //data not found
                return false;
            if (rmData == node) //data found
            {
                if (node.left == null && node.right == null) //no children                              
                    node = null; //relying on the garbage collector to delete memory once there are no more references.                
                else if (node.left == null) //one right child
                {
                    node = node.right;
                    Balance(ref node);
                }
                else if (node.right == null) //one left child
                {
                    node = node.left;
                    Balance(ref node);
                }
                else //two children
                {
                    ref BSTnode<T> min = ref FindMin(ref node.right); //minimum will always have a max of one child
                    node.Equals(min); //finds the minimum node on the right subtree and copies its data into this node
                    nodes++; //since we will remove one extra node that only has one child, we have to account for the extra remove call.
                    Remove(ref node, ref min); //deletes the node that was brought up
                    Balance(ref node);
                }
                nodes--;
                return true;
            }
            else if (node < rmData)
                return Remove(ref rmData, ref node.right); //traverses right subtree
            else
                return Remove(ref rmData, ref node.left); //traverses left subtree
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
            if (node == null) //not found
                return null;

            if (input == node) //node found
                return node;

            if (input < node) //check left subtree
                return Find(ref input, node.left);

            return Find(ref input, node.right); //check right subtree
        }

        private bool Contains(ref BSTnode<T> input, BSTnode<T> node)
        {
            if (Find(ref input, node) == null)
                return false;
            return true;
        }

        void MakeEmpty(BSTnode<T> node) //recursive function that delet
        {
            if (node != null)
            {
                MakeEmpty(node.left);
                MakeEmpty(node.right);
                node = null;
            }
        }

        private void InOrder(ref BSTnode<T> node) //recusively outputs tree inorder traversal
        {
            if (node != null)
            {
                InOrder(ref node.left);
                Console.Write(node.ToString() + " ");
                InOrder(ref node.right);
            }
        }

        private void PreOrder(ref BSTnode<T> node) //recusively outputs tree preorder traversal
        {
            if (node != null)
            {
                Console.Write(node.ToString() + " ");
                PreOrder(ref node.left);
                PreOrder(ref node.right);
            }
        }

        private void PostOrder(ref BSTnode<T> node) //recusively outputs tree postorder traversal
        {
            if (node != null)
            {
                PostOrder(ref node.left);
                PostOrder(ref node.right);
                Console.Write(node.ToString() + " ");
            }
        }

        //very wide and not optimized. but it prints the tree vertically which is very easy to read.
        //Helper function more for debugging purposes.
        private int HorizontalOrder(ref BSTnode<T> node, int space, int spaceIncrease, int unbalancedTrees)
        {
            if (node == null)
                return 0;

            int balance = node.BalanceFactor();
            space += spaceIncrease; //Increase distance between levels 
            HorizontalOrder(ref node.right, space, spaceIncrease, unbalancedTrees);

            //Print current node after space 
            for (int i = spaceIncrease; i < space; i++)
                Console.Write(" ");
            Console.Write(node.ToString() + ", " + balance + '\n');

            if (balance > 1 || balance < -1) //keeps track of any trees that arent balanced.
                unbalancedTrees++;

            HorizontalOrder(ref node.left, space, spaceIncrease, unbalancedTrees);
            return unbalancedTrees;
        }

        private int Depth(ref BSTnode<T> node) //recursively checks for the node that is deepest in the tree.
        {
            if (node == null)
                return 0;            
            int leftDepth = Depth(ref node.left);
            int rightDepth = Depth(ref node.right);
            if (leftDepth > rightDepth)
                return leftDepth + 1;   
            return rightDepth + 1;                           
        }

        ~AVLtree()
        {
            MakeEmpty(root);
        }

        //prints out the tree horizontally and displays the number of unbalanced trees.
        public void HorizontalOrder(int spaceBetweenData)
        {
            int unbalancedTrees = HorizontalOrder(ref root, 0, spaceBetweenData, 0);
            Console.WriteLine("\n\tUnbalanced SubTrees: " + unbalancedTrees);
        }

        public override T FindMin()
        {
            return FindMin(ref root).Value();
        }

        public override T FindMax()
        {
            return FindMax(ref root).Value();
        }

        public T Find(T input)
        {
            BSTnode<T> inputAsNode = new BSTnode<T>(input);
            return Find(ref inputAsNode, root).Value();
        }

        public override bool IsEmpty()
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
            BSTnode<T> inputAsNode = new BSTnode<T>(input); //blank node with input data. allows private function to compare nodes
            Insert(ref inputAsNode, ref root);
        }

        public override bool Contains(T input)
        {
            BSTnode<T> inputAsNode = new BSTnode<T>(input); //blank node with input data. allows private function to compare nodes
            return Contains(ref inputAsNode, root);
        }

        public override bool Remove(T input)
        {
            BSTnode<T> inputAsNode = new BSTnode<T>(input); //blank node with input data. allows private function to compare nodes
            if (root == inputAsNode)
            {
                root = RemoveRoot(ref root);
                return true;
            }
            return Remove(ref inputAsNode, ref root);            
        }

        public override int Count()
        {
            return this.nodes;
        }

        public override int Depth()
        {
            if (nodes > 0)
                return Depth(ref root) - 1; //depth returned will always be larger by 1            
            return 0;
        }

        public override int TheoreticalMinDepth()
        {
            if (nodes > 0)
                return (int)Math.Log(nodes, 2); //Formula for best case binary tree is log base 2 of n in a perfectly balanced tree.
            return 0;
        }

    }
}
