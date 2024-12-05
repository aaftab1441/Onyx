using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Sixoclock.Onyx.Migrations
{
    public partial class Bill_Migration_Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BillingStatuses_BillingStatusId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BillingTypes_BillingTypeId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillStatusId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillTypeId",
                table: "Bills");

            migrationBuilder.AlterColumn<int>(
                name: "BillingTypeId",
                table: "Bills",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BillingStatusId",
                table: "Bills",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BillingStatuses_BillingStatusId",
                table: "Bills",
                column: "BillingStatusId",
                principalTable: "BillingStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BillingTypes_BillingTypeId",
                table: "Bills",
                column: "BillingTypeId",
                principalTable: "BillingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BillingStatuses_BillingStatusId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_BillingTypes_BillingTypeId",
                table: "Bills");

            migrationBuilder.AlterColumn<int>(
                name: "BillingTypeId",
                table: "Bills",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BillingStatusId",
                table: "Bills",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BillStatusId",
                table: "Bills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BillTypeId",
                table: "Bills",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BillingStatuses_BillingStatusId",
                table: "Bills",
                column: "BillingStatusId",
                principalTable: "BillingStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_BillingTypes_BillingTypeId",
                table: "Bills",
                column: "BillingTypeId",
                principalTable: "BillingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
