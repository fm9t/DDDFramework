/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-12 22:02
** desc: 用户登录事件
** Ver : V1.0.0
***********************************************************************/

using ZsjTest.Infrastructure;

namespace ZsjTest.Domain.DomainEvent.Event;

public class UserLoginEvent(int userId) : EventBase
{
    public int UserId { get; set; } = userId;
}
