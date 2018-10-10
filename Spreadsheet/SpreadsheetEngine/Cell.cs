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
        protected int cellColumnIndex;
        protected int cellRowndex;
        protected string cellText;
        protected string cellParsedValue;

        public event PropertyChangedEventHandler PropertyChanged;

        public Cell(int column, int row)
        {
            cellColumnIndex = row;
            cellRowndex = column;
        }

        public int RowIndex
        {
            get { return cellRowndex; }            
        }
        public int ColumnIndex
        {
            get { return cellColumnIndex; }        
        }
        public string Value
        {
            get { return cellParsedValue; }
        }

        public string Text
        {
            get { return cellText; }
            set
            {
                if (value != cellText)
                {
                    cellText = value;
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
