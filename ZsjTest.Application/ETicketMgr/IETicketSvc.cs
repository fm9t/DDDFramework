/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 14:29
** desc: ETicket应用服务接口
** Ver : V1.0.0
************************************************************************/

using ZsjTest.Infrastructure;

namespace ZsjTest.Application.ETicketMgr;

[ScopedDepency]
public interface IETicketSvc
{
    Task<ETicketDto> GetETicketByIdAsync(int ticketId);
    Task<List<ETicketDto>> GetETicketDtosAsync(ETicketQueryParamDto queryParam);
    Task<int> GetETicketDtosCountAsync(ETicketQueryParamDto queryParam);
    Task SaveETicketAsync(ETicketDto eTicketDto);
}
