/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 08:42
** desc: 自定义的Controller基类
** Ver : V1.0.0
********************************************************************/
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using ZsjTest.Infrastructure;
using ZsjTest.WebApi.Filter;

namespace ZsjTest.WebApi.Controllers;

[ParamLogger]
public class ZsjBaseController(
    ICacheStore cacheStore,
    IOptionsMonitor<AppSettings> options,
    IStringLocalizer localizer,
    ILogger<ZsjBaseController> logger) : ControllerBase
{
    public AppSettings Setting { get; set; } = options.CurrentValue;
    public IStringLocalizer Localizer { get; set; } = localizer;
    public ILogger Logger { get; set; } = logger;
    public ICacheStore CacheStore { get; set; } = cacheStore;
    protected CancellationToken CancelToken { get; set; }

    public string? ApiUserId { get; set; }

    [ApiExplorerSettings(IgnoreApi = true)]
    public void SetCancelToken(CancellationToken token)
    {
        CancelToken = token;
    }
}
