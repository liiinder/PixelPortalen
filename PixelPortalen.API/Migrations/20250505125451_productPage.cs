using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelPortalen.API.Migrations
{
    /// <inheritdoc />
    public partial class productPage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CustomerRatings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRatings_ProductId",
                table: "CustomerRatings",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerRatings_Products_ProductId",
                table: "CustomerRatings",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerRatings_Products_ProductId",
                table: "CustomerRatings");

            migrationBuilder.DropIndex(
                name: "IX_CustomerRatings_ProductId",
                table: "CustomerRatings");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CustomerRatings");
        }
    }
}
