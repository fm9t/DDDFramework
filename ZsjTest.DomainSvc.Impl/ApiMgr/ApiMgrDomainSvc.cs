/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-10 14:45
** desc: Api管理领域服务实现
** Ver : V1.0.0
***********************************************************************/

using Microsoft.Extensions.Logging;
using System.Collections.Specialized;
using System.Web;
using ZsjTest.Domain.ApIMgr;
using ZsjTest.Infrastructure;

namespace ZsjTest.DomainSvc.Impl.ApiMgr;

[NeedIntercept]
[AllowLogInterceptor]
public class ApiMgrDomainSvc(ICacheStore cacheStore, ILogger<ApiMgrDomainSvc> logger) : IApiMgrDomainSvc
{
    //测试使用
    private readonly List<ApiClientInfo> _apiClientInfos =
    [
        new ApiClientInfo{
            AppId = "ZSJTest1",
            AppSecret = "ZSJTest1AppSecret",
            NeedCheckTs = true,
            NeedCheckNonce = true,
            NeedCheckSignStr = true,
            IsActive = true,
            TsWindow = 10 * 1000,
            },
        new ApiClientInfo{
            AppId = "ZSJTest2",
            AppSecret = "ZSJTest2AppSecret",
            NeedCheckTs = false,
            NeedCheckNonce = false,
            NeedCheckSignStr = false,
            IsActive = true,
            TsWindow = 15 * 1000,
            },
    ];

    [AllowCacheInterceptor(CacheTime = 7200)]
    public async Task<ApiClientInfo> GetApiClientInfoAsync(string appId)
    {
       var apiClient =
                _apiClientInfos.FirstOrDefault(c => c.AppId.ToLower() == appId.ToLower() && c.IsActive) ??
                throw new Exception(LocalizerStr.AppClientInvalid);
        return await Task.FromResult(apiClient);
    }

    public async Task<bool> IsApiCallUrlValidAsync(string url)
    {
        var uri = new Uri(url);
        var queryStrings = HttpUtility.ParseQueryString(uri.Query) ??
            throw new Exception(LocalizerStr.AppClientInvalid);
        if (!IsExistQueryString(queryStrings, PubConst.AppidName))
        {
            logger.LogError("url: {url}, api invalid: {msg}", url, "不存在appid值");
            throw new Exception(LocalizerStr.AppClientInvalid);
        }

        var apiClient = await GetApiClientInfoAsync(queryStrings[PubConst.AppidName]!);
        if (CheckTs(apiClient, queryStrings) && CheckNonce(apiClient, queryStrings)
            &&CheckSignStr(apiClient, queryStrings))
        {
            return true;
        }
        return false;
    }

    private static bool IsExistQueryString(
            NameValueCollection queryStrings, string queryStringName)
    {
        if (queryStrings.AllKeys.Contains(
            queryStringName,
            StringComparer.OrdinalIgnoreCase))
        {
            return true;
        }
        return false;
    }

    private bool CheckTs(ApiClientInfo apiClient, NameValueCollection queryStrings)
    {
        if (apiClient.NeedCheckTs)
        {
            if (!IsExistQueryString(queryStrings, PubConst.TsName))
            {
                logger.LogError("api invalid: {msg}", "不存在ts值");
                return false;
            }
            if (long.TryParse(queryStrings[PubConst.TsName], out long ts))
            {
                if (Math.Abs(PubTools.GetUnixTimeStamp() - ts) > apiClient.TsWindow)
                {
                    logger.LogError("api invalid: {msg}", "ts值过期");
                    return false;
                }
            }
            else
            {
                logger.LogError("api invalid: {msg}", "ts值无效");
                return false;
            }

            logger.LogDebug("api check ts OK");
        }
        return true;
    }

    private bool CheckNonce(ApiClientInfo apiClient, NameValueCollection queryStrings)
    {
        if (apiClient.NeedCheckNonce)
        {
            if (!IsExistQueryString(queryStrings, PubConst.NonceName))
            {
                logger.LogError("api invalid: {msg}", "不存在nonce值");
                return false;
            }
            string nonce = queryStrings[PubConst.NonceName]!;
            if (cacheStore.Get<int?>($"{nameof(ApiMgrDomainSvc)}_{nameof(CheckNonce)}_{nonce}") != null)
            {
                logger.LogError("api invalid: {msg}", "nonce值已存在");
                return false;
            }
            cacheStore.Add(1, $"{nameof(ApiMgrDomainSvc)}_{nameof(CheckNonce)}_{nonce}", new TimeSpan(0, 0, apiClient.TsWindow));
            logger.LogDebug("api check nonce OK");
        }
        return true;
    }

    private bool CheckSignStr(ApiClientInfo apiClient, NameValueCollection queryStrings)
    {
        if (apiClient.NeedCheckSignStr)
        {
            if (!IsExistQueryString(queryStrings, PubConst.SignStrName))
            {
                logger.LogError("api invalid: {msg}", "不存在signStr值");
                return false;
            }
            string signStr = queryStrings[PubConst.SignStrName]!;
            string[] keys = queryStrings.AllKeys!;
            List<string> forSignStrList = [];
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToLower() != PubConst.SignStrName.ToLower() && !string.IsNullOrEmpty(queryStrings[keys[i]]))
                {
                    forSignStrList.Add(keys[i] + queryStrings[keys[i]]);
                }
            }
            forSignStrList.Sort();
            string forSignStr = string.Join(string.Empty, forSignStrList) + PubConst.SecretName + apiClient.AppSecret;

            logger.LogDebug("forSignStr: {input}", forSignStr);
            string tempSign = PubTools.Hash256(forSignStr);

            logger.LogDebug("singStr: {input}, {caculate}", signStr, tempSign);
            if (signStr.ToLower() != tempSign.ToLower())
            {
                return false;
            }
            logger.LogDebug("api check signStr OK");
        }
        return true;
    }
}
