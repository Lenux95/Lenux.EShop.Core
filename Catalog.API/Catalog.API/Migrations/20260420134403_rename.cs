using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_CatalogBrands_CatalogBrandId",
                table: "CatalogItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CatalogItems_CatalogTypes_CatalogTypeId",
                table: "CatalogItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogItems",
                table: "CatalogItems");

            migrationBuilder.RenameTable(
                name: "CatalogItems",
                newName: "Products");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "CatalogTypes",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CatalogTypes",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "CatalogBrands",
                newName: "brand");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CatalogBrands",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "RestockThreshold",
                table: "Products",
                newName: "restock_threshold");

            migrationBuilder.RenameColumn(
                name: "PictureFileName",
                table: "Products",
                newName: "picture_file_name");

            migrationBuilder.RenameColumn(
                name: "MaxStockThreshold",
                table: "Products",
                newName: "max_stock_threshold");

            migrationBuilder.RenameColumn(
                name: "AvailableStock",
                table: "Products",
                newName: "available_stock");

            migrationBuilder.RenameIndex(
                name: "IX_CatalogItems_CatalogTypeId",
                table: "Products",
                newName: "IX_Products_CatalogTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_CatalogItems_CatalogBrandId",
                table: "Products",
                newName: "IX_Products_CatalogBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CatalogBrands_CatalogBrandId",
                table: "Products",
                column: "CatalogBrandId",
                principalTable: "CatalogBrands",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CatalogTypes_CatalogTypeId",
                table: "Products",
                column: "CatalogTypeId",
                principalTable: "CatalogTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CatalogBrands_CatalogBrandId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_CatalogTypes_CatalogTypeId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "CatalogItems");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "CatalogTypes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CatalogTypes",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "brand",
                table: "CatalogBrands",
                newName: "Brand");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CatalogBrands",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "CatalogItems",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "CatalogItems",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "CatalogItems",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "restock_threshold",
                table: "CatalogItems",
                newName: "RestockThreshold");

            migrationBuilder.RenameColumn(
                name: "picture_file_name",
                table: "CatalogItems",
                newName: "PictureFileName");

            migrationBuilder.RenameColumn(
                name: "max_stock_threshold",
                table: "CatalogItems",
                newName: "MaxStockThreshold");

            migrationBuilder.RenameColumn(
                name: "available_stock",
                table: "CatalogItems",
                newName: "AvailableStock");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CatalogTypeId",
                table: "CatalogItems",
                newName: "IX_CatalogItems_CatalogTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CatalogBrandId",
                table: "CatalogItems",
                newName: "IX_CatalogItems_CatalogBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogItems",
                table: "CatalogItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_CatalogBrands_CatalogBrandId",
                table: "CatalogItems",
                column: "CatalogBrandId",
                principalTable: "CatalogBrands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CatalogItems_CatalogTypes_CatalogTypeId",
                table: "CatalogItems",
                column: "CatalogTypeId",
                principalTable: "CatalogTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
