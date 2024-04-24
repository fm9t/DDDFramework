/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-12 21:38
** desc: 事件基类，所有事件均需继承此类
** Ver : V1.0.0
************************************************************************/

namespace ZsjTest.Infrastructure;

public class EventBase
{
    public DateTime OccuredOn { get; set; } = DateTime.Now;
}
