/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-04-10 22:45
** desc: 反向代理中间件
** Ver : V1.0.0
***********************************************************************/

using Microsoft.AspNetCore.Http.Extensions;
using ZsjTest.ReverseProxy.Models;

namespace ZsjTest.ReverseProxy.Middleware;

public static class ReverseProxyExtensions
{
    public static IApplicationBuilder UseReverseProxy(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ReverseProxyMiddleware>();
    }
}

public class ReverseProxyMiddleware(RequestDelegate next)
{
    public async Task Invoke(
        HttpContext context,
        IHttpClientFactory httpClientFactory,
        AppSettings appSettings,
        ILogger<ReverseProxyMiddleware> logger)
    {
        var targetUri = BuildTargetUri(context.Request, appSettings, logger);
        //string message = $"origin url:{context.Request.GetDisplayUrl()}, target url: {targetUri?.ToString()}";
        logger.LogDebug("origin url: {originUrl}, target Url: {targetUrl}",
            context.Request.GetDisplayUrl(), targetUri?.ToString());
        if (targetUri != null)
        {
            var targetRequest = CreateTargetRequest(context, targetUri);
            var httpClient = httpClientFactory.CreateClient();
            using var responseMessage = await httpClient.SendAsync(targetRequest, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted);
            context.Response.StatusCode = (int)responseMessage.StatusCode;
            CopyFromTargetResponseHeaders(context, responseMessage);
            await responseMessage.Content.CopyToAsync(context.Response.Body);
            return;
        }
        await next(context);
    }

    private static HttpRequestMessage CreateTargetRequest(HttpContext context, Uri targetUri)
    {
        var newRequest = new HttpRequestMessage();
        CopyFromOriginalRequestContentAndHeaders(context, newRequest);

        newRequest.RequestUri = targetUri;
        newRequest.Headers.Host = targetUri.Host;
        newRequest.Method = GetMethod(context.Request.Method);

        return newRequest;
    }

    private static void CopyFromOriginalRequestContentAndHeaders(HttpContext context, HttpRequestMessage requestMessage)
    {
        if (context.Request.Body != null)
        {
            var streamContent = new StreamContent(context.Request.Body);
            requestMessage.Content = streamContent;
        }

        foreach (var header in context.Request.Headers)
        {
            if (requestMessage.Content != null)
            {
                bool result = requestMessage.Content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                if (!result)
                {
                    requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                }
            }
            else
            {
                requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }
        }
    }

    private static void CopyFromTargetResponseHeaders(HttpContext context, HttpResponseMessage responseMessage)
    {
        foreach (var header in responseMessage.Headers)
        {
            context.Response.Headers[header.Key] = header.Value.ToArray();
        }

        foreach (var header in responseMessage.Content.Headers)
        {
            context.Response.Headers[header.Key] = header.Value.ToArray();
        }
        context.Response.Headers.Remove("transfer-encoding");
    }
    private static HttpMethod GetMethod(string method)
    {
        if (HttpMethods.IsDelete(method)) return HttpMethod.Delete;
        if (HttpMethods.IsGet(method)) return HttpMethod.Get;
        if (HttpMethods.IsHead(method)) return HttpMethod.Head;
        if (HttpMethods.IsOptions(method)) return HttpMethod.Options;
        if (HttpMethods.IsPost(method)) return HttpMethod.Post;
        if (HttpMethods.IsPut(method)) return HttpMethod.Put;
        if (HttpMethods.IsTrace(method)) return HttpMethod.Trace;
        return new HttpMethod(method);
    }

    private static Uri? BuildTargetUri(
        HttpRequest request, AppSettings appSettings, ILogger<ReverseProxyMiddleware> logger)
    {
        Uri? targetUri = null;

        if (request.Path.StartsWithSegments(appSettings.UrlPrefix, out var remainingPath))
        {
            if (request.Query.Keys.Contains(Utils.AppidName,
                StringComparer.OrdinalIgnoreCase))
            {
                string appId = request.Query[Utils.AppidName]!;
                if (appSettings.AppInfo != null)
                {
                    var appInfo = appSettings.AppInfo.FirstOrDefault(c => c.AppId.ToLower() == appId.ToLower());
                    if (appInfo != null && !string.IsNullOrWhiteSpace(appInfo.AppSecret))
                    {
                        long ts = Utils.GetUnixTimeStamp();
                        string nonce = Utils.GetRandomStr(8);
                        List<string> forSignStrList = [];
                        foreach (var q in request.Query)
                        {
                            if (!string.IsNullOrWhiteSpace(q.Value))
                            {
                                forSignStrList.Add(q.Key + q.Value);
                            }
                        }
                        forSignStrList.Add(Utils.TsName + ts.ToString());
                        forSignStrList.Add(Utils.NonceName + nonce);
                        forSignStrList.Sort();
                        forSignStrList.Add(Utils.SecretName + appInfo.AppSecret);
                        string signStr = Utils.Hash256(string.Join(string.Empty, forSignStrList));
                        logger.LogDebug("forSignStr:{forSignStr}, signStr:{signStr}", string.Join(string.Empty, forSignStrList), signStr);
                        targetUri = new Uri(appSettings.TargetUrl + remainingPath + request.QueryString
                            + $"&{Utils.TsName}={ts}&{Utils.NonceName}={nonce}&{Utils.SignStrName}={signStr}");
                        return targetUri;
                    }
                }
            }
            targetUri = new Uri(appSettings.TargetUrl + remainingPath + request.QueryString);
        }

        return targetUri;
    }
}
