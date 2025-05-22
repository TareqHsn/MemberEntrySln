using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberEntry.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameInEnglish = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NameInBangla = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Father = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mother = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PoliticalView = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsUnderSurveillance = table.Column<bool>(type: "bit", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReligionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaritalStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfessionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NIDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.MemberId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
