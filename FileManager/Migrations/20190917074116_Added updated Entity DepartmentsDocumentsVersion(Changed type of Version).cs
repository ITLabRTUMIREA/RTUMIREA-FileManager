using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.Migrations
{
    public partial class AddedupdatedEntityDepartmentsDocumentsVersionChangedtypeofVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Version",
                table: "DepartmentsDocumentsVersion",
                nullable: false,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Version",
                table: "DepartmentsDocumentsVersion",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
