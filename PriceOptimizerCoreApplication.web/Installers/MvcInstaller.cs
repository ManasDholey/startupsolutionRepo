using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceOptimizerCoreApplication.web.Authorization;
using PriceOptimizerCoreApplication.web.Rendering;
using PriceOptimizerCoreApplication.web.Util;

namespace PriceOptimizerCoreApplication.web.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(config => {
                config.DefaultScheme = "AccountsCookie";
                config.DefaultChallengeScheme = "oidc";
            })
               .AddCookie("AccountsCookie")
               .AddOpenIdConnect("oidc", config => {
                   config.Authority = "https://localhost:44334/";
                   config.ClientId = "client_id__mvc_PriceOptimizerCoreApplication.web";
                   config.ClientSecret = "client_secret_mvc_PriceOptimizerCoreApplication.web";
                   config.SaveTokens = true;
                   config.ResponseType = "code";
                   config.SignedOutCallbackPath = "/Home/Index";

                   config.ClaimActions.MapUniqueJsonKey("PriceOptimizerCoreApplication.web", "rc.chottu");

                   // two trips to load claims in to the cookie
                   // but the id token is smaller !
                   config.GetClaimsFromUserInfoEndpoint = true;

                   // configure scope
                   config.Scope.Clear();
                   config.Scope.Add("openid");
                   config.Scope.Add("rc.chottu");
                   config.Scope.Add("AccountsServer.Api");
               }); 
            services.AddSingleton<IAuthorizationHandler, WorksForCompanyHandler>();
            services.AddSingleton<IRazorViewToStringRenderer, RazorViewToStringRenderer>();
            services.AddTransient<EmailHelper>();
            services.AddTransient<Credential>();
            services.AddSingleton<Common>();
            services.AddHttpClient();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddControllers().AddNewtonsoftJson();
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                //options.Filters.Add<ValidationFilter>();
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
                .AddFluentValidation(mvcConfiguration => mvcConfiguration.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddCors();
            
        }
    }
}
