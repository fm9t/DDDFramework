/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:07
** desc: 用户信息领域服务实现
** Ver : V1.0.0
***********************************************************************/

using Microsoft.EntityFrameworkCore;
using ZsjTest.Domain.Repository.UserDomain;
using ZsjTest.Domain.UserMgr;
using ZsjTest.Infrastructure;

namespace ZsjTest.DomainSvc.Impl.UserMgr;

[NeedIntercept]
[AllowLogInterceptor]
public class UserInfoDomainSvc(
    UserInfoDbContext userInfoDbContext)
    : IUserInfoDomainSvc
{
    /// <summary>
    /// 此方法仅供演示refresh token作用，实际使用时需要考虑refresh token过期时间等信息
    /// </summary>
    /// <param name="refreshToken">Refresh Token</param>
    /// <returns>UserInfo</returns>
    public async Task<UserInfo> GetUserInfoByRefreshTokenAsync(string refreshToken)
    {
        var userInfo = await userInfoDbContext.UserInfos.FirstOrDefaultAsync(
            c => c.PasswordQuestion == refreshToken) ?? throw new Exception(LocalizerStr.InvalidAuthrization);
        return userInfo;
    }

    public async Task<UserInfo?> GetUserInfoAsync(string loginName, string password)
    {
        byte[] pwdMd5 = PubTools.Md5Bytes(password);
        return await userInfoDbContext.UserInfos.FirstOrDefaultAsync(
            c => !string.IsNullOrEmpty(c.LoginName) &&
            c.LoginName.ToUpper() == loginName.ToUpper()
            && c.Password == pwdMd5);
    }

    public async Task SaveRefreshTokenAsync(UserInfo userInfo, string refreshToken)
    {
        userInfo.PasswordQuestion = refreshToken;
        if (userInfoDbContext.Entry(userInfo).State == EntityState.Detached)
        {
            userInfoDbContext.Entry(userInfo).State = EntityState.Modified;
        }
        await userInfoDbContext.SaveChangesAsync();
    }

    public async Task RemoveRefreshTokenAsync(int userId)
    {
        var userInfo = await userInfoDbContext.UserInfos.FindAsync(userId)
            ?? throw new Exception(LocalizerStr.InvalidAuthrization);
        userInfo.PasswordQuestion = null;
        await userInfoDbContext.SaveChangesAsync();
    }
}
