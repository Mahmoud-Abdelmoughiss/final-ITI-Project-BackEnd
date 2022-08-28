using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerseApplication.Migrations
{
    public partial class up2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Parteners_PartenerID",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "PartenerID",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "product_Images",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "product_Images",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "product_Images",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Parteners_PartenerID",
                table: "Products",
                column: "PartenerID",
                principalTable: "Parteners",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Parteners_PartenerID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "product_Images");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "product_Images");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "product_Images");

            migrationBuilder.AlterColumn<int>(
                name: "PartenerID",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Parteners_PartenerID",
                table: "Products",
                column: "PartenerID",
                principalTable: "Parteners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
