using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PixelPortalen.API.Migrations
{
    /// <inheritdoc />
    public partial class changedSomeColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreProduct_Genre_GenresGenreId",
                table: "GenreProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreProduct_Products_ProductsProductId",
                table: "GenreProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_ProductCategoryCategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ProductCategoryCategoryId",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_ProductCategoryCategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Order",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductsProductId",
                table: "GenreProduct",
                newName: "ProductsId");

            migrationBuilder.RenameColumn(
                name: "GenresGenreId",
                table: "GenreProduct",
                newName: "GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_GenreProduct_ProductsProductId",
                table: "GenreProduct",
                newName: "IX_GenreProduct_ProductsId");

            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Genre",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "Genre",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Genre_GenreName",
                table: "Genre",
                newName: "IX_Genre_Name");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Customer",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Category",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Category",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Category_CategoryName",
                table: "Category",
                newName: "IX_Category_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreProduct_Genre_GenresId",
                table: "GenreProduct",
                column: "GenresId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreProduct_Products_ProductsId",
                table: "GenreProduct",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Category_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreProduct_Genre_GenresId",
                table: "GenreProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreProduct_Products_ProductsId",
                table: "GenreProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "ProductCategoryCategoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_ProductCategoryCategoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Order",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "ProductsId",
                table: "GenreProduct",
                newName: "ProductsProductId");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "GenreProduct",
                newName: "GenresGenreId");

            migrationBuilder.RenameIndex(
                name: "IX_GenreProduct_ProductsId",
                table: "GenreProduct",
                newName: "IX_GenreProduct_ProductsProductId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Genre",
                newName: "GenreName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Genre",
                newName: "GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Genre_Name",
                table: "Genre",
                newName: "IX_Genre_GenreName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Customer",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Category",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Category",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Name",
                table: "Category",
                newName: "IX_Category_CategoryName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                columns: new[] { "OrderId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GenreProduct_Genre_GenresGenreId",
                table: "GenreProduct",
                column: "GenresGenreId",
                principalTable: "Genre",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreProduct_Products_ProductsProductId",
                table: "GenreProduct",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Category_ProductCategoryCategoryId",
                table: "Products",
                column: "ProductCategoryCategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
