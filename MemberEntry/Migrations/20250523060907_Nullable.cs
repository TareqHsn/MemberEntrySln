using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberEntry.Migrations
{
    /// <inheritdoc />
    public partial class Nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1ae1f42-75b2-4604-97ec-10f844b1962f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ef6ec95-3600-464b-80ce-83cb3c0ce689", "AQAAAAIAAYagAAAAELWc6xdMOECDP/sWwUSO19tNN4LcV7vmkUzUUUgzn7M3b6JnQ9G3OTCSLITFbe3vfg==", "19efdb2f-f89a-4f57-9cfa-d2c1c8821ec0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1ae1f42-75b2-4604-97ec-10f844b1962f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f04505e8-fe19-410e-861a-786c4f16bb3e", "AQAAAAIAAYagAAAAEFQQtJupLyNltDQnUI+bGdqw5ksRQ291a8wlDZ3M1xntsXm48XliMFn8vbZLeKdkuQ==", "dc10ea77-94a9-4dd4-aedc-51de1b1b8e32" });
        }
    }
}
