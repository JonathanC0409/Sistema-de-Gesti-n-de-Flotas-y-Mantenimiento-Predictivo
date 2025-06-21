using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SistemaLogs.Api.Migrations
{
    /// <inheritdoc />
    public partial class v02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlertasMantenimientos",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CamionCodigo = table.Column<int>(type: "integer", nullable: false),
                    TipoMantenimiento = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    FechaGeneracion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Resuelta = table.Column<bool>(type: "boolean", nullable: false),
                    FechaResolucion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertasMantenimientos", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "LecturasSensores",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CamionCodigo = table.Column<int>(type: "integer", nullable: false),
                    Kilometraje = table.Column<double>(type: "double precision", nullable: false),
                    EstadoMotor = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Comentarios = table.Column<string>(type: "text", nullable: false),
                    NivelGasolina = table.Column<double>(type: "double precision", nullable: false),
                    NivelAceite = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LecturasSensores", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "LogsAuditorias",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Accion = table.Column<string>(type: "text", nullable: false),
                    Entidad = table.Column<string>(type: "text", nullable: false),
                    EntidadCodigo = table.Column<int>(type: "integer", nullable: false),
                    Usuario = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Detalles = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsAuditorias", x => x.Codigo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertasMantenimientos");

            migrationBuilder.DropTable(
                name: "LecturasSensores");

            migrationBuilder.DropTable(
                name: "LogsAuditorias");
        }
    }
}
