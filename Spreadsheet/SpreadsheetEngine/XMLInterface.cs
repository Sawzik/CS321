﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace CptS321
{
    public class XMLInterface : IDisposable
    {
        private Stream stream;

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
            XElement root = new XElement("spreadsheet");
            foreach (Cell cell in cells)
            {
                string cellCoordAsString = ""; //converting cell location to a string
                cellCoordAsString += (char)(cell.ColumnIndex + 'A');
                cellCoordAsString += (cell.RowIndex + 1).ToString();

                // saves the data into XML
                XElement xmlCell = new XElement("cell", 
                        new XAttribute("name", cellCoordAsString),
                        new XElement("text", cell.Text)
                );
                //Debug.WriteLine(xmlCell);
                root.Add(xmlCell);
            }
            xmlSheet.Add(root); // puts all the elements from root into XMLsheet
            xmlSheet.Save(stream); // saves to file
        }

        public Spreadsheet XMLLoad()
        {
            Spreadsheet sheet = new Spreadsheet(26, 50); //makes a 26 by 50 spreadsheet
            XDocument xmlSheet = XDocument.Load(stream); //loads the xml from the stream

            // reads data from XML into a list of SheetCell Structs
            List<SheetCell> cellList
               = (from cell in xmlSheet.Element("spreadsheet")?.Elements("cell")
                  select new SheetCell
                  {
                      Name = cell.Attribute("name").Value,
                      Text = cell.Element("text").Value
                  }).ToList();

            foreach (SheetCell cell in cellList)            
                sheet.GetCell(cell.Name).Text = cell.Text; // updates the cell in the sheet with the one from XML
            
            return sheet; 
        }

        public void Dispose()
        {
            stream.Close();
        }
    }

    // basic structure to help parse XML
    struct SheetCell
    {
        public string Name;
        public string Text;
    }
}
