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
        string StringExpression; //used to sabe the expression for display purposes. Can just use a tree traversal later.

        public class OperatorToken
        {
            private string symbol;
            int precedence;
            bool rightAssociative;

            public OperatorToken(string inputSymbol, int inputPrecedence, bool inputRightAssociative)
            {
                symbol = inputSymbol;
                precedence = inputPrecedence;
                rightAssociative = inputRightAssociative;
            }

            public string Symbol { get { return this.symbol; } }
            public int Precedence { get { return this.precedence; } }
            public bool RightAssociative { get { return this.rightAssociative; } }
        }

        // Dictionary that contains a list of all the operators supported by the ExpTree and their precedence
        private static readonly Dictionary<string, OperatorToken> operators = new Dictionary<string, OperatorToken>
        {
            {"*", new OperatorToken("*", 3, false) },
            {"/", new OperatorToken("/", 3, false) },
            {"+", new OperatorToken("+", 2, false) },
            {"-", new OperatorToken("-", 2, false) }
        };

        public ExpTree(string expression)
        {
            StringExpression = expression; //saves the expression for easy display purposes
            root = ConstructTreeFromTokens(ShuntingYard(expression));
        }

        public static List<string> Split(string source, string regexPattern)
        {
            List<string> splitString = new List<string>();
            MatchCollection matches = Regex.Matches(source, regexPattern, RegexOptions.IgnorePatternWhitespace);
            int currIndex = 0; // a counter to keep track of where we are in the string.
            if (matches.Count < 1) //if there are no matches then we dont need to split.
            {
                splitString.Add(source); //puts the single token in the list
            }
            else
            {
                foreach (Match match in matches)
                {
                    if (match.Index > currIndex) // when the current match IS the regexPattern
                    {
                        splitString.Add(source.Substring(currIndex, match.Index - currIndex)); //adds the matched part of the string as Token of the list.
                    }
                    splitString.Add(match.Value); // adds the non matched part of the string into the list
                    currIndex = match.Index + match.Length; //Update the current index to the end of the matched string.
                }
                if (currIndex < source.Length) // if there is an unmatched string at the end of the source string
                {
                    splitString.Add(source.Substring(currIndex));
                }
            }
            return splitString;
        }

        public List<string> ShuntingYard(string expression)
        {
            List<string> tokens = Split(expression, @"[-+\*/\(\)]"); // Splits the expression up into individual tokens
            Stack<string> stack = new Stack<string>();
            List<string> postFix = new List<string>();
            foreach (string tok in tokens)
            {
                if (operators.TryGetValue(tok, out OperatorToken op1))
                {
                    while (stack.Count > 0 && operators.TryGetValue(stack.Peek(), out OperatorToken op2))
                    {
                        int c = op1.Precedence.CompareTo(op2.Precedence);
                        if (c < 0 || !op1.RightAssociative && c <= 0)                        
                            postFix.Add(stack.Pop());                        
                        else                        
                            break;                        
                    }
                    stack.Push(tok);
                }
                else if (tok == "(")                
                    stack.Push(tok);               
                else if (tok == ")")
                {
                    string top = "";
                    while (stack.Count > 0 && (top = stack.Pop()) != "(")                    
                        postFix.Add(top);                    
                    if (top != "(")
                        throw new ArgumentException("No matching left parenthesis.");
                }
                else
                    postFix.Add(tok); //push it to the output in postFix notation
            }
            while (stack.Count > 0)
            {
                string top = stack.Pop();
                if (!operators.ContainsKey(top)) throw new ArgumentException("No matching right parenthesis");
                postFix.Add(top);
            }
            return postFix;
        }
        
        private ExpNode ConstructTreeFromTokens(List<string> expression)
        {
            Stack<ExpNode> stack = new Stack<ExpNode>();
            foreach (string tok in expression)
            {
                if (operators.TryGetValue(tok, out OperatorToken op)) //if the token is an operator
                {
                    ExpNode newrightNode = stack.Pop(); //grabs the right and left nodes from the stack
                    ExpNode newLeftNode = stack.Pop();
                    ExpNode newOpNode = new OpNode(ref newLeftNode, ref newrightNode, op.Symbol[0]); // Creates an OpNode with the nodes we just popped off the stack.
                    stack.Push(newOpNode); // Adds the new operator node to the stack.
                }
                else //operand
                {
                    stack.Push(MakeDataNode(tok)); //push the operand node on to the stack
                }
            } //when there are no more tokens. There should only be one node on the stack.
            return stack.Pop(); // returns the root node of the tree.
        }

        //simple function that turns a string into a Data node.
        private ExpNode MakeDataNode(string operand)
        {
            double number;
            bool isDouble = double.TryParse(operand, out number); //only stores the operand in number if it is actually a double
            if (isDouble)
            {
                ValNode numNode = new ValNode(number); //makes a Numerical node with the operand's value
                return numNode;
            }
            else
            {
                VarNode varNode = new VarNode(operand); //makes a variable node with the operands variable name. 
                return varNode;
            }
        }

        // public facing interface to set a variable node.
        public void SetVar(string varName, double varValue)
        {
            VarNode search = new VarNode(varName, varValue); //creating a node so that it is easier to compare between nodes. This node stores the value of the new variable
            if (root as OpNode != null) //if root is an operator node
                SetVarNode(ref search, root as OpNode);
            else if (VariableNodeCompare(search, root)) //if root is a Variable node and has the same variable name
            {
                (root as VarNode).Value = varValue; //sets the root value to the new variable
            }
        }

        // recursive function to set a variable node.
        private void SetVarNode(ref VarNode input, OpNode node)
        {
            if (VariableNodeCompare(input, node.left)) //if the left node is a Variable node and has the same variable name
            {
                (node.left as VarNode).Value = input.Value;
            }
            else if (VariableNodeCompare(input, node.right)) //if the right node is a Variable node and has the same variable name
            {
                (node.right as VarNode).Value = input.Value;
            }
            else
            {
                if (node.right as OpNode != null) //if the right side node is an operator node
                    SetVarNode(ref input, node.right as OpNode); //check right subtree
                if (node.left as OpNode != null) //if the right side node is an operator node
                    SetVarNode(ref input, node.left as OpNode); //check right subtree
            }
        }
       
        public override string ToString()
        {
            return StringExpression;
        }

        //returns true only if both nodes are VariableNodes and the names of the variables are the same.
        public bool VariableNodeCompare(ExpNode inputLeftNode, ExpNode inputRightNode) 
        {
            VarNode leftNodeAsVar = inputLeftNode as VarNode;
            VarNode righttNodeAsVar = inputRightNode as VarNode;
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
