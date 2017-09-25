using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SGO.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimiento_DetalleSubItem_DetalleSubItemID",
                table: "Movimiento");

            migrationBuilder.AddColumn<int>(
                name: "ObraID",
                table: "Movimiento",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_ObraID",
                table: "Movimiento",
                column: "ObraID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimiento_SubItem_DetalleSubItemID",
                table: "Movimiento",
                column: "DetalleSubItemID",
                principalTable: "SubItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movimiento_Obra_ObraID",
                table: "Movimiento",
                column: "ObraID",
                principalTable: "Obra",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movimiento_SubItem_DetalleSubItemID",
                table: "Movimiento");

            migrationBuilder.DropForeignKey(
                name: "FK_Movimiento_Obra_ObraID",
                table: "Movimiento");

            migrationBuilder.DropIndex(
                name: "IX_Movimiento_ObraID",
                table: "Movimiento");

            migrationBuilder.DropColumn(
                name: "ObraID",
                table: "Movimiento");

            migrationBuilder.AddForeignKey(
                name: "FK_Movimiento_DetalleSubItem_DetalleSubItemID",
                table: "Movimiento",
                column: "DetalleSubItemID",
                principalTable: "DetalleSubItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
