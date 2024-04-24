using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZsjTest.Domain.Repository.Impl.SQLite.DbMigrations.eticketdb
{
    /// <inheritdoc />
    public partial class eticketdb02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "ETickets",
                type: "BLOB",
                maxLength: 8,
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "ETickets");
        }
    }
}
