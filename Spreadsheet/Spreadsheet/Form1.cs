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
            sheet.CellPropertyChanged += Sheet_CellPropertyChanged; //subscribes to the sheets event handler
            MakeCells();
            //sheet.GetCell(1, 1).Text = "This is a test";
            dataGridView1.Rows[1].Cells[1].Value = "This is a test";
            //DemoCells();
        }

        private void Sheet_CellPropertyChanged(object sender, EventArgs e)
        {
            Cell cell = (Cell)sender; //casts sender as a cell
            //dataGridView1.Rows[cell.RowIndex].Cells[1].Value = cell.Value; //updates the dataGridView's cells with the cells new value.
            dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value; //updates the dataGridView's cells with the cells new value.
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

        private void DemoCells()
        {
            Random rand = new Random();
            for (int i = 0; i < 25; i++)            
                sheet.GetCell(rand.Next(25), rand.Next(50)).Text = "Random String";
            for(int i = 1; i < 50; i++)
                sheet.GetCell(2, i).Text = "This is cell B" + i.ToString();
            for (int i = 1; i < 50; i++)
                sheet.GetCell(2, i).Text = "=B" + i.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DemoCells();
        }
    }
}
