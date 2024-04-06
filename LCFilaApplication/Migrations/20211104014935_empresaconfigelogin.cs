using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCFilaApplication.Migrations
{
    public partial class empresaconfigelogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Pessoas",
                type: "varchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EmpresaLoginId",
                table: "Filas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmpresaConfiguracaos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NomeDaEmpresa = table.Column<string>(type: "varchar(100)", nullable: false),
                    LinkLogodaEmpresa = table.Column<string>(type: "varchar(150)", nullable: true),
                    CorPrincipalEmpresa = table.Column<string>(type: "varchar(50)", nullable: true),
                    CorSegundariaEmpresa = table.Column<string>(type: "varchar(200)", nullable: true),
                    FooterEmpresa = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaConfiguracaos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmpresaLogins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NomeEmpresa = table.Column<string>(type: "varchar(100)", nullable: false),
                    CNPJ = table.Column<string>(type: "varchar(14)", nullable: false),
                    IdAdminEmpresa = table.Column<Guid>(nullable: false),
                    EmpresaConfiguracaoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpresaLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmpresaLogins_EmpresaConfiguracaos_EmpresaConfiguracaoId",
                        column: x => x.EmpresaConfiguracaoId,
                        principalTable: "EmpresaConfiguracaos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(100)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(100)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(100)", nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(100)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "varchar(100)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "varchar(100)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    EmpresaLoginId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentityUser_EmpresaLogins_EmpresaLoginId",
                        column: x => x.EmpresaLoginId,
                        principalTable: "EmpresaLogins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filas_EmpresaLoginId",
                table: "Filas",
                column: "EmpresaLoginId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpresaLogins_EmpresaConfiguracaoId",
                table: "EmpresaLogins",
                column: "EmpresaConfiguracaoId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentityUser_EmpresaLoginId",
                table: "IdentityUser",
                column: "EmpresaLoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Filas_EmpresaLogins_EmpresaLoginId",
                table: "Filas",
                column: "EmpresaLoginId",
                principalTable: "EmpresaLogins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filas_EmpresaLogins_EmpresaLoginId",
                table: "Filas");

            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DropTable(
                name: "EmpresaLogins");

            migrationBuilder.DropTable(
                name: "EmpresaConfiguracaos");

            migrationBuilder.DropIndex(
                name: "IX_Filas_EmpresaLoginId",
                table: "Filas");

            migrationBuilder.DropColumn(
                name: "EmpresaLoginId",
                table: "Filas");

            migrationBuilder.AlterColumn<string>(
                name: "Celular",
                table: "Pessoas",
                type: "varchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldNullable: true);
        }
    }
}
