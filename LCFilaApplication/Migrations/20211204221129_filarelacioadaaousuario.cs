using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCFilaApplication.Migrations
{
    public partial class filarelacioadaaousuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Filas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Filas",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Filas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Tipofila",
                table: "Filas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Filas",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Filas");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Filas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Filas");

            migrationBuilder.DropColumn(
                name: "Tipofila",
                table: "Filas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Filas");
        }
    }
}
