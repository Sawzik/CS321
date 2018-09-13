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
                data.Add(rand.Next(20000));            
                                                                                                                    //Complexity:
            //Dictionary                                                                                            //Time:         Storage:
            Dictionary<int, int> dict = new Dictionary<int, int>();                                                 //1         
            foreach (int item in data)                                                                              //N *           1
                if (!dict.ContainsKey(item.GetHashCode()))                                                          //  2
                    dict.Add(item.GetHashCode(), item);                                                             //  2           N
            int dictStorage = dict.Count();                                                                         //2             1
                                                                                                                    //3 + 3N        2 + N

            //O(1) storage complexity
            int oStorage = 0;   //number of dublicates in the list                                                  //1             1
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
            int sortedStorage = 0;                                                                                  //1             1
            for (int firstItem = 0; firstItem < data.Count(); firstItem++)                                          //1             N
            {                                                                                                       //              \/
                while (firstItem != data.Count() - 1 && data[firstItem] == data[firstItem + 1]) firstItem++;        //              Part of N Time
                sortedStorage++;                                                                                    //              1
            }                                                                                                       //2             1 + 2N

            StringBuilder textOutput = new StringBuilder();
            textOutput.AppendLine("1. HashSet Method : " + dictStorage.ToString());
            textOutput.AppendLine("\tMy method uses a foreach loop of time complexity O(n) that adds each unique element into the dictionary. ");
            textOutput.AppendLine("\tEach iteration checks if the element is already in the dictionary by utilizing the ContainsKey() function, ");
            textOutput.AppendLine("\twhich according to MSDN documentation, has a time complexity of O(1). If the element is not in the ");
            textOutput.AppendLine("\tdictionary already then it is added to it with time complexity O(1). The total time complexity of the loop is n ");
            textOutput.AppendLine("\ttimes the time complexity of each of the operations within it: O(1) for the assignment of the foreach loop's ");
            textOutput.AppendLine("\tvariable with each iteration, O(1) to check if the dictionary has the element or not, and O(1) to add the item ");
            textOutput.AppendLine("\tto the dictionary. This leaves the total time complexity for the loop as O(3n). There is also the a complexity ");
            textOutput.AppendLine("\tof O(1) to initialize the dictionary object and a time complexity of O(2) to initialize a variable that stores the ");
            textOutput.AppendLine("\tdictionary's Count() function. Adding this up gives a total time complexity of O(1 + 2 + n(1 + 1 + 1)) which ");
            textOutput.AppendLine("\tsimplifies to O(3 + 3n). This function can be referred to as having O(n) time complexity.");
            textOutput.AppendLine("2. O(1) Storage Method : " + oStorage.ToString());
            textOutput.AppendLine("3. Sorted Method : " + sortedStorage.ToString());
            textBox1.AppendText(textOutput.ToString());
        }
    }
}