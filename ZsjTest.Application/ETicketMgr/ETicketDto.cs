/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 14:22
** desc: ETicketDTO
** Ver : V1.0.0
**********************************************************************/

namespace ZsjTest.Application.ETicketMgr;

public class ETicketDto
{
    public int TicketId { get; set; }

    public string? TicketSubject { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public string? ApplicationName { get; set; }

    public string? ModuleName { get; set; }

    public decimal? Status { get; set; }

    public string? Solution { get; set; }

    public string? Description { get; set; }

    public string? TypeName { get; set; }

    public int? CreateBy { get; set; }

    public int? LastUpdateBy { get; set; }

    public byte[] RowVersion { get; set; } = [];

}
