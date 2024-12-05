using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Sixoclock.Onyx.Migrations
{
    public partial class AbpUser_change_for_overview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalChargeTime",
                table: "AbpUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalCo2Saved",
                table: "AbpUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalChargeTime",
                table: "AbpUsers");

            migrationBuilder.DropColumn(
                name: "TotalCo2Saved",
                table: "AbpUsers");
        }
    }
}
