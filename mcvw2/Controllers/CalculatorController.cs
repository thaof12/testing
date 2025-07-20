using Microsoft.AspNetCore.Mvc;

namespace mcvw2.Controllers
{
    public class CalculatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Calculate(double a, double b, string op)
        {
            double result = 0;
            switch(op)
            {
                case "+": result = a + b; break;
                case "-": result = a - b; break;
                case "*": result = a * b; break;
                case "/":
                    if (b != 0)
                        result = a / b;
                    else
                        return BadRequest("Division by zero is not allowed.");
                    break;
                default:
                    return BadRequest("Invalid operation.");
                case "%":
                    if (b != 0)
                        result = a % b;
                    else
                        return BadRequest("Division by zero is not allowed.");
                    break;
                case "^": result = Math.Pow(a, b); break;
            }
            ViewBag.Result = result;
            ViewBag.Op = op;
            ViewBag.A = a; ViewBag.B = b;
            return View("Index");
        }
    }
}
