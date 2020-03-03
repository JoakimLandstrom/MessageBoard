using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace App
{
    internal static class Server
    {
        private static void Main()
        {
            CreateHostBuilder().Build().Run();
        }

        private static IHostBuilder CreateHostBuilder()
            => Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                    builder.UseKestrel().UseStartup<Startup>());
    }
}