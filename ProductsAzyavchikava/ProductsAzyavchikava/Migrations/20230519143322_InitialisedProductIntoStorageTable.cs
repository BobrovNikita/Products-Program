using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsAzyavchikava.Migrations
{
    /// <inheritdoc />
    public partial class InitialisedProductIntoStorageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductIntoStorages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIntoStorages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductIntoStorages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId");
                    table.ForeignKey(
                        name: "FK_ProductIntoStorages_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "StorageId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductIntoStorages_ProductId",
                table: "ProductIntoStorages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIntoStorages_StorageId",
                table: "ProductIntoStorages",
                column: "StorageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductIntoStorages");
        }
    }
}
