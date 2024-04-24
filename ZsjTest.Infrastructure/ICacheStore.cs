/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 08:48
** desc: 缓存接口 
** Ver : V1.0.0
***********************************************************************/

namespace ZsjTest.Infrastructure;

[SingletonDepency]
public interface ICacheStore
{
    void Add<TItem>(TItem item, string key, TimeSpan? expirationTime = null, bool isSlideExpire = false);
    TItem? Get<TItem>(string key);
    object? Get(string key);
    void Remove(string key);
    void Remove(string key, int waitMilliSeconds);
}
