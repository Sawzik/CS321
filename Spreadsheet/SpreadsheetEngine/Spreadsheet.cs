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

        private string CalculateValue(string text)
        {
            if (text.Length > 0 && text[0] == '=')
            {
                text = text.Substring(1); //removes the first character of the string, which is =
                if (text.Length > 1) // If there is a coordinate after the equals
                {
                    MatchCollection splitOperands = Regex.Matches(text, @"\w+\.?\d*"); //temporary way to get all the variables.
                    foreach (Match mat in splitOperands)
                    {
                        if (!Regex.Match(mat.Value, @"^\d+").Success) // if the match starts with a number then its not a coordinate and we dont have to retrieve a value.
                            text = text.Replace(mat.Value, GetCell(mat.Value).Value); //replaces that substring in the text with that cell's value.   
                    }
                }
                return text;
            }
            else
                return text;
        }

        private void Spreadsheet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var cell = (SpreadsheetCell)sender; //casts the sending object as a SpreadsheetCell
            cell.SetValue(CalculateValue(cell.Text)); //updates cell value if it needs to be

            CellPropertyChanged?.Invoke(sender, e); //fancy way to only call if CellPropertyChanged isnt null
        }
    }
}
