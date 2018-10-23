using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace CptS321
{
    public class ExpTree
    {
        ExpNode root = null;
        string StringExpression;

        public ExpTree(string expression)
        {
            StringExpression = expression; //saves the expression for display purposes. Can just use a tree traversal later.
            root = ConstructTree(expression);
        }

        private ExpNode ConstructTree(string expression)
        {
            Regex opRegex = new Regex(@"[-+\*/]"); //matches if there is an operator anywhere in the string
            Regex varRegex = new Regex(@"\w+\z"); //matches the last alphanumeric characters in the string
            Match op = opRegex.Match(expression);
            Match var = varRegex.Match(expression);

            if (op.Success) // if there is an operator
            {
                for (int i = expression.Length - 1; i >= 0; i--)
                {
                    if (expression[i] == '-' || expression[i] == '+' || expression[i] == '/' || expression[i] == '*')
                    {
                        OperatorNode newNode = new OperatorNode(expression[i]);
                        newNode.left = ConstructTree(expression.Substring(0, i)); //calls construct node on the current string up until just before the operator
                        newNode.right = ConstructTree(expression.Substring(i + 1)); //calls construct node on the current string after the operator
                        return newNode;
                    }
                }
            }
            return MakeDataNode(var.Value); //if there is no operators, then this must be a variable or value.
        }

        private ExpNode ConstructNode(string expression)
        {
            Regex varRegex = new Regex(@"\w+\z"); //matches the last alphanumeric characters in the string
            Match var = varRegex.Match(expression);
            return MakeDataNode(var.Value);
        }

        private ExpNode MakeDataNode(string operand)
        {
            double number;
            bool isDouble = double.TryParse(operand, out number);
            if (isDouble)
            {
                NumericalNode numNode = new NumericalNode(number);
                return numNode;
            }
            else
            {
                VariableNode varNode = new VariableNode(operand);
                return varNode;
            }
        }

        public void SetVar(string varName, double varValue)
        {
            VariableNode search = new VariableNode(varName, varValue); //creating a node so that it is easier to compare between nodes. This node stores the value of the new variable
            if (root as OperatorNode != null) //if the root is an operator node
                SetVarNode(ref search, root as OperatorNode);
            else if (VariableNodeCompare(search, root))
            {
                (root as VariableNode).Value = varValue; //sets the root value to the new variable
            }
        }

        private void SetVarNode(ref VariableNode input, OperatorNode node)
        {
            if (VariableNodeCompare(input, node.left))
            {
                (node.left as VariableNode).Value = input.Value;
            }
            else if (VariableNodeCompare(input, node.right))
            {
                (node.right as VariableNode).Value = input.Value;
            }
            else
            {
                if (node.right as OperatorNode != null) //if the right side node is an operator node
                    SetVarNode(ref input, node.right as OperatorNode); //check right subtree
                if (node.left as OperatorNode != null) //if the right side node is an operator node
                    SetVarNode(ref input, node.left as OperatorNode); //check right subtree
            }
        }

        public override string ToString()
        {
            return StringExpression;
        }

        //returns true only if both nodes are VariableNodes and the names of the variables are the same.
        public bool VariableNodeCompare(ExpNode inputLeftNode, ExpNode inputRightNode) 
        {
            VariableNode leftNodeAsVar = inputLeftNode as VariableNode;
            VariableNode righttNodeAsVar = inputRightNode as VariableNode;
            if (leftNodeAsVar == null || righttNodeAsVar == null) //both are null, so they are not both variableNodes, and are not equal
                return false;
            if (leftNodeAsVar.Variable == righttNodeAsVar.Variable) //variables match the same string
                return true;
            return false; //if the nodes do not contain the same string.
        }

        public double Eval()
        {
            return root.Eval();
        }
    }
}
