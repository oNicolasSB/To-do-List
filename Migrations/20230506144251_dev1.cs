using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace to_do_list.Migrations
{
    /// <inheritdoc />
    public partial class dev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Senha = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Listas",
                columns: table => new
                {
                    IdLista = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Arquivada = table.Column<bool>(type: "INTEGER", nullable: false),
                    FkUsuario = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listas", x => x.IdLista);
                    table.ForeignKey(
                        name: "FK_Listas_Usuarios_FkUsuario",
                        column: x => x.FkUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    IdTarefa = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    EntregaEstimada = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EntregaFinal = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Concluido = table.Column<bool>(type: "INTEGER", nullable: false),
                    FkLista = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.IdTarefa);
                    table.ForeignKey(
                        name: "FK_Tarefas_Listas_FkLista",
                        column: x => x.FkLista,
                        principalTable: "Listas",
                        principalColumn: "IdLista",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listas_FkUsuario",
                table: "Listas",
                column: "FkUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_FkLista",
                table: "Tarefas",
                column: "FkLista");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarefas");

            migrationBuilder.DropTable(
                name: "Listas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
