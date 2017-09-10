using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SGO.Migrations
{
    public partial class Complete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receptor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receptor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rubro",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numeracion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rubro", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TipoItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Caracter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoItem", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Unidad",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidad", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SubRubro",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RubroID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRubro", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubRubro_Rubro_RubroID",
                        column: x => x.RubroID,
                        principalTable: "Rubro",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Obra",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coeficiente = table.Column<double>(type: "float", nullable: false),
                    InsFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModFechaID = table.Column<int>(type: "int", nullable: true),
                    ModUsuarioID = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obra", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Obra_Usuario_ModFechaID",
                        column: x => x.ModFechaID,
                        principalTable: "Usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Obra_Usuario_ModUsuarioID",
                        column: x => x.ModUsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubRubroID = table.Column<int>(type: "int", nullable: true),
                    UnidadID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Item_SubRubro_SubRubroID",
                        column: x => x.SubRubroID,
                        principalTable: "SubRubro",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Item_Unidad_UnidadID",
                        column: x => x.UnidadID,
                        principalTable: "Unidad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemID = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrecioUnitario = table.Column<double>(type: "float", nullable: false),
                    TipoItemID = table.Column<int>(type: "int", nullable: true),
                    UnidadID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubItem_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubItem_TipoItem_TipoItemID",
                        column: x => x.TipoItemID,
                        principalTable: "TipoItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubItem_Unidad_UnidadID",
                        column: x => x.UnidadID,
                        principalTable: "Unidad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleSubItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<double>(type: "float", nullable: false),
                    ObraID = table.Column<int>(type: "int", nullable: true),
                    PrecioUnitario = table.Column<double>(type: "float", nullable: false),
                    SubItemID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleSubItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DetalleSubItem_Obra_ObraID",
                        column: x => x.ObraID,
                        principalTable: "Obra",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetalleSubItem_SubItem_SubItemID",
                        column: x => x.SubItemID,
                        principalTable: "SubItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Movimiento",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cantidad = table.Column<double>(type: "float", nullable: false),
                    DetalleSubItemID = table.Column<int>(type: "int", nullable: true),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModFecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModUsuarioID = table.Column<int>(type: "int", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceptorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimiento", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Movimiento_DetalleSubItem_DetalleSubItemID",
                        column: x => x.DetalleSubItemID,
                        principalTable: "DetalleSubItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimiento_Usuario_ModUsuarioID",
                        column: x => x.ModUsuarioID,
                        principalTable: "Usuario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimiento_Receptor_ReceptorID",
                        column: x => x.ReceptorID,
                        principalTable: "Receptor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleSubItem_ObraID",
                table: "DetalleSubItem",
                column: "ObraID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleSubItem_SubItemID",
                table: "DetalleSubItem",
                column: "SubItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_SubRubroID",
                table: "Item",
                column: "SubRubroID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_UnidadID",
                table: "Item",
                column: "UnidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_DetalleSubItemID",
                table: "Movimiento",
                column: "DetalleSubItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_ModUsuarioID",
                table: "Movimiento",
                column: "ModUsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_ReceptorID",
                table: "Movimiento",
                column: "ReceptorID");

            migrationBuilder.CreateIndex(
                name: "IX_Obra_ModFechaID",
                table: "Obra",
                column: "ModFechaID");

            migrationBuilder.CreateIndex(
                name: "IX_Obra_ModUsuarioID",
                table: "Obra",
                column: "ModUsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_SubItem_ItemID",
                table: "SubItem",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_SubItem_TipoItemID",
                table: "SubItem",
                column: "TipoItemID");

            migrationBuilder.CreateIndex(
                name: "IX_SubItem_UnidadID",
                table: "SubItem",
                column: "UnidadID");

            migrationBuilder.CreateIndex(
                name: "IX_SubRubro_RubroID",
                table: "SubRubro",
                column: "RubroID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimiento");

            migrationBuilder.DropTable(
                name: "DetalleSubItem");

            migrationBuilder.DropTable(
                name: "Receptor");

            migrationBuilder.DropTable(
                name: "Obra");

            migrationBuilder.DropTable(
                name: "SubItem");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "TipoItem");

            migrationBuilder.DropTable(
                name: "SubRubro");

            migrationBuilder.DropTable(
                name: "Unidad");

            migrationBuilder.DropTable(
                name: "Rubro");
        }
    }
}
