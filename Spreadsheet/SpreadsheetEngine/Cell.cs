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
            get { return cRowIndex; }            
        }
        public int ColumnIndex
        {
            get { return cColumnIndex; }        
        }
        public string Value
        {
            get { return cValue; }
        }

        public string Text
        {
            get { return cText; }
            set
            {
                if (value != cText)
                {
                    cText = value;
                    //OnPropertyChanged("text"); //event that the text was changed.
                }
            }
        }

        protected void OnPropertChanged(string name)
        {
            //if (ProperyChanged != null)
            //    PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
