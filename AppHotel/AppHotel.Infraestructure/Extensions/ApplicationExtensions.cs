using Microsoft.AspNetCore.Builder;
using AppHotel.Infraestructure.Middlewares;

namespace AppHotel.Infraestructure.Extensions
{
    public static class ApplicationExtensions
    {
        public static IApplicationBuilder AddMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ExceptionHandler));
            return app;
        }
    }
}
