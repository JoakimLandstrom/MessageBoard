using App.Auth;
using App.DataAccess;
using App.Services;
using App.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc();

            services
                .AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, AuthHandler>("BasicAuthentication", null);
            
            services
                .AddScoped<IMessageService, MessageService>()
                .AddScoped<IUserService, UserService>()
                .AddSingleton<IModelCache, ModelCache>();

            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddSwaggerDocument();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseGlobalExceptionHandler()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .UseOpenApi()
                .UseSwaggerUi3();
        }
    }
}