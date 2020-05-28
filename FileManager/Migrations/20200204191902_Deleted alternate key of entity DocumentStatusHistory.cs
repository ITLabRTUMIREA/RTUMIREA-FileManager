using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.Migrations
{
    public partial class DeletedalternatekeyofentityDocumentStatusHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_DocumentStatusHistory_DocumentStatusId_DepartmentsDocumentId",
                table: "DocumentStatusHistory");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentStatusHistory_DocumentStatusId",
                table: "DocumentStatusHistory",
                column: "DocumentStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DocumentStatusHistory_DocumentStatusId",
                table: "DocumentStatusHistory");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_DocumentStatusHistory_DocumentStatusId_DepartmentsDocumentId",
                table: "DocumentStatusHistory",
                columns: new[] { "DocumentStatusId", "DepartmentsDocumentId" });
        }
    }
}
