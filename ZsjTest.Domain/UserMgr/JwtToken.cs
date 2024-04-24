/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 11:11
** desc: Jwt Token类
** Ver : V1.0.0
**********************************************************************/

using ZsjTest.Infrastructure;

namespace ZsjTest.Domain.UserMgr;

public class JwtToken
{
    public string Access_token { get; set; } = string.Empty;
    public int Expires_in { get; set; }
    public string Token_type { get; set; } = PubConst.DefaultTokenType;
}
