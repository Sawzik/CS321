using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Spreadsheet
{
    public abstract class Cell : INotifyPropertyChanged
    {
        protected int cellColumn;
        protected int cellRow;
        protected string cellData;
        protected string cellParsed;

        public event PropertyChangedEventHandler PropertyChanged;

        public Cell(int column, int row)
        {
            cellColumn = column;
            cellRow = row;
        }
           
        }
    }
}
