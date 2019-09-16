using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.Migrations
{
    public partial class AddedpropertiestoDepartmentsDocumentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "DepartmentsDocument",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "DepartmentsDocument",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "DepartmentsDocument");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "DepartmentsDocument");
        }
    }
}
