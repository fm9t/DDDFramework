/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: 数据库连接基类
** Ver : V1.0.0
***********************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ZsjTest.Domain.Repository;

public abstract class DbContextOptionMgr<T>(IConfiguration config) where T : DbContext
{
    protected virtual string ConnectionString
    {
        get
        {
            string connectionName = config!.GetSection(
                $"ConnectionName:{typeof(T).FullName}").Get<string>()!;
            return config!.GetConnectionString(connectionName)!;
        }
    }

    public abstract DbContextOptions<T> Options { get; }    
}
