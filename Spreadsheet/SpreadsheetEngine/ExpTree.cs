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
        ExpNode root;

        public ExpTree(string expression)
        {
            Regex varRegex = new Regex(@"(?<var>\w+\z?)");
            Regex opRegex = new Regex(@"(?<op>\-|\+|/|\*)");

            Match variables = varRegex.Match(expression);
            Match operators = opRegex.Match(expression);


            while (operators.Success)
            {
                Console.WriteLine("\t'{0}' at index [{1}, {2}]", variables.Value, variables.Index, variables.Index + variables.Length);
                Console.WriteLine("Operator {0} at index [{1}, {2}]", operators.Value, operators.Index, operators.Index + operators.Length);
                Console.WriteLine("\t'{0}' at index [{1}, {2}]", variables.NextMatch().Value, variables.NextMatch().Index, variables.NextMatch().Index + variables.NextMatch().Length);
                
                ExpNode leftNode = makeDataNode(variables.Value);
                ExpNode rightNode = makeDataNode(variables.NextMatch().Value);
                OperatorNode opNode = new OperatorNode(ref leftNode, ref rightNode, operators.Value[0]);

                operators = operators.NextMatch();
                variables = variables.NextMatch().NextMatch();
            }
            
        }

        private ExpNode makeDataNode(string operand)
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

        private void ConstructTree(ref ExpNode currRoot)
        {

        }

        public void SetVar(string varName, double varValue)
        {
            VariableNode search = new VariableNode(varName, varValue);
            SetVarNode(ref search, ref root);
        }

        private void SetVarNode(ref VariableNode input, ref ExpNode node)
        {
            if (VariableNodeCompare(input, node))
            {
                VariableNode nodeAsVar = node as VariableNode;
                nodeAsVar.CopyNode(input);
            }

            SetVarNode(ref input, ref node.GetLeftNode() ); //check left subtree
            SetVarNode(ref input, ref node.GetRightNode() ); //check right subtree
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
