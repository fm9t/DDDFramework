/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 08:59
** desc: 常量定义
** Ver : V1.0.0
**********************************************************************/

namespace ZsjTest.Infrastructure;
public class PubConst
{
    public static readonly string OKResponseMessage = "OK";
    public static readonly string PasswordGrantType = "password";
    public static readonly string ClientIdKey = "client_id";
    public static readonly string ScopeKey = "scope";
    public static readonly string AuthorizationKey = "Authorization";
    public static readonly char JwtSplitChar = '.';
    public static readonly string JwtClientNameKey = "ClientName";
    public static readonly string JwtUserNameKey = "Username";
    public static readonly string JtiClaimName = "jti";
    public const string DataSyncApiName = "DataSync";
    public const string ApplicationApiName = "APP";
    public const string AdminApiName = "Admin";
    public static readonly string DefaultTokenType = "Bearer";
    public static readonly string JwtTokenSecurityKeyCacheName =
        "JwtTokenSecurityKeyCacheName";
    public static readonly string TsName = "ts";
    public static readonly string SecretName = "secret";
    public static readonly string AppidName = "appid";
    public static readonly string NonceName = "nonce";
    public static readonly string SignStrName = "signStr";
    public static readonly string IEventHandlerInterfaceName = "IEventHandler`1";
}
