/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 11:03
** desc: AutoMapper配置文件
** Ver : V1.0.0
**********************************************************************/

using AutoMapper;
using ZsjTest.Application.ETicketMgr;
using ZsjTest.Application.UserLogin;
using ZsjTest.Domain.ETicketMgr;
using ZsjTest.Domain.UserMgr;

namespace ZsjTest.Application.Impl;

public class AutoMappProfile : Profile
{
    public AutoMappProfile()
    {
        CreateMap<JwtToken, TokenResponse>().ForMember(c => c.UserInfo, opt => opt.Ignore());
        CreateMap<UserInfo, UserInfoDto>();
        CreateMap<ETicket, ETicketDto>();
        CreateMap<ETicketDto, ETicket>();
        CreateMap<ETicketQueryParamDto, ETicketQueryParam>();
    }
}
