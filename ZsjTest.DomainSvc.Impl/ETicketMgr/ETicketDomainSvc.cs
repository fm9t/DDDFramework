/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 14:11
** desc: ETicket领域服务实现
** Ver : V1.0.0
*************************************************************************/

using Microsoft.EntityFrameworkCore;
using ZsjTest.Domain.ETicketMgr;
using ZsjTest.Domain.Repository.ETicketDomain;
using ZsjTest.Infrastructure;

namespace ZsjTest.DomainSvc.Impl.ETicketMgr;

[NeedIntercept]
[AllowLogInterceptor]
public class ETicketDomainSvc(ETicketDbContext dbContext)
    : IETicketDomainSvc
{
    public async Task<ETicket?> GetETicketByKeyAsync(int ticketId)
    {
        return await dbContext.ETickets.FindAsync(ticketId);
    }

    public async Task<List<ETicket>> GetETicketsAsync(ETicketQueryParam queryParam,
        params SortPredicate<ETicket>[] sortPredicates)
    {
        var q = BuildCommonQuery(queryParam);
        q = Utils.HelperTool.AddSortAndPage(
            q, sortPredicates, queryParam.PageSize, queryParam.PageIndex);
        return await q.ToListAsync();
    }

    public async Task<int> GetETicketsCountAsync(ETicketQueryParam queryParam)
    {
        return await BuildCommonQuery(queryParam).CountAsync();
    }

    public async Task SaveETicketAsync(ETicket eTicket)
    {
        if (eTicket.TicketId == 0)
        {
            dbContext.ETickets.Add(eTicket);
        }
        else
        {
            if (dbContext.Entry(eTicket).State == EntityState.Detached)
            {
                dbContext.Entry(eTicket).State = EntityState.Modified;
            }
            // dbContext.ETickets.Update(eTicket);
        }
        await dbContext.SaveChangesAsync();
    }

    private IQueryable<ETicket> BuildCommonQuery(ETicketQueryParam queryParam)
    {
        var q = dbContext.ETickets.AsNoTracking();
        if (queryParam.TicketId != 0)
        {
            q.Where(c => c.TicketId == queryParam.TicketId);
        }
        if (!string.IsNullOrWhiteSpace(queryParam.TicketSubject))
        {
            q = q.Where(c => c.TicketSubject != null && c.TicketSubject.ToUpper().Contains(queryParam.TicketSubject.ToUpper()));
        }
        if (queryParam.CreatedDateFrom.HasValue)
        {
            q = q.Where(c => c.CreateDate >= queryParam.CreatedDateFrom.Value);
        }
        if (queryParam.CreateDateTimeTo.HasValue)
        {
            q = q.Where(c => c.CreateDate <= queryParam.CreateDateTimeTo.Value);
        }
        return q;
    }
}
