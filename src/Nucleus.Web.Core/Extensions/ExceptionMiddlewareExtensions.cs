using Microsoft.AspNetCore.Builder;
using Nucleus.Web.Core.CustomMiddleware;

namespace Nucleus.Web.Core.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}