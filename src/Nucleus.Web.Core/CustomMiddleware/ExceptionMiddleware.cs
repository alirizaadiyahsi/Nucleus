using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Nucleus.Utilities.Exceptions;
using Serilog;

namespace Nucleus.Web.Core.CustomMiddleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            if (ex.StackTrace != null)
            {
                Log.Error(ex.StackTrace);

                foreach (var exception in ex.GetInnerExceptions())
                {
                    Log.Error(exception.Message);
                    if (exception.StackTrace != null) Log.Error(exception.StackTrace);
                }
            }

            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonConvert.SerializeObject(new
        {
            ResultCode = 0,
            ErrorMessage = exception.Message
        });

        return context.Response.WriteAsync(result);
    }
}