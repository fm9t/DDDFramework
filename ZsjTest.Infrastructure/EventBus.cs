/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-12 21:38
** desc: 事件总线
** Ver : V1.0.0
************************************************************************/

using System.Collections.Concurrent;

namespace ZsjTest.Infrastructure;

[ScopedDepency]
public class EventBus(IServiceProvider serviceProvider)
{
    private static readonly ConcurrentDictionary<string, List<Type>>
        _eventAndHandlerMapping = new();

    public static void Subscribe(Type eventType, Type eventHandlerType)
    {
        if (!_eventAndHandlerMapping.ContainsKey(eventType.FullName!))
        {
            _eventAndHandlerMapping.TryAdd(eventType.FullName!, new List<Type> { });
        }

        _eventAndHandlerMapping[eventType.FullName!].Add(eventHandlerType);
    }

    public static void Unsubscribe(Type eventType, Type eventHandlerType)
    {
        if (_eventAndHandlerMapping.TryGetValue(eventType.FullName!, out List<Type>? value))
        {
            value.Remove(eventHandlerType);
        }
        if (_eventAndHandlerMapping[eventType.FullName!].Count == 0)
        {
            _eventAndHandlerMapping.TryRemove(eventType.FullName!, out _);
        }
    }

    public void Publish<T>(T o) where T : EventBase
    {
        if (_eventAndHandlerMapping.TryGetValue(o.GetType().FullName!, out List<Type>? value))
        {
            foreach (var handler in value)
            {
                if (serviceProvider.GetService(handler) is IEventHandler<T> service)
                {
                    Task.Run(async () =>
                    {
                        await service.RunAsync(o);
                    });
                }
            }
        }
    }
}
