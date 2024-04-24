/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 09:32
** desc: 用户登录api
** Ver : V1.0.0
********************************************************************/

using Asp.Versioning;
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
public class UserLoginsController(
    ICacheStore cacheStore, IOptionsMonitor<AppSettings> options,
    IStringLocalizer localizer, ILogger<UserLoginsController> logger,
    IValidateLoginSvc validateLoginSvc)
    : ZsjBaseController(cacheStore, options, localizer, logger)
{
    [HttpPost]
    [ModelUrlDecode(nameof(loginInfoDto))]
    [TimeOutCheck]
    [ApiSignCheckFilter]
    public async Task<TokenResponse> Post([FromBody] LoginInfoDto loginInfoDto)
    {
        if (string.IsNullOrEmpty(loginInfoDto.UserName) ||
                string.IsNullOrEmpty(loginInfoDto.Password))
        {
            throw new Exception(Localizer[LocalizerStr.UserNameAndPassordError]);
        }

        return await validateLoginSvc.ValidateUserAsync(loginInfoDto);
    }

    [Route("refresh")]
    [HttpPost]
    [TimeOutCheck]
    public async Task<TokenResponse> Post([FromBody] string refreshToken)
    {
        return await validateLoginSvc.RefreshUserTokenAsync(refreshToken);
    }

    [Route("revokerefreshtoken")]
    [HttpPost]
    [TimeOutCheck]
    // [Authorize]
    public async Task Post()
    {
        if (!string.IsNullOrWhiteSpace(ApiUserId))
        {
            //清除用户对应的refresh token
            await validateLoginSvc.RemoveTokenAsync(ApiUserId);
        }
    }
}
