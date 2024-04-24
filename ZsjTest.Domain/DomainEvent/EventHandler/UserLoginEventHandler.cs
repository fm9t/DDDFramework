/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-12 22:02
** desc: 用户登录事件处理程序
** Ver : V1.0.0
***********************************************************************/

using Microsoft.Extensions.Logging;
using ZsjTest.Domain.DomainEvent.Event;
using ZsjTest.Infrastructure;

namespace ZsjTest.Domain.DomainEvent.EventHandler;

[ScopedDepency]
public class UserLoginEventHandler(ILogger<UserLoginEventHandler> logger) : IEventHandler<UserLoginEvent>
{
    public void Run(UserLoginEvent obj)
    {
        RunAsync(obj).GetAwaiter().GetResult();
    }

    public async Task RunAsync(UserLoginEvent obj)
    {
        try
        {
            File.AppendAllText("user_log_history.log",
                $"user: {obj.UserId} login at {obj.OccuredOn: yyyy-MM-dd HH:mm:ss}\r\n");
            await Task.FromResult(0);
        }
        catch (Exception ex)
        {
            logger.LogError("Logger source: {sourname}, event: {eventname}, {@event} error: {errormessage}, {stacktrace}",
                nameof(UserLoginEventHandler), nameof(UserLoginEvent), obj, ex.Message, ex.StackTrace);
        }
    }
}
