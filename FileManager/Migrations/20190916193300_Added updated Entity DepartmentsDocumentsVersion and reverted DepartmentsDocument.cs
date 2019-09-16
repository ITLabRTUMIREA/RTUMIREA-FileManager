using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.Migrations
{
    public partial class AddedupdatedEntityDepartmentsDocumentsVersionandrevertedDepartmentsDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "DepartmentsDocument");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "DepartmentsDocument");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "DepartmentsDocumentsVersion",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "DepartmentsDocumentsVersion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "DepartmentsDocumentsVersion");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "DepartmentsDocumentsVersion");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "DepartmentsDocument",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "DepartmentsDocument",
                nullable: true);
        }
    }
}
