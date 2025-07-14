using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlamGearAdmin.Data.Migrations
{
    /// <inheritdoc />
    public partial class CGGSchema100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Admin",
                newName: "MiddleName");

            migrationBuilder.AddColumn<string>(
                name: "FamilyName",
                table: "Admin",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GivenName",
                table: "Admin",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FamilyName",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "GivenName",
                table: "Admin");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Admin",
                newName: "FullName");
        }
    }
}
