/*******************************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-21 22:17
** desc: AppSettings.json中的一些配置的映射类
** Ver : V1.0.0
********************************************************************/

namespace ZsjTest.Infrastructure;

public class AppSettings
{
    public static readonly string Position = "AppSettings";
    public int CacheSizeLimit { get; set; }
    public int CacheDefaultExpireSec { get; set; }
    public string DateFormate { get; set; } = string.Empty;
    public long MaxRequestBodySize { get; set; }
    public int ProcessTimeout { get; set; }
    public string SigningCredential { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Aud { get; set; } = string.Empty;
    public int ClientTokenLifeTime { get; set; }
    public List<string> AllowCrosUrls { get; set; } = default!;
    public string AutoDIPrefix { get; set; } = string.Empty;
    public string DiConfigFiles { get; set; } = string.Empty;
    public string SerilogDestructureName { get; set; } = string.Empty;
    public bool NeedCheckApiSign { get; set; }
}
