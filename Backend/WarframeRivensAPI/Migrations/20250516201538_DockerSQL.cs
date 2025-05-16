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
                        name: "FK_Favoritos_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favoritos_Rivens_RivenId",
                        column: x => x.RivenId,
                        principalTable: "Rivens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ofertas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdRiven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RivenId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PrecioVenta = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Ofertas_AspNetUsers_IdComprador",
                        column: x => x.IdComprador,
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
                    IdRiven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RivenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdComprador = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdVendedor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PrecioVenta = table.Column<int>(type: "int", nullable: false),
                    FechaVenta = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    { "5a3a3046-e2ab-4752-a538-712e9bd59752", null, "admin", "ADMIN" },
                    { "9f1426a2-b729-4d30-99ae-b27efad1f1cd", null, "basic", "BASIC" },
                    { "ea2b316f-75fb-4150-a882-e7c99d52f2af", null, "confirmado", "CONFIRMADO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Icono", "LockoutEnabled", "LockoutEnd", "Nickname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SteamId", "TwoFactorEnabled", "UserName", "WarframeNick" },
                values: new object[,]
                {
                    { "356cf89a-c1ac-488c-81eb-f91ae74ec5a9", 0, "0a0933cc-310f-4ba4-a4b9-3242c0dad5b9", "confirmado@warriven.com", true, null, false, null, "Usuario confirmado", "CONFIRMADO@WARRIVEN.COM", "CONFIRMADO@WARRIVEN.COM", "AQAAAAIAAYagAAAAEOlbGEbi5soF1HG+vC5qrzmLjmHm4QNCR4QUud5ocgt22ZXt9LTAQboCN6waFcX9Tg==", null, false, "a351dc54-f5f9-41d5-9d10-c71ab21fef79", null, false, "confirmado@warriven.com", null },
                    { "4ca4b1f2-0053-4fa4-890f-4a0a1b4b6f7e", 0, "30ed7f83-7426-4b17-b548-d858bcef2d5f", "admin@warriven.com", true, null, false, null, "Administrador", "ADMIN@WARRIVEN.COM", "ADMIN@WARRIVEN.COM", "AQAAAAIAAYagAAAAEFyHRZIXm/Hwvj9tjJR7v4aZUX2WuRUCyqFxBAX5Xx0SbDnflSRmuSwTSWzDPmNXgA==", null, false, "7fd0b839-3d70-4f72-a6f8-4516ccda4999", null, false, "admin@warriven.com", null },
                    { "9f79a04d-78cb-45fc-bec7-3767d5a803e5", 0, "99c21530-a053-46a3-ad56-e2d3e3c06388", "visitante@warriven.com", true, null, false, null, "Usuario Visitante", "VISITANTE@WARRIVEN.COM", "VISITANTE@WARRIVEN.COM", "AQAAAAIAAYagAAAAEMIdYfOpd6h1RyCwrccvVdXntwDr0Uh6pFWfLkchi3syo7aSekku4T+Z10akSkTn2Q==", null, false, "fb0666f0-5022-4cb5-b555-a03e569f9f46", null, false, "visitante@warriven.com", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ea2b316f-75fb-4150-a882-e7c99d52f2af", "356cf89a-c1ac-488c-81eb-f91ae74ec5a9" },
                    { "5a3a3046-e2ab-4752-a538-712e9bd59752", "4ca4b1f2-0053-4fa4-890f-4a0a1b4b6f7e" },
                    { "9f1426a2-b729-4d30-99ae-b27efad1f1cd", "9f79a04d-78cb-45fc-bec7-3767d5a803e5" }
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
                name: "Favoritos");

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
