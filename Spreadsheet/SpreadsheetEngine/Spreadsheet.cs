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
                cValue = value;
            }
        }

        public event PropertyChangedEventHandler CellPropertyChanged;
        private SpreadsheetCell[,] cells;

        int cColumnCount;
        int cRowCount;

        public int ColumnCount { get { return cColumnCount; } }
        public int RowCount { get { return cRowCount; } }

        public Spreadsheet(int columns, int rows)
        {
            cColumnCount = columns;
            cRowCount = rows;

            //creation of a 2D array of spreadsheetCells (references)
            cells = new SpreadsheetCell[columns, rows];
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    cells[i, j] = new SpreadsheetCell(i, j); //makes a SpreadsheetCell with its location in the array.
                    //cells[i, j].PropertyChanged += Spreadsheet_PropertyChanged;
                }
            }
        }
        
    }
}
