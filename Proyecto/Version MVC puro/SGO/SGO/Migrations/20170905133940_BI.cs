using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SGO.Migrations
{
    public partial class BI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mail",
                table: "Usuario");

            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Finalizada",
                table: "Obra",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "Finalizada",
                table: "Obra");

            migrationBuilder.AddColumn<string>(
                name: "Mail",
                table: "Usuario",
                nullable: true);
        }
    }
}
