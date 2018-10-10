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
        protected int cellColumn;
        protected int cellRow;
        protected string cellData;
        protected string cellParsed;

        public event PropertyChangedEventHandler PropertyChanged;

        public Cell(int column, int row)
        {
            cellColumn = row;
            cellRow = column;
        }

        public int RowIndex
        {
            get { return cellRow; }            
        }
        public int ColumnIndex
        {
            get { return cellColumn; }        
        }
        public string Value
        {
            get { return cellParsed; }
        }

        public string Text
        {
            get { return cellData; }
            set
            {
                if (value != cellData)
                {
                    cellData = value;
                    OnPropertyChanged("text"); //event that the text was changed.
                }
            }
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
