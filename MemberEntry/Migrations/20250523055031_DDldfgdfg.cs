using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberEntry.Migrations
{
    /// <inheritdoc />
    public partial class DDldfgdfg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1ae1f42-75b2-4604-97ec-10f844b1962f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f04505e8-fe19-410e-861a-786c4f16bb3e", "AQAAAAIAAYagAAAAEFQQtJupLyNltDQnUI+bGdqw5ksRQ291a8wlDZ3M1xntsXm48XliMFn8vbZLeKdkuQ==", "dc10ea77-94a9-4dd4-aedc-51de1b1b8e32" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1ae1f42-75b2-4604-97ec-10f844b1962f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a14e531-5e5b-485b-838a-187e0ffdf549", "AQAAAAIAAYagAAAAEIkDAZ84jQGxJ82/aHPiziIQnRtUndhZOyUdaIaB7C002yoxuvuDgrKJlG1YcGnVSQ==", "7d073f2f-f9d8-463e-b6f7-7ba0da95167c" });
        }
    }
}
