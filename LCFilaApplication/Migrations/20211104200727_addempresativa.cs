using Microsoft.EntityFrameworkCore.Migrations;

namespace LCFilaApplication.Migrations
{
    public partial class addempresativa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "EmpresaLogins",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "EmpresaLogins");
        }
    }
}
