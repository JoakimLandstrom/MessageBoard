using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;


namespace App.Utilities
{
    public static class ExceptionHandler
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(builder => builder.Run(async ctx =>
            {
                ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
                ctx.Response.ContentType = "application/json";

                var feature = ctx.Features.Get<IExceptionHandlerFeature>();
                if (feature != null)
                    await ctx.Response.WriteAsync(JsonSerializer.Serialize(feature.Error.Message));
            }));
        }
    }
}