/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-21 22:17
** desc: 程序启动时的一些操作
** Ver : V1.0.0
********************************************************************/

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Core;
using System.Reflection;
using System.Runtime.Loader;
using System.Security.Cryptography;
using ZsjTest.Application.UserLogin;
using ZsjTest.Infrastructure;
using ZsjTest.WebApi.Common;
using ZsjTest.WebApi.Filter;

namespace ZsjTest.WebApi;

public class StartUp
{
    public static string MyAllowSpecificOrigins { get; } = "_myAllowSpecificOrigins";

    public static void InitialGlobalSerilog(WebApplicationBuilder builder)
    {
        AddSerilogDestructureSvc(builder);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .ReadFrom.Services(builder.Services.BuildServiceProvider())
            .Destructure.ByTransforming<LoginInfoDto>(_ => new { _.UserName, Password = "********" })
            .Destructure.ByTransforming<UserInfoDto>(_ => new {
                _.UserId,
                _.UserNumber,
                EngLastName = "****",
                EngMidName = "****",
                EngFirstName = "****",
                NativeName = "****",
                Email = "********",
            })
            .Destructure.ByTransforming<TokenResponse>(_ => new {
                _.UserInfo,
                Access_token = "******************",
                _.Expires_in,
                _.Token_type,
                Refresh_Token = "*********",
            })
            .CreateLogger();
    }

