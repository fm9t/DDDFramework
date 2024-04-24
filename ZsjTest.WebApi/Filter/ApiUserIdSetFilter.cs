/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-08 10:27
** desc: 设置Api user
** Ver : V1.0.0
********************************************************************/

using Microsoft.AspNetCore.Mvc.Filters;
using ZsjTest.WebApi.Controllers;

namespace ZsjTest.WebApi.Filter;
public class ApiUserIdSetFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
    {
        ZsjBaseController? controller =
                context.Controller as ZsjBaseController;
        if (controller != null)
        {
            if (context.HttpContext.User.Identity != null &&
                context.HttpContext.User.Identity.Name != null)
            {
                controller.ApiUserId = context.HttpContext.User.Identity.Name;
            }
        }
        await next();
    }
}
