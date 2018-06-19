using Microsoft.AspNetCore.Mvc;
using test_net_core_mvc.Helpers;

namespace test_net_core_mvc.Controllers
{
    public class OperationsController : Controller
    {
        public OperationsController() { }

        [HttpGet]
        public float MultiplyAndSubstrac(float number1, float number2, float number3)
        {
            return MathHelper.Substract(MathHelper.Multiply(number1, number2), number3);
        }

        [HttpGet]
        public float DivideAndSum(float number1, float number2, float number3)
        {
            return MathHelper.Sum(MathHelper.Divide(number1, number2), number3);
        }
    }
}
