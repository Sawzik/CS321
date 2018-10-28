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
        string StringExpression; //used to sabe the expression for display purposes. Can just use a tree traversal later.

        public class Token
        {
            public string symbol { get; } // read only data.
            public int precedence { get; }
            public bool leftAssociative { get; }

            public Token(string inputSymbol, int inputPrecedence, bool inputLeftAssociative)
            {
                symbol = inputSymbol;
                precedence = inputPrecedence;
                leftAssociative = inputLeftAssociative;
            }
        }

        // Dictionary that contains a list of all the operators supported by the ExpTree and their precedence
        private static readonly Dictionary<string, Token> operators = new Token[]
        {
            new Token("*", 3, false),
            new Token("/", 3, true),
            new Token("+", 2, false),
            new Token("-", 2, true)
        }.ToDictionary(op => op.symbol);

        public ExpTree(string expression)
        {
            StringExpression = expression; //saves the expression for display purposes
            root = ConstructTree(expression); //constructs the tree at the root
        }

        public void ShuntingYard(string expression)
        {
            Regex opRegex = new Regex(@"[-+\*/]"); //matches if there is an operator
            string[] tokens = opRegex.Split(expression); // Splits the expression up into individual tokens separated by operators.
            foreach (string str in tokens)
                Console.WriteLine(str);
        }

        // Lame non Shunting Yard algorithm
        private ExpNode ConstructTree(string expression)
        {
            Regex opRegex = new Regex(@"[-+\*/]"); //matches if there is an operator anywhere in the string
            Regex varRegex = new Regex(@"[\w\.]+\z"); //matches the last alphanumeric characters in the string
            Match op = opRegex.Match(expression);
            Match var = varRegex.Match(expression);

            if (op.Success) // if there is an operator
            {
                for (int i = expression.Length - 1; i >= 0; i--)
                {
                    if (opRegex.Match(expression[i].ToString()).Success) // If the character is an operator
                    {
                        OpNode newNode = new OpNode(expression[i]);
                        newNode.left = ConstructTree(expression.Substring(0, i)); //calls construct node on the current string up until just before the operator
                        newNode.right = ConstructTree(expression.Substring(i + 1)); //calls construct node on the current string after the operator
                        return newNode;
                    }
                }
            }
            return MakeDataNode(var.Value); //if there is no operators, then this must be a variable or value.
        }

        //Unused function
        private ExpNode ConstructNode(string expression)
        {
            Regex varRegex = new Regex(@"\w+\z"); //matches the last alphanumeric characters in the string
            Match var = varRegex.Match(expression);
            return MakeDataNode(var.Value);
        }

        //simple function that turns a string into a Data node.
        private ExpNode MakeDataNode(string operand)
        {
            double number;
            bool isDouble = double.TryParse(operand, out number); //only stores the operand in number if it is actually a double
            if (isDouble)
            {
                ValNode numNode = new ValNode(number); //makes a Numerical node with the operand's value
                return numNode;
            }
            else
            {
                VarNode varNode = new VarNode(operand); //makes a variable node with the operands variable name. 
                return varNode;
            }
        }

        public void SetVar(string varName, double varValue)
        {
            VarNode search = new VarNode(varName, varValue); //creating a node so that it is easier to compare between nodes. This node stores the value of the new variable
            if (root as OpNode != null) //if root is an operator node
                SetVarNode(ref search, root as OpNode);
            else if (VariableNodeCompare(search, root)) //if root is a Variable node and has the same variable name
            {
                (root as VarNode).Value = varValue; //sets the root value to the new variable
            }
        }

        private void SetVarNode(ref VarNode input, OpNode node)
        {
            if (VariableNodeCompare(input, node.left)) //if the left node is a Variable node and has the same variable name
            {
                (node.left as VarNode).Value = input.Value;
            }
            else if (VariableNodeCompare(input, node.right)) //if the right node is a Variable node and has the same variable name
            {
                (node.right as VarNode).Value = input.Value;
            }
            else
            {
                if (node.right as OpNode != null) //if the right side node is an operator node
                    SetVarNode(ref input, node.right as OpNode); //check right subtree
                if (node.left as OpNode != null) //if the right side node is an operator node
                    SetVarNode(ref input, node.left as OpNode); //check right subtree
            }
        }

        public override string ToString()
        {
            return StringExpression;
        }

        //returns true only if both nodes are VariableNodes and the names of the variables are the same.
        public bool VariableNodeCompare(ExpNode inputLeftNode, ExpNode inputRightNode) 
        {
            VarNode leftNodeAsVar = inputLeftNode as VarNode;
            VarNode righttNodeAsVar = inputRightNode as VarNode;
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
