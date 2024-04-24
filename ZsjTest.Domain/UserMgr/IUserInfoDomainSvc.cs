/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:02
** desc: 用户信息领域服务接口
** Ver : V1.0.0
**********************************************************************/

using ZsjTest.Infrastructure;

namespace ZsjTest.Domain.UserMgr;

[ScopedDepency]
public interface IUserInfoDomainSvc
{
    Task<UserInfo?> GetUserInfoAsync(string loginName, string password);

    Task SaveRefreshTokenAsync(UserInfo userInfo, string refreshToken);

    Task<UserInfo> GetUserInfoByRefreshTokenAsync(string refreshToken);

    Task RemoveRefreshTokenAsync(int userId);
}
