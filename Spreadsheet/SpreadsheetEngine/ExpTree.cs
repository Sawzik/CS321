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

            Console.WriteLine("{0} matches found in:\n\t{1}", variables.Count, expression);
            Console.WriteLine("{0} matches found in:\n\t{1}", operators.Count, expression);

            foreach (Match match in variables)
            {
                Console.WriteLine("'{0}'\n", match.Value);
            }

            foreach (Match match in operators)
            {            
                Console.WriteLine("'{0}'\n", match.Value);
            }
        }

        public void SetVar(string varName, double varValue)
        {

        }

        private VariableNode FindVarNode(string varName)
        {

        }

        public double Eval()
        {
            return 0; //placeholder
        }
    }
}
