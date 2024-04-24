/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-10 14:45
** desc: Api管理
** Ver : V1.0.0
***********************************************************************/

namespace ZsjTest.Domain.ApIMgr;

public class ApiClientInfo
{
    public string AppId { get; set; } = string.Empty;
    public string AppSecret { get; set; } = string.Empty;
    public bool NeedCheckTs { get; set; }
    public bool NeedCheckNonce { get; set; }
    public bool NeedCheckSignStr { get; set; }
    public int TsWindow { get; set; }
    public bool IsActive { get; set; }

    
}
