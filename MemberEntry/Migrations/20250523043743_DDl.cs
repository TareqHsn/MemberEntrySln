using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberEntry.Migrations
{
    /// <inheritdoc />
    public partial class DDl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PassportTypeId",
                table: "Members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PassportTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassportTypes", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1ae1f42-75b2-4604-97ec-10f844b1962f",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4bbbf32e-f259-4659-9823-876805e73a07", "tareq@yahoo.com", "AQAAAAIAAYagAAAAECVtSi354i1yPZEDCwCLc25l4RMgDhMWyJamMC9uURTP8F3vPOai18ky2WN/djMxEg==", "2b6ae9f1-99e1-421c-bfa9-010373900fe3" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassportTypes");

            migrationBuilder.DropColumn(
                name: "PassportTypeId",
                table: "Members");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1ae1f42-75b2-4604-97ec-10f844b1962f",
                columns: new[] { "ConcurrencyStamp", "Email", "PasswordHash", "SecurityStamp" },
                values: new object[] { "09fbc452-7ba7-4e8d-b63c-081c763122bf", "tareqkb@yahoo.com", "AQAAAAIAAYagAAAAEKMrzhp0rqIn2YE2KNxiDOv6WUN4AK251im2qQIGS42+E1qNfnFTRrwVqor5rWjzOw==", "33d2faac-f16d-4009-b340-ebe8a28d0d7b" });
        }
    }
}
