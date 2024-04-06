using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LCFilaApplication.Migrations
{
    public partial class setupinicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataInicio = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: false),
                    TempoMedio = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilaMercadorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FiladeMercadoriasId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilaMercadorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilaMercadorias_Filas_FiladeMercadoriasId",
                        column: x => x.FiladeMercadoriasId,
                        principalTable: "Filas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FilaPessoas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FiladePessoasId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilaPessoas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilaPessoas_Filas_FiladePessoasId",
                        column: x => x.FiladePessoasId,
                        principalTable: "Filas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FilaVeiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FiladeVeiculosId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilaVeiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilaVeiculos_Filas_FiladeVeiculosId",
                        column: x => x.FiladeVeiculosId,
                        principalTable: "Filas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mercadorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FilaId = table.Column<Guid>(nullable: false),
                    Identificacao = table.Column<string>(type: "varchar(50)", nullable: false),
                    Dimensoes = table.Column<string>(type: "varchar(50)", nullable: true),
                    Peso = table.Column<string>(type: "varchar(10)", nullable: true),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    FilaMercadoriaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercadorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mercadorias_Filas_FilaId",
                        column: x => x.FilaId,
                        principalTable: "Filas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mercadorias_FilaMercadorias_FilaMercadoriaId",
                        column: x => x.FilaMercadoriaId,
                        principalTable: "FilaMercadorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FilaId = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Documento = table.Column<string>(type: "varchar(14)", nullable: true),
                    Celular = table.Column<string>(type: "varchar(10)", nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    FilaPessoaId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pessoas_Filas_FilaId",
                        column: x => x.FilaId,
                        principalTable: "Filas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pessoas_FilaPessoas_FilaPessoaId",
                        column: x => x.FilaPessoaId,
                        principalTable: "FilaPessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FilaId = table.Column<Guid>(nullable: false),
                    Placa = table.Column<string>(type: "varchar(10)", nullable: false),
                    Fabricante = table.Column<string>(type: "varchar(50)", nullable: true),
                    Modelo = table.Column<string>(type: "varchar(200)", nullable: true),
                    TipoVeiculo = table.Column<string>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    FilaVeiculoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veiculos_Filas_FilaId",
                        column: x => x.FilaId,
                        principalTable: "Filas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Veiculos_FilaVeiculos_FilaVeiculoId",
                        column: x => x.FilaVeiculoId,
                        principalTable: "FilaVeiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilaMercadorias_FiladeMercadoriasId",
                table: "FilaMercadorias",
                column: "FiladeMercadoriasId");

            migrationBuilder.CreateIndex(
                name: "IX_FilaPessoas_FiladePessoasId",
                table: "FilaPessoas",
                column: "FiladePessoasId");

            migrationBuilder.CreateIndex(
                name: "IX_FilaVeiculos_FiladeVeiculosId",
                table: "FilaVeiculos",
                column: "FiladeVeiculosId");

            migrationBuilder.CreateIndex(
                name: "IX_Mercadorias_FilaId",
                table: "Mercadorias",
                column: "FilaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mercadorias_FilaMercadoriaId",
                table: "Mercadorias",
                column: "FilaMercadoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_FilaId",
                table: "Pessoas",
                column: "FilaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_FilaPessoaId",
                table: "Pessoas",
                column: "FilaPessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_FilaId",
                table: "Veiculos",
                column: "FilaId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculos_FilaVeiculoId",
                table: "Veiculos",
                column: "FilaVeiculoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mercadorias");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Veiculos");

            migrationBuilder.DropTable(
                name: "FilaMercadorias");

            migrationBuilder.DropTable(
                name: "FilaPessoas");

            migrationBuilder.DropTable(
                name: "FilaVeiculos");

            migrationBuilder.DropTable(
                name: "Filas");
        }
    }
}
