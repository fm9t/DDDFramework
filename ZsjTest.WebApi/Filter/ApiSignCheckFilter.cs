/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-10 16:17
** desc: Api签名验证过滤器
** Ver : V1.0.0
********************************************************************/

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZsjTest.Application.ApiMgr;
using ZsjTest.Infrastructure;

namespace ZsjTest.WebApi.Filter;

public class ApiSignCheckFilterAttribute : TypeFilterAttribute
{
    public ApiSignCheckFilterAttribute() : base(typeof(ApiSignCheckFilter))
    { }
}

public class ApiSignCheckFilter(IApiMgrSvc apiMgrSvc, AppSettings appSettings) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if(appSettings.NeedCheckApiSign && !await apiMgrSvc.CheckUrlValidAsync(context.HttpContext.Request.GetDisplayUrl()))
        {
            throw new Exception(LocalizerStr.ApiCallInvalid);
        }
        await next();
    }
}
