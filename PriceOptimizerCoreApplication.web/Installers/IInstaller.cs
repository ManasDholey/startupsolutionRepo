using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PriceOptimizerCoreApplication.web.Installers
{
   public  interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
