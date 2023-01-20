using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsAzyavchikava.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product_Types",
                columns: table => new
                {
                    ProductTypeId = table.Column<Guid>(name: "Product_TypeId", type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(name: "Product_Name", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TypeName = table.Column<string>(name: "Type_Name", type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Types", x => x.ProductTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    ShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopNumber = table.Column<int>(name: "Shop_Number", type: "int", nullable: false),
                    ShopName = table.Column<string>(name: "Shop_Name", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShopAdress = table.Column<string>(name: "Shop_Adress", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShopPhone = table.Column<string>(name: "Shop_Phone", type: "nvarchar(max)", nullable: false),
                    ShopArea = table.Column<string>(name: "Shop_Area", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.ShopId);
                });

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StorageNumber = table.Column<int>(name: "Storage_Number", type: "int", nullable: false),
                    StorageAdress = table.Column<string>(name: "Storage_Adress", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StoragePurpose = table.Column<string>(name: "Storage_Purpose", type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.StorageId);
                });

            migrationBuilder.CreateTable(
                name: "Shop_Types",
                columns: table => new
                {
                    ShopTypeId = table.Column<Guid>(name: "Shop_TypeId", type: "uniqueidentifier", nullable: false),
                    ShopCount = table.Column<int>(name: "Shop_Count", type: "int", nullable: false),
                    ProductTypeId = table.Column<Guid>(name: "Product_TypeId", type: "uniqueidentifier", nullable: false),
                    ShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shop_Types", x => x.ShopTypeId);
                    table.ForeignKey(
                        name: "FK_Shop_Types_Product_Types_Product_TypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "Product_Types",
                        principalColumn: "Product_TypeId");
                    table.ForeignKey(
                        name: "FK_Shop_Types_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VendorCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Hatch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    NDS = table.Column<int>(type: "int", nullable: false),
                    Markup = table.Column<int>(type: "int", nullable: false),
                    RetailPrice = table.Column<int>(name: "Retail_Price", type: "int", nullable: false),
                    Production = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WeightPerPrice = table.Column<int>(name: "Weight_Per_Price", type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Availability = table.Column<bool>(type: "bit", nullable: false),
                    ProductTypeId = table.Column<Guid>(name: "Product_TypeId", type: "uniqueidentifier", nullable: false),
                    StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Product_Types_Product_TypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "Product_Types",
                        principalColumn: "Product_TypeId");
                    table.ForeignKey(
                        name: "FK_Products_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "StorageId");
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductsCount = table.Column<int>(name: "Products_Count", type: "int", nullable: false),
                    RequestCost = table.Column<int>(name: "Request_Cost", type: "int", nullable: false),
                    NdsSum = table.Column<int>(name: "Nds_Sum", type: "int", nullable: false),
                    CostWithNDS = table.Column<int>(name: "Cost_With_NDS", type: "int", nullable: false),
                    NumberPackages = table.Column<int>(name: "Number_Packages", type: "int", nullable: false),
                    Weigh = table.Column<int>(type: "int", nullable: false),
                    Car = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Driver = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.RequestId);
                    table.ForeignKey(
                        name: "FK_Requests_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId");
                    table.ForeignKey(
                        name: "FK_Requests_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "StorageId");
                });

            migrationBuilder.CreateTable(
                name: "ProductIntoShops",
                columns: table => new
                {
                    ProductIntoShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIntoShops", x => x.ProductIntoShopId);
                    table.ForeignKey(
                        name: "FK_ProductIntoShops_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_ProductIntoShops_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "ShopId");
                });

            migrationBuilder.CreateTable(
                name: "CompositionRequests",
                columns: table => new
                {
                    CompositionRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Sum = table.Column<int>(type: "int", nullable: false),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompositionRequests", x => x.CompositionRequestId);
                    table.ForeignKey(
                        name: "FK_CompositionRequests_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_CompositionRequests_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "RequestId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompositionRequests_ProductId",
                table: "CompositionRequests",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CompositionRequests_RequestId",
                table: "CompositionRequests",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIntoShops_ProductId",
                table: "ProductIntoShops",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIntoShops_ShopId",
                table: "ProductIntoShops",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Product_TypeId",
                table: "Products",
                column: "Product_TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StorageId",
                table: "Products",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ShopId",
                table: "Requests",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_StorageId",
                table: "Requests",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_Types_Product_TypeId",
                table: "Shop_Types",
                column: "Product_TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shop_Types_ShopId",
                table: "Shop_Types",
                column: "ShopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompositionRequests");

            migrationBuilder.DropTable(
                name: "ProductIntoShops");

            migrationBuilder.DropTable(
                name: "Shop_Types");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Product_Types");

            migrationBuilder.DropTable(
                name: "Storages");
        }
    }
}
