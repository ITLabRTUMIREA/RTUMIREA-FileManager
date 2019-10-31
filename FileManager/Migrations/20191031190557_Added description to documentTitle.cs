using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.Migrations
{
    public partial class AddeddescriptiontodocumentTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DocumentTitle",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DocumentTitle");
        }
    }
}
