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

        public event PropertyChangedEventHandler PropertyChanged; //event to handle when a property of a cell changes
        public event PropertyChangedEventHandler ValueChanged; //event to handle when a property of a cell changes

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
                    cellText = value; //change the value
                    OnPropertyChanged("Text"); //event that the text was changed.                     
                }           
            }
        }

        protected void OnPropertyChanged(string name)
        {
            if (name == "Text")
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); //fancy way of only using PropertyChanged if it isn't null
            if (name == "Value")
                ValueChanged?.Invoke(this, new PropertyChangedEventArgs(name)); //fancy way of only using PropertyChanged if it isn't null
        }

        public void OnValueChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Text");
        }

    }

    public class SpreadsheetCell : Cell
    {
        private HashSet<SpreadsheetCell> referencedCells = new HashSet<SpreadsheetCell>();

        public SpreadsheetCell(int column, int row) : base(column, row) { } //uses the base cell class constructor.

        public void SetValue(string value)
        {
            if (value != cellParsedValue)
            {
                cellParsedValue = value; //cellText from abstract cell class
                OnPropertyChanged("Value");
            }
        }

        public HashSet<SpreadsheetCell> ReferencedCells { get { return referencedCells; } } 

        public void RemoveReferenceToCell(SpreadsheetCell cell)
        {
            referencedCells.Remove(cell);
        }

    }
}
