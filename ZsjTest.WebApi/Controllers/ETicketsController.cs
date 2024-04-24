/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 09:32
** desc: ETicket Api
** Ver : V1.0.0
********************************************************************/

using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ZsjTest.Application.ETicketMgr;
using ZsjTest.Infrastructure;
using ZsjTest.WebApi.Filter;

namespace ZsjTest.WebApi.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ETicketsController(
    ICacheStore cacheStore, IOptionsMonitor<AppSettings> options,
    IStringLocalizer localizer, ILogger<ETicketsController> logger,
    IETicketSvc eTicketSvc)
    : ZsjBaseController(cacheStore, options, localizer, logger)
{
    [HttpGet("{id}")]
    [ApiSignCheckFilter]
    [Authorize(Roles = PubConst.ApplicationApiName +
            "," + PubConst.DataSyncApiName)]
    public async Task<ETicketDto> Get(int id)
    {
        return await eTicketSvc.GetETicketByIdAsync(id);
    }

    // FromQuery参数尽量扁平化，不要使用复杂类型
    [HttpGet]
    [ApiSignCheckFilter]
    [Authorize(Roles = PubConst.ApplicationApiName +
            "," + PubConst.DataSyncApiName)]
    public async Task<List<ETicketDto>> Get(
        [FromQuery] ETicketQueryParamDto queryParam)
    {
        var result = await eTicketSvc.GetETicketDtosAsync(queryParam);
        return result;
    }

    [Route("stats")]
    [HttpGet]
    [ApiSignCheckFilter]
    [Authorize(Roles = PubConst.ApplicationApiName +
            "," + PubConst.DataSyncApiName)]
    public async Task<int> GetStats([FromQuery] ETicketQueryParamDto queryParam)
    {
        return await eTicketSvc.GetETicketDtosCountAsync(queryParam);
    }

    [HttpPost]
    [ApiSignCheckFilter]
    [Authorize(Roles = PubConst.ApplicationApiName +
            "," + PubConst.DataSyncApiName)]
    public async Task Post([FromBody] ETicketDto eTicketDto)
    {
        await eTicketSvc.SaveETicketAsync(eTicketDto);
    }

}
