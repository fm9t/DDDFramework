/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 13:59
** desc: ETicket领域模型
** Ver : V1.0.0
**********************************************************************/

namespace ZsjTest.Domain.ETicketMgr;

public class ETicket : IRowVersion
{
    public int TicketId { get; set; }

    public string? TicketSubject { get; set; }

    public decimal? CreatedBy { get; set; }

    public DateTime? CreateDate { get; set; }

    public decimal? RaisedBy { get; set; }

    public DateTime? RaiseDate { get; set; }

    public decimal? AssignTo { get; set; }

    public DateTime? AssignDate { get; set; }

    public decimal? BeginBy { get; set; }

    public DateTime? BeginDate { get; set; }

    public decimal? EndBy { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? DeliveredBy { get; set; }

    public DateTime? DeliverDate { get; set; }

    public decimal? CancelledBy { get; set; }

    public DateTime? CancelDate { get; set; }

    public decimal? ClosedBy { get; set; }

    public DateTime? CloseDate { get; set; }

    public decimal? LastUpdatedBy { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public string? ApplicationName { get; set; }

    public string? ModuleName { get; set; }

    public decimal? Status { get; set; }

    public decimal? Priority { get; set; }

    public decimal? Type { get; set; }

    public string? Solution { get; set; }

    public string? Description { get; set; }

    public string? RaisedDept { get; set; }

    public string? Cancelreason { get; set; }

    public decimal? DebuggedBy { get; set; }

    public DateTime? DebugDate { get; set; }

    public decimal? SatisfactoryLevel { get; set; }

    public string? SatisfactoryReason { get; set; }

    public decimal? PlanTime { get; set; }

    public decimal? FactTime { get; set; }

    public decimal? RequestTime { get; set; }

    public int? IssueType { get; set; }

    public decimal? ApprovedBy { get; set; }

    public DateTime? ApproveDate { get; set; }

    public byte? ResponsePriority { get; set; }

    public string? ExpectedTangibleBenefits { get; set; }

    public string? ExpectedIntangibleBenefits { get; set; }

    public string? ExpectedCostSaveings { get; set; }

    public string? ResponsePriorityText { get; set; }

    public string? TypeName { get; set; }

    public int? CreateBy { get; set; }

    public int? LastUpdateBy { get; set; }

    public string? Impactanalysis { get; set; }

    public string? Workloadestimation { get; set; }

    public DateTime? Schecomdate { get; set; }

    public string? Countermeasure { get; set; }

    public string? Performance { get; set; }

    public bool? Noteststepflag { get; set; }

    public bool? ProgramMoveFlag { get; set; }

    public string? MoveFileList { get; set; }

    public int? EndUser { get; set; }
    public byte[] RowVersion { get; set; } = [];

}
