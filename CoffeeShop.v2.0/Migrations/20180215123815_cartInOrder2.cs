using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoffeeShop.v2.Migrations
{
    public partial class cartInOrder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coffees_Orders_OrderId",
                table: "Coffees");

            migrationBuilder.DropIndex(
                name: "IX_Coffees_OrderId",
                table: "Coffees");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Coffees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Coffees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coffees_OrderId",
                table: "Coffees",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coffees_Orders_OrderId",
                table: "Coffees",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
