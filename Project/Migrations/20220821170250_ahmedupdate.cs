using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerseApplication.Migrations
{
    public partial class ahmedupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PartnerId",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PartnerId",
                table: "Discounts");
        }
    }
}
