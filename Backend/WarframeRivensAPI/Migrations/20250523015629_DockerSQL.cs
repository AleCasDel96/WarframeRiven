using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarframeRivensAPI.Migrations
{
    /// <inheritdoc />
    public partial class DockerSQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WarframeNick = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SteamId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rivens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Arma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Polaridad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Maestria = table.Column<int>(type: "int", nullable: false),
                    Atrib1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor1 = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: false),
                    Atrib2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor2 = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: true),
                    Atrib3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valor3 = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: true),
                    DAtrib = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DValor = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: true),
                    IdPropietario = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WarUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rivens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rivens_AspNetUsers_IdPropietario",
                        column: x => x.IdPropietario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rivens_AspNetUsers_WarUserId",
                        column: x => x.WarUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ofertas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdRiven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RivenId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdVendedor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrecioVenta = table.Column<int>(type: "int", nullable: false),
                    FechaVenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disponibilidad = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ofertas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ofertas_AspNetUsers_IdVendedor",
                        column: x => x.IdVendedor,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ofertas_Rivens_RivenId",
                        column: x => x.RivenId,
                        principalTable: "Rivens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdOferta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdRiven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RivenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdVendedor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdComprador = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrecioVenta = table.Column<int>(type: "int", nullable: false),
                    FechaVenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Finalizado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_AspNetUsers_IdComprador",
                        column: x => x.IdComprador,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventas_AspNetUsers_IdVendedor",
                        column: x => x.IdVendedor,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventas_Rivens_RivenId",
                        column: x => x.RivenId,
                        principalTable: "Rivens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a3f39fb-b0db-4c77-acee-5dc313fde69a", null, "basic", "BASIC" },
                    { "42bb912a-9e7f-462b-bde1-ee14ddac9e32", null, "confirmado", "CONFIRMADO" },
                    { "bd305fdc-9f92-42ab-9954-d8d593814e1c", null, "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Icono", "LockoutEnabled", "LockoutEnd", "Nickname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SteamId", "TwoFactorEnabled", "UserName", "WarframeNick" },
                values: new object[,]
                {
                    { "13ebb16b-3761-44b7-9a60-148f0c20e616", 0, "36b59ace-7372-4246-b5c1-7ba5b5395d1c", "admin@warriven.com", true, null, false, null, "Administrador", "ADMIN@WARRIVEN.COM", "ADMIN@WARRIVEN.COM", "AQAAAAIAAYagAAAAEITvlAIojcppoWzI0oAEPXBIojw6aqZBrVJAEXN+xz7jO67oW3HhhZRJFoK7KmhScQ==", null, false, "daa8e4e3-b88e-4e51-9a1e-b59f7be41d2e", null, false, "admin@warriven.com", null },
                    { "3e4b6234-c1e6-47d4-ae5f-9373e40576ff", 0, "06a431fe-ff20-48e0-920e-f738211eb395", "confirmado@warriven.com", true, null, false, null, "Usuario confirmado", "CONFIRMADO@WARRIVEN.COM", "CONFIRMADO@WARRIVEN.COM", "AQAAAAIAAYagAAAAEAHmY34KJC7nKGie/lJo+Ql5PtGiNBNs3nWSQgGjViNbZlOzuZBzpXf+6USGyafF2A==", null, false, "2cf59bbe-a007-46aa-a7b9-c6b5e996b97a", null, false, "confirmado@warriven.com", null },
                    { "9875403d-08ad-4f90-b8bf-5eb560ca128f", 0, "e5cb5180-be2b-410d-8713-ada6bf010c8c", "visitante@warriven.com", true, null, false, null, "Usuario Visitante", "VISITANTE@WARRIVEN.COM", "VISITANTE@WARRIVEN.COM", "AQAAAAIAAYagAAAAEBRh1OoUQ2dM0jGFYpfQhQzhn7XBjw6VW3cHkcj0Brs4+DotIjliMXPnP9PW66z87Q==", null, false, "4806ea6e-2fc2-428e-98b2-58ca57663a21", null, false, "visitante@warriven.com", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "bd305fdc-9f92-42ab-9954-d8d593814e1c", "13ebb16b-3761-44b7-9a60-148f0c20e616" },
                    { "42bb912a-9e7f-462b-bde1-ee14ddac9e32", "3e4b6234-c1e6-47d4-ae5f-9373e40576ff" },
                    { "1a3f39fb-b0db-4c77-acee-5dc313fde69a", "9875403d-08ad-4f90-b8bf-5eb560ca128f" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_IdVendedor",
                table: "Ofertas",
                column: "IdVendedor");

            migrationBuilder.CreateIndex(
                name: "IX_Ofertas_RivenId",
                table: "Ofertas",
                column: "RivenId");

            migrationBuilder.CreateIndex(
                name: "IX_Rivens_IdPropietario",
                table: "Rivens",
                column: "IdPropietario");

            migrationBuilder.CreateIndex(
                name: "IX_Rivens_WarUserId",
                table: "Rivens",
                column: "WarUserId");

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Ofertas");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Rivens");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
