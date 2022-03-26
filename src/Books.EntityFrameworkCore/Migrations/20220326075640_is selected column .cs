using Microsoft.EntityFrameworkCore.Migrations;

namespace Books.Migrations
{
    public partial class isselectedcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "StudentSelectedBooks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "StudentSelectedBooks");
        }
    }
}
