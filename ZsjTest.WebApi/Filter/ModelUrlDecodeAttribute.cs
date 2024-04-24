/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 09:49
** desc: 对Body中的JSON进行转码
** Ver : V1.0.0
************************************************************************/

using Microsoft.AspNetCore.Mvc.Filters;
using ZsjTest.Application.Utils;
using ZsjTest.WebApi.Controllers;

namespace ZsjTest.WebApi.Filter;

public class ModelUrlDecodeAttribute(string paramName) : ActionFilterAttribute
{
    private readonly string _paramName = paramName;

    public override async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
    {
        try
        {
            var o = context.ActionArguments[_paramName];

            if (o != null)
            {
                if (typeof(IEnumerable<CanUrlDecodeDto>).IsAssignableFrom(o.GetType()))
                {
                    var list = o as IEnumerable<CanUrlDecodeDto>;
                    if (list != null)
                    {
                        foreach (var li in list)
                        {
                            li.UrlDecode();
                        }
                    }
                }
                else
                {
                    if (o is CanUrlDecodeDto el)
                    {
                        el.UrlDecode();
                    }
                }
            }
        }
        catch
        {
            ZsjBaseController? controller =
                context.Controller as ZsjBaseController;
            controller?.Logger.LogError(
                $"ModelUrlDecode error: {context.Controller.GetType().Name}, " +
                $"{context.ActionDescriptor.DisplayName}, {_paramName}");
        }

        await base.OnActionExecutionAsync(context, next);
    }
}
