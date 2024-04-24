/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-10 22:45
** desc: 反向代理
** Ver : V1.0.0
***********************************************************************/

using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using ZsjTest.ReverseProxy;
using ZsjTest.ReverseProxy.Middleware;
using ZsjTest.ReverseProxy.Models;

StartUp.InitialGlobalSerilog();

try
{
    Log.Information("Starting host...");
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddHealthChecks();
    builder.Services.Configure<AppSettings>(
        builder.Configuration.GetSection(AppSettings.Position));
    var appSettings = new AppSettings();
    builder.Configuration.GetSection(AppSettings.Position).Bind(appSettings);
    builder.Services.AddSingleton(appSettings);
    builder.Host.UseSerilog();
    bool isSetCors = StartUp.SetCors(builder, appSettings);
    builder.Services.AddHttpClient();

    var app = builder.Build();
    app.MapHealthChecks("/health");

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
    app.UseSerilogRequestLogging();
    app.UseException();
    if (isSetCors)
    {
        app.UseCors(StartUp.MyAllowSpecificOrigins);
    }
    app.UseReverseProxy();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}
