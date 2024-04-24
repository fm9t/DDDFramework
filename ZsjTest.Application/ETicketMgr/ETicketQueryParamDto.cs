/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 14:22
** desc: ETicket查询dto参数
** Ver : V1.0.0
**********************************************************************/

using Microsoft.VisualBasic.FileIO;
using ZsjTest.Application.Utils;

namespace ZsjTest.Application.ETicketMgr;

public class ETicketQueryParamDto : QueryParam
{
    public int TicketId { get; set; }
    public string? TicketSubject { get; set; }
    public DateTime? CreatedDateFrom { get; set; }
    public DateTime? CreateDateTimeTo { get; set; }

    protected override Dictionary<string, string> SpecialSortFieldMaps
    {
        get
        {
            var dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "id", "TicketId" }
            };
            return dic;
        }
    }
}
