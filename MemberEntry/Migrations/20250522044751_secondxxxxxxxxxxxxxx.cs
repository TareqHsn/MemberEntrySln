using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberEntry.Migrations
{
    /// <inheritdoc />
    public partial class secondxxxxxxxxxxxxxx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsUnderSurveillance",
                table: "Members",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Members",
                newName: "IsUnderSurveillance");
        }
    }
}
