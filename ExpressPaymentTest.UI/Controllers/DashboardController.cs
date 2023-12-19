using Microsoft.AspNetCore.Mvc;

namespace ExpressPaymentTest.UI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Display()
        {
            return View();
        }

    }
}
