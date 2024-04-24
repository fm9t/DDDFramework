using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZsjTest.Domain.Repository.Impl.SQLite.DbMigrations.userinfodb
{
    /// <inheritdoc />
    public partial class userinfodb01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserType = table.Column<int>(type: "INTEGER", nullable: true),
                    UserGuid = table.Column<string>(type: "TEXT", nullable: false),
                    UserNumber = table.Column<string>(type: "TEXT", nullable: true),
                    EngLastName = table.Column<string>(type: "TEXT", nullable: true),
                    EngMidName = table.Column<string>(type: "TEXT", nullable: true),
                    EngFirstName = table.Column<string>(type: "TEXT", nullable: true),
                    NativeName = table.Column<string>(type: "TEXT", nullable: true),
                    Readonly = table.Column<bool>(type: "INTEGER", nullable: true),
                    LoginName = table.Column<string>(type: "TEXT", nullable: true),
                    LowercaseLoginName = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<byte[]>(type: "BLOB", nullable: true),
                    CityId = table.Column<int>(type: "INTEGER", nullable: true),
                    DirectLine = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneExt = table.Column<string>(type: "TEXT", nullable: true),
                    OfficeMobile = table.Column<string>(type: "TEXT", nullable: true),
                    FaxNum = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: true),
                    EnableFlag = table.Column<bool>(type: "INTEGER", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "INTEGER", nullable: true),
                    PasswordQuestion = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordAnswer = table.Column<byte[]>(type: "BLOB", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastPasswordchangedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OnlineFlag = table.Column<bool>(type: "INTEGER", nullable: true),
                    LockedoutFlag = table.Column<bool>(type: "INTEGER", nullable: true),
                    LastLockedoutDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ApproveFlag = table.Column<bool>(type: "INTEGER", nullable: true),
                    Failedpasswordattemptcount = table.Column<int>(type: "INTEGER", nullable: true),
                    Failedpasswordattemptwndwstart = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Failedpasswordanswerattmptcnt = table.Column<int>(type: "INTEGER", nullable: true),
                    Failedpasswordanswerattmpstart = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Remark = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordExpiredFlag = table.Column<bool>(type: "INTEGER", nullable: true),
                    CreateBy = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastUpdateBy = table.Column<int>(type: "INTEGER", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AccountDisableDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ConsId = table.Column<int>(type: "INTEGER", nullable: true),
                    LeaveNoticeMailedFlag = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "UserInfos",
                columns: new[] { "UserId", "AccountDisableDate", "ApproveFlag", "CityId", "ConsId", "CreateBy", "CreateDate", "DeleteFlag", "DirectLine", "Email", "EmployeeId", "EnableFlag", "EngFirstName", "EngLastName", "EngMidName", "Failedpasswordanswerattmpstart", "Failedpasswordanswerattmptcnt", "Failedpasswordattemptcount", "Failedpasswordattemptwndwstart", "FaxNum", "LastLockedoutDate", "LastLoginDate", "LastPasswordchangedDate", "LastUpdateBy", "LastUpdateDate", "LeaveNoticeMailedFlag", "LockedoutFlag", "LoginName", "LowercaseLoginName", "NativeName", "OfficeMobile", "OnlineFlag", "Password", "PasswordAnswer", "PasswordExpiredFlag", "PasswordQuestion", "PhoneExt", "Readonly", "Remark", "UserGuid", "UserNumber", "UserType" },
                values: new object[] { 1, null, null, null, null, 123, new DateTime(2023, 1, 10, 15, 10, 10, 0, DateTimeKind.Unspecified), null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 456, new DateTime(2023, 1, 13, 9, 8, 8, 0, DateTimeKind.Unspecified), null, null, "sjzhou", null, "张三", null, null, new byte[] { 194, 227, 181, 159, 22, 112, 53, 73, 239, 161, 94, 109, 174, 200, 222, 24 }, null, null, null, null, null, null, "dda6f9ee803344dc800447c720cfa7b0", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfos");
        }
    }
}
