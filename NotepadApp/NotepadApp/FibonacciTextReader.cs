//Isaac Schultz 11583435

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace NotepadApp
{
    public class FibonacciTextReader : System.IO.TextReader
    {
        public int lines = 0;
        BigInteger twoBack = 0;
        BigInteger oneBack = 0;
        BigInteger current = 1;
        int currentNumber = 0;

        public FibonacciTextReader(int lineInput)
        {
            lines = lineInput + 1; //since I am outputing the 0th term if the sequence, I need to increase the count by 1.
        }

        public override String ReadLine()
        {
            if (current == 1 && oneBack == 0) //0th Number
            {
                oneBack = 1;
                current = 1;
                currentNumber = 1;
                return "0: 0";
            }
            else if (current == 1 && oneBack == 1) //1st number
            {
                oneBack = 1;
                current = 2;
                currentNumber = 2;
                return "1: 1";
            }
            else //any other number in the sequence
            {
                twoBack = oneBack;
                oneBack = current;
                current = twoBack + oneBack;
                return currentNumber++ + ": " + twoBack.ToString();
            }
        }

        public override String ReadToEnd()
        {

            StringBuilder output = new StringBuilder();

            //loads each iteration into the stringbuilder, appending a new line for each number.
            for (int i = lines; i != 0; i--)
                output.AppendLine(this.ReadLine());

            return output.ToString();
        }
    }
}
