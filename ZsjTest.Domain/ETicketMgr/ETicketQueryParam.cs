/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 13:59
** desc: ETicket查询参数类
** Ver : V1.0.0
***********************************************************************/

namespace ZsjTest.Domain.ETicketMgr;

public class ETicketQueryParam
{
    public int TicketId { get; set; }
    public string? TicketSubject { get; set; }
    public DateTime? CreatedDateFrom { get; set; }
    public DateTime? CreateDateTimeTo { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
