/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: ETicket DB 连接信息基类
** Ver : V1.0.0
**********************************************************************/

using Microsoft.Extensions.Configuration;
using ZsjTest.Infrastructure;

namespace ZsjTest.Domain.Repository.ETicketDomain;

[SingletonDepency]
public abstract class ETicketDbContextOptionsMgr(IConfiguration config)
    : DbContextOptionMgr<ETicketDbContext>(config)
{
}

