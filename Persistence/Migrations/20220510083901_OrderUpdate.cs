using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class OrderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(8126),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 495, DateTimeKind.Local).AddTicks(3238));

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(3466));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemove",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InsertTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(6345));

            migrationBuilder.AddColumn<bool>(
                name: "IsRemove",
                table: "OrderItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemoveTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateTime",
                table: "OrderItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(819),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 495, DateTimeKind.Local).AddTicks(981));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(3061),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 494, DateTimeKind.Local).AddTicks(3672));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(8503),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 494, DateTimeKind.Local).AddTicks(8628));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(6446),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 494, DateTimeKind.Local).AddTicks(6671));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(717),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 494, DateTimeKind.Local).AddTicks(1402));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 86, DateTimeKind.Local).AddTicks(6274),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 493, DateTimeKind.Local).AddTicks(6917));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 86, DateTimeKind.Local).AddTicks(8416),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 493, DateTimeKind.Local).AddTicks(9042));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsRemove",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "InsertTime",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "IsRemove",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "RemoveTime",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "UpdateTime",
                table: "OrderItems");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "UserAddresses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 495, DateTimeKind.Local).AddTicks(3238),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(8126));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 495, DateTimeKind.Local).AddTicks(981),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 88, DateTimeKind.Local).AddTicks(819));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 494, DateTimeKind.Local).AddTicks(3672),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(3061));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemImage",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 494, DateTimeKind.Local).AddTicks(8628),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(8503));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogItemFeature",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 494, DateTimeKind.Local).AddTicks(6671),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(6446));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "CatalogBrand",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 494, DateTimeKind.Local).AddTicks(1402),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 87, DateTimeKind.Local).AddTicks(717));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 493, DateTimeKind.Local).AddTicks(6917),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 86, DateTimeKind.Local).AddTicks(6274));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertTime",
                table: "BasketItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 10, 11, 40, 51, 493, DateTimeKind.Local).AddTicks(9042),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 10, 13, 9, 1, 86, DateTimeKind.Local).AddTicks(8416));
        }
    }
}
