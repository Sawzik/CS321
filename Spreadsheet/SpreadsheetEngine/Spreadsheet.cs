//Isaac Schultz 11583435
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CptS321
{
    public class Spreadsheet
    {
        class SpreadsheetVarNode : VarNode
        {
            public SpreadsheetVarNode(string varInput, ref Cell[,] cells) //constructor for the variablenode with defaults if no data is specified
            {
                int column = varInput[0] - 'A'; //subtacts an ascii A from the first part of the coordinate to get a number from 0 o 26.
                int row = Int32.Parse(varInput.Substring(1)) - 1; //Removes the letter from the string and converts it to an int. Subtracts 1 because array indexes start at 0, not 1.

                numericalValue = double.Parse(cells[column, row].Value); //returns that cell at the parsed location.
                variable = varInput;
            }
        }

        class SpreadsheetTree : ExpTree
        {
            public SpreadsheetTree(string expression) : base(expression) { } //uses the base cell class constructor.
            protected Cell[,] cellArray; // used to look up variable names from the spreadsheet.
            public SpreadsheetTree(string expression, ref Cell[,] cells)
            {
                cellArray = cells; //saves a reference to spreadsheet cells
                StringExpression = expression; //saves the expression for easy display purposes
                root = ConstructTreeFromTokens(ShuntingYard(expression));
            }

            protected override ExpNode MakeDataNode(string operand) //the only functionality we need to change is the handling of variable nodes
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
                    VarNode varNode = new SpreadsheetVarNode(operand, ref cellArray); //makes a variable node with the operands variable name. 
                    return varNode;
                }
            }
        }

        private int columnCount;
        private int rowCount;        
        public int ColumnCount { get { return columnCount; } }
        public int RowCount { get { return rowCount; } }
        public event PropertyChangedEventHandler CellPropertyChanged; //event that signals when a cell has changed

        private SpreadsheetCell[,] cells;
        public Spreadsheet(int columns, int rows)
        {
            columnCount = columns;
            rowCount = rows;

            cells = new SpreadsheetCell[columns, rows]; //allocating memory
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    cells[i, j] = new SpreadsheetCell(i, j); //creates a cell with its position in the array.
                    cells[i, j].PropertyChanged += Spreadsheet_PropertyChanged; //tells the cell to call Spreadsheet_propertyChanged when PropertyChanged is called.              
                }
            }
        }
        
        //gets a cell at a position in the spreadsheet
        public Cell GetCell(int column, int row)
        {
            if (column > columnCount || row > rowCount)
                throw new IndexOutOfRangeException("GetCell");
            return cells[column, row];
        }

        //same as normal GetCell, but can parse strings
        public Cell GetCell(string strCoords)
        {
            int column = strCoords[0] - 'A'; //subtacts an ascii A from the first part of the coordinate to get a number from 0 o 26.
            int row = Int32.Parse(strCoords.Substring(1)) - 1; //Removes the letter from the string and converts it to an int. Subtracts 1 because array indexes start at 0, not 1.

            if (column > columnCount || row > rowCount)
                throw new IndexOutOfRangeException("GetCell");
            return cells[column, row]; //returns that cell at the parsed location.
        }

        private string CalculateValue(string text)
        {
            if (text.Length > 0)
            {
                if (text[0] != '=') //when it isnt a calculated cell.
                    return text;
                text = text.Substring(1); //removes the first character of the string, which is =
            }

            return GetCell(text).Value; //returns the value of the cell that is being referenced.
        }

        private void Spreadsheet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var cell = (SpreadsheetCell)sender; //casts the sending object as a SpreadsheetCell
            cell.SetValue(CalculateValue(cell.Text)); //updates cell value if it needs to be

            CellPropertyChanged?.Invoke(sender, e); //fancy way to only call if CellPropertyChanged isnt null
        }
    }
}
