/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: UserInfo DB 连接信息基类
** Ver : V1.0.0
***********************************************************************/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ZsjTest.Infrastructure;

namespace ZsjTest.Domain.Repository.UserDomain;

[SingletonDepency]
public abstract class UserInfoDbContextOptionsMgr(IConfiguration config)
    : DbContextOptionMgr<UserInfoDbContext>(config)
{
}

