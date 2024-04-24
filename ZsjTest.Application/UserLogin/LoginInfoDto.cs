/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 09:47
** desc: 用户登录信息DTO
** Ver : V1.0.0
**********************************************************************/

using ZsjTest.Application.Utils;

namespace ZsjTest.Application.UserLogin;

public class LoginInfoDto : CanUrlDecodeDto
{
    public string UserName { get; set;} = string.Empty;
    public string Password { get; set;} = string.Empty;

    public override string ToString()
    {
        return $"UserName:{UserName}, Password:{Password}";
    }
}
