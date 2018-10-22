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
            Regex varRegex = new Regex(@"[/\*-+]?(?<var>\w+\z?)");
            Regex opRegex = new Regex(@"(?<op>\-|\+|/|\*)");

            MatchCollection variables = varRegex.Matches(expression);
            MatchCollection operators = opRegex.Matches(expression);

            Console.WriteLine("{0} variables found in: {1}", variables.Count, expression);
            foreach (Match match in variables)
            {
                Console.WriteLine("\t'{0}' at '{1}'\n", match.Value, match.Index);
                double number;
                bool isDouble = double.TryParse(match.Value, out number);
                if (isDouble)
                {
                    NumericalNode varNode = new NumericalNode(number);
                }
                
            }

            Console.WriteLine("{0} operators found in: {1}", operators.Count, expression);
            foreach (Match match in operators)
            {
                Console.WriteLine("\t'{0}' at '{1}'\n", match.Value, match.Index);
            }
        }

        private void ConstructTree()
        {

        }

        public void SetVar(string varName, double varValue)
        {
            VariableNode search = new VariableNode(varValue, varName);
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
