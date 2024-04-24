/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 09:49
** desc: 用户登录验证接口
** Ver : V1.0.0
**********************************************************************/

using ZsjTest.Infrastructure;

namespace ZsjTest.Application.UserLogin;

[ScopedDepency]
public interface IValidateLoginSvc
{
    Task<TokenResponse> ValidateUserAsync(LoginInfoDto loginInfoDto);

    Task<TokenResponse> RefreshUserTokenAsync(string refreshToken);

    Task RemoveTokenAsync(string userId);

    int TestCacheInterceptor(string sss);
}
