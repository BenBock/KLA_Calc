using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KLA_Calculator_2.Models;

namespace KLA_Calculator_2.Tests
{
    [TestClass]
    public class SimpleCalcTests
    {
        [TestMethod]
        public void additionTest()
        {
            CalculatorFormula formula = new CalculatorFormula("1 + 2");
            string result = formula.performCalculation();

            Assert.AreEqual("3", result);
        }

        [TestMethod]
        public void subtractionTest()
        {
            CalculatorFormula formula = new CalculatorFormula("1 - 2");
            string result = formula.performCalculation();

            Assert.AreEqual("-1", result);
        }

        [TestMethod]
        public void multiplicationTest()
        {
            CalculatorFormula formula = new CalculatorFormula("1 * 2");
            string result = formula.performCalculation();

            Assert.AreEqual("2", result);
        }

        [TestMethod]
        public void divisionTest()
        {
            CalculatorFormula formula = new CalculatorFormula("1 / 2");
            string result = formula.performCalculation();

            Assert.AreEqual("0.5", result);
        }

        [TestMethod]
        public void moduloTest()
        {
            CalculatorFormula formula = new CalculatorFormula("1 % 2");
            string result = formula.performCalculation();

            Assert.AreEqual("1", result);
        }

        [TestMethod]
        public void negativeTest()
        {
            CalculatorFormula formula = new CalculatorFormula("1 + -2");
            string result = formula.performCalculation();

            Assert.AreEqual("-1", result);
        }

        [TestMethod]
        public void orderOfOps_AddMult_Test()
        {
            CalculatorFormula formula = new CalculatorFormula("1 + 2 * 3");
            string result = formula.performCalculation();

            Assert.AreEqual("7", result);
        }

        [TestMethod]
        public void orderOfOps_AddMultAdd_Test()
        {
            CalculatorFormula formula = new CalculatorFormula("1 + 2 * 3 + 4");
            string result = formula.performCalculation();

            Assert.AreEqual("11", result);
        }

        [TestMethod]
        public void orderOfOps_AddMultMult_Test()
        {
            CalculatorFormula formula = new CalculatorFormula("1 + 2 * 3 * 4");
            string result = formula.performCalculation();

            Assert.AreEqual("25", result);
        }

        [TestMethod]
        public void orderOfOps_AddMultMultAdd_Test()
        {
            CalculatorFormula formula = new CalculatorFormula("1 + 2 * 3 * 4 + 5");
            string result = formula.performCalculation();

            Assert.AreEqual("30", result);
        }
    }
}
