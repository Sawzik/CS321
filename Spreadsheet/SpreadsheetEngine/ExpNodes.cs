using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    public abstract class ExpNode
    {
        ExpNode left;
        ExpNode right;

        public ExpNode(ref ExpNode leftNode, ref ExpNode rightNode)
        {
            left = leftNode;
            right = rightNode;
        }

        public ExpNode LeftNode
        {
            set { left = value; }
            get { return left; }
        }

        public ExpNode RightNode
        {
            set { right = value; }
            get { return right; }
        }
    }

    public class NumericalNode : ExpNode
    {
        double numericalValue;

        public NumericalNode(ref ExpNode leftNode, ref ExpNode rightNode) : base(ref leftNode, ref rightNode) { } //uses the base cell class constructor.

        public double Value
        {
            set { numericalValue = value; }
            get { return numericalValue; }
        }
    }

    public class VariableNode : ExpNode
    {
        double numericalValue;
        string variable;

        public VariableNode(ref ExpNode leftNode, ref ExpNode rightNode) : base(ref leftNode, ref rightNode) { } //uses the base cell class constructor.

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
    }

    public class OperatorNode : ExpNode
    {
        char operation;

        public OperatorNode(ref ExpNode leftNode, ref ExpNode rightNode) : base(ref leftNode, ref rightNode) { } //uses the base cell class constructor.

        public char Operator
        {
            set { operation = value; }
            get { return operation; }
        }
    }
}
