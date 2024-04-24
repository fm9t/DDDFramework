/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: 生成数据库脚本
** Ver : V1.0.0
***********************************************************************/
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ZsjTest.Domain.Repository.Impl.SQLite.UserDomain;
using ZsjTest.Domain.Repository.UserDomain;

namespace ZsjTest.Domain.Repository.Impl.SQLite.DbDesign;

internal class UserInfoDbFactory : IDesignTimeDbContextFactory<UserInfoDbContextImpl>
{
    // 最后的--加空格之后的内容会转发为args, 因此可以将连接串作为参数传入
    // dotnet ef migrations add userinfodb01 --context UserInfoDbContextImpl --output-dir DbMigrations/userinfodb -- "data source=test.db"
    // dotnet ef migrations script --context UserInfoDbContextImpl --output DbMigrations/sql/userinfodb01.sql -- "data source=test.db"
    public UserInfoDbContextImpl CreateDbContext(string[] args)
    {        
        var optionsBuilder = new DbContextOptionsBuilder<UserInfoDbContext>();
        optionsBuilder.UseSqlite(args[0]);
        return new UserInfoDbContextImpl(optionsBuilder.Options);
    }
}
