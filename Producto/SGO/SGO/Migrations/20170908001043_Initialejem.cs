using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SGO.Migrations
{
    public partial class Initialejem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimiento_SubItem_DetalleSubItemID",
                table: "Movimiento");

            migrationBuilder.DropIndex(
                name: "IX_Movimiento_DetalleSubItemID",
                table: "Movimiento");

            migrationBuilder.DropColumn(
                name: "DetalleSubItemID",
                table: "Movimiento");

            migrationBuilder.AddColumn<int>(
                name: "SubItemID",
                table: "Movimiento",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_SubItemID",
                table: "Movimiento",
                column: "SubItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimiento_SubItem_SubItemID",
                table: "Movimiento",
                column: "SubItemID",
                principalTable: "SubItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimiento_SubItem_SubItemID",
                table: "Movimiento");

            migrationBuilder.DropIndex(
                name: "IX_Movimiento_SubItemID",
                table: "Movimiento");

            migrationBuilder.DropColumn(
                name: "SubItemID",
                table: "Movimiento");

            migrationBuilder.AddColumn<int>(
                name: "DetalleSubItemID",
                table: "Movimiento",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_DetalleSubItemID",
                table: "Movimiento",
                column: "DetalleSubItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimiento_SubItem_DetalleSubItemID",
                table: "Movimiento",
                column: "DetalleSubItemID",
                principalTable: "SubItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
