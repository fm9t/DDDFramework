/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-10 23:27
** desc: 未捕获异常的处理
** Ver : V1.0.0
********************************************************************/

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;

namespace ZsjTest.ReverseProxy.Middleware;

public static class UnCatchException
{    public static IApplicationBuilder UseException(this IApplicationBuilder app)
    {
        return app.UseExceptionHandler(configure =>
        {
            configure.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var ex = exceptionHandlerPathFeature?.Error;
                if (ex != null)
                {
                    var _logger = context.RequestServices.GetService<ILogger<IExceptionHandlerPathFeature>>();
                    _logger?.LogError("{Url}, {method}, {errormessage}, {errorstacktrace}", context.Request.GetEncodedPathAndQuery(),
                        nameof(UnCatchException), ex?.Message, ex?.StackTrace);
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json;charset=utf-8";
                    string errorMessage = ex?.Message ?? "Unknown error";
                    if (ex?.InnerException != null)
                    {
                        errorMessage = ex.InnerException.Message;
                    }
                    await context.Response.WriteAsJsonAsync(errorMessage);
                }
            });
        });
    }
}
