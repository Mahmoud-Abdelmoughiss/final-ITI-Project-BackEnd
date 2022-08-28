using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerseApplication.Migrations
{
    public partial class Requesttable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "numberOfBranches",
                table: "Requests",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "numberOfBranches",
                table: "Requests");
        }
    }
}
