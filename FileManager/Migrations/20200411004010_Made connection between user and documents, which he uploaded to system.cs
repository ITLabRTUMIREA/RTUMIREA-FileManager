using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.Migrations
{
    public partial class Madeconnectionbetweenuseranddocumentswhichheuploadedtosystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "DepartmentsDocumentsVersion",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentsDocumentsVersion_UserId",
                table: "DepartmentsDocumentsVersion",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentsDocumentsVersion_AspNetUsers_UserId",
                table: "DepartmentsDocumentsVersion",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentsDocumentsVersion_AspNetUsers_UserId",
                table: "DepartmentsDocumentsVersion");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentsDocumentsVersion_UserId",
                table: "DepartmentsDocumentsVersion");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DepartmentsDocumentsVersion");
        }
    }
}
