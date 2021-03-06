﻿//Isaac Schultz 11583435
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
    public partial class SpreadSheetForm : Form
    {
        private Spreadsheet sheet;
        private SpreadsheetCell selectedCell; //reference to the current cell being edited

        public SpreadSheetForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sheet = new Spreadsheet(26, 50); //makes a 50 by 26 spreadsheet
            sheet.CellPropertyChanged += Sheet_CellPropertyChanged; //subscribes to the sheets event handler
            selectedCell = sheet.GetCell(0, 0) as SpreadsheetCell; //sets a default selected cell to prevent crashes
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
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
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

        private void ForceSheetUpdate()
        {
            for (int i = 0; i < sheet.ColumnCount; i++)
            {
                for (int j = 0; j < sheet.RowCount; j++)
                {
                    Cell cell = sheet.GetCell(i, j); //creates a cell with its position in the array.
                    dataGridView1.Rows[cell.RowIndex].Cells[cell.ColumnIndex].Value = cell.Value; //updates the dataGridView's cells with the cells new value.
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedCell.Text = textBox1.Text; //save the text currently being typed
            try
            {
                selectedCell = sheet.GetCell(e.ColumnIndex, e.RowIndex) as SpreadsheetCell; //saves a reference to the current cell
                textBox1.Text = selectedCell.Text; //updates the textbox with the text of the cell
            }
            catch (IndexOutOfRangeException exception) //if the cell clicked is something outside of the spreadsheet
            {
                Console.WriteLine(exception.Message);
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                selectedCell.Text = textBox1.Text;
                e.Handled = true;
            }
        }

        private void demoCellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DemoCells();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.ShowDialog();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //options for filetypes in the open file window
            openFileDialog1.Filter = "All files (*.*)|*.*|xml files (*.xml)|*.xml";
            openFileDialog1.FilterIndex = 2;

            //checks if the user selected a file
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {   // loading from the xml file
                using (XMLInterface loadFile = new XMLInterface(openFileDialog1.OpenFile()))
                    if (loadFile != null) //if the path isnt null
                    {
                        try
                        {
                            sheet = loadFile.XMLLoad();
                            sheet.CellPropertyChanged += Sheet_CellPropertyChanged; //subscribes to the sheets event handler    
                            selectedCell = sheet.GetCell(0, 49) as SpreadsheetCell; //sets a default selected cell to prevent crashes
                            ForceSheetUpdate();
                        }
                        catch (Exception except)
                        {
                            Console.WriteLine(except.Message);
                        }
                    }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //options for filetypes in the save file window
            saveFileDialog1.Filter = "All files (*.*)|*.*|xml files (*.xml)|*.xml";
            saveFileDialog1.FilterIndex = 2;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //using cleans up after itself by calling Dispose once its done.
                using (XMLInterface saveFile = new XMLInterface(saveFileDialog1.OpenFile())) //opens the save file window and makes an XMLInterface with the path        
                    if (saveFile != null) //if the path isnt null
                        saveFile.XMLSave(sheet.GetUsedCells()); //write to the file
            }
        }
    }
}
