using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Migrations
{
    public partial class studentsmandatorybookstable_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentMandatoryBooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicGradeBookId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentMandatoryBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentMandatoryBooks_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentMandatoryBooks_AcademicGradeBooks_AcademicGradeBookId",
                        column: x => x.AcademicGradeBookId,
                        principalTable: "AcademicGradeBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentMandatoryBooks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentMandatoryBooks_AcademicGradeBookId",
                table: "StudentMandatoryBooks",
                column: "AcademicGradeBookId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMandatoryBooks_StudentId",
                table: "StudentMandatoryBooks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMandatoryBooks_UserId",
                table: "StudentMandatoryBooks",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentMandatoryBooks");
        }
    }
}
