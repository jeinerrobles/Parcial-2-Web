using Microsoft.EntityFrameworkCore.Migrations;

namespace Datos.Migrations
{
    public partial class SegundaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CursoId",
                table: "Inscripcions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inscripcions_CursoId",
                table: "Inscripcions",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inscripcions_Cursos_CursoId",
                table: "Inscripcions",
                column: "CursoId",
                principalTable: "Cursos",
                principalColumn: "CursoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inscripcions_Cursos_CursoId",
                table: "Inscripcions");

            migrationBuilder.DropIndex(
                name: "IX_Inscripcions_CursoId",
                table: "Inscripcions");

            migrationBuilder.AlterColumn<string>(
                name: "CursoId",
                table: "Inscripcions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
