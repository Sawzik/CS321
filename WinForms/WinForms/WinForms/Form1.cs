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
            for (int i = 0; i != 10000; i++)
            {
                data.Add(rand.Next(20000));
            }

            Console.WriteLine(data.Count);


            //Dictionary
            Dictionary<int, int> dict = new Dictionary<int, int>(); //1    
            foreach (int item in data)                              //N *
                if (!dict.ContainsKey(item.GetHashCode()))          //1
                    dict.Add(item.GetHashCode(), item);             //1

            int dictNumber = dict.Count();                          //1 = O(2 + 2N)

            //O(1) storage complexity
            int oStorage = 0;                                       //1
            foreach (int firstItem in data)                         //1
            {
                foreach (int secondItem in data)                    //1
                    if (firstItem != secondItem) oStorage++;
            }


            //Sorted O(1) storage and O(n) time complexity


            textBox1.AppendText(dictNumber.ToString() + "\nMemes" + oStorage.ToString());
        }
    }
}
