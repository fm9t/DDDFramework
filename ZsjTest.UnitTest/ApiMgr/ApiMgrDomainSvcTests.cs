/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-13 14:11
** desc: ApiMgr领域服务单元测试
** Ver : V1.0.0
*************************************************************************/

using Microsoft.Extensions.Logging;
using Moq;
using ZsjTest.Infrastructure;

namespace ZsjTest.DomainSvc.Impl.ApiMgr.Tests;

[TestClass()]
public class ApiMgrDomainSvcTests
{
    private readonly ApiMgrDomainSvc _domainSvc;
    public ApiMgrDomainSvcTests()
    {
        Mock<ICacheStore> mockCacheStore = new();
        Mock<ILogger<ApiMgrDomainSvc>> mockLogger = new();
        _domainSvc = new ApiMgrDomainSvc(mockCacheStore.Object, mockLogger.Object);
    }

    [TestMethod()]
    public async Task GetApiClientInfoAsyncSuccessTest()
    {
        var apiClennt = await _domainSvc.GetApiClientInfoAsync("ZSJTest1");
        Assert.AreEqual("ZSJTest1", apiClennt.AppId);
    }

    [TestMethod()]
    public async Task GetApiClientInfoAsyncFailedTest()
    {
        await Assert.ThrowsExceptionAsync<Exception>(() => _domainSvc.GetApiClientInfoAsync("XXXXX"));
    }
}
