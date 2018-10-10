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

            public event PropertyChangedEventHandler CellPropertyChanged;
        }
    }
}
