/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: UserInfo DB 基类
** Ver : V1.0.0
***********************************************************************/

using Microsoft.EntityFrameworkCore;
using ZsjTest.Domain.UserMgr;
using ZsjTest.Infrastructure;

namespace ZsjTest.Domain.Repository.UserDomain;

[ScopedDepency]
public abstract class UserInfoDbContext : DbContext
{
    public UserInfoDbContext(UserInfoDbContextOptionsMgr mgr) : base(mgr.Options)
    {
    }

    public UserInfoDbContext(DbContextOptions<UserInfoDbContext> options) : base(options)
    {
    }

    public DbSet<UserInfo> UserInfos { get; set; } = null!;
}
