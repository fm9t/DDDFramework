CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "UserInfos" (
    "UserId" INTEGER NOT NULL CONSTRAINT "PK_UserInfos" PRIMARY KEY AUTOINCREMENT,
    "UserType" INTEGER NULL,
    "UserGuid" TEXT NOT NULL,
    "UserNumber" TEXT NULL,
    "EngLastName" TEXT NULL,
    "EngMidName" TEXT NULL,
    "EngFirstName" TEXT NULL,
    "NativeName" TEXT NULL,
    "Readonly" INTEGER NULL,
    "LoginName" TEXT NULL,
    "LowercaseLoginName" TEXT NULL,
    "Password" BLOB NULL,
    "CityId" INTEGER NULL,
    "DirectLine" TEXT NULL,
    "PhoneExt" TEXT NULL,
    "OfficeMobile" TEXT NULL,
    "FaxNum" TEXT NULL,
    "Email" TEXT NULL,
    "EmployeeId" INTEGER NULL,
    "EnableFlag" INTEGER NULL,
    "DeleteFlag" INTEGER NULL,
    "PasswordQuestion" TEXT NULL,
    "PasswordAnswer" BLOB NULL,
    "LastLoginDate" TEXT NULL,
    "LastPasswordchangedDate" TEXT NULL,
    "OnlineFlag" INTEGER NULL,
    "LockedoutFlag" INTEGER NULL,
    "LastLockedoutDate" TEXT NULL,
    "ApproveFlag" INTEGER NULL,
    "Failedpasswordattemptcount" INTEGER NULL,
    "Failedpasswordattemptwndwstart" TEXT NULL,
    "Failedpasswordanswerattmptcnt" INTEGER NULL,
    "Failedpasswordanswerattmpstart" TEXT NULL,
    "Remark" TEXT NULL,
    "PasswordExpiredFlag" INTEGER NULL,
    "CreateBy" INTEGER NOT NULL,
    "CreateDate" TEXT NOT NULL,
    "LastUpdateBy" INTEGER NULL,
    "LastUpdateDate" TEXT NULL,
    "AccountDisableDate" TEXT NULL,
    "ConsId" INTEGER NULL,
    "LeaveNoticeMailedFlag" INTEGER NULL
);

INSERT INTO "UserInfos" ("UserId", "AccountDisableDate", "ApproveFlag", "CityId", "ConsId", "CreateBy", "CreateDate", "DeleteFlag", "DirectLine", "Email", "EmployeeId", "EnableFlag", "EngFirstName", "EngLastName", "EngMidName", "Failedpasswordanswerattmpstart", "Failedpasswordanswerattmptcnt", "Failedpasswordattemptcount", "Failedpasswordattemptwndwstart", "FaxNum", "LastLockedoutDate", "LastLoginDate", "LastPasswordchangedDate", "LastUpdateBy", "LastUpdateDate", "LeaveNoticeMailedFlag", "LockedoutFlag", "LoginName", "LowercaseLoginName", "NativeName", "OfficeMobile", "OnlineFlag", "Password", "PasswordAnswer", "PasswordExpiredFlag", "PasswordQuestion", "PhoneExt", "Readonly", "Remark", "UserGuid", "UserNumber", "UserType")
VALUES (1, NULL, NULL, NULL, NULL, 123, '2023-01-10 15:10:10', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 456, '2023-01-13 09:08:08', NULL, NULL, 'sjzhou', NULL, '张三', NULL, NULL, X'C2E3B59F16703549EFA15E6DAEC8DE18', NULL, NULL, NULL, NULL, NULL, NULL, 'dda6f9ee803344dc800447c720cfa7b0', NULL, NULL);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240403065033_userinfodb01', '7.0.17');

COMMIT;

