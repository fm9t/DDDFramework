/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-13 14:11
** desc: 方便测试使用的内存数据库ETicketDbContext
** Ver : V1.0.0
*************************************************************************/

using Microsoft.EntityFrameworkCore;
using ZsjTest.Domain.ETicketMgr;
using ZsjTest.Domain.Repository.ETicketDomain;

namespace ZsjTest.UnitTest.ETicketMgr;
public class ETicketDbContextInMemory : ETicketDbContext
{
    public ETicketDbContextInMemory() : base(
        new DbContextOptionsBuilder<ETicketDbContext>().UseInMemoryDatabase("TestDataBase").Options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ETicket>().HasKey(c => c.TicketId);
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
