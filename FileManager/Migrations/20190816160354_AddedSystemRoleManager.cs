using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.Migrations
{
    public partial class AddedSystemRoleManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "AspNetUserDepartmentRoles");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserDepartmentRole_RoleId",
                table: "AspNetUserDepartmentRoles",
                newName: "IX_AspNetUserDepartmentRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserDepartmentRole_DepartmentId",
                table: "AspNetUserDepartmentRoles",
                newName: "IX_AspNetUserDepartmentRoles_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserDepartmentRoles",
                table: "AspNetUserDepartmentRoles",
                columns: new[] { "UserId", "RoleId", "DepartmentId" });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserDepartmentRoles_Department_DepartmentId",
                table: "AspNetUserDepartmentRoles",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserDepartmentRoles_AspNetRoles_RoleId",
                table: "AspNetUserDepartmentRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserDepartmentRoles_AspNetUsers_UserId",
                table: "AspNetUserDepartmentRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserDepartmentRoles_Department_DepartmentId",
                table: "AspNetUserDepartmentRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserDepartmentRoles_AspNetRoles_RoleId",
                table: "AspNetUserDepartmentRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserDepartmentRoles_AspNetUsers_UserId",
                table: "AspNetUserDepartmentRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserDepartmentRoles",
                table: "AspNetUserDepartmentRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserDepartmentRoles",
                newName: "AspNetUserDepartmentRole");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserDepartmentRoles_RoleId",
                table: "AspNetUserDepartmentRole",
                newName: "IX_AspNetUserDepartmentRole_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserDepartmentRoles_DepartmentId",
                table: "AspNetUserDepartmentRole",
                newName: "IX_AspNetUserDepartmentRole_DepartmentId");

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
    }
}
