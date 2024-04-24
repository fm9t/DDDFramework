/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: UserInfo DB 连接相关信息
** Ver : V1.0.0
***********************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ZsjTest.Domain.Repository.UserDomain;

namespace ZsjTest.Domain.Repository.Impl.UserDomain;

public class UserInfoDbContextOptionsMgrImpl(IConfiguration config, ILoggerFactory loggerFactory) : UserInfoDbContextOptionsMgr(config)
{
    public override DbContextOptions<UserInfoDbContext> Options
    {
        get
        {
            return new DbContextOptionsBuilder<UserInfoDbContext>()
                .UseLoggerFactory(loggerFactory)
                .UseOracle(ConnectionString,
                b => b.UseOracleSQLCompatibility("11")).Options;
        }
    }
}
