using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CptS321;
using System.Text.RegularExpressions;

namespace ConsoleMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex test = new Regex(@"(\w+)(\-|\+|/|\*)(\w+\z)");
            string text = "A1-A2-B4-memes";
            MatchCollection matches = test.Matches(text);

            Console.WriteLine("{0} matches found in:\n   {1}",
                          matches.Count,
                          text);

            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;

                Console.WriteLine("'{0}'",
                                  groups["3"].Value);
            }


            Console.ReadKey();
        }
    }
}
