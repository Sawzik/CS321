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
        private SpreadsheetCell selectedCell; //reference to the current cell being edited

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sheet = new Spreadsheet(26, 50); //makes a 50 by 26 spreadsheet
            sheet.CellPropertyChanged += Sheet_CellPropertyChanged; //subscribes to the sheets event handler
            MakeCells();
        }

        private void Sheet_CellPropertyChanged(object sender, EventArgs e)
        {
            Cell cell = (Cell)sender; //casts sender as a cell
            dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value; //updates the dataGridView's cells with the cells new value.
        }

        private void MakeCells()
        {
            for (char i = 'A'; i < 'Z' + 1 ; i++) //counts up to Z
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn(); //makes a column TextBox
                column.HeaderText = i.ToString(); //sets the column header to the letter in its position
                dataGridView1.Columns.Add(column); //adds the column into the dataGridView
            }
            for (int i = 0; i < 50; i++) //counts up to Z
            {
                dataGridView1.Rows.Add(); //adds a row
                dataGridView1.Rows[i].HeaderCell.Value = ((i + 1).ToString()); //then sets its header to the number of that row
            }
            dataGridView1.RowHeadersWidth = 50; //makes header big enough to see the numbers
        }

        //demo to show that changing values in the spreadsheet changes them in the dataGridView
        private void DemoCells()
        {
            //randomly puts "random string" in 50 cells
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
                sheet.GetCell(rand.Next(25), rand.Next(50)).Text = "Random String";

            //fills all the cells in column B to "This is cell B#"
            for (int i = 0; i < sheet.RowCount; i++)
                sheet.GetCell(1, i).Text = "This is cell B" + (i+1).ToString();

            //fills all the cells in column C# to what is in B#
            for (int i = 0; i < sheet.RowCount; i++)
                sheet.GetCell(2, i).Text = "=B" + (i + 1).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DemoCells();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedCell = sheet.GetCell(e.ColumnIndex, e.RowIndex) as SpreadsheetCell; //saves a reference to the current cell
            object GUIcell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            if (GUIcell != null) // If the cell isn't empty
                textBox1.Text = GUIcell.ToString(); //updates the textbox with the value of the cell
            else
                textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            selectedCell.Text = textBox1.Text;
        }

    }
}
