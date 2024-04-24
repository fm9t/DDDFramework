/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-12 12:28
** desc: 记录controller参数信息
** Ver : V1.0.0
************************************************************************/

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using ZsjTest.WebApi.Controllers;

namespace ZsjTest.WebApi.Filter;

public class ParamLoggerAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var o = context.ActionArguments;
        ZsjBaseController? controller =
            context.Controller as ZsjBaseController;
        controller?.Logger.LogDebug(
            "Logger source: {url} {httpmethod} from: {userIp} controller: {logSource}, method: {method}, args: {@args}",
            context.HttpContext.Request.GetDisplayUrl(), context.HttpContext.Request.Method,
            context.HttpContext.Connection.RemoteIpAddress,
            context.Controller.GetType().Name, context.ActionDescriptor.DisplayName, o);
     
        await base.OnActionExecutionAsync(context, next);
    }
}
