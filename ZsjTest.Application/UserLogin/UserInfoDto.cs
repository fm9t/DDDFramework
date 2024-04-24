/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-24 09:37
** desc: 用户信息DTO
** Ver : V1.0.0
**********************************************************************/

namespace ZsjTest.Application.UserLogin;

public class UserInfoDto
{
    public int UserId { get; set; }

    public string? UserNumber { get; set; }

    public string? EngLastName { get; set; }

    public string? EngMidName { get; set; }

    public string? EngFirstName { get; set; }

    public string? NativeName { get; set; }

    public string? Email { get; set; }

}
