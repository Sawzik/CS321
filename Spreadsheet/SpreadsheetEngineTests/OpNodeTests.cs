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
    public class OpNodeTests
    {

        [TestMethod()]
        public void EvalMinusTest()
        {
            // Arrange
            double testLeftValue = 11.3214;
            double testRightValue = 3485;
            ExpNode testLeftNode = new ValNode(testLeftValue);
            ExpNode testRightNode = new ValNode(testRightValue);

            OpNode testOpNode = new OpNode(ref testLeftNode, ref testRightNode, '-');

            // Act and Assert
            Assert.AreEqual((testLeftValue - testRightValue), testOpNode.Eval());
        }

        [TestMethod()]
        public void EvalPlusTest()
        {
            // Arrange
            double testLeftValue = 11.3214;
            double testRightValue = 3485;
            ExpNode testLeftNode = new ValNode(testLeftValue);
            ExpNode testRightNode = new ValNode(testRightValue);

            OpNode testOpNode = new OpNode(ref testLeftNode, ref testRightNode, '+');

            // Act and Assert
            Assert.AreEqual((testLeftValue + testRightValue), testOpNode.Eval());
        }

        [TestMethod()]
        public void EvalDivideTest()
        {
            // Arrange
            double testLeftValue = 11.3214;
            double testRightValue = 3485;
            ExpNode testLeftNode = new ValNode(testLeftValue);
            ExpNode testRightNode = new ValNode(testRightValue);

            OpNode testOpNode = new OpNode(ref testLeftNode, ref testRightNode, '/');

            // Act and Assert
            Assert.AreEqual((testLeftValue / testRightValue), testOpNode.Eval());     
        }

        [TestMethod()]
        public void EvalMultiplyTest()
        {
            // Arrange
            double testLeftValue = 11.3214;
            double testRightValue = 3485;
            ExpNode testLeftNode = new ValNode(testLeftValue);
            ExpNode testRightNode = new ValNode(testRightValue);

            OpNode testOpNode = new OpNode(ref testLeftNode, ref testRightNode, '*');

            // Act and Assert
            Assert.AreEqual((testLeftValue * testRightValue), testOpNode.Eval());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenEvalIsCalledOnAnUnitializedOpNode_ShouldThrowInvalidOperationException()
        {
            // Arrange
            OpNode testOpNode = new OpNode();

            // Act
            testOpNode.Eval();

            // Assert is handled by the ExpectedException attribute on the test method.
        }
    }
}