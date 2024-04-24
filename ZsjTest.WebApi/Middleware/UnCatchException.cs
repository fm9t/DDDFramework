/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 13:59
** desc: 未捕获异常的处理，只向用户显示异常的message, 不显示详细内容
         如果异常message也很敏感，可以按异常类型过滤，添加自定义异常，
         只允许自定义异常显示message
** Ver : V1.0.0
********************************************************************/

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Localization;
using ZsjTest.Infrastructure;

namespace ZsjTest.WebApi.Middleware;

public static class UnCatchException
{
    public static IApplicationBuilder UseException(this IApplicationBuilder app)
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
                    var localizer = context.RequestServices.GetService<IStringLocalizer>();
                    _logger?.LogError("{Url}, {method}, {errormessage}, {errorstacktrace}", context.Request.GetEncodedPathAndQuery(),
                        nameof(UnCatchException), ex?.Message, ex?.StackTrace);
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json;charset=utf-8";
                    string errorMessage = ex?.Message ?? LocalizerStr.UnknownError;                    
                    if (ex?.InnerException != null)
                    {
                        errorMessage = ex.InnerException.Message;
                    }
                    if (localizer != null)
                    {
                        errorMessage = localizer[errorMessage];
                    }
                    await context.Response.WriteAsJsonAsync(errorMessage);
                }
            });
        });
    }
}
