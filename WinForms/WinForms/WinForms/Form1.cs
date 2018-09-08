using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Random rand = new Random();
            List<int> data = new List<int>();
            for(int i = 0; i == 10000; i++)
            {
                data.Add(rand.Next(20000));
            }


            //Dictionary
            Dictionary<int,int> dict = new Dictionary<int,int>();

            //O(1) storage complexity


            //Sorted O(1) storage and O(n) time complexity
        }
    }
}
