using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Migrations
{
    public partial class addedacademicGradeBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSelectedBooks_StudentBooks_BookId",
                table: "StudentSelectedBooks");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "StudentSelectedBooks",
                newName: "AcademicGradeBookId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSelectedBooks_BookId",
                table: "StudentSelectedBooks",
                newName: "IX_StudentSelectedBooks_AcademicGradeBookId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSelectedBooks_AcademicGradeBooks_AcademicGradeBookId",
                table: "StudentSelectedBooks",
                column: "AcademicGradeBookId",
                principalTable: "AcademicGradeBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSelectedBooks_AcademicGradeBooks_AcademicGradeBookId",
                table: "StudentSelectedBooks");

            migrationBuilder.RenameColumn(
                name: "AcademicGradeBookId",
                table: "StudentSelectedBooks",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSelectedBooks_AcademicGradeBookId",
                table: "StudentSelectedBooks",
                newName: "IX_StudentSelectedBooks_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSelectedBooks_StudentBooks_BookId",
                table: "StudentSelectedBooks",
                column: "BookId",
                principalTable: "StudentBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
