using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SGO.Migrations
{
    public partial class UpdateManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubItem_Item_ItemID",
                table: "SubItem");

            migrationBuilder.DropIndex(
                name: "IX_SubItem_ItemID",
                table: "SubItem");

            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "SubItem");

            migrationBuilder.CreateTable(
                name: "SubItemDeItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemID = table.Column<int>(type: "int", nullable: true),
                    SubItemID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubItemDeItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubItemDeItem_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubItemDeItem_SubItem_SubItemID",
                        column: x => x.SubItemID,
                        principalTable: "SubItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubItemDeItem_ItemID",
                table: "SubItemDeItem",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_SubItemDeItem_SubItemID",
                table: "SubItemDeItem",
                column: "SubItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubItemDeItem");

            migrationBuilder.AddColumn<int>(
                name: "ItemID",
                table: "SubItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubItem_ItemID",
                table: "SubItem",
                column: "ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_SubItem_Item_ItemID",
                table: "SubItem",
                column: "ItemID",
                principalTable: "Item",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
