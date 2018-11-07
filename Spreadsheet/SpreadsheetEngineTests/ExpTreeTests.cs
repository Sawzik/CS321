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
    public class ExpTreeTests
    {
        [TestMethod()]
        public void SetVarChangeTest()
        {
            string variable = "A34";
            double assignedValue = 1326857.19;

            ExpTree testTree = new ExpTree(variable);

            testTree.SetVar(variable, assignedValue);
            Assert.AreEqual(testTree.Eval(), assignedValue);
        }

        [TestMethod()]
        public void SetVarNoChangeTest()
        {
            string variable = "A34";
            double assignedValue = 1326857.19;

            ExpTree testTree = new ExpTree(variable);

            testTree.SetVar(variable + '1', assignedValue);
            Assert.AreNotEqual(testTree.Eval(), assignedValue);
        }

        [TestMethod()]
        public void EvalPlusTest()
        {
            double Value1 = 1326857.19;
            double Value2 = 38497.509;

            ExpTree testTree = new ExpTree(Value1.ToString() + '+' + Value2.ToString());

            Assert.AreEqual(testTree.Eval(), Value1 + Value2);
        }

        [TestMethod()]
        public void EvalMinusTest()
        {
            double Value1 = 1326857.19;
            double Value2 = 38497.509;

            ExpTree testTree = new ExpTree(Value1.ToString() + '-' + Value2.ToString());

            Assert.AreEqual(testTree.Eval(), Value1 - Value2);
        }

        [TestMethod()]
        public void EvalDivideTest()
        {
            double Value1 = 1326857.19;
            double Value2 = 38497.509;

            ExpTree testTree = new ExpTree(Value1.ToString() + '/' + Value2.ToString());

            Assert.AreEqual(testTree.Eval(), Value1 / Value2);
        }

        [TestMethod()]
        public void EvalMultiplyTest()
        {
            double Value1 = 1326857.19;
            double Value2 = 38497.509;

            ExpTree testTree = new ExpTree(Value1.ToString() + '*' + Value2.ToString());

            Assert.AreEqual(testTree.Eval(), Value1 * Value2);
        }

        [TestMethod()]
        public void EvalMultipleOpNodesTest()
        {
            double[] Values = { 1326857.19, 38497.509, 89314871.9874, 879.4, 484, 3, 4579.7777, 7453, 8794 };

            ExpTree testTree = new ExpTree(Values[0].ToString() + '-' + Values[1].ToString() + '+' + Values[2].ToString() + '+'
                                         + Values[3].ToString() + '-' + Values[4].ToString() + '/' + Values[5].ToString() + '+'
                                         + Values[6].ToString() + '+' + Values[7].ToString() + '*' + Values[8].ToString());

            double expectedOutput = Values[0] - Values[1] + Values[2] +
                                    Values[3] - Values[4] / Values[5] +
                                    Values[6] + Values[7] * Values[8];

            Assert.AreEqual(testTree.Eval(), expectedOutput);
        }

        [TestMethod()]
        public void EvalMultipleOpNodes_WithParenthesisTest()
        {
            double[] Values = { 1326857.19, 38497.509, 89314871.9874, 879.4, 484, 3, 4579.7777, 7453, 8794 };

            ExpTree testTree = new ExpTree(Values[0].ToString() + "-(" + Values[1].ToString() + '+' + Values[2].ToString() + '+'
                                         + Values[3].ToString() + '-' + Values[4].ToString() + ")/" + Values[5].ToString() + "+("
                                         + Values[6].ToString() + '+' + Values[7].ToString() + ")*" + Values[8].ToString());

            double expectedOutput = Values[0] - (Values[1] + Values[2] +
                                    Values[3] - Values[4]) / Values[5] + (
                                    Values[6] + Values[7]) * Values[8];

            Assert.AreEqual(testTree.Eval(), expectedOutput);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void EvalMultipleOpNodes_WithMissingClosedParenthesisTest()
        {
            double[] Values = { 1326857.19, 38497.509, 89314871.9874, 879.4, 484, 3, 4579.7777, 7453, 8794 };

            ExpTree testTree = new ExpTree(Values[0].ToString() + "-(" + Values[1].ToString() + '+' + Values[2].ToString() + '+'
                                         + Values[3].ToString() + '-' + Values[4].ToString() + ")/" + Values[5].ToString() + "+("
                                         + Values[6].ToString() + '+' + Values[7].ToString() + "*" + Values[8].ToString());

        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void EvalMultipleOpNodes_WithMissingOpenParenthesisTest()
        {
            double[] Values = { 1326857.19, 38497.509, 89314871.9874, 879.4, 484, 3, 4579.7777, 7453, 8794 };

            ExpTree testTree = new ExpTree(Values[0].ToString() + "-(" + Values[1].ToString() + '+' + Values[2].ToString() + '+'
                                         + Values[3].ToString() + '-' + Values[4].ToString() + "/" + Values[5].ToString() + "+("
                                         + Values[6].ToString() + '+' + Values[7].ToString() + ")*" + Values[8].ToString());

        }

        [TestMethod()]
        public void SetVarNodeRootTest()
        {
            // Arrange
            string testVariable = "hhhhhhhhhhh1";
            double testValue = 1384.34;

            ExpTree testTree = new ExpTree(testVariable);
            testTree.SetVar(testVariable, testValue);

            // Assert
            Assert.AreEqual(testValue, testTree.Eval());
        }

        [TestMethod()]
        public void SetVarNodeNonRootTest()
        {
            // Arrange
            string testVariable = "hhhhhhhhhhh1";
            double testValue = 1384.34;

            ExpTree testTree = new ExpTree(testVariable + "+3454-2348*34/0.5");
            testTree.SetVar(testVariable, testValue);

            // Assert
            Assert.AreEqual(-154825.66, testTree.Eval());
        }
    }
}