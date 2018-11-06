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
        [TestMethod()]
        public void SpreadsheetTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCellTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCellTest1()
        {
            Assert.Fail();
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
            sheet.GetCell("A2").Text = "=A3+A2";
            sheet.GetCell("A3").Text = "34";
            Assert.AreEqual("35", sheet.GetCell("A2").Value);
        }

        [TestMethod()]
        public void CalculateValueTest3()
        {
            Spreadsheet sheet = new Spreadsheet(1, 3);
            sheet.GetCell("A1").Text = "1";
            sheet.GetCell("A2").Text = "35";
            sheet.GetCell("A3").Text = "=A1+A2";
            Assert.AreEqual("35", sheet.GetCell("A2").Value);
        }
    }
}