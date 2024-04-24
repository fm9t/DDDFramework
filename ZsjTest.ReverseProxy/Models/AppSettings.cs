/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-10 22:45
** desc: AppSetting类
** Ver : V1.0.0
***********************************************************************/
namespace ZsjTest.ReverseProxy.Models;

public class AppSettings
{
    public static readonly string Position = "AppSettings";
    public List<string> AllowCrosUrls { get; set; } = default!;
    public string UrlPrefix { get; set; } = string.Empty;
    public string TargetUrl { get; set; } = string.Empty;
    public long MaxRequestBodySize { get; set; }
    public List<AppInfo> AppInfo { get; set; } = default!;
}
