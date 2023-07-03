using Microsoft.AspNetCore.Mvc;
using PriceOptimizerCoreApplication.web.Models;
using PriceOptimizerCoreApplication.web.Rendering;
using PriceOptimizerCoreApplication.web.Util;
using System.Threading.Tasks;

namespace PriceOptimizerCoreApplication.web.Controllers
{
    public class AboutController : Controller
    {
        private readonly EmailHelper _emailHelper;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;
        private readonly Common _common;
        private string filePath = "wwwroot/ErrorLogs/Error.txt";
        private string ControllerName = "ServicesController";
        public AboutController(EmailHelper emailHelper, IRazorViewToStringRenderer razorViewToStringRenderer, Common common)
        {
            _emailHelper = emailHelper;
            _razorViewToStringRenderer = razorViewToStringRenderer;
            _common = common;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Accounting()
        {
            return PartialView("~/Views/Shared/_PartialAccounting.cshtml");
        }
        [HttpGet]
        public IActionResult Footer()
        {
            var model = new Email();
            return PartialView("~/Views/Shared/_PartialFooter.cshtml", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoDirectAccess]
        public async Task<IActionResult> FooterAsync([Bind("To,Subject,Body,Name,RequestVerificationToken")] Email email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var welcomeEmail = email;
                    string viewFromAnotherController = await this.RenderViewToStringAsync("/Views/Shared/EmailTemplateForQuery.cshtml", email);
                    // string viewFromCurrentController = await this.RenderViewToStringAsync("EmailTemplateForQuery", email);
                    // var  body =await  _razorViewToStringRenderer.RenderViewToStringAsync("EmailTemplateForQuery", email);
                    email.Body = viewFromAnotherController;
                    email.IsBodyHtml = true;
                    var result = await _emailHelper.SendEmailAsync(email, ControllerName);
                    if (result == true)
                    {
                        viewFromAnotherController = await this.RenderViewToStringAsync("/Views/Shared/EmailTemplateForWelcome.cshtml", email);
                        welcomeEmail.Body = viewFromAnotherController;
                        welcomeEmail.IsBodyHtml = true;
                        var resultOne = await _emailHelper.SendEmailWelcomeAsync(welcomeEmail, ControllerName);
                        return Json(new { Status = true, Message = "Email successfully send." });
                    }
                    else
                    {
                        return Json(new { Status = false, Message = "There was an error.!" });
                    }

                }
                return Json(new { Status = false, Message = "Please make sure all fields are filled in correctly..!", Html = await this.RenderViewToStringAsync("~/Views/Shared/_PartialFooter.cshtml", email) });
            }
            catch (System.Exception ex)
            {

                var path = _common.GetFilePath(filePath);
                await Util.Utility.LogErrorMessage(ex, path, ControllerName, "FooterAsync");
                return Json(new { Status = false, Message = "There was an error.!" });
            }

        }

    }
}
