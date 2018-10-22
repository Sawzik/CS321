using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321
{
    public abstract class ExpNode
    {
        protected ExpNode left;
        protected ExpNode right;

        public ExpNode()
        {
            left = null;
            right = null;
        }

        public ExpNode(ref ExpNode leftNode, ref ExpNode rightNode)
        {
            left = leftNode;
            right = rightNode;
        }

        public ref ExpNode GetLeftNode() { return ref left; }
        public ref ExpNode GetRightNode() { return ref right; }
        public void SetLeftNode(ref ExpNode node) { left = node; }       
        public void SetRightNode(ref ExpNode node) { right = node; }
        public abstract double Eval();
    }

    public class NumericalNode : ExpNode
    {
        double numericalValue;

        public NumericalNode(ref ExpNode leftNode, ref ExpNode rightNode) : base(ref leftNode, ref rightNode) { } //uses the base cell class constructor.

        public NumericalNode(ref ExpNode leftNode, ref ExpNode rightNode, double numValInput)
        {
            left = leftNode;
            right = rightNode;
            numericalValue = numValInput;
        }

        public NumericalNode(double numValInput)
        {
            left = null;
            right = null;
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

        public VariableNode(ref ExpNode leftNode, ref ExpNode rightNode) : base(ref leftNode, ref rightNode) { } //uses the base cell class constructor.

        public VariableNode(ref ExpNode leftNode, ref ExpNode rightNode, string varInput = "NoData", double numValInput = 0)
        {
            left = leftNode;
            right = rightNode;
            numericalValue = numValInput;
            variable = varInput;
        }

        public VariableNode(string varInput = "NoData", double numValInput = 0)
        {
            left = null;
            right = null;
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

        public void CopyNode(VariableNode copyNode)
        {
            this.SetLeftNode(ref copyNode.GetLeftNode());
            this.SetRightNode(ref copyNode.GetRightNode());
            this.numericalValue = copyNode.numericalValue;
        }
    }

    public class OperatorNode : ExpNode
    {
        char operation;

        public OperatorNode(ref ExpNode leftNode, ref ExpNode rightNode) : base(ref leftNode, ref rightNode) { } //uses the base cell class constructor.

        public OperatorNode(ref ExpNode leftNode, ref ExpNode rightNode, char opInput = '!')
        {
            left = leftNode;
            right = rightNode;
            operation = opInput;
        }

        public OperatorNode(char opInput)
        {
            left = null;
            right = null;
            operation = opInput;
        }

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
