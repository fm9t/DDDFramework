/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 09:19
** desc: 依赖注入的相关标识
** Ver : V1.0.0
********************************************************************/

namespace ZsjTest.WebApi.Common;

public class DepencyInfo
{
    public const string ScopedLifeTime = "Scoped";
    public const string SingletonLifeTime = "Singleton";
    public const string TransientLifeTime = "Transient";

    public const string PositionInSetting = "DepencyInject";

    public string AbstractInfo { get; set; } = "";

    public string ImplementaionInfo { get; set; } = "";

    public string LifeTime { get; set; } = "";
}
