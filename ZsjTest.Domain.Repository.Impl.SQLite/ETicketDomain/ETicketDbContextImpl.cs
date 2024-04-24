/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: ETicket DB
** Ver : V1.0.0
********************************************************************/

using Microsoft.EntityFrameworkCore;
using System.Text;
using ZsjTest.Domain.ETicketMgr;
using ZsjTest.Domain.Repository.ETicketDomain;
using ZsjTest.Domain.Repository.Impl.SQLite.ETicketDomain.DbConfiguration;

namespace ZsjTest.Domain.Repository.Impl.SQLite.ETicketDomain;

public class ETicketDbContextImpl: ETicketDbContext
{
    public ETicketDbContextImpl(ETicketDbContextOptionsMgr mgr) : base(mgr)
    {
    }

    public ETicketDbContextImpl(DbContextOptions<ETicketDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ETicketConfiguration());

        //初始数据
        modelBuilder.Entity<ETicket>().HasData(
            new ETicket
            {
                TicketId = 1,
                TicketSubject = "Test Subject",
                CreateDate = new DateTime(2023, 1, 10, 15, 10, 10),
                LastUpdateDate = new DateTime(2023, 1, 13, 9, 8, 8),
                ApplicationName = "def",
                ModuleName = "abcd",
                Status = 1,
                Solution = "解决方案描述",
                Description = "问题描述如下。",
                TypeName = "Change Request",
                CreateBy = 123,
                LastUpdateBy = 456,
            }
        );
    }
}
