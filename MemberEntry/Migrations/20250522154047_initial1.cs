using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberEntry.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1", null, "SystemAdmin", "SYSTEMADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e1ae1f42-75b2-4604-97ec-10f844b1962f", 0, "c8d546a1-a24e-4e53-81fc-5b4e47a76d47", "tareqkb@yahoo.com", true, "Tareq", "Hossian", false, null, "TAREQ@YAHOO.COM", "TAREQ@YAHOO.COM", "AQAAAAIAAYagAAAAEGaMZkAtMLFZ8UbVBfmRVpSc3rnr4yCc1zjG3cHoLA3ZhNqkGofOwCwyYuxcmsOHDg==", "01861268168", true, "891e7b3f-733b-4e66-8df5-b787d73ad6cc", false, "tareq@yahoo.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "FullName", "Sharif Ahmed", "e1ae1f42-75b2-4604-97ec-10f844b1962f" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "e1ae1f42-75b2-4604-97ec-10f844b1962f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "e1ae1f42-75b2-4604-97ec-10f844b1962f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e1ae1f42-75b2-4604-97ec-10f844b1962f");
        }
    }
}
