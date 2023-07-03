using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace PriceOptimizerCoreApplication.web.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        private readonly TelemetryClient _telemetryClient;

        public ErrorController(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }
        [AllowAnonymous]
        
        public IActionResult Error()
        {
            // Retrieve the exception Details
            var exceptionHandlerPathFeature =
                    HttpContext.Features.Get<IExceptionHandlerPathFeature>();
           // var exception = exceptionHandlerPathFeature?.Error; // Your exception
           // var code = 500; // Internal Server Error by default
           //
           // if (exception is MyNotFoundException) code = 404; // Not Found
           // else if (exception is MyUnauthorizedException) code = 401; // Unauthorized
           // else if (exception is MyException) code = 400; // Bad Request
           //
           // Response.StatusCode = code;

            ViewBag.ExceptionPath = exceptionHandlerPathFeature.Path;
            ViewBag.ExceptionMessage = exceptionHandlerPathFeature.Error.Message;
            ViewBag.StackTrace = exceptionHandlerPathFeature.Error.StackTrace;

            return View("Error");
        }

        [Route("/404")]
        public IActionResult PageNotFound()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            _telemetryClient.TrackEvent("Error.PageNotFound", new Dictionary<string, string>
            {
                ["originalPath"] = originalPath
            });
            return View();
        }
        [Route("/500")]
        public IActionResult AppError()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _telemetryClient.TrackException(exceptionHandlerPathFeature.Error);
            _telemetryClient.TrackEvent("Error.ServerError", new Dictionary<string, string>
            {
                ["originalPath"] = exceptionHandlerPathFeature.Path,
                ["error"] = exceptionHandlerPathFeature.Error.Message
            });
            return View();
        }
    }
}
