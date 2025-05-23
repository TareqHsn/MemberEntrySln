using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberEntry.Migrations
{
    /// <inheritdoc />
    public partial class initial1222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "FirstName", "Tareq" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1ae1f42-75b2-4604-97ec-10f844b1962f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "09fbc452-7ba7-4e8d-b63c-081c763122bf", "AQAAAAIAAYagAAAAEKMrzhp0rqIn2YE2KNxiDOv6WUN4AK251im2qQIGS42+E1qNfnFTRrwVqor5rWjzOw==", "33d2faac-f16d-4009-b340-ebe8a28d0d7b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClaimType", "ClaimValue" },
                values: new object[] { "FullName", "Sharif Ahmed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1ae1f42-75b2-4604-97ec-10f844b1962f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c8d546a1-a24e-4e53-81fc-5b4e47a76d47", "AQAAAAIAAYagAAAAEGaMZkAtMLFZ8UbVBfmRVpSc3rnr4yCc1zjG3cHoLA3ZhNqkGofOwCwyYuxcmsOHDg==", "891e7b3f-733b-4e66-8df5-b787d73ad6cc" });
        }
    }
}
