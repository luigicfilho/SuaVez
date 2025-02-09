using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LCFila.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initialcorrection2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Filas_FilaId",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_FilaId",
                table: "Pessoas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_FilaId",
                table: "Pessoas",
                column: "FilaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Filas_FilaId",
                table: "Pessoas",
                column: "FilaId",
                principalTable: "Filas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