    public static void WebServerSetting(WebApplicationBuilder builder, AppSettings appSettings)
    {
        builder.Services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
            options.EnableForHttps = true;
        });

        builder.Services.Configure<IISServerOptions>(options =>
        {
            options.MaxRequestBodySize = appSettings.MaxRequestBodySize;
        });

        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = appSettings.MaxRequestBodySize;
        });
    }

    public static void AddJwtAuthentication(WebApplicationBuilder builder, AppSettings appSettings)
    {
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = appSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = appSettings.Aud,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(
                    appSettings.SigningCredential),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
                RequireExpirationTime = true,
            };
        });
    }
    public static void SetFilterAndReturnJsonFormat(
        WebApplicationBuilder builder, AppSettings appSettings)
    {
        builder.Services.AddControllers(opt =>
        {
            opt.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
            opt.Filters.Add<ApiUserIdSetFilter>();
        }).AddNewtonsoftJson(opt =>
        {
            opt.SerializerSettings.DateFormatHandling =
                Newtonsoft.Json.DateFormatHandling.IsoDateFormat;
            opt.SerializerSettings.DateFormatString = appSettings.DateFormate;
            opt.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            opt.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore;
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

    public static void SetSupportCultures(WebApplication app)
    {        
        var supportedCultures = new[] { "zh-CN", "en-US" };
        var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);
        app.UseRequestLocalization(localizationOptions);
    }

    public static void AddEventBus(WebApplicationBuilder builder)
    {
        // 注册事件
        foreach (var svc in builder.Services)
        {
            if (svc.ImplementationType != null &&
                svc.ImplementationType.GetInterface(
                    PubConst.IEventHandlerInterfaceName) != null)
            {
                //获取泛型参数类型
                var genericArgs =
                    svc.ImplementationType.GetInterface(
                        PubConst.IEventHandlerInterfaceName)!.GetGenericArguments();
                if (genericArgs.Length == 1)
                {
                    EventBus.Subscribe(genericArgs[0], svc.ImplementationType);
                }
            }
        }
    }

    public static void AddSwagger(WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {            
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            });
            c.OperationFilter<AuthOperationFilter>();
        });
    }

    public static void AddCustomIOC(WebApplicationBuilder builder, AppSettings appSettings)
    {
        List<DepencyInfo> diInfoByConfigs = AddIOCByConfigFile(builder, appSettings);
        AssemblyLoadContext.Default.Resolving +=
            (AssemblyLoadContext arg1, AssemblyName arg2) =>
            {
                string dllPath = Path.Combine(AppContext.BaseDirectory,
                    $"{arg2.Name}.dll");
                return arg1.LoadFromAssemblyPath(dllPath);
            };

        var diAssemblies = Directory.GetFiles(
                AppContext.BaseDirectory,
                $"{appSettings.AutoDIPrefix}*.dll",
                SearchOption.TopDirectoryOnly).
                Select(Path.GetFileNameWithoutExtension).ToArray();

        if (diAssemblies != null && diAssemblies.Length != 0)
        {
            Assembly[] assemblies =
                diAssemblies.Select(c => Assembly.Load(c!)).ToArray();

            AddSerivceByLifeTime(diInfoByConfigs, assemblies, builder, typeof(SingletonDepencyAttribute));
            AddSerivceByLifeTime(diInfoByConfigs, assemblies, builder, typeof(ScopedDepencyAttribute));
            AddSerivceByLifeTime(diInfoByConfigs, assemblies, builder, typeof(TransientDepencyAttribute));
        }
    }

    public static void AddSerilogDestructureSvc(WebApplicationBuilder builder)
    {
        AppSettings appSettings = new AppSettings();
        builder.Configuration.GetSection(AppSettings.Position).Bind(appSettings);
        if (string.IsNullOrWhiteSpace(appSettings.SerilogDestructureName))
        {
            return;
        }

        string dllFileName = appSettings.SerilogDestructureName.Split(',')[1];
        string typeName = appSettings.SerilogDestructureName.Split(',')[0];
        string dllPath = Path.Combine(AppContext.BaseDirectory, $"{dllFileName}.dll");

        TypeLoader typeLoader = new();
        Type implementaionType = typeLoader.LoadType(typeName, dllPath);
        builder.Services.AddSingleton(typeof(IDestructuringPolicy), implementaionType);
    }

    private static Type[] GetAbstractTypeByLifeTimeAttribute(
        Assembly[] assemblies, Type type)
    {
        return assemblies.SelectMany(a => a.GetTypes())
                .Where(x => x.GetCustomAttribute(type, false) != null &&
                    (x.IsInterface || x.IsAbstract)).ToArray();
    }

    private static Type[] GetDirectDIImplementaionTypeByLifeTimeAttribute(
        Assembly[] assemblies, Type type)
    {
        return assemblies.SelectMany(a => a.GetTypes())
                .Where(x => x.GetCustomAttribute(type, false) != null &&
                    !x.IsAbstract && x.IsClass).ToArray();
    }

    private static void AddSerivceByLifeTime(
        List<DepencyInfo> depencyInfos,
        Assembly[] assemblies, WebApplicationBuilder builder, Type lifeTimeType)
    {
        Type[] abstractTypes =
                GetAbstractTypeByLifeTimeAttribute(assemblies, lifeTimeType);

        foreach (var t in abstractTypes)
        {
            //如果通过文件已经注入了，则忽略掉
            if (depencyInfos.Any(c => c.AbstractInfo.Split(',')[0] == t.FullName))
            {
                continue;
            }
            var implType = assemblies.SelectMany(a => a.GetTypes())
                .Where(d => t.IsAssignableFrom(d) && d.IsClass &&
                    d.GetCustomAttribute(lifeTimeType, false) == null).FirstOrDefault();

            if (implType == null)
            {
                continue;
            }

            if (lifeTimeType == typeof(SingletonDepencyAttribute))
            {
                if (implType.GetCustomAttribute(typeof(NeedInterceptAttribute)) != null)
                {
                    builder.Services.AddProxySingleton(t, implType);
                }
                else
                {
                    builder.Services.AddSingleton(t, implType);
                }
            }
            else if (lifeTimeType == typeof(ScopedDepencyAttribute))
            {
                if (implType.GetCustomAttribute(typeof(NeedInterceptAttribute)) != null)
                {
                    builder.Services.AddProxyScoped(t, implType);
                }
                else
                {
                    builder.Services.AddScoped(t, implType);
                }
            }
            else if (lifeTimeType == typeof(TransientDepencyAttribute))
            {
                if (implType.GetCustomAttribute(typeof(NeedInterceptAttribute)) != null)
                {
                    builder.Services.AddProxyTransient(t, implType);
                }
                else
                {
                    builder.Services.AddTransient(t, implType);
                }
            }
        }

        Type[] implTypes =
                GetDirectDIImplementaionTypeByLifeTimeAttribute(assemblies, lifeTimeType);

        foreach (var t in implTypes)
        {
            //如果通过文件已经注入了，则忽略掉
            if (depencyInfos.Any(c => c.ImplementaionInfo.Split(',')[0] == t.FullName))
            {
                continue;
            }

            if (lifeTimeType == typeof(SingletonDepencyAttribute))
            {
                if (t.GetCustomAttribute(typeof(NeedInterceptAttribute), true) != null)
                {
                    builder.Services.AddProxySingleton(t, t);
                }
                else
                {
                    builder.Services.AddSingleton(t);
                }
            }
            else if (lifeTimeType == typeof(ScopedDepencyAttribute))
            {
                if (t.GetCustomAttribute(typeof(NeedInterceptAttribute), true) != null)
                {
                    builder.Services.AddProxyScoped(t, t);
                }
                else
                {
                    builder.Services.AddScoped(t);
                }
            }
            else if (lifeTimeType == typeof(TransientDepencyAttribute))
            {
                if (t.GetCustomAttribute(typeof(NeedInterceptAttribute), true) != null)
                {
                    builder.Services.AddProxyTransient(t, t);
                }
                else
                {
                    builder.Services.AddTransient(t);
                }                    
            }
        }
    }

    private static List<DepencyInfo> AddIOCByConfigFile(WebApplicationBuilder builder, AppSettings appSettings)
    {
        TypeLoader typeLoader = new();
        var depencyInfos = builder.Configuration.GetSection(
            DepencyInfo.PositionInSetting).Get<List<DepencyInfo>>();

        if (depencyInfos == null)
        {
            return [];
        }
        foreach (var depence in depencyInfos)
        {
            string dllFileName = depence.ImplementaionInfo.Split(',')[1];
            string typeName = depence.ImplementaionInfo.Split(',')[0];
            string dllPath = Path.Combine(AppContext.BaseDirectory, $"{dllFileName}.dll");
            Type implementaionType = typeLoader.LoadType(typeName, dllPath);
            Type abstractType = implementaionType;
            if (!string.IsNullOrEmpty(depence.AbstractInfo))
            {
                string abDllFileName = depence.AbstractInfo.Split(',')[1];
                string abTypeName = depence.AbstractInfo.Split(',')[0];
                string abDllPath = Path.Combine(AppContext.BaseDirectory, $"{abDllFileName}.dll");
                abstractType = typeLoader.LoadType(abTypeName, abDllPath);
            }

            switch (depence.LifeTime)
            {
                case DepencyInfo.ScopedLifeTime:
                    if (implementaionType.GetCustomAttribute(typeof(NeedInterceptAttribute)) != null)
                    {

                        builder.Services.AddProxyScoped(abstractType, implementaionType);
                    }
                    else
                    {
                        builder.Services.AddScoped(abstractType, implementaionType);
                    }
                    break;
                case DepencyInfo.SingletonLifeTime:
                    if (implementaionType.GetCustomAttribute(typeof(NeedInterceptAttribute), true) != null)
                    {
                        builder.Services.AddProxySingleton(abstractType, implementaionType);
                    }
                    else
                    { 
                        builder.Services.AddSingleton(abstractType, implementaionType);
                    }
                    break;
                case DepencyInfo.TransientLifeTime:
                    if (implementaionType.GetCustomAttribute(typeof(NeedInterceptAttribute), true) != null)
                    {
                        builder.Services.AddProxyTransient(abstractType, implementaionType);
                    }
                    else
                    {
                        builder.Services.AddTransient(abstractType, implementaionType);
                    }
                    break;
            }
        }

        return depencyInfos;
    }


    private static RsaSecurityKey GetSecurityKey(string base64Key)
    {
        RSA rsa = RSA.Create();
        rsa.ImportRSAPrivateKey(Convert.FromBase64String(base64Key), out _);
        var secretKey = new RsaSecurityKey(rsa);
        return secretKey;
    }

}
