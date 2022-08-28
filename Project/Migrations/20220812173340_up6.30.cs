using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerseApplication.Migrations
{
    public partial class up630 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_shippingDetails_users_userID",
                table: "shippingDetails");

            migrationBuilder.AddColumn<string>(
                name: "StripeTokenID",
                table: "users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "userID",
                table: "shippingDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "FK_shippingDetails_users_userID",
                table: "shippingDetails");

            migrationBuilder.DropColumn(
                name: "StripeTokenID",
                table: "users");

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
