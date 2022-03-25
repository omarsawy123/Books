using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Migrations
{
    public partial class removecolumnstudentsbooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentBooks_AcademicGradeBooks_AcademicGradeBookId",
                table: "StudentBooks");

            migrationBuilder.DropIndex(
                name: "IX_StudentBooks_AcademicGradeBookId",
                table: "StudentBooks");

            migrationBuilder.DropColumn(
                name: "AcademicGradeBookId",
                table: "StudentBooks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcademicGradeBookId",
                table: "StudentBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentBooks_AcademicGradeBookId",
                table: "StudentBooks",
                column: "AcademicGradeBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentBooks_AcademicGradeBooks_AcademicGradeBookId",
                table: "StudentBooks",
                column: "AcademicGradeBookId",
                principalTable: "AcademicGradeBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
