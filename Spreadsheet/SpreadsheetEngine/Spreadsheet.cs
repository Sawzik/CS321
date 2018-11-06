//Isaac Schultz 11583435
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace CptS321
{
    public class Spreadsheet
    {
        public class CellVarNode : VarNode
        {
            Cell[,] cellArray;
            public CellVarNode(string var, ref Cell [,] cells)
            {
                variable = var;
                cellArray = cells;
            }

            public override double Eval()
            {
                int column = variable[0] - 'A'; //subtacts an ascii A from the first part of the coordinate to get a number from 0 o 26.
                int row = Int32.Parse(variable.Substring(1)) - 1; //Removes the letter from the string and converts it to an int. Subtracts 1 because array indexes start at 0, not 1.

                if (column > cellArray.GetLength(0) || row > cellArray.GetLength(1))
                    throw new IndexOutOfRangeException("Coordinate out of range");
                return double.Parse(cellArray[column, row].Value); //returns that cell at the parsed location.
            }
        }

        public class CellExpTree : ExpTree
        {
            Cell[,] cellArray;

            public CellExpTree(string expression, ref Cell [,] cells) : base(expression)
            {
                cellArray = cells;
            }

            protected override ExpNode MakeDataNode(string operand)
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
                    VarNode varNode = new CellVarNode(operand, ref cellArray); //makes a variable node with the cell location as a name and its value as a value
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
                throw new IndexOutOfRangeException("Coordinate out of range");
            return cells[column, row];
        }

        //same as normal GetCell, but can parse strings
        public Cell GetCell(string strCoords)
        {
            int column = strCoords[0] - 'A'; //subtacts an ascii A from the first part of the coordinate to get a number from 0 o 26.
            int row = Int32.Parse(strCoords.Substring(1)) - 1; //Removes the letter from the string and converts it to an int. Subtracts 1 because array indexes start at 0, not 1.

            if (column > columnCount || row > rowCount)
                throw new IndexOutOfRangeException("Coordinate out of range");
            return cells[column, row]; //returns that cell at the parsed location.
        }

        private string CalculateValueWithLink(string text, Cell senderCell)
        {
            if (text.Length > 0 && text[0] == '=' && text.Length > 1) //using & to check if there is anything in the string first. Then checking if it has an equals, and then checking if there is even anything after the equals.
            {
                text = text.Substring(1); //removes the first character of the string, which is =
                if (text.Length > 1) //if there are variables to replace, they will be at least 2 chars
                {
                    MatchCollection splitOperands = Regex.Matches(text, @"\w+\.?\d*"); //temporary way to get all the variables.
                    foreach (Match mat in splitOperands)
                    {
                        if (!Regex.Match(mat.Value, @"^\d+").Success) // if the match starts with a number then its not a coordinate and we dont have to retrieve a value.
                        {
                            SpreadsheetCell cell = GetCell(mat.Value) as SpreadsheetCell;
                            cell.ValueChanged += senderCell.OnValueChanged;
                            text = text.Replace(mat.Value, cell.Value); //replaces that substring in the text with that cell's value.   
                        }
                    }
                    ExpTree tree = new ExpTree(text);
                    text = tree.Eval().ToString();
                }                
                return text;
            }
            else
                return text;
        }

        private string CalculateValue(string text, Cell senderCell)
        {
            if (text.Length > 0 && text[0] == '=' && text.Length > 1) //using & to check if there is anything in the string first. Then checking if it has an equals, and then checking if there is even anything after the equals.
            {
                text = text.Substring(1); //removes the first character of the string, which is =
                if (text.Length > 1) //if there are variables to replace, they will be at least 2 chars
                {
                    ExpTree tree = new ExpTree(text);
                    text = tree.Eval().ToString();
                }
                return text;
            }
            else
                return text;
        }

        private void Spreadsheet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var cell = (SpreadsheetCell)sender; //casts the sending object as a SpreadsheetCell
            if (e.PropertyName == "Text")
            {
                try
                {
                    cell.SetValue(CalculateValueWithLink(cell.Text, cell)); //updates cell value if it needs to be
                }
                catch (Exception ex) //Kind of the nuclear option but there are so many exception types to handle one by one.
                {
                    cell.SetValue("#REF!");
                }
            }
            else
            {
                try
                {
                    cell.SetValue(CalculateValueWithLink(cell.Text, cell)); //updates cell value if it needs to be
                }
                catch (Exception ex) //Kind of the nuclear option but there are so many exception types to handle one by one.
                {
                    cell.SetValue("#REF!");
                }
            }

            CellPropertyChanged?.Invoke(sender, e); //fancy way to only call if CellPropertyChanged isnt null
        }
    }
}
