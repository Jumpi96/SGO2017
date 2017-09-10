using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SGO.Migrations
{
    public partial class DalePelotudo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleSubItem_SubItem_SubItemID",
                table: "DetalleSubItem");

            migrationBuilder.DropIndex(
                name: "IX_DetalleSubItem_SubItemID",
                table: "DetalleSubItem");

            migrationBuilder.DropColumn(
                name: "SubItemID",
                table: "DetalleSubItem");

            migrationBuilder.AddColumn<int>(
                name: "SubItemDeItemID",
                table: "DetalleSubItem",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleSubItem_SubItemDeItemID",
                table: "DetalleSubItem",
                column: "SubItemDeItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleSubItem_SubItemDeItem_SubItemDeItemID",
                table: "DetalleSubItem",
                column: "SubItemDeItemID",
                principalTable: "SubItemDeItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleSubItem_SubItemDeItem_SubItemDeItemID",
                table: "DetalleSubItem");

            migrationBuilder.DropIndex(
                name: "IX_DetalleSubItem_SubItemDeItemID",
                table: "DetalleSubItem");

            migrationBuilder.DropColumn(
                name: "SubItemDeItemID",
                table: "DetalleSubItem");

            migrationBuilder.AddColumn<int>(
                name: "SubItemID",
                table: "DetalleSubItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetalleSubItem_SubItemID",
                table: "DetalleSubItem",
                column: "SubItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleSubItem_SubItem_SubItemID",
                table: "DetalleSubItem",
                column: "SubItemID",
                principalTable: "SubItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
