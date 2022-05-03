using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Filmes.DATA.Migrations
{
    public partial class InicialFilmes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "All");

            migrationBuilder.CreateTable(
                name: "Filme",
                schema: "All",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Sinopse = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Avaliacoes = table.Column<int>(type: "integer", nullable: false),
                    Genero = table.Column<string>(type: "text", nullable: true),
                    Poster = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filme", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                schema: "All",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerfilUsuario",
                schema: "All",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    Role = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilUsuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diretor",
                schema: "All",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Nacionalidade = table.Column<string>(type: "text", nullable: true),
                    GeneroId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diretor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diretor_Genero_GeneroId",
                        column: x => x.GeneroId,
                        principalSchema: "All",
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                schema: "All",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Login = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false),
                    DataHoraCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Foto = table.Column<byte[]>(type: "bytea", nullable: true),
                    PerfilId = table.Column<long>(type: "bigint", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    TemaEscuro = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_PerfilUsuario_PerfilId",
                        column: x => x.PerfilId,
                        principalSchema: "All",
                        principalTable: "PerfilUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmeDiretor",
                schema: "All",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilmeId = table.Column<long>(type: "bigint", nullable: false),
                    DiretorId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmeDiretor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmeDiretor_Diretor_DiretorId",
                        column: x => x.DiretorId,
                        principalSchema: "All",
                        principalTable: "Diretor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmeDiretor_Filme_FilmeId",
                        column: x => x.FilmeId,
                        principalSchema: "All",
                        principalTable: "Filme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "All",
                table: "Genero",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1L, "Masculino" },
                    { 2L, "Feminimo" }
                });

            migrationBuilder.InsertData(
                schema: "All",
                table: "PerfilUsuario",
                columns: new[] { "Id", "Nome", "Role" },
                values: new object[] { 1L, "Master", "Master" });

            migrationBuilder.CreateIndex(
                name: "IX_Diretor_GeneroId",
                schema: "All",
                table: "Diretor",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmeDiretor_DiretorId_FilmeId",
                schema: "All",
                table: "FilmeDiretor",
                columns: new[] { "DiretorId", "FilmeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmeDiretor_FilmeId",
                schema: "All",
                table: "FilmeDiretor",
                column: "FilmeId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Login",
                schema: "All",
                table: "Usuario",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PerfilId",
                schema: "All",
                table: "Usuario",
                column: "PerfilId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmeDiretor",
                schema: "All");

            migrationBuilder.DropTable(
                name: "Usuario",
                schema: "All");

            migrationBuilder.DropTable(
                name: "Diretor",
                schema: "All");

            migrationBuilder.DropTable(
                name: "Filme",
                schema: "All");

            migrationBuilder.DropTable(
                name: "PerfilUsuario",
                schema: "All");

            migrationBuilder.DropTable(
                name: "Genero",
                schema: "All");
        }
    }
}
