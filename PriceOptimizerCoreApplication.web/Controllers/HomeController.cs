using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PriceOptimizerCoreApplication.web.Models;
using PriceOptimizerCoreApplication.web.Rendering;
using PriceOptimizerCoreApplication.web.Util;

namespace PriceOptimizerCoreApplication.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;
        private readonly Common _common;
        private readonly EmailHelper _emailHelper;
        private string filePath = "wwwroot/ErrorLogs/Error.txt";
        private string ControllerName = "HomeController";
        private string IpLogfilePath = "wwwroot/Applog/RemortIplog.txt";
        public HomeController(ILogger<HomeController> logger , EmailHelper emailHelper, IRazorViewToStringRenderer razorViewToStringRenderer, Common common)
        {
            _logger = logger;
            _emailHelper = emailHelper;
            _common = common;
            _razorViewToStringRenderer = razorViewToStringRenderer;
           // _productService = productService;
        }
        [HttpGet ]
        public async Task<IActionResult> Index()
        {
            try
            {
               
                var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;
                var path = _common.GetFilePath(IpLogfilePath);
                var ip = $"Ip Address is {remoteIpAddress}";
                await Util.Utility.LogRemortIp( path, ip);
                _logger.LogInformation(ip);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View();
            }
            // customerViewModel.CustomerModel = new CustomerModel();
            
        }
        [HttpGet]
        public IActionResult Banner()
        {
            try
            {
                return PartialView("~/Views/Home/_PartialBanner.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            
        }

        [HttpGet]
        public IActionResult About()
        {
            try
            {
                return PartialView("~/Views/Home/_PartialAbout.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            
            
        }
        [HttpGet]
        public IActionResult NewsAndBlog()
        {
            try
            {
                return PartialView("~/Views/Home/_PartialNewsAndBlog.cshtml");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
        [HttpGet]
        public IActionResult Subscribe()
        {
            try
            {
                var model = new Subscribe();
                return PartialView("~/Views/Home/_PartialSubscribe.cshtml", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
           
        }   
        //[HttpGet]
        //public IActionResult PriceAndPlans()
        //{
        //    try
        //    {
        //        return PartialView("~/Views/Home/_PartialPriceAndPlans.cshtml");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message);
        //        throw;
        //    }
            
        //}

        [HttpGet]
        public IActionResult Footer()
        {
            try
            {
                var model = new Email();
                return PartialView("~/Views/Shared/_PartialFooter.cshtml", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoDirectAccess]
        public IActionResult Subscribe([Bind("Email,RequestVerificationToken")] Subscribe email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if (IsAjaxRequest(Request))
                    //{
                    string to = "********************";
                    string subject = "One New user Subcribe Our Services " + email.EmailAddress;
                    string body = "New user email address is " + email.EmailAddress;
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(to);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.From = new MailAddress("*******************");
                    mailMessage.IsBodyHtml = false;
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.Port = 587;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("*******************", "*****************");
                    smtpClient.Send(mailMessage);
                    return Json(new { Status = true, Message = "Subscribe successfully done." });
                    //}

                }
                return Json(new { Status = false, Message = "Please make sure all fields are filled in correctly..!" });
            }
            catch (System.Exception ex)
            {

                return Json(new { Status = false, Message = "There was an error.!" });
            }

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

        [HttpGet]
        [NoDirectAccess]
        public async Task<IActionResult> GetHtml(string Options)
        {
            var model = new ServiceRequest();
            string viewFromAnotherController = await this.RenderViewToStringAsync("/Views/Shared/_PartialSelectedService.cshtml", model);
            return Json(new { Status = true,Html= viewFromAnotherController, Message = $"html crete successfully.!{Options}" }); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoDirectAccess]
        public async Task<IActionResult> GetHtml(string Options, ServiceRequest serviceRequest)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    string viewFromAnotherController = await this.RenderViewToStringAsync("/Views/Shared/EmailTemplateForSelectedService.cshtml", serviceRequest);
                    var  email = new Email();
                    email.Body = viewFromAnotherController;
                    email.Name = serviceRequest.Name;
                    email.Subject = "Service Request";
                    email.IsBodyHtml = true;
                    email.To = serviceRequest.Email;
                    var welcomeEmail = email;
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
                return Json(new { Status = false, Message = "Please make sure all fields are filled in correctly..!", Html = await this.RenderViewToStringAsync("~/Views/Shared/_PartialSelectedService.cshtml", serviceRequest) });
            }
            catch (Exception ex)
            {
                var path = _common.GetFilePath(filePath);
                await Util.Utility.LogErrorMessage(ex, path, ControllerName, "FooterAsync");
                return Json(new { Status = false, Message = "There was an error.!" });
            }
        }

            //public IActionResult Privacy()
            //{
            //    return View();
            //}

            //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            //public IActionResult Error()
            //{
            //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            //}
        }
}
