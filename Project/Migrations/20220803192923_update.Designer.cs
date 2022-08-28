﻿// <auto-generated />
using System;
using EcommerseApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcommerseApplication.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220803192923_update")]
    partial class update
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EcommerseApplication.Models.Cart_Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SessionId");

                    b.ToTable("Cart_Items");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Discount", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Descount_Persent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description_Ar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_Ar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("EcommerseApplication.Models.feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("productID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("feedbacks");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Order_Details", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Payment_ID")
                        .HasColumnType("int");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Payment_ID");

                    b.HasIndex("UserID");

                    b.ToTable("order_Details");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Order_Items", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrderID")
                        .HasColumnType("int");

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("order_items");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Partener", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("numberOfBranches")
                        .HasColumnType("int");

                    b.Property<int?>("userID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("userID");

                    b.ToTable("Parteners");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Payment_Details", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Payment_Details");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description_Ar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DiscountID")
                        .HasColumnType("int");

                    b.Property<int?>("InventoryID")
                        .HasColumnType("int");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_Ar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PartenerID")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("subcategoryID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("DiscountID");

                    b.HasIndex("InventoryID");

                    b.HasIndex("PartenerID");

                    b.HasIndex("subcategoryID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Product_Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description_Ar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_Ar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Product_Categorys");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Product_Images", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductID");

                    b.ToTable("product_Images");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Product_Inventory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Product_Inventorys");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Shipper", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("arabicName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("officePhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("shippers");
                });

            modelBuilder.Entity("EcommerseApplication.Models.shippingDetails", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("ALLaddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("addressID")
                        .HasColumnType("int");

                    b.Property<string>("arabicshippingstate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("completeAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("deletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("orderDetailsID")
                        .HasColumnType("int");

                    b.Property<string>("shipName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("shipperID")
                        .HasColumnType("int");

                    b.Property<string>("shippingstate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("userID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("addressID");

                    b.HasIndex("orderDetailsID");

                    b.HasIndex("shipperID");

                    b.HasIndex("userID");

                    b.ToTable("shippingDetails");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Shopping_Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Shopping_Sessions");
                });

            modelBuilder.Entity("EcommerseApplication.Models.subCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("arabicDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("arabicName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("subCategories");
                });

            modelBuilder.Entity("EcommerseApplication.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AcountID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstNameAR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastNameAR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserNameAR")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("birthDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("EcommerseApplication.Models.User_address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AddressLine2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("arabicAddressLine1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("arabicAddressLine2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("arabicCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("arabicCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("User_Addresses");
                });

            modelBuilder.Entity("EcommerseApplication.Models.User_Payement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccountNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("Expiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("PayementType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Provider")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("arabicPayementType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("arabicProvider")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("User_Payements");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Cart_Item", b =>
                {
                    b.HasOne("EcommerseApplication.Models.Product", "product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerseApplication.Models.Shopping_Session", "shopping_Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");

                    b.Navigation("shopping_Session");
                });

            modelBuilder.Entity("EcommerseApplication.Models.feedback", b =>
                {
                    b.HasOne("EcommerseApplication.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Order_Details", b =>
                {
                    b.HasOne("EcommerseApplication.Models.Payment_Details", "Payment_Details")
                        .WithMany()
                        .HasForeignKey("Payment_ID");

                    b.HasOne("EcommerseApplication.Models.User", "User")
                        .WithMany("Order_Details")
                        .HasForeignKey("UserID");

                    b.Navigation("Payment_Details");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Order_Items", b =>
                {
                    b.HasOne("EcommerseApplication.Models.Order_Details", "Order_Detail")
                        .WithMany("Order_Items")
                        .HasForeignKey("OrderID");

                    b.HasOne("EcommerseApplication.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID");

                    b.Navigation("Order_Detail");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Partener", b =>
                {
                    b.HasOne("EcommerseApplication.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("userID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Product", b =>
                {
                    b.HasOne("EcommerseApplication.Models.Product_Category", "Product_Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID");

                    b.HasOne("EcommerseApplication.Models.Discount", "Discount")
                        .WithMany("Products")
                        .HasForeignKey("DiscountID");

                    b.HasOne("EcommerseApplication.Models.Product_Inventory", "Product_Inventory")
                        .WithMany()
                        .HasForeignKey("InventoryID");

                    b.HasOne("EcommerseApplication.Models.Partener", "Partener")
                        .WithMany("Products")
                        .HasForeignKey("PartenerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerseApplication.Models.subCategory", "subcategory")
                        .WithMany("Products")
                        .HasForeignKey("subcategoryID");

                    b.Navigation("Discount");

                    b.Navigation("Partener");

                    b.Navigation("Product_Category");

                    b.Navigation("Product_Inventory");

                    b.Navigation("subcategory");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Product_Images", b =>
                {
                    b.HasOne("EcommerseApplication.Models.Product", "Product")
                        .WithMany("Product_Images")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EcommerseApplication.Models.shippingDetails", b =>
                {
                    b.HasOne("EcommerseApplication.Models.User_address", "user_Address")
                        .WithMany()
                        .HasForeignKey("addressID");

                    b.HasOne("EcommerseApplication.Models.Order_Details", "order_Details")
                        .WithMany()
                        .HasForeignKey("orderDetailsID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerseApplication.Models.Shipper", "shipper")
                        .WithMany("shippingDetails")
                        .HasForeignKey("shipperID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerseApplication.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("userID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("order_Details");

                    b.Navigation("shipper");

                    b.Navigation("user");

                    b.Navigation("user_Address");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Shopping_Session", b =>
                {
                    b.HasOne("EcommerseApplication.Models.User", "user")
                        .WithMany("Shopping_Sessions")
                        .HasForeignKey("UserID");

                    b.Navigation("user");
                });

            modelBuilder.Entity("EcommerseApplication.Models.subCategory", b =>
                {
                    b.HasOne("EcommerseApplication.Models.Product_Category", "category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");
                });

            modelBuilder.Entity("EcommerseApplication.Models.User_address", b =>
                {
                    b.HasOne("EcommerseApplication.Models.User", "user")
                        .WithMany("User_Address")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("EcommerseApplication.Models.User_Payement", b =>
                {
                    b.HasOne("EcommerseApplication.Models.User", "user")
                        .WithMany("User_Pyment")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Discount", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Order_Details", b =>
                {
                    b.Navigation("Order_Items");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Partener", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Product", b =>
                {
                    b.Navigation("Product_Images");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Product_Category", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("EcommerseApplication.Models.Shipper", b =>
                {
                    b.Navigation("shippingDetails");
                });

            modelBuilder.Entity("EcommerseApplication.Models.subCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("EcommerseApplication.Models.User", b =>
                {
                    b.Navigation("Order_Details");

                    b.Navigation("Shopping_Sessions");

                    b.Navigation("User_Address");

                    b.Navigation("User_Pyment");
                });
#pragma warning restore 612, 618
        }
    }
}
