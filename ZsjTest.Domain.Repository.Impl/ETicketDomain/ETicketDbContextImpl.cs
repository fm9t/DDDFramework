/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: ETicket DB
** Ver : V1.0.0
**********************************************************************/

using Microsoft.EntityFrameworkCore;
using ZsjTest.Domain.Repository.ETicketDomain;
using ZsjTest.Domain.Repository.Impl.ETicketDomain.DbConfiguration;

namespace ZsjTest.Domain.Repository.Impl.ETicketDomain;

public class ETicketDbContextImpl(ETicketDbContextOptionsMgr mgr) : ETicketDbContext(mgr)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ETicketConfiguration());
    }
}
