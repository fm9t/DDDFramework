/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: userinfo对应的数据表配置信息
** Ver : V1.0.0
**********************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZsjTest.Domain.UserMgr;

namespace ZsjTest.Domain.Repository.Impl.SQLite.UserDomain.DbConfiguraton;
internal class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.HasKey(e => e.UserId);
    }
}
