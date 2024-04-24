/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-11 16:06
** desc: 日志拦截器
** Ver : V1.0.0
***********************************************************************/

using Microsoft.Extensions.Logging;
using Castle.DynamicProxy;
using System.Reflection;

namespace ZsjTest.Infrastructure;

public class LogInterceptor(ILogger<LogInterceptor> logger) : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        // 判断类或者方法是否标识了AllowLogInterceptor, 只有标识了的才拦截
        if (invocation.InvocationTarget.GetType().GetCustomAttribute(typeof(AllowLogInterceptorAttribute)) != null ||
            Attribute.GetCustomAttribute(invocation.MethodInvocationTarget, typeof(AllowLogInterceptorAttribute)) != null)
        {
            string logPrefix = nameof(LogInterceptor);
            string className = invocation.InvocationTarget.GetType().ToString();
            string methodName = invocation.Method.Name;
            logger.LogDebug("Logger source: {InterceptorName}, className: {className}, methodName: {methodName}, args: {@args}",
                logPrefix, className, methodName, invocation.Arguments);

            try
            {
                invocation.Proceed();
                var returnType = invocation.Method.ReturnType;
                if (returnType != typeof(void))
                {
                    var returnValue = invocation.ReturnValue;
                    if (returnType == typeof(Task))
                    {
                        logger.LogDebug("Logger source: {InterceptorName}, className: {className}, methodName: {methodName} end",
                            logPrefix, className, methodName);
                    }
                    else if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                    {
                        var task = (Task)returnValue;
                        _ = task.ContinueWith((taskResult) =>
                        {
                            if (taskResult.IsFaulted)
                            {
                                if (taskResult.Exception != null)
                                {
                                    Exception ex = taskResult.Exception;
                                    if (ex.InnerException != null)
                                    {
                                        ex = ex.InnerException;
                                    }
                                    if (ex is AggregateException exception)
                                    {
                                        ex = exception.InnerExceptions[0];
                                    }
                                    while (ex.InnerException != null)
                                    {
                                        ex = ex.InnerException;
                                    }
                                    logger.LogDebug("Logger source: {InterceptorName}, className: {className}, methodName: {methodName} error, message: {errormessage}, stacktrace: {stacktrace}",
                                        logPrefix, className, methodName, ex.Message, ex.StackTrace);
                                }
                                else
                                {
                                    logger.LogDebug("Logger source: {InterceptorName}, className: {className}, methodName: {methodName} error, message: {errormessage}",
                                        logPrefix, className, methodName, "Unknown error");
                                }
                            }
                            else
                            {
                                var result =
                                    taskResult?.GetType()?.GetProperty("Result")?.GetValue(taskResult, null);
                                logger.LogDebug("Logger source: {InterceptorName}, className: {className}, methodName: {methodName}, result: {@result}",
                                        logPrefix, className, methodName, result);
                            }
                        });
                    }
                    else
                    {
                        logger.LogDebug("Logger source: {InterceptorName}, className: {className}, methodName: {methodName}, result: {@result}",
                                        logPrefix, className, methodName, returnValue);
                    }
                }
                else
                {
                    logger.LogDebug("Logger source: {InterceptorName}, className: {className}, methodName: {methodName} end",
                            logPrefix, className, methodName);
                }

            }
            catch (Exception ex)
            {
                Exception tempEx = ex;
                while (tempEx.InnerException != null)
                {
                    tempEx = tempEx.InnerException;
                }
                logger.LogDebug("Logger source: {InterceptorName}, className: {className}, methodName: {methodName} error, message: {errormessage}, stacktrace: {stacktrace}",
                                        logPrefix, className, methodName, tempEx.Message, tempEx.StackTrace);
                throw;
            }
        }
        else
        {
            invocation.Proceed();
        }
    }
}
