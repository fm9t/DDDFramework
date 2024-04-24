/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 14:29
** desc: ETicket应用服务实现
** Ver : V1.0.0
*********************************************************************/

using AutoMapper;
using Microsoft.Extensions.Localization;
using ZsjTest.Application.ETicketMgr;
using ZsjTest.Domain.ETicketMgr;
using ZsjTest.Infrastructure;

namespace ZsjTest.Application.Impl.ETicketMgr;

[NeedIntercept]
[AllowLogInterceptor]
public class ETicketSvc(
    IMapper mapper,
    IStringLocalizer localizer,
    IETicketDomainSvc eTicketDomainSvc) : IETicketSvc
{
    public async Task<ETicketDto> GetETicketByIdAsync(int ticketId)
    {
        var result = await eTicketDomainSvc.GetETicketByKeyAsync(ticketId)
            ?? throw new Exception(localizer[LocalizerStr.NotFound]);
        return mapper.Map<ETicketDto>(result);
    }

    public async Task<List<ETicketDto>> GetETicketDtosAsync(ETicketQueryParamDto queryParam)
    {
        var eticketQuery = mapper.Map<ETicketQueryParam>(queryParam);
        var result = await eTicketDomainSvc.GetETicketsAsync(eticketQuery,
            queryParam.BuildSortInfo<ETicket>());
        return mapper.Map<List<ETicketDto>>(result);
    }

    public async Task<int> GetETicketDtosCountAsync(ETicketQueryParamDto queryParam)
    {
        var eticketQuery = mapper.Map<ETicketQueryParam>(queryParam);
        return await eTicketDomainSvc.GetETicketsCountAsync(eticketQuery);
    }

    public async Task SaveETicketAsync(ETicketDto eTicketDto)
    {
        var eticket = mapper.Map<ETicket>(eTicketDto);
        await eTicketDomainSvc.SaveETicketAsync(eticket);
    }
}
