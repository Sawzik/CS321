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

    public class ValNode : ExpNode
    {
        double numericalValue;

        public ValNode(double numValInput) //constructor for a NumericalNode.
        {
            numericalValue = numValInput; //initializes numericalValue
        }

        public double Value //getter/setter for numericalValue
        {
            set { numericalValue = value; }
            get { return numericalValue; }
        }

        public override double Eval() { return numericalValue; } //evaluation of a numerical node is just it's value
    }

    public class VarNode : ExpNode
    {
        double numericalValue;
        string variable;

        public VarNode(string varInput = "NoData", double numValInput = 0) //constructor for the variablenode with defaults if no data is specified
        {
            numericalValue = numValInput;
            variable = varInput;
        }

        public double Value //getter/setter for numericalValue
        {
            set { numericalValue = value; }
            get { return numericalValue; }
        }

        public string Variable //getter/setter for Variable
        {
            set { variable = value; }
            get { return variable; }
        }

        public override double Eval() { return numericalValue; } //evaluation of a variable node is just it's value
    }

    public class OpNode : ExpNode
    {
        public ExpNode left; //this is the only kind of node that can have children
        public ExpNode right;
        char operation;

        public OpNode(ref ExpNode leftNode, ref ExpNode rightNode, char opInput = '!') //extended constructor with all fields
        {
            left = leftNode;
            right = rightNode;
            operation = opInput;
        }

        public OpNode(ref ExpNode leftNode, char opInput = '!') // constructor used in factory. Deals with the left node only
        {
            left = leftNode;
            right = null;
            operation = opInput;
        }

        public OpNode(char opInput = '!') // constructor for a node with no children
        {
            left = null;
            right = null;
            operation = opInput;
        }

        public char Operator //getter/setter for the operator
        {
            set { operation = value; }
            get { return operation; }
        }

        public override double Eval() //OperatorNodes need to hand evalutaion based on their operator
        {
            switch (Operator)
            {
                case '-':
                    return this.left.Eval() - this.right.Eval(); //traverses left and right subtrees, calling the eval function and returning their operation.
                case '+':
                    return this.left.Eval() + this.right.Eval();
                case '/':
                    return this.left.Eval() / this.right.Eval();
                case '*':
                    return this.left.Eval() * this.right.Eval();
            }
            return 0.0; //return value for a non specified operator.
        }
    }
}
