using Microsoft.AspNetCore.Mvc;
using PriceOptimizerCoreApplication.web.Models;

namespace PriceOptimizerCoreApplication.web.Controllers
{
    public class EmailTemplateController : Controller
    {
        [HttpGet]
        public IActionResult EmailTemplateQuery()
        {
            var model = new Email();
            return View("~/Views/Shared/EmailTemplateForQuery.cshtml",model);
        }
        [HttpGet]
        public IActionResult EmailTemplateWelcome()
        {
            return View("~/Views/Shared/EmailTemplateForWelcome.cshtml");
        }
    }
}
