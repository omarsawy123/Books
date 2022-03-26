using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Migrations
{
    public partial class addedacademicGradeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcademicGradeClassId",
                table: "StudentSelectedBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSelectedBooks_AcademicGradeClassId",
                table: "StudentSelectedBooks",
                column: "AcademicGradeClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSelectedBooks_AcademicGradeClasses_AcademicGradeClassId",
                table: "StudentSelectedBooks",
                column: "AcademicGradeClassId",
                principalTable: "AcademicGradeClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSelectedBooks_AcademicGradeClasses_AcademicGradeClassId",
                table: "StudentSelectedBooks");

            migrationBuilder.DropIndex(
                name: "IX_StudentSelectedBooks_AcademicGradeClassId",
                table: "StudentSelectedBooks");

            migrationBuilder.DropColumn(
                name: "AcademicGradeClassId",
                table: "StudentSelectedBooks");
        }
    }
}
