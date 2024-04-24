/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 09:32
** desc: 测试用api
** Ver : V1.0.0
********************************************************************/

using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using ZsjTest.Application.UserLogin;
using ZsjTest.Infrastructure;
using ZsjTest.WebApi.Filter;

namespace ZsjTest.WebApi.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ValuesController(
    ICacheStore cacheStore, IOptionsMonitor<AppSettings> options,
    IStringLocalizer localizer, ILogger<ZsjBaseController> logger,
    IValidateLoginSvc validateLoginSvc)
    : ZsjBaseController(cacheStore, options, localizer, logger)
{
    [HttpGet]
    //[Authorize]
    [TimeOutCheck(10)]
    public IEnumerable<string> Get()
    {
        Thread.Sleep(30000);
        return new string[] { "value1", "value2" };
    }

    [HttpGet("{id}")]
    public string Get(int id)
    {
        return validateLoginSvc.TestCacheInterceptor("abcdefg").ToString();
        //return "value";
    }

    [HttpPost]
    [ModelUrlDecode(nameof(loginInfoDto))]
    public string Post([FromBody] LoginInfoDto loginInfoDto)
    {
        return loginInfoDto.ToString();
    }
}
