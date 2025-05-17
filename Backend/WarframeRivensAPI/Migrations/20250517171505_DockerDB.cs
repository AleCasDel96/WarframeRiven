using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarframeRivensAPI.Migrations
{
    /// <inheritdoc />
    public partial class DockerDB : Migration
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
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RivenId = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    IdRiven = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RivenId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdVendedor = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdComprador = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    { "3b9b4d06-155b-4bc1-8f84-16844712cbdb", null, "confirmado", "CONFIRMADO" },
                    { "857386d0-7bf6-421a-8d93-fcf566089fc9", null, "basic", "BASIC" },
                    { "fdb47132-4b6c-44e1-94d9-5188a7d3b6ba", null, "admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Icono", "LockoutEnabled", "LockoutEnd", "Nickname", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "SteamId", "TwoFactorEnabled", "UserName", "WarframeNick" },
                values: new object[,]
                {
                    { "39d6cfde-e971-499a-acb1-88c61bf8655b", 0, "7a5c0832-d185-4998-9dcd-c5193b1af9f9", "visitante@warriven.com", true, null, false, null, "Usuario Visitante", "VISITANTE@WARRIVEN.COM", "VISITANTE@WARRIVEN.COM", "AQAAAAIAAYagAAAAEOGKwKpToXEgbmJGe3vZbH8DLSMo8DAR8Ko+mnzgmlojYb1WPbSNB71BgYuMluc11g==", null, false, "38b7a1f5-509c-43eb-94e3-a5ed5f568ccf", null, false, "visitante@warriven.com", null },
                    { "87c31868-6577-4930-98f6-b61e8d6d01ab", 0, "b2083ddd-43b8-4cbf-b1ee-72b72fde26ab", "admin@warriven.com", true, null, false, null, "Administrador", "ADMIN@WARRIVEN.COM", "ADMIN@WARRIVEN.COM", "AQAAAAIAAYagAAAAEAG8rsfz2wGu4U3iz6vVXeUPYgv8ylsKEaSGkVqZE9PCcSIvc6iBPi1EY7M2nCDniQ==", null, false, "d5caa715-dd7f-4f4b-a2b0-dbdad9af1aec", null, false, "admin@warriven.com", null },
                    { "a3bf9d97-4643-4c82-8112-043b547bfeab", 0, "bbfffbd2-88e1-4239-8226-141741eba2fc", "confirmado@warriven.com", true, null, false, null, "Usuario confirmado", "CONFIRMADO@WARRIVEN.COM", "CONFIRMADO@WARRIVEN.COM", "AQAAAAIAAYagAAAAEGbdd0NuvBVSuT0D08Y4X+rWU6fIBIGG7NxKiaEmpr4CsMmHK2LruP9GFRXY6YoS0Q==", null, false, "6c463d86-de4f-4afa-8661-c47fd0e1f1b0", null, false, "confirmado@warriven.com", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "857386d0-7bf6-421a-8d93-fcf566089fc9", "39d6cfde-e971-499a-acb1-88c61bf8655b" },
                    { "fdb47132-4b6c-44e1-94d9-5188a7d3b6ba", "87c31868-6577-4930-98f6-b61e8d6d01ab" },
                    { "3b9b4d06-155b-4bc1-8f84-16844712cbdb", "a3bf9d97-4643-4c82-8112-043b547bfeab" }
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
