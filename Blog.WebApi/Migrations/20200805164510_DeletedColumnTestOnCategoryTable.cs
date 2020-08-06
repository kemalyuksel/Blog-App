using Microsoft.EntityFrameworkCore.Migrations;

namespace Blog.WebApi.Migrations
{
    public partial class DeletedColumnTestOnCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Test",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
