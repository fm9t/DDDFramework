/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-12 22:02
** desc: 用户登录失败事件
** Ver : V1.0.0
***********************************************************************/

using ZsjTest.Infrastructure;

namespace ZsjTest.Application.ApplicationEvent.Event;

public class UserLoginFailedEvent(string userName, string password) : EventBase
{
    public string UserName { get; set; } = userName;
    public string Password { get; set; } = password;
}
