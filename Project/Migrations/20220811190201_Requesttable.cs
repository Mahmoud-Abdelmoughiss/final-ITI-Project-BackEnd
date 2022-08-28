using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerseApplication.Migrations
{
    public partial class Requesttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shippingDetails_users_userID",
                table: "shippingDetails");

            migrationBuilder.AlterColumn<int>(
                name: "userID",
                table: "shippingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityId",
                table: "shippers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_shippers_IdentityId",
                table: "shippers",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_shippers_AspNetUsers_IdentityId",
                table: "shippers",
                column: "IdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_shippingDetails_users_userID",
                table: "shippingDetails",
                column: "userID",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shippers_AspNetUsers_IdentityId",
                table: "shippers");

            migrationBuilder.DropForeignKey(
                name: "FK_shippingDetails_users_userID",
                table: "shippingDetails");

            migrationBuilder.DropIndex(
                name: "IX_shippers_IdentityId",
                table: "shippers");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "shippers");

            migrationBuilder.AlterColumn<int>(
                name: "userID",
                table: "shippingDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_shippingDetails_users_userID",
                table: "shippingDetails",
                column: "userID",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
