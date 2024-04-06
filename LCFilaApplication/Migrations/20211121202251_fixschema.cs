using Microsoft.EntityFrameworkCore.Migrations;

namespace LCFilaApplication.Migrations
{
    public partial class fixschema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Veiculos",
                schema: "Identity",
                newName: "Veiculos");

            migrationBuilder.RenameTable(
                name: "Pessoas",
                schema: "Identity",
                newName: "Pessoas");

            migrationBuilder.RenameTable(
                name: "Mercadorias",
                schema: "Identity",
                newName: "Mercadorias");

            migrationBuilder.RenameTable(
                name: "FilaVeiculos",
                schema: "Identity",
                newName: "FilaVeiculos");

            migrationBuilder.RenameTable(
                name: "Filas",
                schema: "Identity",
                newName: "Filas");

            migrationBuilder.RenameTable(
                name: "FilaPessoas",
                schema: "Identity",
                newName: "FilaPessoas");

            migrationBuilder.RenameTable(
                name: "FilaMercadorias",
                schema: "Identity",
                newName: "FilaMercadorias");

            migrationBuilder.RenameTable(
                name: "EmpresaLogins",
                schema: "Identity",
                newName: "EmpresaLogins");

            migrationBuilder.RenameTable(
                name: "EmpresaConfiguracaos",
                schema: "Identity",
                newName: "EmpresaConfiguracaos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Veiculos",
                newName: "Veiculos",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "Pessoas",
                newName: "Pessoas",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "Mercadorias",
                newName: "Mercadorias",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "FilaVeiculos",
                newName: "FilaVeiculos",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "Filas",
                newName: "Filas",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "FilaPessoas",
                newName: "FilaPessoas",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "FilaMercadorias",
                newName: "FilaMercadorias",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "EmpresaLogins",
                newName: "EmpresaLogins",
                newSchema: "Identity");

            migrationBuilder.RenameTable(
                name: "EmpresaConfiguracaos",
                newName: "EmpresaConfiguracaos",
                newSchema: "Identity");
        }
    }
}
