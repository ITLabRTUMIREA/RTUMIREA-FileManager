using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.Migrations
{
    public partial class madeconnectionbetweenuseranddocumentstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "DocumentStatusHistory",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DocumentStatusHistory_UserId",
                table: "DocumentStatusHistory",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentStatusHistory_AspNetUsers_UserId",
                table: "DocumentStatusHistory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentStatusHistory_AspNetUsers_UserId",
                table: "DocumentStatusHistory");

            migrationBuilder.DropIndex(
                name: "IX_DocumentStatusHistory_UserId",
                table: "DocumentStatusHistory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DocumentStatusHistory");
        }
    }
}
