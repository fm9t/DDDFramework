CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "ETickets" (
    "TicketId" INTEGER NOT NULL CONSTRAINT "PK_ETickets" PRIMARY KEY AUTOINCREMENT,
    "TicketSubject" TEXT NULL,
    "CreatedBy" TEXT NULL,
    "CreateDate" TEXT NULL,
    "RaisedBy" TEXT NULL,
    "RaiseDate" TEXT NULL,
    "AssignTo" TEXT NULL,
    "AssignDate" TEXT NULL,
    "BeginBy" TEXT NULL,
    "BeginDate" TEXT NULL,
    "EndBy" TEXT NULL,
    "EndDate" TEXT NULL,
    "DeliveredBy" TEXT NULL,
    "DeliverDate" TEXT NULL,
    "CancelledBy" TEXT NULL,
    "CancelDate" TEXT NULL,
    "ClosedBy" TEXT NULL,
    "CloseDate" TEXT NULL,
    "LastUpdatedBy" TEXT NULL,
    "LastUpdateDate" TEXT NULL,
    "ApplicationName" TEXT NULL,
    "ModuleName" TEXT NULL,
    "Status" TEXT NULL,
    "Priority" TEXT NULL,
    "Type" TEXT NULL,
    "Solution" TEXT NULL,
    "Description" TEXT NULL,
    "RaisedDept" TEXT NULL,
    "Cancelreason" TEXT NULL,
    "DebuggedBy" TEXT NULL,
    "DebugDate" TEXT NULL,
    "SatisfactoryLevel" TEXT NULL,
    "SatisfactoryReason" TEXT NULL,
    "PlanTime" TEXT NULL,
    "FactTime" TEXT NULL,
    "RequestTime" TEXT NULL,
    "IssueType" INTEGER NULL,
    "ApprovedBy" TEXT NULL,
    "ApproveDate" TEXT NULL,
    "ResponsePriority" INTEGER NULL,
    "ExpectedTangibleBenefits" TEXT NULL,
    "ExpectedIntangibleBenefits" TEXT NULL,
    "ExpectedCostSaveings" TEXT NULL,
    "ResponsePriorityText" TEXT NULL,
    "TypeName" TEXT NULL,
    "CreateBy" INTEGER NULL,
    "LastUpdateBy" INTEGER NULL,
    "Impactanalysis" TEXT NULL,
    "Workloadestimation" TEXT NULL,
    "Schecomdate" TEXT NULL,
    "Countermeasure" TEXT NULL,
    "Performance" TEXT NULL,
    "Noteststepflag" INTEGER NULL,
    "ProgramMoveFlag" INTEGER NULL,
    "MoveFileList" TEXT NULL,
    "EndUser" INTEGER NULL
);

INSERT INTO "ETickets" ("TicketId", "ApplicationName", "ApproveDate", "ApprovedBy", "AssignDate", "AssignTo", "BeginBy", "BeginDate", "CancelDate", "CancelledBy", "Cancelreason", "CloseDate", "ClosedBy", "Countermeasure", "CreateBy", "CreateDate", "CreatedBy", "DebugDate", "DebuggedBy", "DeliverDate", "DeliveredBy", "Description", "EndBy", "EndDate", "EndUser", "ExpectedCostSaveings", "ExpectedIntangibleBenefits", "ExpectedTangibleBenefits", "FactTime", "Impactanalysis", "IssueType", "LastUpdateBy", "LastUpdateDate", "LastUpdatedBy", "ModuleName", "MoveFileList", "Noteststepflag", "Performance", "PlanTime", "Priority", "ProgramMoveFlag", "RaiseDate", "RaisedBy", "RaisedDept", "RequestTime", "ResponsePriority", "ResponsePriorityText", "SatisfactoryLevel", "SatisfactoryReason", "Schecomdate", "Solution", "Status", "TicketSubject", "Type", "TypeName", "Workloadestimation")
VALUES (1, 'def', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 123, '2023-01-10 15:10:10', NULL, NULL, NULL, NULL, NULL, '问题描述如下。', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 456, '2023-01-13 09:08:08', NULL, 'abcd', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '解决方案描述', '1.0', 'Test Subject', NULL, 'Change Request', NULL);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240403064647_eticketdb01', '7.0.17');

COMMIT;

