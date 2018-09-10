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
                                                                                                                    //Complexity:
            //Dictionary                                                                                            //Time:         Storage:
            Dictionary<int, int> dict = new Dictionary<int, int>();                                                 //1         
            foreach (int item in data)                                                                              //N *           1
                if (!dict.ContainsKey(item.GetHashCode()))                                                          //  1
                    dict.Add(item.GetHashCode(), item);                                                             //  1           N
            int dictStorage = dict.Count();                                                                          //1             1
                                                                                                                    //2 + 3N        2 + N

            //O(1) storage complexity
            int oStorage = 0;           //number of dublicates in the list                                          //1             1
            for (int firstItem = 0; firstItem < data.Count(); firstItem++)                                          //N *           1                        
                for (int secondItem = firstItem + 1; secondItem < data.Count(); secondItem++)                       //  N *         1                
                    if (data[firstItem] == data[secondItem])                                                        //    1
                    {
                        oStorage++; //If we know there is a duplicate we dont have to check for more                //    1
                        break;                                                                                      //    1
                    }                        
            oStorage = data.Count() - oStorage; //subtracts the duplicates from the total number                    //1
                                                                                                                    //1 + 4N^2      3

            //Sorted O(1) storage and O(n) time complexity
            data.Sort();                                                                                            //Ignored
            int sortedStorage = 0;                                                                                   //1             1
            for (int firstItem = 0; firstItem < data.Count(); firstItem++)                                          //1             N
            {                                                                                                       //              \/
                while (firstItem != data.Count() - 1 && data[firstItem] == data[firstItem + 1]) firstItem++;        //              Part of N Time
                sortedStorage++;                                                                                     //              1
            }                                                                                                       //2             1 + 2N

            StringBuilder textOutput = new StringBuilder();
            textOutput.AppendLine("1. HashSet Method : " + dictStorage.ToString());
            textOutput.AppendLine("\tPlaceholder explanation of time complexity of HashSet Function");
            textOutput.AppendLine("2. O(1) Storage Method : " + oStorage.ToString());
            textOutput.AppendLine("3. Sorted Method : " + sortedStorage.ToString());
            textBox1.AppendText(textOutput.ToString());
        }
    }
}