using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Sixoclock.Onyx.Migrations
{
    public partial class CustomerTenantRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerTenantId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerTenantId",
                table: "Customers",
                column: "CustomerTenantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AbpTenants_CustomerTenantId",
                table: "Customers",
                column: "CustomerTenantId",
                principalTable: "AbpTenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AbpTenants_CustomerTenantId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerTenantId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerTenantId",
                table: "Customers");
        }
    }
}
