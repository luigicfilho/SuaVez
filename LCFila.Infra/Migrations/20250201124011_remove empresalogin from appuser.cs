using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LCFila.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Removeempresaloginfromappuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_EmpresasLogin_empresaLoginId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "empresaLoginId",
                table: "AspNetUsers",
                newName: "EmpresaLoginId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_empresaLoginId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_EmpresaLoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_EmpresasLogin_EmpresaLoginId",
                table: "AspNetUsers",
                column: "EmpresaLoginId",
                principalTable: "EmpresasLogin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_EmpresasLogin_EmpresaLoginId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "EmpresaLoginId",
                table: "AspNetUsers",
                newName: "empresaLoginId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_EmpresaLoginId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_empresaLoginId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_EmpresasLogin_empresaLoginId",
                table: "AspNetUsers",
                column: "empresaLoginId",
                principalTable: "EmpresasLogin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
