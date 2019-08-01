using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.Migrations
{
    public partial class addPrimaryKeyForUserDepartmentRoleEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Department_DepartmentID",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserDepartmentRole");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Department",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DepartmentID",
                table: "AspNetUserDepartmentRole",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserDepartmentRole",
                newName: "IX_AspNetUserDepartmentRole_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_DepartmentID",
                table: "AspNetUserDepartmentRole",
                newName: "IX_AspNetUserDepartmentRole_DepartmentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "AspNetUserDepartmentRole",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUserDepartmentRole_UserId_RoleId",
                table: "AspNetUserDepartmentRole",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserDepartmentRole",
                table: "AspNetUserDepartmentRole",
                columns: new[] { "UserId", "RoleId", "DepartmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserDepartmentRole_Department_DepartmentId",
                table: "AspNetUserDepartmentRole",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserDepartmentRole_AspNetRoles_RoleId",
                table: "AspNetUserDepartmentRole",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserDepartmentRole_AspNetUsers_UserId",
                table: "AspNetUserDepartmentRole",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserDepartmentRole_Department_DepartmentId",
                table: "AspNetUserDepartmentRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserDepartmentRole_AspNetRoles_RoleId",
                table: "AspNetUserDepartmentRole");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserDepartmentRole_AspNetUsers_UserId",
                table: "AspNetUserDepartmentRole");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUserDepartmentRole_UserId_RoleId",
                table: "AspNetUserDepartmentRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserDepartmentRole",
                table: "AspNetUserDepartmentRole");

            migrationBuilder.RenameTable(
                name: "AspNetUserDepartmentRole",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Department",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "AspNetUserRoles",
                newName: "DepartmentID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserDepartmentRole_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserDepartmentRole_DepartmentId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_DepartmentID");

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentID",
                table: "AspNetUserRoles",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Department_DepartmentID",
                table: "AspNetUserRoles",
                column: "DepartmentID",
                principalTable: "Department",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
