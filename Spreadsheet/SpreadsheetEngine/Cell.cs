//Isaac Schultz 11583435

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CptS321
{
    public abstract class Cell : INotifyPropertyChanged
    {
        protected int cRowIndex;
        protected int cColumnIndex;
        protected string cText;
        protected string cValue;

        public event PropertyChangedEventHandler PropertyChanged;

        public Cell(int column, int row)
        {
            cColumnIndex = row;
            cRowIndex = column;
        }

        public int RowIndex
        {

        }
    }
}
