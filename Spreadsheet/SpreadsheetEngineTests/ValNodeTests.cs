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
    public class ValNodeTests
    {
        [TestMethod()]
        public void ValNodeTest()
        {
            // Arrange
            double testValue = 1587.34;

            // Act
            ValNode testVarNode = new ValNode(testValue);

            // Assert
            Assert.AreEqual(testVarNode.Value, testValue);
        }

        [TestMethod()]
        public void EvalTest()
        {
            // Arrange
            double testValue = 1587.34;

            // Act
            ValNode testVarNode = new ValNode(testValue);

            // Assert
            Assert.AreEqual(testVarNode.Eval(), testValue);
        }
    }
}