/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: ETicket DB 基类
** Ver : V1.0.0
**********************************************************************/

using Microsoft.EntityFrameworkCore;
using ZsjTest.Domain.ETicketMgr;
using ZsjTest.Infrastructure;

namespace ZsjTest.Domain.Repository.ETicketDomain;

[ScopedDepency]
public abstract class ETicketDbContext : DbContext
{
    public ETicketDbContext(ETicketDbContextOptionsMgr mgr) : base(mgr.Options)
    {
    }

    public ETicketDbContext(DbContextOptions<ETicketDbContext> options) : base(options)
    {
    }

    public DbSet<ETicket> ETickets { get; set; } = null!;
}

