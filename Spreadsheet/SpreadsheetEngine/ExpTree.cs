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
            StringExpression = expression;
            Regex varRegex = new Regex(@"(?<var>\w+\z?)");
            Regex opRegex = new Regex(@"(?<op>\-|\+|/|\*)");

            MatchCollection variables = varRegex.Matches(expression);
            MatchCollection operators = opRegex.Matches(expression);


            if (operators.Count > 0) //assuming user input is correct. If there is an operator
            {
                root = ConstructTree(expression);
            }
            else
            {
                root = MakeDataNode(variables[0].Value); //sets the root to a single variable/numerical Node
            }
            Console.WriteLine("MEMES");
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
            return MakeDataNode(var.Value);
        }

        private ExpNode ConstructNode(string expression)
        {
            Regex varRegex = new Regex(@"\w+\z"); //matches the last alphanumeric characters in the string
            Match var = varRegex.Match(expression);
            return MakeDataNode(var.Value);
        }

        //private void ConstructTree(ref ExpNode currNode, Match variables, Match operators)
        //{
        //    if (operators.Success) // if there are more operators that need to be loaded
        //    {
        //        currNode = new OperatorNode(operators.Value[0]);
        //        ConstructTree(ref (currNode as OperatorNode).Right(), variables.NextMatch(), operators.NextMatch());
        //        (currNode as OperatorNode).Left() = MakeDataNode(variables.Value); // Assinging left node to the current Valriable match
        //    }
        //    else //when there are no more operations to load
        //    {
        //        currNode = MakeDataNode(variables.Value);
        //    }
        //}

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

        //public void SetVar(string varName, double varValue)
        //{
        //    VariableNode search = new VariableNode(varName, varValue);
        //    if (root as VariableNode != null)
        //        SetVarNode(ref search, ref (root as OperatorNode));
        //}

        //private void SetVarNode(ref VariableNode input, ref OperatorNode node)
        //{
        //    if (VariableNodeCompare(input, node.Left()))
        //    {
        //        ref VariableNode nodeAsVar = ref node as VariableNode;
        //        nodeAsVar.CopyNode(input);
        //    }

        //    SetVarNode(ref input, ref node.Left()); //check left subtree
        //    SetVarNode(ref input, ref node.Right()); //check right subtree
        //}

        public string ToString()
        {
            return StringExpression;
        }

        public bool VariableNodeCompare(ExpNode inputLeftNode, ExpNode inputRightNode)
        {
            VariableNode leftNodeAsVar = (VariableNode)inputLeftNode;
            VariableNode righttNodeAsVar = (VariableNode)inputRightNode;
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
