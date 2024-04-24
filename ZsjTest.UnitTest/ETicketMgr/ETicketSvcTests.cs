/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-13 14:11
** desc: ETicket应用层服务单元测试
** Ver : V1.0.0
*************************************************************************/

using AutoMapper;
using Microsoft.Extensions.Localization;
using Moq;
using ZsjTest.DomainSvc.Impl.ETicketMgr;
using ZsjTest.UnitTest.ETicketMgr;

namespace ZsjTest.Application.Impl.ETicketMgr.Tests;

[TestClass()]
public class ETicketSvcTests
{
    private readonly ETicketSvc _eTicketSvc;
    public ETicketSvcTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMappProfile>();
        });
        var mapper = config.CreateMapper();
        Mock<IStringLocalizer> mockLocalizer = new();
        ETicketDomainSvc eTicketDomainSvc = new(new ETicketDbContextInMemory());
        _eTicketSvc = new ETicketSvc(mapper, mockLocalizer.Object, eTicketDomainSvc);
    }

    [TestMethod()]
    public async Task GetETicketDtosAsyncTest()
    {
        var result = await _eTicketSvc.GetETicketDtosAsync(new Application.ETicketMgr.ETicketQueryParamDto
        {
            CreatedDateFrom = new DateTime(2023, 1, 1),
            CreateDateTimeTo = new DateTime(2023, 1, 30),
            SortFields = ["Id", "ApplicationName"],
            SortDirs = ["ASC","DESC"],
        });
        Assert.AreEqual(1, result.Count);
    }
}
