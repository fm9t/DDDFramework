/*********************************************************************
** Copyright(c) 2024  All Rights Reserved.
** auth: ZSJ
** mail: 
** date: 2024-03-22 10:32
** desc: ETicket表数据库映射信息
** Ver : V1.0.0
**********************************************************************/

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ZsjTest.Domain.ETicketMgr;

namespace ZsjTest.Domain.Repository.Impl.ETicketDomain.DbConfiguration;
internal class ETicketConfiguration : IEntityTypeConfiguration<ETicket>
{
    // can use dotnet ef tools for create the table mapping
    // dotnet ef dbcontext scaffold "[connection string]" Microsoft.EntityFrameworkCore.Sqlite -o Models -t ETICKETS -t USERINFOS --context-dir Context --no-onconfiguring

    public void Configure(EntityTypeBuilder<ETicket> builder)
    {
        builder.HasKey(e => e.TicketId);

        builder.ToTable("IM_CR_TICKETS", "CMDB");

        builder.Property(e => e.TicketId)
            .HasColumnName("TICKET_ID").UseHiLo("SEQ_IM_CR_TICKETS");
        builder.Property(e => e.ApplicationName)
            .HasMaxLength(20)
            .IsUnicode(false)
            .HasColumnName("APPLICATION_NAME");
        builder.Property(e => e.ApproveDate)
            .HasColumnType("DATE")
            .HasColumnName("APPROVE_DATE");
        builder.Property(e => e.ApprovedBy)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("APPROVED_BY");
        builder.Property(e => e.AssignDate)
            .HasColumnType("DATE")
            .HasColumnName("ASSIGN_DATE");
        builder.Property(e => e.AssignTo)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("ASSIGN_TO");
        builder.Property(e => e.BeginBy)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("BEGIN_BY");
        builder.Property(e => e.BeginDate)
            .HasColumnType("DATE")
            .HasColumnName("BEGIN_DATE");
        builder.Property(e => e.CancelDate)
            .HasColumnType("DATE")
            .HasColumnName("CANCEL_DATE");
        builder.Property(e => e.CancelledBy)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("CANCELLED_BY");
        builder.Property(e => e.Cancelreason)
            .HasMaxLength(2000)
            .IsUnicode(false)
            .HasColumnName("CANCELREASON");
        builder.Property(e => e.CloseDate)
            .HasColumnType("DATE")
            .HasColumnName("CLOSE_DATE");
        builder.Property(e => e.ClosedBy)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("CLOSED_BY");
        builder.Property(e => e.Countermeasure)
            .HasMaxLength(2000)
            .IsUnicode(false)
            .HasColumnName("COUNTERMEASURE");
        builder.Property(e => e.CreateBy)
            .HasPrecision(10)
            .HasColumnName("CREATE_BY");
        builder.Property(e => e.CreateDate)
            .HasColumnType("DATE")
            .HasColumnName("CREATE_DATE");
        builder.Property(e => e.CreatedBy)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("CREATED_BY");
        builder.Property(e => e.DebugDate)
            .HasColumnType("DATE")
            .HasColumnName("DEBUG_DATE");
        builder.Property(e => e.DebuggedBy)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("DEBUGGED_BY");
        builder.Property(e => e.DeliverDate)
            .HasColumnType("DATE")
            .HasColumnName("DELIVER_DATE");
        builder.Property(e => e.DeliveredBy)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("DELIVERED_BY");
        builder.Property(e => e.Description)
            .HasMaxLength(2000)
            .IsUnicode(false)
            .HasColumnName("DESCRIPTION");
        builder.Property(e => e.EndBy)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("END_BY");
        builder.Property(e => e.EndDate)
            .HasColumnType("DATE")
            .HasColumnName("END_DATE");
        builder.Property(e => e.EndUser)
            .HasPrecision(9)
            .HasColumnName("END_USER");
        builder.Property(e => e.ExpectedCostSaveings)
            .HasMaxLength(2000)
            .IsUnicode(false)
            .HasColumnName("EXPECTED_COST_SAVEINGS");
        builder.Property(e => e.ExpectedIntangibleBenefits)
            .HasMaxLength(2000)
            .IsUnicode(false)
            .HasColumnName("EXPECTED_INTANGIBLE_BENEFITS");
        builder.Property(e => e.ExpectedTangibleBenefits)
            .HasMaxLength(2000)
            .IsUnicode(false)
            .HasColumnName("EXPECTED_TANGIBLE_BENEFITS");
        builder.Property(e => e.FactTime)
            .HasColumnType("NUMBER(10,1)")
            .HasColumnName("FACT_TIME");
        builder.Property(e => e.Impactanalysis)
            .HasMaxLength(2000)
            .IsUnicode(false)
            .HasColumnName("IMPACTANALYSIS");
        builder.Property(e => e.IssueType)
            .HasPrecision(10)
            .HasColumnName("ISSUE_TYPE");
        builder.Property(e => e.LastUpdateBy)
            .HasPrecision(10)
            .HasColumnName("LAST_UPDATE_BY");
        builder.Property(e => e.LastUpdateDate)
            .HasColumnType("DATE")
            .HasColumnName("LAST_UPDATE_DATE");
        builder.Property(e => e.LastUpdatedBy)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("LAST_UPDATED_BY");
        builder.Property(e => e.ModuleName)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasColumnName("MODULE_NAME");
        builder.Property(e => e.MoveFileList)
            .IsUnicode(false)
            .HasColumnName("MOVE_FILE_LIST");
        builder.Property(e => e.Noteststepflag)
            .HasPrecision(1)
            .HasDefaultValueSql("(0)\n")
            .HasColumnName("NOTESTSTEPFLAG");
        builder.Property(e => e.Performance)
            .HasMaxLength(2000)
            .IsUnicode(false)
            .HasColumnName("PERFORMANCE");
        builder.Property(e => e.PlanTime)
            .HasColumnType("NUMBER(10,1)")
            .HasColumnName("PLAN_TIME");
        builder.Property(e => e.Priority)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("PRIORITY");
        builder.Property(e => e.ProgramMoveFlag)
            .HasPrecision(1)
            .HasDefaultValueSql("0\n\n")
            .HasColumnName("PROGRAM_MOVE_FLAG");
        builder.Property(e => e.RaiseDate)
            .HasColumnType("DATE")
            .HasColumnName("RAISE_DATE");
        builder.Property(e => e.RaisedBy)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("RAISED_BY");
        builder.Property(e => e.RaisedDept)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasColumnName("RAISED_DEPT");
        builder.Property(e => e.RequestTime)
            .HasColumnType("NUMBER(10,1)")
            .HasColumnName("REQUEST_TIME");
        builder.Property(e => e.ResponsePriority)
            .HasPrecision(2)
            .HasColumnName("RESPONSE_PRIORITY");
        builder.Property(e => e.ResponsePriorityText)
            .HasMaxLength(50)
            .IsUnicode(false)
            .HasColumnName("RESPONSE_PRIORITY_TEXT");
        builder.Property(e => e.SatisfactoryLevel)
            .HasColumnType("NUMBER")
            .HasColumnName("SATISFACTORY_LEVEL");
        builder.Property(e => e.SatisfactoryReason)
            .HasMaxLength(2000)
            .IsUnicode(false)
            .HasColumnName("SATISFACTORY_REASON");
        builder.Property(e => e.Schecomdate)
            .HasColumnType("DATE")
            .HasColumnName("SCHECOMDATE");
        builder.Property(e => e.Solution)
            .HasMaxLength(2000)
            .IsUnicode(false)
            .HasColumnName("SOLUTION");
        builder.Property(e => e.Status)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("STATUS");
        builder.Property(e => e.TicketSubject)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("TICKET_SUBJECT");
        builder.Property(e => e.Type)
            .HasColumnType("NUMBER(38)")
            .HasColumnName("TYPE");
        builder.Property(e => e.TypeName)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("TYPE_NAME");
        builder.Property(e => e.Workloadestimation)
            .HasMaxLength(2000)
            .IsUnicode(false)
            .HasColumnName("WORKLOADESTIMATION");
        builder.Ignore(e => e.RowVersion);
    }
}
