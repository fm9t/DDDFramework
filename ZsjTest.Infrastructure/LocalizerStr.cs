/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 08:57
** desc: 资源字符串键值类，会以对应键值在Resource中查找对应语言的字符串，
         如果未找到，直接返回该键值，因此最好使用默认语言来表示键值
** Ver : V1.0.0
************************************************************************/

namespace ZsjTest.Infrastructure;

public class LocalizerStr
{
    public static readonly string TestLocalizerStr = "测试资源文件";

    public static readonly string InvalidAuthrization = "认证无效";

    public static readonly string NotFound = "未找到对应地址";

    public static readonly string RequestError = "请求出错";

    public static readonly string InvalidRequest = "无效请求";

    public static readonly string UnknownError = "未知错误";

    public static readonly string ArgumentsError = "参数错误";

    public static readonly string UserNameAndPassordError = "用户名密码不正确";

    public static readonly string TimeOut = "运行超时";
    public static readonly string SortFieldInvalid = "排序列无效";
    public static readonly string AppClientInvalid = "APP无效";
    public static readonly string ApiCallInvalid = "Api非法调用";
}
