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
        Stream stream;

        public XMLInterface()
        {
            stream = null;
        }

        public XMLInterface(Stream constructStream)
        {
            stream = constructStream;
        }

        public void XMLSave(List<Cell> cells)
        {
            XDocument xmlSheet = new XDocument();
            XElement root = new XElement("Spreadsheet");
            foreach (Cell cell in cells)
            {
                string cellCoordAsString = ""; //converting cell location to a string
                cellCoordAsString += (char)(cell.ColumnIndex + 1 + 'A');
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
            xmlSheet.Save(stream); // saves to file
            Debug.WriteLine(xmlSheet);
        }

        public Spreadsheet XMLLoad()
        {
            Spreadsheet sheet = new Spreadsheet(26, 50); //makes a 26 by 50 spreadsheet
            XDocument xmlSheet = XDocument.Load(stream); //loads the xml from the stream


            //XDocument xdoc1 = XDocument.Load("your xml Path");
            //Student objStudent = new Student();
            List<SheetCell> cellList
               = (from cell in xmlSheet.Element("Spreadsheet").Elements("Cell")
                  select new SheetCell
                  {
                      Name = cell.Attribute("Name").Value,
                      Text = cell.Element("Text").Value,
                      Value = cell.Element("Value").Value
                  }).ToList();



            Debug.WriteLine(xmlSheet);
            return sheet; 
        }
    }

    struct SheetCell
    {
        public string Name;
        public string Text;
        public string Value;
    }
}
