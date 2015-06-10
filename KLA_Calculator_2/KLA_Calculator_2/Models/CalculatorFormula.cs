using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace KLA_Calculator_2.Models
{
    /// <summary>
    /// A class representing a formula. Allows construction of a formula and eventual
    /// calculation of the formula and return of the result.
    /// </summary>
    public class CalculatorFormula
    {
        /// <summary>
        /// A string representation of the formula for human understanding
        /// </summary>
        String strFormula;

        /// <summary>
        /// The numbers currently included in the formula
        /// </summary>
        List<double> numbers;
        /// <summary>
        /// The operations included in the formula
        /// </summary>
        List<string> operations;

        /// <summary>
        /// The constructor for a formula. Takes in and stores a provided formula generated
        /// in the CalculationIO view.
        /// </summary>
        /// <param name="inputFormula"></param>
        public CalculatorFormula(string inputFormula)
        {
            strFormula = inputFormula;

            numbers = new List<double>();
            operations = new List<string>();
        }

        /// <summary>
        /// Parses the string representation of the formula into its numbers
        /// and operations. Also determines if there are cases in which the order
        /// of operations must be maintained.
        /// </summary>
        /// <returns>True if success. False if an error</returns>
        private bool parseFormula()
        {
            try
            {
                string opPattern = " [+-/*%] ";
                string[] splitNumbers = Regex.Split(strFormula, opPattern);
                string digitPattern = "(-)?[0123456789.]";
                string[] splitOperations = Regex.Split(strFormula, digitPattern);

                // Convert each string to a double
                foreach (string strNum in splitNumbers)
                {
                    double newNum;
                    // If the number is negative, convert number and then set as negative
                    if (strNum.Contains("-"))
                        newNum = Convert.ToDouble(strNum.Substring(1)) * -1;
                    else
                        newNum = Convert.ToDouble(strNum);

                    numbers.Add(newNum);
                }

                // Ensure that no white spaces are added as operations
                foreach (string strOp in splitOperations)
                {
                    if (Regex.IsMatch(strOp, opPattern))
                        operations.Add(strOp);
                }

                return true;
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error during parseFormula: " + exc.Message);
                return false;
            }
        }

        /// <summary>
        /// Performs the calculation of the provided formula.
        /// </summary>
        /// <returns>The result of the stored formula</returns>
        public string performCalculation()
        {
            if (parseFormula())
            {
                double result = numbers[0];

                string orderOfOpsPatt = "[/*%]";

                int opIndex = 0;
                double newNumber;
                string currentOp;
                string nextOp;

                // Iterate through each operation
                while (opIndex < operations.Count)
                {
                    currentOp = operations[opIndex].Trim();
                    // Current number pair is current result value and opIndex in the numbers list
                    newNumber = numbers[opIndex + 1];

                    // Only perform the below steps if currentOp is not the final operation
                    if (opIndex < operations.Count - 1)
                    {
                        nextOp = operations[opIndex + 1].Trim();

                        // Must check one operation ahead to determine if there will be an order of operations issue
                        //     While loop in case of consecutive */% operators 
                        while (Regex.IsMatch(nextOp, orderOfOpsPatt))
                        {
                            opIndex++;
                            newNumber = performOperation(newNumber, numbers[opIndex + 1], nextOp);

                            // Only proceed to check the next operation if the previous operation was not the final
                            if (opIndex < operations.Count - 1)
                                nextOp = operations[opIndex + 1].Trim();
                            else
                                break;
                        }
                    }

                    // After checking next operation(s) for */%, perform current operation on current result
                    //   and the result of the following operations (if applicable)
                    result = performOperation(result, newNumber, currentOp);

                    opIndex++;
                }

                return result.ToString();
            }
            else
                return "Invalid formula.";
        }

        /// <summary>
        /// Performs the requested operation on the provided numbers
        /// </summary>
        /// <param name="num1">First number</param>
        /// <param name="num2">Second number</param>
        /// <param name="op">Operation to perform</param>
        /// <returns>Result of operation on provided numbers</returns>
        private double performOperation(double num1, double num2, string op)
        {
            double result = 0;

            if (op == "+")
                result = num1 + num2;
            else if (op == "-")
                result = num1 - num2;
            else if (op == "*")
                result = num1 * num2;
            else if (op == "/")
                result = num1 / num2;
            else if (op == "%")
                result = num1 % num2;

            return result;
        }
    }
}