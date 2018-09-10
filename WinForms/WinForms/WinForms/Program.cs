using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //Random rand = new Random();
            //List<int> data = new List<int>();
            //for (int i = 0; i == 10000; i++)
            //{
            //    data.Add(rand.Next(20000));
            //}

            //Console.WriteLine(data.Count);


            ////Dictionary
            //Dictionary<int, int> dict = new Dictionary<int, int>();
            //foreach (int item in data)
            //    dict.Add(item % 13, item);
            //int dictNumber = dict.Count();

            ////O(1) storage complexity


            ////Sorted O(1) storage and O(n) time complexity
        }
    }
}
