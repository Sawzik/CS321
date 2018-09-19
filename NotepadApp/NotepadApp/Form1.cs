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
using System.Numerics;

namespace NotepadApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Load from file event handler
        private void loadFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //checks if the user selected a file
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.IO.StreamReader selectedFile = new System.IO.StreamReader(openFileDialog1.FileName);
                LoadText(selectedFile);
            }
        }

        //loads the data from a textreader into the textbox
        private void LoadText(System.IO.TextReader tr)
        {
            textBox1.Text = tr.ReadToEnd();
            tr.Close();
        }

        private void load50toolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader firstFifty = new FibonacciTextReader(50);
            LoadText(firstFifty);
        }

        private void load100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FibonacciTextReader firstHundred = new FibonacciTextReader(100);
            LoadText(firstHundred);
        }
    }
}
