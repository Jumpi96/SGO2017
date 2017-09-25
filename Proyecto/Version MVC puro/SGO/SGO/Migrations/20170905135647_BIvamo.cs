using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SGO.Migrations
{
    public partial class BIvamo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Obra_Usuario_ModFechaID",
                table: "Obra");

            migrationBuilder.DropIndex(
                name: "IX_Obra_ModFechaID",
                table: "Obra");

            migrationBuilder.DropColumn(
                name: "ModFechaID",
                table: "Obra");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModFecha",
                table: "Obra",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModFecha",
                table: "Obra");

            migrationBuilder.AddColumn<int>(
                name: "ModFechaID",
                table: "Obra",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Obra_ModFechaID",
                table: "Obra",
                column: "ModFechaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Obra_Usuario_ModFechaID",
                table: "Obra",
                column: "ModFechaID",
                principalTable: "Usuario",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
