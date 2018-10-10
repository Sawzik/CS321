//Isaac Schultz 11583435
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CptS321
{
    class Spreadsheet
    {
        private class SpreadsheetCell : Cell
        {
            public SpreadsheetCell(int column, int row) : base(column, row) { } //uses the base cell class constructor.

            public void SetValue(string value)
            {
                cellText = value; //cellText from abstract cell class
            }
        }

        private int columnCount;
        private int rowCount;
        public event PropertyChangedEventHandler CellPropertyChanged;
        public int ColumnCount { get { return columnCount; } }
        public int RowCount { get { return rowCount; } }

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
       
        public Cell GetCell(int column, int row)
        {
            return cells[column, row];
        }

        //same as normal GetCell, but can parse strings
        public Cell GetCell(string strCoords)
        {
            int column = strCoords[0] - 'A'; //subtacts an ascii A from the first part of the coordinate to get a number from 0 o 26.
            int row = Int32.Parse(strCoords.Substring(1)) - 1; //Removes the letter from the string and converts it to an int. Subtracts 1 because array indexes start at 0, not 1.
            return cells[column, row]; //returns that cell at the parsed location.
        }

        private string CalculateValue(string text)
        {
            if (text[0] != '=') //when it isnt a calculated cell.
                return text;
            string calculatedString = text.Substring(1); //removes the first character of the string, which is =
            return GetCell(calculatedString).Value; //returns the value of the cell that is being referenced.
        }

        private void Spreadsheet_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var cell = (SpreadsheetCell)sender; //casts the sending object as a SpreadsheetCell
            cell.SetValue(CalculateValue(cell.Text)); //updates cell value if it needs to be

            if (CellPropertyChanged != null) //only calls event if the property is actually changed.
                CellPropertyChanged(sender, e);
        }
    }
}
