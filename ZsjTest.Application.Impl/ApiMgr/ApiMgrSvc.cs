/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-13 12:29
** desc: Api管理应用服务实现
** Ver : V1.0.0
************************************************************************/

using ZsjTest.Application.ApiMgr;
using ZsjTest.Domain.ApIMgr;

namespace ZsjTest.Application.Impl.ApiMgr;

public class ApiMgrSvc(IApiMgrDomainSvc apiMgrDomainSvc) : IApiMgrSvc
{
    public async Task<bool> CheckUrlValidAsync(string url)
    {
        return await apiMgrDomainSvc.IsApiCallUrlValidAsync(url);
    }
}
