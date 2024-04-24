/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-12 21:38
** desc: 事件处理程序接口
** Ver : V1.0.0
************************************************************************/

namespace ZsjTest.Infrastructure;
public interface IEventHandler<T> where T : EventBase
{
    void Run(T obj);

    Task RunAsync(T obj);
}
