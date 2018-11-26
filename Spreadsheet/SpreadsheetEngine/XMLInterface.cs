using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Linq;

using System.Diagnostics;

namespace CptS321
{
    public class XMLInterface
    {
        public void XMLSave(/*Stream saveStream,*/ List<Cell> cells)
        {
            XDocument xmlSheet = new XDocument();
            XElement root = new XElement("Spreadsheet");
            foreach (Cell cell in cells)
            {
                string cellCoordAsString = "";
                cellCoordAsString += (char)(cell.ColumnIndex + 'A');
                cellCoordAsString += cell.RowIndex.ToString();


                XElement xmlCell = new XElement("Cell",
                        new XAttribute("Name", cellCoordAsString),
                        new XElement("Text", cell.Text),
                        new XElement("Value", cell.Value)
                );
                //Debug.WriteLine(xmlCell);
                root.Add(xmlCell);
            }
            xmlSheet.Add(root);
            Debug.WriteLine(xmlSheet);
        }
    }
}
