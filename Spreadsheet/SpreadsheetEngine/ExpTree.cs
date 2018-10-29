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

        public class Token
        {
            private string symbol;
            int precedence;
            bool rightAssociative;

            public Token(string inputSymbol, int inputPrecedence, bool inputRightAssociative)
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
        private static readonly Dictionary<string, Token> operators = new Dictionary<string, Token>
        {
            {"(", new Token("(", 4, false) },
            {")", new Token(")", 4, false) },
            {"*", new Token("*", 3, false) },
            {"/", new Token("/", 3, false) },
            {"+", new Token("+", 2, false) },
            {"-", new Token("-", 2, false) }
        };

        public ExpTree(string expression)
        {
            StringExpression = expression; //saves the expression for display purposes
            root = ConstructTree(expression); //constructs the tree at the root
        }

        public static List<Token> SplitIntoTokens(string source, string regexPattern)
        {
            List<Token> splitString = new List<Token>();
            MatchCollection matches = Regex.Matches(source, regexPattern, RegexOptions.IgnorePatternWhitespace);
            int currIndex = 0; // a counter to keep track of where we are in the string.
            if (matches.Count < 1) //if there are no matches then we dont need to split.
            {
                Token newToken = new Token(source, 1, false);
                splitString.Add(newToken); //puts the single token in the list
            }
            else
            {
                foreach (Match match in matches)
                {
                    if (match.Index > currIndex) // when the current match IS the regexPattern
                    {
                        Token newOperatorToken = new Token(source.Substring(currIndex, match.Index - currIndex), 1, false);
                        splitString.Add(newOperatorToken); //adds the matched part of the string as Token of the list.
                    }
                    //creates a token that contains that substring. has precedence 1 since it is a value
                    Token newToken = new Token(match.Value, operators[match.Value].Precedence, false);
                    splitString.Add(newToken); // adds the non matched part of the string into the list
                    currIndex = match.Index + match.Length; //Update the current index to the end of the matched string.
                }
                if (currIndex < source.Length) // if there is an unmatched string at the end of the source string
                {
                    //creates a token that contains that substring. has precedence 1 since it is a value
                    Token newToken = new Token(source.Substring(currIndex), 1, false);
                    splitString.Add(newToken);
                }
            }
            return splitString;
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
                    //creates a token that contains that substring.
                    splitString.Add(source.Substring(currIndex));
                }
            }
            return splitString;
        }

        public void ShuntingYard(string expression)
        {
            List<string> tokens = Split(expression, @"[-+\*/\(\)]"); // Splits the expression up into individual tokens
            Stack<string> stack = new Stack<string>();
            Stack<string> postFix = new Stack<string>();
            foreach (string tok in tokens) //debug to print the tokens.
                Console.WriteLine(tok);
            foreach (string tok in tokens)
            {
                Match token = Regex.Match(expression, @"[-+\*/]");
                if (!token.Success) //if the token is a value               
                    postFix.Push(tok); //push it to the output in postFix notation
                else if (operators.TryGetValue(tok, out Token operatorToken1))// when the token is an operator.
                {
                    while (stack.Count > 0 && operators.TryGetValue(tok, out Token operatorToken2))
                    {
                        int comparePrecedence = operatorToken1.Precedence - operatorToken2.Precedence;
                        if (comparePrecedence < 0 || !operatorToken1.RightAssociative && comparePrecedence <= 0)
                            postFix.Push(stack.Pop());
                        else
                            break;
                    }
                    stack.Push(tok);
                }
                else if (tok == "(")
                {
                    stack.Push(tok);
                }
                else if (tok == ")")
                {
                    string top = "";
                    while ( stack.Count > 0 && (top = stack.Pop()) != "(")
                    {
                        postFix.Push(top);
                    }
                    if (top != "(")
                        throw new ArgumentException("No matching left parenthesis.");
                }
            }
            while (stack.Count > 0)
            {
                string top = stack.Pop();
                if (!operators.ContainsKey(top)) throw new ArgumentException("No matching right parenthesis");
                postFix.Push(top);
            }           
        }

        public void ShuntingYardToken(string expression)
        {
            List<Token> tokens = SplitIntoTokens(expression, @"[-+\*/\(\)]"); // Splits the expression up into individual tokens
            if (tokens.Count < 2)   //if there is only one value and nothing else
                return;
            Stack<Token> stack = new Stack<Token>();
            Stack<Token> postFix = new Stack<Token>();
            foreach (Token tok in tokens) //debug to print the tokens.
                Console.WriteLine(tok.Symbol + tok.Precedence); 
            foreach (Token tok in tokens)
            {
                if (tok.Precedence == 1) //if the token is a value. values have precedence 1.                
                    postFix.Push(tok); //push it to the output in postFix notation
                else if (operators.ContainsKey(tok.Symbol))// when the token is an operator.
                {
                    while (stack.Count > 0 && operators.ContainsKey(stack.Peek().Symbol))
                    {
                        int comparePrecedence = tok.Precedence - stack.Peek().Precedence;
                        if (comparePrecedence < 0 || !stack.Peek().RightAssociative && comparePrecedence <= 0)
                            postFix.Push(stack.Pop());
                        else
                            break;
                    }
                    stack.Push(tok);
                }
                else if (false)
                {

                }
            }
        }

        // Lame non Shunting Yard algorithm
        private ExpNode ConstructTree(string expression)
        {
            Regex opRegex = new Regex(@"[-+\*/]"); //matches if there is an operator anywhere in the string
            Regex varRegex = new Regex(@"[\w\.]+\z"); //matches the last alphanumeric characters in the string
            Match op = opRegex.Match(expression);
            Match var = varRegex.Match(expression);

            if (op.Success) // if there is an operator
            {
                for (int i = expression.Length - 1; i >= 0; i--)
                {
                    if (opRegex.Match(expression[i].ToString()).Success) // If the character is an operator
                    {
                        OpNode newNode = new OpNode(expression[i]);
                        newNode.left = ConstructTree(expression.Substring(0, i)); //calls construct node on the current string up until just before the operator
                        newNode.right = ConstructTree(expression.Substring(i + 1)); //calls construct node on the current string after the operator
                        return newNode;
                    }
                }
            }
            return MakeDataNode(var.Value); //if there is no operators, then this must be a variable or value.
        }

        //Unused function
        private ExpNode ConstructNode(string expression)
        {
            Regex varRegex = new Regex(@"\w+\z"); //matches the last alphanumeric characters in the string
            Match var = varRegex.Match(expression);
            return MakeDataNode(var.Value);
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
