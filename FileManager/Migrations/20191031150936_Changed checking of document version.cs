using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.Migrations
{
    public partial class Changedcheckingofdocumentversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "DepartmentsDocumentsVersion");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadedDateTime",
                table: "DepartmentsDocumentsVersion",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadedDateTime",
                table: "DepartmentsDocumentsVersion");

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "DepartmentsDocumentsVersion",
                nullable: false,
                defaultValue: 0);
        }
    }
}
