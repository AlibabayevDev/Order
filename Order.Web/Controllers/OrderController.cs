using Microsoft.AspNetCore.Mvc;

namespace Order.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
