using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ejemplo_EF_Avanzado1.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alumno",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    lu = table.Column<int>(type: "integer", nullable: false),
                    nombre = table.Column<string>(type: "text", nullable: true),
                    nota = table.Column<decimal>(type: "numeric", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_alumno", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tarea",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    titulo = table.Column<string>(type: "text", nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    fecha_entrega = table.Column<DateOnly>(type: "date", nullable: false),
                    entregada = table.Column<bool>(type: "boolean", nullable: false),
                    alumno_id = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tarea", x => x.id);
                    table.ForeignKey(
                        name: "fk_tarea_alumno_alumno_id",
                        column: x => x.alumno_id,
                        principalTable: "alumno",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "alumno",
                columns: new[] { "id", "deleted_at", "is_deleted", "lu", "nombre", "nota" },
                values: new object[,]
                {
                    { 1, null, false, 12001, "Lucas Pérez", 8.5m },
                    { 2, null, false, 12002, "Sofía Ramírez", 9.0m },
                    { 3, null, false, 12003, "Mateo González", 6.5m },
                    { 4, null, false, 12004, "Valentina López", 7.0m }
                });

            migrationBuilder.InsertData(
                table: "tarea",
                columns: new[] { "id", "alumno_id", "deleted_at", "descripcion", "entregada", "fecha_entrega", "is_deleted", "titulo" },
                values: new object[,]
                {
                    { 1, 1, null, "Implementar ordenamiento burbuja", true, new DateOnly(2025, 4, 10), false, "TP1 - Algoritmos" },
                    { 2, 1, null, "Diseñar modelo relacional", false, new DateOnly(2025, 4, 20), false, "TP2 - Base de Datos" },
                    { 3, 2, null, "Implementar ordenamiento burbuja", true, new DateOnly(2025, 4, 10), false, "TP1 - Algoritmos" },
                    { 4, 3, null, "Configurar una red local", false, new DateOnly(2025, 5, 1), false, "TP1 - Redes" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_alumno_lu",
                table: "alumno",
                column: "lu",
                unique: true,
                filter: "is_deleted = false");

            migrationBuilder.CreateIndex(
                name: "ix_tarea_alumno_id",
                table: "tarea",
                column: "alumno_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tarea");

            migrationBuilder.DropTable(
                name: "alumno");
        }
    }
}
