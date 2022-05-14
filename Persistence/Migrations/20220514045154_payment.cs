using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 715, DateTimeKind.Local).AddTicks(4878),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(8126));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 715, DateTimeKind.Local).AddTicks(963),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(3466));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 715, DateTimeKind.Local).AddTicks(3243),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(6345));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 714, DateTimeKind.Local).AddTicks(8535),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(819));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 714, DateTimeKind.Local).AddTicks(1900),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(3061));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 714, DateTimeKind.Local).AddTicks(6500),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(8503));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 714, DateTimeKind.Local).AddTicks(4676),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(6446));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 713, DateTimeKind.Local).AddTicks(9590),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(717));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 713, DateTimeKind.Local).AddTicks(5655),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 86, DateTimeKind.Local).AddTicks(6274));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 713, DateTimeKind.Local).AddTicks(7554),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 86, DateTimeKind.Local).AddTicks(8416));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(8126),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 715, DateTimeKind.Local).AddTicks(4878));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(3466),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 715, DateTimeKind.Local).AddTicks(963));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(6345),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 715, DateTimeKind.Local).AddTicks(3243));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(819),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 714, DateTimeKind.Local).AddTicks(8535));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(3061),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 714, DateTimeKind.Local).AddTicks(1900));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(8503),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 714, DateTimeKind.Local).AddTicks(6500));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(6446),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 714, DateTimeKind.Local).AddTicks(4676));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(717),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 713, DateTimeKind.Local).AddTicks(9590));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 86, DateTimeKind.Local).AddTicks(6274),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 713, DateTimeKind.Local).AddTicks(5655));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 86, DateTimeKind.Local).AddTicks(8416),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 14, 9, 21, 53, 713, DateTimeKind.Local).AddTicks(7554));
        }
    }
}
