using Asp.Versioning;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Localization;
using Serilog;
using ZsjTest.Infrastructure;
using ZsjTest.WebApi;
using ZsjTest.WebApi.Middleware;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.Configure<AppSettings>(
        builder.Configuration.GetSection(AppSettings.Position));
    var appSettings = new AppSettings();
    builder.Configuration.GetSection(AppSettings.Position).Bind(appSettings);
    builder.Services.AddSingleton(appSettings);

    StartUp.InitialGlobalSerilog(builder);

    Log.Information("Starting host...");

    builder.Services.AddHealthChecks();
    builder.Host.UseSerilog();

    builder.Services.AddSingleton(new ProxyGenerator());
    builder.Services.AddScoped<IInterceptor, LogInterceptor>();
    builder.Services.AddScoped<IInterceptor, CacheInterceptor>();

    StartUp.WebServerSetting(builder, appSettings);
    StartUp.AddJwtAuthentication(builder, appSettings);
    StartUp.SetFilterAndReturnJsonFormat(builder, appSettings);
    StartUp.AddSwagger(builder);
    StartUp.AddCustomIOC(builder, appSettings);

    builder.Services.AddLocalization();
    builder.Services.AddSingleton<IStringLocalizer, LocalizerService>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddApiVersioning(v =>
    {
        v.ReportApiVersions = true;
        v.AssumeDefaultVersionWhenUnspecified = true;
        v.DefaultApiVersion = new ApiVersion(1, 0);
    });

    bool isSetCors = StartUp.SetCors(builder, appSettings);

    var app = builder.Build();

    StartUp.AddEventBus(builder);

    app.MapHealthChecks("/health");
    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders =
                ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });
    app.UseSerilogRequestLogging();
    app.UseResponseCompression();
    StartUp.SetSupportCultures(app);

    app.UseException();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    if (isSetCors)
    {
        app.UseCors(StartUp.MyAllowSpecificOrigins);
    }
    app.UseAuthorization();

    app.MapControllers();

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
