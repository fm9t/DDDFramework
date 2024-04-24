/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 11:03
** desc: 用户登录验证
** Ver : V1.0.0
**********************************************************************/

using AutoMapper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ZsjTest.Application.ApplicationEvent.Event;
using ZsjTest.Application.UserLogin;
using ZsjTest.Domain.DomainEvent.Event;
using ZsjTest.Domain.UserMgr;
using ZsjTest.Infrastructure;

namespace ZsjTest.Application.Impl.UserLogin;

[NeedIntercept]
[AllowLogInterceptor]
public class ValidateLoginSvc(
    IOptionsMonitor<AppSettings> options,
    IMapper mapper,
    IStringLocalizer localizer,
    IUserInfoDomainSvc userInfoDomainSvc,
    EventBus eventBus) : IValidateLoginSvc
{
    public async Task<TokenResponse> ValidateUserAsync(LoginInfoDto loginInfoDto)
    {
        var userInfo = await userInfoDomainSvc.GetUserInfoAsync(
            loginInfoDto.UserName, loginInfoDto.Password);
        if (userInfo == null)
        {
            eventBus.Publish(new UserLoginFailedEvent(loginInfoDto.UserName, loginInfoDto.Password));
            throw new Exception(localizer[LocalizerStr.UserNameAndPassordError]);
        }

        eventBus.Publish(new UserLoginEvent(userInfo.UserId));

        var jwtToken =  await userInfo.BuildNewTokenAsync(options.CurrentValue);
        string refreshToken = Guid.NewGuid().ToString("N");
        await userInfoDomainSvc.SaveRefreshTokenAsync(userInfo, refreshToken);
        var result = mapper.Map<TokenResponse>(jwtToken);
        result.Refresh_Token = refreshToken;
        var userInfoDto = mapper.Map<UserInfoDto>(userInfo);
        result.UserInfo = userInfoDto;
        return result;
    }

    public async Task<TokenResponse> RefreshUserTokenAsync(string refreshToken)
    {
        var userInfo = await userInfoDomainSvc.GetUserInfoByRefreshTokenAsync(refreshToken);
        return mapper.Map<TokenResponse>(await userInfo.BuildNewTokenAsync(options.CurrentValue));
    }

    public async Task RemoveTokenAsync(string userId)
    {
        if (int.TryParse(userId, out int uid))
        {
            await userInfoDomainSvc.RemoveRefreshTokenAsync(uid);
        }
    }

    [AllowCacheInterceptor(CacheTime = 300)]
    public int TestCacheInterceptor(string sss)
    {
        Thread.Sleep(1000 * 10);
        return DateTime.Now.Second;
    }
}
