using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerseApplication.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descount_Persent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Payment_Details",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment_Details", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Product_Categorys",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Categorys", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Product_Inventorys",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Inventorys", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "shippers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    arabicName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    officePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shippers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserNameAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcountID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNameAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNameAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "subCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    arabicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    arabicDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_subCategories_Product_Categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Product_Categorys",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    productID = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_feedbacks_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "order_Details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Payment_ID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_Details_Payment_Details_Payment_ID",
                        column: x => x.Payment_ID,
                        principalTable: "Payment_Details",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_order_Details_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Parteners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numberOfBranches = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parteners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parteners_users_userID",
                        column: x => x.userID,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shopping_Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shopping_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shopping_Sessions_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "User_Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    arabicAddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    arabicAddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    arabicCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    arabicCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Addresses_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);//NoAction
                });

            migrationBuilder.CreateTable(
                name: "User_Payements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayementType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    arabicPayementType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    arabicProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNo = table.Column<int>(type: "int", nullable: false),
                    Expiry = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Payements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Payements_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_Ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    InventoryID = table.Column<int>(type: "int", nullable: true),
                    DiscountID = table.Column<int>(type: "int", nullable: true),
                    PartenerID = table.Column<int>(type: "int", nullable: false),
                    subcategoryID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Discounts_DiscountID",
                        column: x => x.DiscountID,
                        principalTable: "Discounts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Products_Parteners_PartenerID",
                        column: x => x.PartenerID,
                        principalTable: "Parteners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Products_Product_Categorys_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Product_Categorys",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Products_Product_Inventorys_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Product_Inventorys",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Products_subCategories_subcategoryID",
                        column: x => x.subcategoryID,
                        principalTable: "subCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "shippingDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shipName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    shippingstate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    arabicshippingstate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userID = table.Column<int>(type: "int", nullable: false),
                    shipperID = table.Column<int>(type: "int", nullable: false),
                    orderDetailsID = table.Column<int>(type: "int", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    completeAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    deletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ALLaddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shippingDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_shippingDetails_order_Details_orderDetailsID",
                        column: x => x.orderDetailsID,
                        principalTable: "order_Details",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_shippingDetails_shippers_shipperID",
                        column: x => x.shipperID,
                        principalTable: "shippers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_shippingDetails_User_Addresses_addressID",
                        column: x => x.addressID,
                        principalTable: "User_Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_shippingDetails_users_userID",
                        column: x => x.userID,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Cart_Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cart_Items_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Cart_Items_Shopping_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Shopping_Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_items", x => x.ID);
                    table.ForeignKey(
                        name: "FK_order_items_order_Details_OrderID",
                        column: x => x.OrderID,
                        principalTable: "order_Details",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_order_items_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "product_Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_Images_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Items_ProductId",
                table: "Cart_Items",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_Items_SessionId",
                table: "Cart_Items",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_UserID",
                table: "feedbacks",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_order_Details_Payment_ID",
                table: "order_Details",
                column: "Payment_ID");

            migrationBuilder.CreateIndex(
                name: "IX_order_Details_UserID",
                table: "order_Details",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_OrderID",
                table: "order_items",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_ProductID",
                table: "order_items",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Parteners_userID",
                table: "Parteners",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_product_Images_ProductID",
                table: "product_Images",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DiscountID",
                table: "Products",
                column: "DiscountID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoryID",
                table: "Products",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PartenerID",
                table: "Products",
                column: "PartenerID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_subcategoryID",
                table: "Products",
                column: "subcategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_shippingDetails_addressID",
                table: "shippingDetails",
                column: "addressID");

            migrationBuilder.CreateIndex(
                name: "IX_shippingDetails_orderDetailsID",
                table: "shippingDetails",
                column: "orderDetailsID");

            migrationBuilder.CreateIndex(
                name: "IX_shippingDetails_shipperID",
                table: "shippingDetails",
                column: "shipperID");

            migrationBuilder.CreateIndex(
                name: "IX_shippingDetails_userID",
                table: "shippingDetails",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Shopping_Sessions_UserID",
                table: "Shopping_Sessions",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_subCategories_CategoryId",
                table: "subCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Addresses_UserId",
                table: "User_Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Payements_UserId",
                table: "User_Payements",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart_Items");

            migrationBuilder.DropTable(
                name: "feedbacks");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "product_Images");

            migrationBuilder.DropTable(
                name: "shippingDetails");

            migrationBuilder.DropTable(
                name: "User_Payements");

            migrationBuilder.DropTable(
                name: "Shopping_Sessions");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "order_Details");

            migrationBuilder.DropTable(
                name: "shippers");

            migrationBuilder.DropTable(
                name: "User_Addresses");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Parteners");

            migrationBuilder.DropTable(
                name: "Product_Inventorys");

            migrationBuilder.DropTable(
                name: "subCategories");

            migrationBuilder.DropTable(
                name: "Payment_Details");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "Product_Categorys");
        }
    }
}
