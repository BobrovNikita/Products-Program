using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsAzyavchikava.Migrations
{
    /// <inheritdoc />
    public partial class DeleteNdsFromRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost_With_NDS",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Nds_Sum",
                table: "Requests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cost_With_NDS",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Nds_Sum",
                table: "Requests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
