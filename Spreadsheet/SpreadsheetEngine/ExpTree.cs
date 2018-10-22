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
        

        public ExpTree(string expression)
        {
            Regex StringParser = new Regex(@"(?<Variable>\w+)[+-/*]");
        }

        public void SetVar(string varName, double varValue)
        {

        }

        public double Eval()
        {
            return 0; //placeholder
        }
    }
}
