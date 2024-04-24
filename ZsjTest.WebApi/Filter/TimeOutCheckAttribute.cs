/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 09:14
** desc: 超时过滤器
** Ver : V1.0.0
********************************************************************/

using Microsoft.AspNetCore.Mvc.Filters;
using ZsjTest.Infrastructure;
using ZsjTest.WebApi.Controllers;

namespace ZsjTest.WebApi.Filter;

public class TimeOutCheckAttribute(int timeOut = 0) : ActionFilterAttribute
{
    private readonly int _timeOut = timeOut;

    public override async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.Controller is ZsjBaseController controller)
        {
            controller!.Logger.LogDebug(
                $"GlobalTimeOutAction run : {context.Controller.GetType().Name}, " +
                $"{context.ActionDescriptor.DisplayName}");

            var cancellationTokenSource = new CancellationTokenSource();
            var taskToAwait = Task.Run(async () =>
            {
                controller!.SetCancelToken(cancellationTokenSource.Token);
                await base.OnActionExecutionAsync(context, next);
            }, cancellationTokenSource.Token);

            await PubTools.AwaitWithTimeout(
                taskToAwait,
                cancellationTokenSource,
                _timeOut == 0 ?
                    controller!.Setting.ProcessTimeout * 1000 : _timeOut * 1000,
                controller!.Localizer[LocalizerStr.TimeOut]);
        }
        else
        {
            await base.OnActionExecutionAsync(context, next);
        }
    }
}
