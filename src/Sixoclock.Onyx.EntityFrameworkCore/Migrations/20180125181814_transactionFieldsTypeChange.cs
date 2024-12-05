using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Sixoclock.Onyx.Migrations
{
    public partial class transactionFieldsTypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "KwhDelivered",
                table: "Transactions",
                type: "real",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<float>(
                name: "Value",
                table: "MeterValues",
                type: "real",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "KwhDelivered",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "MeterValues",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
