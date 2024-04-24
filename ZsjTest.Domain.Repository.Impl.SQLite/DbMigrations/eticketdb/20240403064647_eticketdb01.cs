using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZsjTest.Domain.Repository.Impl.SQLite.DbMigrations.eticketdb
{
    /// <inheritdoc />
    public partial class eticketdb01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ETickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TicketSubject = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<decimal>(type: "TEXT", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RaisedBy = table.Column<decimal>(type: "TEXT", nullable: true),
                    RaiseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AssignTo = table.Column<decimal>(type: "TEXT", nullable: true),
                    AssignDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    BeginBy = table.Column<decimal>(type: "TEXT", nullable: true),
                    BeginDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EndBy = table.Column<decimal>(type: "TEXT", nullable: true),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeliveredBy = table.Column<decimal>(type: "TEXT", nullable: true),
                    DeliverDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CancelledBy = table.Column<decimal>(type: "TEXT", nullable: true),
                    CancelDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ClosedBy = table.Column<decimal>(type: "TEXT", nullable: true),
                    CloseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastUpdatedBy = table.Column<decimal>(type: "TEXT", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ApplicationName = table.Column<string>(type: "TEXT", nullable: true),
                    ModuleName = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<decimal>(type: "TEXT", nullable: true),
                    Priority = table.Column<decimal>(type: "TEXT", nullable: true),
                    Type = table.Column<decimal>(type: "TEXT", nullable: true),
                    Solution = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    RaisedDept = table.Column<string>(type: "TEXT", nullable: true),
                    Cancelreason = table.Column<string>(type: "TEXT", nullable: true),
                    DebuggedBy = table.Column<decimal>(type: "TEXT", nullable: true),
                    DebugDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SatisfactoryLevel = table.Column<decimal>(type: "TEXT", nullable: true),
                    SatisfactoryReason = table.Column<string>(type: "TEXT", nullable: true),
                    PlanTime = table.Column<decimal>(type: "TEXT", nullable: true),
                    FactTime = table.Column<decimal>(type: "TEXT", nullable: true),
                    RequestTime = table.Column<decimal>(type: "TEXT", nullable: true),
                    IssueType = table.Column<int>(type: "INTEGER", nullable: true),
                    ApprovedBy = table.Column<decimal>(type: "TEXT", nullable: true),
                    ApproveDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ResponsePriority = table.Column<byte>(type: "INTEGER", nullable: true),
                    ExpectedTangibleBenefits = table.Column<string>(type: "TEXT", nullable: true),
                    ExpectedIntangibleBenefits = table.Column<string>(type: "TEXT", nullable: true),
                    ExpectedCostSaveings = table.Column<string>(type: "TEXT", nullable: true),
                    ResponsePriorityText = table.Column<string>(type: "TEXT", nullable: true),
                    TypeName = table.Column<string>(type: "TEXT", nullable: true),
                    CreateBy = table.Column<int>(type: "INTEGER", nullable: true),
                    LastUpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
                    Impactanalysis = table.Column<string>(type: "TEXT", nullable: true),
                    Workloadestimation = table.Column<string>(type: "TEXT", nullable: true),
                    Schecomdate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Countermeasure = table.Column<string>(type: "TEXT", nullable: true),
                    Performance = table.Column<string>(type: "TEXT", nullable: true),
                    Noteststepflag = table.Column<bool>(type: "INTEGER", nullable: true),
                    ProgramMoveFlag = table.Column<bool>(type: "INTEGER", nullable: true),
                    MoveFileList = table.Column<string>(type: "TEXT", nullable: true),
                    EndUser = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ETickets", x => x.TicketId);
                });

            migrationBuilder.InsertData(
                table: "ETickets",
                columns: new[] { "TicketId", "ApplicationName", "ApproveDate", "ApprovedBy", "AssignDate", "AssignTo", "BeginBy", "BeginDate", "CancelDate", "CancelledBy", "Cancelreason", "CloseDate", "ClosedBy", "Countermeasure", "CreateBy", "CreateDate", "CreatedBy", "DebugDate", "DebuggedBy", "DeliverDate", "DeliveredBy", "Description", "EndBy", "EndDate", "EndUser", "ExpectedCostSaveings", "ExpectedIntangibleBenefits", "ExpectedTangibleBenefits", "FactTime", "Impactanalysis", "IssueType", "LastUpdateBy", "LastUpdateDate", "LastUpdatedBy", "ModuleName", "MoveFileList", "Noteststepflag", "Performance", "PlanTime", "Priority", "ProgramMoveFlag", "RaiseDate", "RaisedBy", "RaisedDept", "RequestTime", "ResponsePriority", "ResponsePriorityText", "SatisfactoryLevel", "SatisfactoryReason", "Schecomdate", "Solution", "Status", "TicketSubject", "Type", "TypeName", "Workloadestimation" },
                values: new object[] { 1, "def", null, null, null, null, null, null, null, null, null, null, null, null, 123, new DateTime(2023, 1, 10, 15, 10, 10, 0, DateTimeKind.Unspecified), null, null, null, null, null, "问题描述如下。", null, null, null, null, null, null, null, null, null, 456, new DateTime(2023, 1, 13, 9, 8, 8, 0, DateTimeKind.Unspecified), null, "abcd", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, "解决方案描述", 1m, "Test Subject", null, "Change Request", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ETickets");
        }
    }
}
