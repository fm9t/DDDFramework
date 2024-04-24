/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: UserInfo DB
** Ver : V1.0.0
***********************************************************************/

using Microsoft.EntityFrameworkCore;
using ZsjTest.Domain.Repository.Impl.UserDomain.DbConfiguraton;
using ZsjTest.Domain.Repository.UserDomain;

namespace ZsjTest.Domain.Repository.Impl.UserDomain;

public class UserInfoDbContextImpl(UserInfoDbContextOptionsMgr mgr) : UserInfoDbContext(mgr)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserInfoConfiguration());
    }
}
