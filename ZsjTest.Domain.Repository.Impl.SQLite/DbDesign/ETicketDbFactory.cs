/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: 生成数据库脚本
** Ver : V1.0.0
*********************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ZsjTest.Domain.Repository.ETicketDomain;
using ZsjTest.Domain.Repository.Impl.SQLite.ETicketDomain;

namespace ZsjTest.Domain.Repository.Impl.SQLite.DbDesign;
internal class ETicketDbFactory : IDesignTimeDbContextFactory<ETicketDbContextImpl>
{
    // 最后的--加空格之后的内容会转发为args, 因此可以将连接串作为参数传入
    // dotnet ef migrations add eticketdb01 --context ETicketDbContextImpl --output-dir DbMigrations/eticketdb -- "data source=test.db"
    // dotnet ef migrations add eticketdb02 --context ETicketDbContextImpl --output-dir DbMigrations/eticketdb -- "data source=test.db"
    // dotnet ef migrations script --context ETicketDbContextImpl --output DbMigrations/sql/eticketdb01.sql -- "data source=test.db"
    // dotnet ef migrations script --context ETicketDbContextImpl eticketdb01 --output DbMigrations/sql/eticketdb02.sql -- "data source=test.db"
    public ETicketDbContextImpl CreateDbContext(string[] args)
    {        
        var optionsBuilder = new DbContextOptionsBuilder<ETicketDbContext>();
        optionsBuilder.UseSqlite(args[0]);
        return new ETicketDbContextImpl(optionsBuilder.Options);
    }
}
