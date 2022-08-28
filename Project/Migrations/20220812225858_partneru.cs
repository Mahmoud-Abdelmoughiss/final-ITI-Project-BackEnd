using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerseApplication.Migrations
{
    public partial class partneru : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "Parteners",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Parteners_IdentityId",
                table: "Parteners",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parteners_AspNetUsers_IdentityId",
                table: "Parteners",
                column: "IdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parteners_AspNetUsers_IdentityId",
                table: "Parteners");

            migrationBuilder.DropIndex(
                name: "IX_Parteners_IdentityId",
                table: "Parteners");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Parteners");
        }
    }
}
