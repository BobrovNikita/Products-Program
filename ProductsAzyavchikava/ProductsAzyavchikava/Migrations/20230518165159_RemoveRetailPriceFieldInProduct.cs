using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsAzyavchikava.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRetailPriceFieldInProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Retail_Price",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Retail_Price",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
