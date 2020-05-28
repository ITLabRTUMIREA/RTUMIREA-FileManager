using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.Migrations
{
    public partial class ChangedprimarykeyforDocumentStatusHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentStatusHistory",
                table: "DocumentStatusHistory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentStatusHistory",
                table: "DocumentStatusHistory",
                column: "Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_DocumentStatusHistory_DocumentStatusId_DepartmentsDocumentId",
                table: "DocumentStatusHistory",
                columns: new[] { "DocumentStatusId", "DepartmentsDocumentId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentStatusHistory",
                table: "DocumentStatusHistory");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_DocumentStatusHistory_DocumentStatusId_DepartmentsDocumentId",
                table: "DocumentStatusHistory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentStatusHistory",
                table: "DocumentStatusHistory",
                columns: new[] { "DocumentStatusId", "DepartmentsDocumentId" });
        }
    }
}
