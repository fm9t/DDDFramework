/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-11 16:06
** desc: 缓存拦截器，仅供演示拦截器功能
** Ver : V1.0.0
***********************************************************************/

using Microsoft.Extensions.Logging;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ZsjTest.Infrastructure;

public class CacheInterceptor(ILogger<CacheInterceptor> logger, ICacheStore cacheStore) : IInterceptor
{
    private readonly JsonSerializerSettings _settings = new()
    {
        DateFormatString = "yyyy-MM-dd HH:mm:ss",
        ContractResolver =
                new CamelCasePropertyNamesContractResolver(),
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };
    public void Intercept(IInvocation invocation)
    {
        // 判断方法是否标识了AllowCacheInterceptorAttribute, 只有标识了的才拦截
        var attribute =
            Attribute.GetCustomAttribute(invocation.MethodInvocationTarget, typeof(AllowCacheInterceptorAttribute));
        var returnType = invocation.Method.ReturnType;
        if (attribute == null || returnType == typeof(void) || returnType == typeof(Task))
        {
            invocation.Proceed();
        }
        else
        {
            logger.LogDebug("className: {className}, methodName: {methodName}, message: {message}",
                nameof(invocation.TargetType), invocation.Method.Name, nameof(CacheInterceptor));

            int cacheTime = (attribute as AllowCacheInterceptorAttribute)!.CacheTime;
            string cacheKey = nameof(invocation.InvocationTarget) + "_" + invocation.Method.Name
                + "_" + JsonConvert.SerializeObject(invocation.Arguments, _settings);
            var ret = cacheStore.Get(cacheKey);

            bool isTaskRetrn = returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>);

            if (ret != null)
            {
                logger.LogDebug("className: {className}, methodName: {methodName}, message: {message}, get result: {result}",
                    nameof(invocation.TargetType), invocation.Method.Name, nameof(CacheInterceptor), JsonConvert.SerializeObject(ret, _settings));
                if (isTaskRetrn)
                {
                    var originalType = returnType.GetGenericArguments()[0];
                    var lastRet = Convert.ChangeType(ret, originalType);
                    invocation.ReturnValue = Task.FromResult(Convert.ChangeType(ret, originalType));
                }
                else
                {
                    invocation.ReturnValue = Convert.ChangeType(ret, returnType);
                }
            }
            else
            {
                invocation.Proceed();
                var returnValue = invocation.ReturnValue;
                if (isTaskRetrn)
                {
                    var task = (Task)returnValue;
                    _ = task.ContinueWith((taskResult) =>
                    {
                        if (!taskResult.IsFaulted)
                        {
                            var result =
                                taskResult?.GetType()?.GetProperty("Result")?.GetValue(taskResult, null);
                            cacheStore.Add(ret, cacheKey, new TimeSpan(0, 0, cacheTime));
                        }
                    });
                }
                else
                {
                    cacheStore.Add(returnValue, cacheKey, new TimeSpan(0, 0, cacheTime));
                }
            }
        }
    }
}
