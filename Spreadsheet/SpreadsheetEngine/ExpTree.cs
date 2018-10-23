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

        public ExpTree(string expression)
        {
            Regex varRegex = new Regex(@"(?<var>\w+\z?)");
            Regex opRegex = new Regex(@"(?<op>\-|\+|/|\*)");

            Match variables = varRegex.Match(expression);
            Match operators = opRegex.Match(expression);

            if (operators.Success) //assuming user input is correct. If there is an operator
            {
                ExpNode node = root; //saves root in currRoot
                do
                {
                    ExpNode leftNode = MakeDataNode(variables.Value);
                    if (operators.Success) // if there are more operators that need to be loaded
                    {
                        root = new OperatorNode(ref leftNode, operators.Value[0]);
                        root = (root as OperatorNode).Right();
                    }
                    else //when there are no more operations to load
                    {
                        root = MakeDataNode(variables.Value);
                    }
                    operators = operators.NextMatch();
                    variables = variables.NextMatch();
                } while (variables.Success);

                Console.WriteLine("memes");
            }
            else
            {
                root = MakeDataNode(variables.Value); //sets the root to a single variable/numerical Node
            }
        }

        private void ConstructTree(ref ExpNode currRoot, ref ExpNode leftSideNode, ref Match variables, ref Match operators)
        {

            ExpNode leftNode = MakeDataNode(variables.Value);
            if (operators.Success) // if there are more operators that need to be loaded
            {
                root = new OperatorNode(ref leftNode, operators.Value[0]);
                //root = (root as OperatorNode).Right();
            }
            else //when there are no more operations to load
            {
                root = MakeDataNode(variables.Value);
            }
            operators = operators.NextMatch();
            variables = variables.NextMatch();

            if (variables.Success)
                ConstructTree(ref (currRoot as OperatorNode).Right(), ref leftSideNode, ref variables, ref operators);
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

        //public void SetVar(string varName, double varValue)
        //{
        //    VariableNode search = new VariableNode(varName, varValue);
        //    if (root as VariableNode != null)
        //        SetVarNode(ref search, ref root as OperatorNode);
        //}

        //private void SetVarNode(ref VariableNode input, ref OperatorNode node)
        //{
        //    if (VariableNodeCompare(input, node.Left()))
        //    {
        //        ref VariableNode nodeAsVar = ref node as VariableNode;
        //        nodeAsVar.CopyNode(input);
        //    }

        //    SetVarNode(ref input, ref node.Left() ); //check left subtree
        //    SetVarNode(ref input, ref node.Right() ); //check right subtree
        //}

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
