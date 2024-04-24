/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-13 14:11
** desc: ETicket领域服务单元测试
** Ver : V1.0.0
*************************************************************************/

using ZsjTest.Domain.Repository.ETicketDomain;
using ZsjTest.Domain.ETicketMgr;
using ZsjTest.UnitTest.ETicketMgr;

namespace ZsjTest.DomainSvc.Impl.ETicketMgr.Tests;

[TestClass()]
public class ETicketDomainSvcTests
{    
    private readonly ETicketDomainSvc _service;
    private readonly ETicketDbContext eTicketDbContext;
    public ETicketDomainSvcTests()
    {        
        eTicketDbContext = new ETicketDbContextInMemory();
        _service = new ETicketDomainSvc(eTicketDbContext);
    }

    [TestMethod()]
    public async Task GetETicketsAsyncTest()
    {
        var result = await _service.GetETicketsAsync(new ETicketQueryParam
        {
            CreatedDateFrom = new DateTime(2023, 1, 1),
            CreateDateTimeTo = new DateTime(2023, 1, 20),
        });
        Assert.AreEqual(1, result.Count());
    }

    [TestMethod()]
    public async Task GetETicketByKeyTest()
    {
        Assert.AreEqual(1, (await _service.GetETicketByKeyAsync(1))?.TicketId);
    }

    [TestMethod()]
    public async Task GetETicketsCountAsyncTest()
    {
        var cnt = await _service.GetETicketsCountAsync(new ETicketQueryParam
        {
            CreatedDateFrom = new DateTime(2023, 1, 1),
            CreateDateTimeTo = new DateTime(2023, 1, 20)
        });
        Assert.AreEqual(1, cnt);
    }

    [TestMethod()]
    public async Task SaveETicketAsyncInsertTest()
    {
        var etk = new ETicket
        {
            TicketId = 0,
            TicketSubject = "Test Subject",
            CreateDate = new DateTime(2023, 3, 10, 15, 10, 10),
            LastUpdateDate = new DateTime(2023, 5, 13, 9, 8, 8),
            ApplicationName = "def",
            ModuleName = "abcd",
            Status = 1,
            Solution = "解决方案描述",
            Description = "问题描述如下。",
            TypeName = "Change Request",
            CreateBy = 123,
            LastUpdateBy = 456,
        };
        await _service.SaveETicketAsync(etk);
        Assert.AreEqual(2, (await _service.GetETicketByKeyAsync(2))?.TicketId);
    }

    [TestMethod()]
    public async Task SaveETicketAsyncUpdateTest()
    {
        var etk = new ETicket
        {
            TicketId = 1,
            TicketSubject = "Test Subject222",
            CreateDate = new DateTime(2023, 3, 10, 15, 10, 10),
            LastUpdateDate = new DateTime(2023, 5, 13, 9, 8, 8),
            ApplicationName = "def",
            ModuleName = "abcd",
            Status = 1,
            Solution = "解决方案描述",
            Description = "问题描述如下。",
            TypeName = "Change Request",
            CreateBy = 123,
            LastUpdateBy = 456,
        };
        await _service.SaveETicketAsync(etk);
        Assert.AreEqual("Test Subject222",
            (await _service.GetETicketByKeyAsync(1))?.TicketSubject);
    }
}
