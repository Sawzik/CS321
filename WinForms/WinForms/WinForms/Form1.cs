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
            Dictionary<int, int> dict = new Dictionary<int, int>();     //1    
            foreach (int item in data)                                  //N *
                if (!dict.ContainsKey(item.GetHashCode()))              //1
                    dict.Add(item.GetHashCode(), item);                 //1

            int dictNumber = dict.Count();                              //1 = O(2 + 2N)

            //O(1) storage complexity
            int oStorage = 0;  //number of non-dublicate permutations   //1
            for (int firstItem = 0; firstItem != data.Count(); firstItem++)                             //1
            {
                for (int secondItem = firstItem + 1; secondItem != data.Count(); secondItem++)                         //1
                    if (data[firstItem] == data[secondItem]) oStorage++;
            }

            //oStorage = oStorage - (data.Count() * data.Count());
            oStorage /= 2;

            //Sorted O(1) storage and O(n) time complexity

            data.Sort();                                                                                            //Ignored
            int sortedNumber = 0;                                                                                   //1 Storage     1 Time
            for (int firstItem = 0; firstItem != data.Count(); firstItem++)                                         //1 Storage     N Time
            {                                                                                                       //                \/
                while (firstItem != data.Count() - 1 && data[firstItem] == data[firstItem + 1]) firstItem++;        //              Part of N Time
                sortedNumber++;                                                                                     //              1 Time
            }

            textBox1.AppendText(dictNumber.ToString() + "\t" + oStorage.ToString() + "\t" + sortedNumber.ToString());
        }
    }
}