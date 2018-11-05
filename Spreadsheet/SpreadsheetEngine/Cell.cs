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
        protected int cellRowIndex;
        protected string cellText;
        protected string cellParsedValue;
        protected ExpTree valueTree;

        public event PropertyChangedEventHandler PropertyChanged; //event to handle when a property of a cell changes

        //constructor that sets column and row indexes.
        public Cell(int column, int row)
        {
            cellColumnIndex = column;
            cellRowIndex = row;
        }

        //getters of cell properties.
        public int RowIndex { get { return cellRowIndex; } }
        public int ColumnIndex { get { return cellColumnIndex; } }
        public string Value { get { return cellParsedValue; } }

        public string Text
        {
            get { return cellText; }
            set
            {
                if (value != cellText) //if the value being set is different than what is already in the cell.
                {
                    if (value[0] == '=')
                    {
                        valueTree = new ExpTree(value.Substring(1));
                        cellText = valueTree.Eval().ToString(); //change the value
                        OnPropertyChanged("variable"); //event that the text was changed.
                    }
                    else
                    {
                        cellText = value;
                        OnPropertyChanged("text"); //event that the text was changed.
                    }
                }
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); //fancy way of only using PropertyChanged if it isn't null
        }
    }
}
