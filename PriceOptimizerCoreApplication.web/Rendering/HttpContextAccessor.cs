using Microsoft.AspNetCore.Http;
using System.Threading;

namespace PriceOptimizerCoreApplication.web.Rendering
{
    public class HttpContextAccessor : IHttpContextAccessor
    {
        private static AsyncLocal<HttpContext> _httpContextCurrent = new AsyncLocal<HttpContext>();
        HttpContext IHttpContextAccessor.HttpContext { get => _httpContextCurrent.Value; set => _httpContextCurrent.Value = value; }
        //public HttpContext HttpContext { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}
