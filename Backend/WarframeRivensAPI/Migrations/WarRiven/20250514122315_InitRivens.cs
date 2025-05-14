using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarframeRivensAPI.Migrations.WarRiven
{
    /// <inheritdoc />
    public partial class InitRivens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WarframeNick = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SteamId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rivens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Arma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPropietario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Atrib1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor1 = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: false),
                    Atrib2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor2 = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: true),
                    Atrib3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor3 = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: true),
                    DAtrib = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DValor = table.Column<decimal>(type: "decimal(10,3)", precision: 10, scale: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rivens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rivens_WarUser_IdPropietario",
                        column: x => x.IdPropietario,
                        principalTable: "WarUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favoritos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdRiven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RivenId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IdUser = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favoritos_Rivens_RivenId",
                        column: x => x.RivenId,
                        principalTable: "Rivens",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favoritos_WarUser_IdUser",
                        column: x => x.IdUser,
                        principalTable: "WarUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ofertas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdRiven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RivenId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PrecioVenta = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IdComprador = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaVenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disponibilidad = table.Column<bool>(type: "bit", nullable: false),
                    Partida = table.Column<bool>(type: "bit", nullable: false),
                    Destino = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ofertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ofertas_Rivens_RivenId",
                        column: x => x.RivenId,
                        principalTable: "Rivens",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ofertas_WarUser_IdComprador",
                        column: x => x.IdComprador,
                        principalTable: "WarUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdRiven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RivenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdComprador = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdVendedor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrecioVenta = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    FechaVenta = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_Rivens_RivenId",
                        column: x => x.RivenId,
                        principalTable: "Rivens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_WarUser_IdComprador",
                        column: x => x.IdComprador,
                        principalTable: "WarUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventas_WarUser_IdVendedor",
                        column: x => x.IdVendedor,
                        principalTable: "WarUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_IdUser",
                table: "Favoritos",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_RivenId",
                table: "Favoritos",
                column: "RivenId");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_IdComprador",
                table: "Ofertas",
                column: "IdComprador");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_RivenId",
                table: "Ofertas",
                column: "RivenId");

            migrationBuilder.CreateIndex(
                name: "IX_Rivens_IdPropietario",
                table: "Rivens",
                column: "IdPropietario");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdComprador",
                table: "Ventas",
                column: "IdComprador");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdVendedor",
                table: "Ventas",
                column: "IdVendedor");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_RivenId",
                table: "Ventas",
                column: "RivenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoritos");

            migrationBuilder.DropTable(
                name: "Ofertas");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Rivens");

            migrationBuilder.DropTable(
                name: "WarUser");
        }
    }
}
