using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberEntry.Migrations
{
    /// <inheritdoc />
    public partial class DDlttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1ae1f42-75b2-4604-97ec-10f844b1962f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a14e531-5e5b-485b-838a-187e0ffdf549", "AQAAAAIAAYagAAAAEIkDAZ84jQGxJ82/aHPiziIQnRtUndhZOyUdaIaB7C002yoxuvuDgrKJlG1YcGnVSQ==", "7d073f2f-f9d8-463e-b6f7-7ba0da95167c" });

            migrationBuilder.CreateIndex(
                name: "IX_Members_PassportTypeId",
                table: "Members",
                column: "PassportTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_PassportTypes_PassportTypeId",
                table: "Members",
                column: "PassportTypeId",
                principalTable: "PassportTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_PassportTypes_PassportTypeId",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Members_PassportTypeId",
                table: "Members");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1ae1f42-75b2-4604-97ec-10f844b1962f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4bbbf32e-f259-4659-9823-876805e73a07", "AQAAAAIAAYagAAAAECVtSi354i1yPZEDCwCLc25l4RMgDhMWyJamMC9uURTP8F3vPOai18ky2WN/djMxEg==", "2b6ae9f1-99e1-421c-bfa9-010373900fe3" });
        }
    }
}
