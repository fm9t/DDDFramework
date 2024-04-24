/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 08:48
** desc: 使用MemoryCache作为缓存接口的默认实现
** Ver : V1.0.0
***********************************************************************/
using Microsoft.Extensions.Caching.Memory;

namespace ZsjTest.Infrastructure;

public class MemoryCacheStore : ICacheStore
{
    private readonly MemoryCache _memoryCache;
    private readonly AppSettings _appSetting;
    public MemoryCacheStore(AppSettings appSettings)
    {
        _appSetting = appSettings;
        _memoryCache = new MemoryCache(new MemoryCacheOptions
        {
            SizeLimit = appSettings.CacheSizeLimit
        });
    }
    public void Add<TItem>(TItem item, string key, TimeSpan? expirationTime = null, bool isSlideExpire = false)
    {
        MemoryCacheEntryOptions mo = new()
        {
            Size = 1
        };

        if (expirationTime.HasValue)
        {
            if (isSlideExpire)
            {
                mo.SlidingExpiration = expirationTime;
            }
            else
            {
                mo.AbsoluteExpirationRelativeToNow = expirationTime;
            }
        }
        else
        {
            TimeSpan ts = new TimeSpan(0, 0, _appSetting.CacheDefaultExpireSec);
            if (isSlideExpire)
            {
                mo.SlidingExpiration = ts;
            }
            else
            {
                mo.AbsoluteExpirationRelativeToNow = ts;
            }
        }
        _memoryCache.Set(key, item, mo);
    }

    public TItem? Get<TItem>(string key)
    {
        _memoryCache.TryGetValue<TItem?>(key, out TItem? value);
        return value;
    }

    public object? Get(string key)
    {
        _memoryCache.TryGetValue(key, out object? value);
        return value;
    }

    public void Remove(string key)
    {
        _memoryCache.Remove(key);
    }

    public void Remove(string key, int waitMilliSeconds)
    {
        Task.Run(() =>
        {
            Task.Delay(waitMilliSeconds);
            Remove(key);
        });
    }
}
