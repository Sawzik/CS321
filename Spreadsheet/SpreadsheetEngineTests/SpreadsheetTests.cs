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
        public void CalculateValueTest()
        {
            Spreadsheet sheet = new Spreadsheet(5, 5);
            sheet.GetCell("A1").Text = "1";
            sheet.GetCell("A2").Text = "=A1";
            Assert.AreEqual("1", sheet.GetCell("A2").Value);
        }
    }
}