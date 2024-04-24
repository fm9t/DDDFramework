/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 13:59
** desc: ETicket领域服务接口
** Ver : V1.0.0
**********************************************************************/

using ZsjTest.Infrastructure;

namespace ZsjTest.Domain.ETicketMgr;

[ScopedDepency]
public interface IETicketDomainSvc
{
    public Task<ETicket?> GetETicketByKeyAsync(int ticketId);

    Task<List<ETicket>> GetETicketsAsync(ETicketQueryParam queryParam,
        params SortPredicate<ETicket>[] sortPredicates);
    Task<int> GetETicketsCountAsync(ETicketQueryParam queryParam);

    public Task SaveETicketAsync(ETicket eTicket);
}
