/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-13 12:29
** desc: Api管理应用服务接口
** Ver : V1.0.0
************************************************************************/

using ZsjTest.Infrastructure;

namespace ZsjTest.Application.ApiMgr;

[ScopedDepency]
public interface IApiMgrSvc
{
    Task<bool> CheckUrlValidAsync(string url);
}
