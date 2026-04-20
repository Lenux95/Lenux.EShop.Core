using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.API.Migrations
{
    /// <inheritdoc />
    public partial class renameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogTypes",
                table: "CatalogTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogBrands",
                table: "CatalogBrands");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "catalog_item");

            migrationBuilder.RenameTable(
                name: "CatalogTypes",
                newName: "catalog_type");

            migrationBuilder.RenameTable(
                name: "CatalogBrands",
                newName: "catalog_brand");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "catalog_item",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "CatalogTypeId",
                table: "catalog_item",
                newName: "catalog_type_id");

            migrationBuilder.RenameColumn(
                name: "CatalogBrandId",
                table: "catalog_item",
                newName: "catalog_brand_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CatalogTypeId",
                table: "catalog_item",
                newName: "IX_catalog_item_catalog_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CatalogBrandId",
                table: "catalog_item",
                newName: "IX_catalog_item_catalog_brand_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_catalog_item",
                table: "catalog_item",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_catalog_type",
                table: "catalog_type",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_catalog_brand",
                table: "catalog_brand",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_catalog_item_catalog_brand_catalog_brand_id",
                table: "catalog_item",
                column: "catalog_brand_id",
                principalTable: "catalog_brand",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_catalog_item_catalog_type_catalog_type_id",
                table: "catalog_item",
                column: "catalog_type_id",
                principalTable: "catalog_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_catalog_item_catalog_brand_catalog_brand_id",
                table: "catalog_item");

            migrationBuilder.DropForeignKey(
                name: "FK_catalog_item_catalog_type_catalog_type_id",
                table: "catalog_item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_catalog_type",
                table: "catalog_type");

            migrationBuilder.DropPrimaryKey(
                name: "PK_catalog_item",
                table: "catalog_item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_catalog_brand",
                table: "catalog_brand");

            migrationBuilder.RenameTable(
                name: "catalog_type",
                newName: "CatalogTypes");

            migrationBuilder.RenameTable(
                name: "catalog_item",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "catalog_brand",
                newName: "CatalogBrands");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "catalog_type_id",
                table: "Products",
                newName: "CatalogTypeId");

            migrationBuilder.RenameColumn(
                name: "catalog_brand_id",
                table: "Products",
                newName: "CatalogBrandId");

            migrationBuilder.RenameIndex(
                name: "IX_catalog_item_catalog_type_id",
                table: "Products",
                newName: "IX_Products_CatalogTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_catalog_item_catalog_brand_id",
                table: "Products",
                newName: "IX_Products_CatalogBrandId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogTypes",
                table: "CatalogTypes",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogBrands",
                table: "CatalogBrands",
                column: "id");

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
    }
}
