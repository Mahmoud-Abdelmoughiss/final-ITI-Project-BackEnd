using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerseApplication.Migrations
{
    public partial class ms : Migration
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

            migrationBuilder.CreateTable(
                name: "shipperRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    arabicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    officePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shipperRequests", x => x.Id);
                });

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

            migrationBuilder.DropTable(
                name: "shipperRequests");

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
