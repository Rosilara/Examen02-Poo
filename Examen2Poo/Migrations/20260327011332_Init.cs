using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen2Poo.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "empleados",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    apellido = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    documento = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    fecha_contratacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    departamento = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    puesto_trabajo = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    salario_base = table.Column<decimal>(type: "TEXT", nullable: false),
                    activo = table.Column<bool>(type: "INTEGER", nullable: false),
                    created_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<string>(type: "TEXT", nullable: true),
                    updated_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empleados", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "planillas",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    periodo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fecha_pago = table.Column<DateTime>(type: "TEXT", nullable: false),
                    estado = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    created_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<string>(type: "TEXT", nullable: true),
                    updated_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_planillas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "detalles_planilla",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    planilla_id = table.Column<int>(type: "INTEGER", nullable: false),
                    empleado_id = table.Column<int>(type: "INTEGER", nullable: false),
                    salario_base = table.Column<decimal>(type: "TEXT", nullable: false),
                    horas_extra = table.Column<decimal>(type: "TEXT", nullable: false),
                    monto_horas_extra = table.Column<decimal>(type: "TEXT", nullable: false),
                    bonificaciones = table.Column<decimal>(type: "TEXT", nullable: false),
                    deducciones = table.Column<decimal>(type: "TEXT", nullable: false),
                    salario_neto = table.Column<decimal>(type: "TEXT", nullable: false),
                    comentarios = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    created_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<string>(type: "TEXT", nullable: true),
                    updated_by_id = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalles_planilla", x => x.id);
                    table.ForeignKey(
                        name: "FK_detalles_planilla_empleados_empleado_id",
                        column: x => x.empleado_id,
                        principalTable: "empleados",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalles_planilla_planillas_planilla_id",
                        column: x => x.planilla_id,
                        principalTable: "planillas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_detalles_planilla_empleado_id",
                table: "detalles_planilla",
                column: "empleado_id");

            migrationBuilder.CreateIndex(
                name: "IX_detalles_planilla_planilla_id",
                table: "detalles_planilla",
                column: "planilla_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detalles_planilla");

            migrationBuilder.DropTable(
                name: "empleados");

            migrationBuilder.DropTable(
                name: "planillas");
        }
    }
}
