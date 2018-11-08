using Microsoft.VisualStudio.TestTools.UnitTesting;
using CptS321;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CptS321.Tests
{
    [TestClass()]
    public class SpreadsheetTests
    {
        //[TestMethod()]
        //public void SpreadsheetTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void GetCellCoordsTest()
        {
            Spreadsheet sheet = new Spreadsheet(1, 1);
            sheet.GetCell(0,0).Text = "1";            
            Assert.AreEqual("1", sheet.GetCell(0,0).Value);
        }

        [TestMethod()]
        public void GetCellLetterTest()
        {
            Spreadsheet sheet = new Spreadsheet(1, 1);
            sheet.GetCell("A1").Text = "1";
            Assert.AreEqual("1", sheet.GetCell("A1").Value);
        }

        [TestMethod()]
        public void CalculateValueTest1()
        {
            Spreadsheet sheet = new Spreadsheet(1, 2);
            sheet.GetCell("A1").Text = "1";
            sheet.GetCell("A2").Text = "=A1";
            Assert.AreEqual("1", sheet.GetCell("A2").Value);
        }

        [TestMethod()]
        public void CalculateValueTest2()
        {
            Spreadsheet sheet = new Spreadsheet(1, 3);
            sheet.GetCell("A1").Text = "1";
            sheet.GetCell("A2").Text = "34";
            sheet.GetCell("A3").Text = "=A1+A2";
            sheet.GetCell("A2").Text = "35";
            Assert.AreEqual("36", sheet.GetCell("A3").Value);
        }

        [TestMethod()]
        public void CalculateValueTest3()
        {
            Spreadsheet sheet = new Spreadsheet(1, 3);
            sheet.GetCell("A1").Text = "1";
            sheet.GetCell("A2").Text = "35";
            sheet.GetCell("A3").Text = "=A1+A2";
            Assert.AreEqual("36", sheet.GetCell("A3").Value);
        }

        [TestMethod()]
        public void CalculateValueTest4()
        {
            Spreadsheet sheet = new Spreadsheet(1, 3);
            sheet.GetCell("A1").Text = "=A2+A3";
            sheet.GetCell("A2").Text = "35";
            sheet.GetCell("A3").Text = "1";
            Assert.AreEqual("36", sheet.GetCell("A1").Value);
        }

        [TestMethod()]
        public void CalculateValueTest5()
        {
            Spreadsheet sheet = new Spreadsheet(1, 4);
            sheet.GetCell("A1").Text = "=A2+A3";
            sheet.GetCell("A2").Text = "35";
            sheet.GetCell("A3").Text = "1";
            sheet.GetCell("A4").Text = "=A3-A1";
            Assert.AreEqual("-35", sheet.GetCell("A4").Value);
        }

        [TestMethod()]
        public void CalculateValueTest6()
        {
            Spreadsheet sheet = new Spreadsheet(1, 4);
            sheet.GetCell("A1").Text = "=A2+A3";
            sheet.GetCell("A2").Text = "35";
            sheet.GetCell("A3").Text = "1";
            sheet.GetCell("A4").Text = "=A3-A1";
            sheet.GetCell("A2").Text = "34";
            Assert.AreEqual("-34", sheet.GetCell("A4").Value);
        }

        [TestMethod()]
        public void CalculateValueTest7()
        {
            Spreadsheet sheet = new Spreadsheet(1, 4);
            sheet.GetCell("A1").Text = "=A2+A3";
            sheet.GetCell("A2").Text = "35";
            sheet.GetCell("A3").Text = "1";
            sheet.GetCell("A4").Text = "=A3-A1";
            sheet.GetCell("A2").Text = "34";
            sheet.GetCell("A4").Text = "=A1+A2+A3";
            Assert.AreEqual("70", sheet.GetCell("A4").Value);
        }

        [TestMethod()]
        public void CalculateValueTest8()
        {
            Spreadsheet sheet = new Spreadsheet(1, 5);
            sheet.GetCell("A1").Text = "1";
            sheet.GetCell("A2").Text = "2";
            sheet.GetCell("A3").Text = "3";
            sheet.GetCell("A4").Text = "4";
            sheet.GetCell("A5").Text = "=A1+A2+A3";
            sheet.GetCell("A5").Text = "=A1";
            Assert.AreEqual("1", sheet.GetCell("A5").Value);
        }

        [TestMethod()]
        public void ReferenceToString()
        {
            Spreadsheet sheet = new Spreadsheet(1, 2);
            sheet.GetCell("A1").Text = "=A2";
            sheet.GetCell("A2").Text = "2";
            Assert.AreEqual("2", sheet.GetCell("A1").Value);
        }

        [TestMethod()]
        public void ReferenceToString_WithCalculatedValue()
        {
            Spreadsheet sheet = new Spreadsheet(1, 2);
            sheet.GetCell("A1").Text = "=A2";
            sheet.GetCell("A2").Text = "=2+47";
            Assert.AreEqual("49", sheet.GetCell("A1").Value);
        }


    }
}