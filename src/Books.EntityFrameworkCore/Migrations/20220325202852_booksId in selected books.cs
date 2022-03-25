using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Migrations
{
    public partial class booksIdinselectedbooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentBooks_StudentSelectedBooks_StudentSelectedBooksId",
                table: "StudentBooks");

            migrationBuilder.DropIndex(
                name: "IX_StudentBooks_StudentSelectedBooksId",
                table: "StudentBooks");

            migrationBuilder.DropColumn(
                name: "StudentSelectedBooksId",
                table: "StudentBooks");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "StudentSelectedBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentSelectedBooks_BookId",
                table: "StudentSelectedBooks",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSelectedBooks_StudentBooks_BookId",
                table: "StudentSelectedBooks",
                column: "BookId",
                principalTable: "StudentBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSelectedBooks_StudentBooks_BookId",
                table: "StudentSelectedBooks");

            migrationBuilder.DropIndex(
                name: "IX_StudentSelectedBooks_BookId",
                table: "StudentSelectedBooks");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "StudentSelectedBooks");

            migrationBuilder.AddColumn<int>(
                name: "StudentSelectedBooksId",
                table: "StudentBooks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentBooks_StudentSelectedBooksId",
                table: "StudentBooks",
                column: "StudentSelectedBooksId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentBooks_StudentSelectedBooks_StudentSelectedBooksId",
                table: "StudentBooks",
                column: "StudentSelectedBooksId",
                principalTable: "StudentSelectedBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
