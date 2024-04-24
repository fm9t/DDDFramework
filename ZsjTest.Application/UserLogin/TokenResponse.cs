/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 09:48
** desc: 用户登录成功后返回的Token信息
** Ver : V1.0.0
**********************************************************************/

using ZsjTest.Infrastructure;

namespace ZsjTest.Application.UserLogin;
public class TokenResponse
{
    public UserInfoDto? UserInfo { get; set; }
    public string Access_token { get; set; } = string.Empty;
    public int Expires_in { get; set; }
    public string Token_type { get; set; } = PubConst.DefaultTokenType;

    public string? Refresh_Token { get; set; }
}
