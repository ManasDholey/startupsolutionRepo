using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PriceOptimizerCoreApplication.web.Models;

namespace PriceOptimizerCoreApplication.web.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Footer()
        {
            var model = new Email();
            return PartialView("~/Views/Shared/_PartialFooter.cshtml", model);
        }

    }
}
