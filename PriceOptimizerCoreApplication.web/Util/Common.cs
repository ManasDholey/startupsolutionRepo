using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace PriceOptimizerCoreApplication.web.Util
{
    public class Common
    {
        private readonly IWebHostEnvironment _env;
        public Common(IWebHostEnvironment env)
        {
            _env = env;
        }
        public string GetFilePath(string fPath)
        {
            return Path.Combine(_env.ContentRootPath,fPath);
        }
    }
}
