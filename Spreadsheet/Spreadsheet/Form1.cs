//Isaac Schultz 11583435
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CptS321;

namespace SpreadsheetForm
{
    public partial class Form1 : Form
    {
        private Spreadsheet sheet;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sheet = new Spreadsheet(26, 50);
            sheet.CellPropertyChanged += Sheet_CellPropertyChanged; //adds listener to sheets event handler
            MakeCells();
            sheet.GetCell(1, 1).Text = "This is a test";
        }

        private void Sheet_CellPropertyChanged(object sender, EventArgs e)
        {
            Cell cell = (Cell)sender; //casts sender as a cell
            dataGridView1[cell.RowIndex, cell.ColumnIndex].Value = cell.Value; //updates the dataGridView's cells with the cells new value.
        }

        private void MakeCells()
        {
            for (char i = 'A'; i < 'Z' + 1 ; i++) //counts up to Z
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = i.ToString();
                dataGridView1.Columns.Add(column);
            }
            for (int i = 0; i < 50; i++) //counts up to Z
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = ((i + 1).ToString());
            }
            dataGridView1.RowHeadersWidth = 50; //makes header big enough to see the numbers
        }


    }
}
