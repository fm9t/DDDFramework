/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-10 22:17
** desc: 程序启动时的一些操作
** Ver : V1.0.0
************************************************************************/

using Microsoft.AspNetCore.Server.Kestrel.Core;
using Serilog;
using ZsjTest.ReverseProxy.Models;

namespace ZsjTest.ReverseProxy;

public class StartUp
{
    public static string MyAllowSpecificOrigins { get; } = "_myAllowSpecificOrigins";

    public static void InitialGlobalSerilog()
    {
        string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json",
                optional: true, reloadOnChange: true);
        if (!string.IsNullOrEmpty(env))
        {
            configBuilder.AddJsonFile($"appsettings.{env}.json", optional: true,
                reloadOnChange: true);
        }
        var config = configBuilder.AddEnvironmentVariables().Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();
    }

    public static void WebServerSetting(WebApplicationBuilder builder, AppSettings appSettings)
    {
        builder.Services.Configure<IISServerOptions>(options =>
        {
            options.MaxRequestBodySize = appSettings.MaxRequestBodySize;
        });

        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = appSettings.MaxRequestBodySize;
        });
    }

    public static bool SetCors(WebApplicationBuilder builder, AppSettings appSettings)
    {
        var alloCorsUrl = appSettings.AllowCrosUrls;
        if (alloCorsUrl != null && alloCorsUrl.Count > 0)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins([.. alloCorsUrl]).AllowAnyMethod()
                            .AllowAnyHeader().AllowCredentials();
                    });
            });
            return true;
        }
        return false;
    }
}
