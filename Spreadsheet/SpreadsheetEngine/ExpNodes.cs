using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    public abstract class ExpNode
    {
        public abstract double Eval();
    }

    public class NumericalNode : ExpNode
    {
        double numericalValue;

        public NumericalNode(double numValInput)
        {
            numericalValue = numValInput;
        }

        public double Value
        {
            set { numericalValue = value; }
            get { return numericalValue; }
        }

        public override double Eval() { return numericalValue; }
    }

    public class VariableNode : ExpNode
    {
        double numericalValue;
        string variable;

        public VariableNode(string varInput = "NoData", double numValInput = 0)
        {
            numericalValue = numValInput;
            variable = varInput;
        }

        public double Value
        {
            set { numericalValue = value; }
            get { return numericalValue; }
        }

        public string Variable
        {
            set { variable = value; }
            get { return variable; }
        }

        public override double Eval() { return numericalValue; }
    }

    public class OperatorNode : ExpNode
    {
        ExpNode left;
        ExpNode right;
        char operation;

        public OperatorNode(ref ExpNode leftNode, ref ExpNode rightNode, char opInput = '!')
        {
            left = leftNode;
            right = rightNode;
            operation = opInput;
        }

        public OperatorNode(ref ExpNode leftNode, char opInput = '!')
        {
            left = leftNode;
            right = null;
            operation = opInput;
        }

        public ref ExpNode Left() { return ref left; }
        public ref ExpNode Right() { return ref right; }

        public char Operator
        {
            set { operation = value; }
            get { return operation; }
        }

        public override double Eval()
        {
                switch(Operator)
                {
                    case '-':
                        return this.left.Eval() - this.right.Eval();
                    case '+':
                        return this.left.Eval() + this.right.Eval();
                    case '/':
                        return this.left.Eval() / this.right.Eval();
                    case '*':
                        return this.left.Eval() * this.right.Eval();
                }
            return 0.0; 
        }
    }
}
