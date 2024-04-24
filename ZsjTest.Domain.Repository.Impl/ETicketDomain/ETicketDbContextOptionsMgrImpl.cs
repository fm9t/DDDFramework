/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: ETicket DB连接相关信息
** Ver : V1.0.0
**********************************************************************/

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ZsjTest.Domain.Repository.ETicketDomain;

namespace ZsjTest.Domain.Repository.Impl.ETicketDomain;
public class ETicketDbContextOptionsMgrImpl(IConfiguration config, ILoggerFactory loggerFactory) : ETicketDbContextOptionsMgr(config)
{
    public override DbContextOptions<ETicketDbContext> Options
    {
        get
        {
            return new DbContextOptionsBuilder<ETicketDbContext>()
                .UseLoggerFactory(loggerFactory)
                .UseOracle(ConnectionString,
                b => b.UseOracleSQLCompatibility("11")).Options;
        }
    }
}
