using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spreadsheet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            for (char i = 'A'; i != 'Z'; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = i.ToString();
                dataGridView1.Columns.Add(column);
            }
        }
    }
}
