using KLA_Calculator_MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KLA_Calculator_MVC5.Controllers
{
    public class CalculatorController : Controller
    {
        // GET: /Calculator/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Calculator/CalculationIO
        public ActionResult CalculationIO()
        {
            return View();
        }

        [HttpPost]
        public string ProcessInputFormula(string inputFormula)
        {
            CalculatorFormula formula = new CalculatorFormula(Convert.ToString(inputFormula));

            return formula.performCalculation();
        }
    }
}