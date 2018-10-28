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
            double Value1 = 1326857.19;
            double Value2 = 38497.509;
            double Value3 = 89314871.9874;

            ExpTree testTree = new ExpTree(Value1.ToString() + '+' + Value2.ToString() + '+' + Value3.ToString());

            Assert.AreEqual(testTree.Eval(), Value1 + Value2 + Value3);
        }
    }
}