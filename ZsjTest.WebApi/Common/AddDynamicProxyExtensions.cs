/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-11 16:19
** desc: AddDynamicProxy扩展
** Ver : V1.0.0
***********************************************************************/

using Castle.DynamicProxy;

namespace ZsjTest.WebApi.Common;

public static class AddDynamicProxyExtensions
{
    public static void AddProxyScoped
        (this IServiceCollection services, Type t, Type impl)
    {
        services.AddScoped(impl);
        services.AddScoped(t, serviceProvider =>
        {
            var proxyGenerator = serviceProvider
                .GetRequiredService<ProxyGenerator>();
            var actual = serviceProvider.GetRequiredService(impl);
            var interceptors = serviceProvider
                .GetServices<IInterceptor>().ToArray();
            return proxyGenerator.CreateInterfaceProxyWithTarget(t, actual, interceptors);
        });
    }

    public static void AddProxySingleton
        (this IServiceCollection services, Type t, Type impl)
    {
        services.AddSingleton(impl);
        services.AddSingleton(t, serviceProvider =>
        {
            var proxyGenerator = serviceProvider
                .GetRequiredService<ProxyGenerator>();
            var actual = serviceProvider.GetRequiredService(impl);
            var interceptors = serviceProvider
                .GetServices<IInterceptor>().ToArray();
            return proxyGenerator.CreateInterfaceProxyWithTarget(t, actual, interceptors);
        });
    }

    public static void AddProxyTransient
        (this IServiceCollection services, Type t, Type impl)
    {
        services.AddTransient(impl);
        services.AddTransient(t, serviceProvider =>
        {
            var proxyGenerator = serviceProvider
                .GetRequiredService<ProxyGenerator>();
            var actual = serviceProvider.GetRequiredService(impl);
            var interceptors = serviceProvider
                .GetServices<IInterceptor>().ToArray();
            return proxyGenerator.CreateInterfaceProxyWithTarget(t, actual, interceptors);
        });
    }
}
