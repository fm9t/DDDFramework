/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: UserInfo DB
** Ver : V1.0.0
**********************************************************************/

using Microsoft.EntityFrameworkCore;
using ZsjTest.Domain.Repository.Impl.SQLite.UserDomain.DbConfiguraton;
using ZsjTest.Domain.Repository.UserDomain;
using ZsjTest.Domain.UserMgr;
using ZsjTest.Infrastructure;

namespace ZsjTest.Domain.Repository.Impl.SQLite.UserDomain;

public class UserInfoDbContextImpl : UserInfoDbContext
{
    public UserInfoDbContextImpl(UserInfoDbContextOptionsMgr mgr) : base(mgr)
    {
    }

    public UserInfoDbContextImpl(DbContextOptions<UserInfoDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserInfoConfiguration());

        //初始数据
        modelBuilder.Entity<UserInfo>().HasData(
            new UserInfo
            {
                UserId = 1,
                NativeName = "张三",
                LoginName = "sjzhou",
                UserGuid = Guid.NewGuid().ToString("N"),
                Password = PubTools.Md5Bytes("welcome01"),
                CreateDate = new DateTime(2023, 1, 10, 15, 10, 10),
                LastUpdateDate = new DateTime(2023, 1, 13, 9, 8, 8),
                CreateBy = 123,
                LastUpdateBy = 456,
            }
        );
    }
}
