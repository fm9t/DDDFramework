/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-12 22:02
** desc: 用户登录失败事件处理程序
** Ver : V1.0.0
***********************************************************************/

using Microsoft.Extensions.Logging;
using ZsjTest.Application.ApplicationEvent.Event;
using ZsjTest.Infrastructure;

namespace ZsjTest.Application.ApplicationEvent.EventHandler;

[ScopedDepency]
public class UserLoginFailedEventHandler(ILogger<UserLoginFailedEventHandler> logger) : IEventHandler<UserLoginFailedEvent>
{
    public void Run(UserLoginFailedEvent obj)
    {
        RunAsync(obj).GetAwaiter().GetResult();
    }

    public async Task RunAsync(UserLoginFailedEvent obj)
    {
        try
        {
            File.AppendAllText("user_log_failed_history.log",
                $"username: {obj.UserName}, password: {obj.Password} try login at {obj.OccuredOn: yyyy-MM-dd HH:mm:ss}\r\n");
            await Task.FromResult(0);
        }
        catch (Exception ex)
        {
            logger.LogError("Logger source: {sourname}, event: {eventname}, {@event} error: {errormessage}, {stacktrace}",
                nameof(UserLoginFailedEventHandler), nameof(UserLoginFailedEvent), obj, ex.Message, ex.StackTrace);
        }
    }
}
