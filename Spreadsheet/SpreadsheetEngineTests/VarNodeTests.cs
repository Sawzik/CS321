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
    public class VarNodeTests
    {
        [TestMethod()]
        public void VarNodeVariableTest()
        {
            // Arrange
            string testVariable = "hhhhhhhhhhh1";
            double testValue = 1384.34;

            // Act
            VarNode testVarNode = new VarNode(testVariable, testValue);

            // Assert
            bool varNameEqual = testVarNode.Variable == testVariable;
            bool varValueEqual = testVarNode.Value == testValue;

            Assert.AreEqual(varNameEqual, varValueEqual);
        }

        [TestMethod()]
        public void VarNodeEmptyConstructorTest()
        {
            // Arrange and Act
            VarNode testVarNode = new VarNode();
            
            // Assert
            bool varNameEqual = testVarNode.Variable == "NoData";
            bool varValueEqual = testVarNode.Value == 0;

            Assert.AreEqual(varNameEqual, varValueEqual);
        }

        [TestMethod()]
        public void EvalTest()
        {
            // Arrange
            string testVariable = "hhhhhhhhhhh1";
            double testValue = 1384.34;

            // Act
            VarNode testVarNode = new VarNode(testVariable, testValue);

            // Assert
            Assert.AreEqual(testValue, testVarNode.Eval());
        }
    }
}