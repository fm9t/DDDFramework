/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-10 14:45
** desc: Api管理领域服务接口
** Ver : V1.0.0
**********************************************************************/

using ZsjTest.Infrastructure;

namespace ZsjTest.Domain.ApIMgr;

[ScopedDepency]
public interface IApiMgrDomainSvc
{
    Task<ApiClientInfo> GetApiClientInfoAsync(string appId);
    Task<bool> IsApiCallUrlValidAsync(string url);
}
