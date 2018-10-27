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
            double testLeftValue = 11.3214;
            double testRightValue = 3485;
            ExpNode testLeftNode = new ValNode(testLeftValue);
            ExpNode testRightNode = new ValNode(testRightValue);

            OpNode testOpNode = new OpNode(ref testLeftNode, ref testRightNode, '-');

            Assert.AreEqual((testLeftValue - testRightValue), testOpNode.Eval());
        }

        [TestMethod()]
        public void EvalPlusTest()
        {
            double testLeftValue = 11.3214;
            double testRightValue = 3485;
            ExpNode testLeftNode = new ValNode(testLeftValue);
            ExpNode testRightNode = new ValNode(testRightValue);

            OpNode testOpNode = new OpNode(ref testLeftNode, ref testRightNode, '+');

            Assert.AreEqual((testLeftValue + testRightValue), testOpNode.Eval());
        }

        [TestMethod()]
        public void EvalDivideTest()
        {
            double testLeftValue = 11.3214;
            double testRightValue = 3485;
            ExpNode testLeftNode = new ValNode(testLeftValue);
            ExpNode testRightNode = new ValNode(testRightValue);

            OpNode testOpNode = new OpNode(ref testLeftNode, ref testRightNode, '/');

            Assert.AreEqual((testLeftValue / testRightValue), testOpNode.Eval());     
        }

        [TestMethod()]
        public void EvalMultiplyTest()
        {
            double testLeftValue = 11.3214;
            double testRightValue = 3485;
            ExpNode testLeftNode = new ValNode(testLeftValue);
            ExpNode testRightNode = new ValNode(testRightValue);

            OpNode testOpNode = new OpNode(ref testLeftNode, ref testRightNode, '*');

            Assert.AreEqual((testLeftValue * testRightValue), testOpNode.Eval());
        }

    }
}