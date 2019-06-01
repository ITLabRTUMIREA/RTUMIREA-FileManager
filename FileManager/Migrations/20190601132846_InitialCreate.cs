using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileManager.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FistName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departament",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departament", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentStatus",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Year",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Year", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    DepartamentID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Departament_DepartamentID",
                        column: x => x.DepartamentID,
                        principalTable: "Departament",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "RoleStatus",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DocumentStatusID = table.Column<Guid>(nullable: false),
                    RoleID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleStatus", x => new { x.RoleID, x.DocumentStatusID });
                    table.ForeignKey(
                        name: "FK_RoleStatus_DocumentStatus_DocumentStatusID",
                        column: x => x.DocumentStatusID,
                        principalTable: "DocumentStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleStatus_AspNetRoles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTitle",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DocumentTypeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTitle", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DocumentTitle_DocumentType_DocumentTypeID",
                        column: x => x.DocumentTypeID,
                        principalTable: "DocumentType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YearDocumentTitle",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    YearID = table.Column<Guid>(nullable: false),
                    DocumentTitleID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearDocumentTitle", x => x.ID);
                    table.UniqueConstraint("AK_YearDocumentTitle_DocumentTitleID_YearID", x => new { x.DocumentTitleID, x.YearID });
                    table.ForeignKey(
                        name: "FK_YearDocumentTitle_DocumentTitle_DocumentTitleID",
                        column: x => x.DocumentTitleID,
                        principalTable: "DocumentTitle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YearDocumentTitle_Year_YearID",
                        column: x => x.YearID,
                        principalTable: "Year",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartamentsDocument",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    YearDocumentTitleID = table.Column<Guid>(nullable: false),
                    DepartamentID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartamentsDocument", x => x.ID);
                    table.UniqueConstraint("AK_DepartamentsDocument_DepartamentID_YearDocumentTitleID", x => new { x.DepartamentID, x.YearDocumentTitleID });
                    table.ForeignKey(
                        name: "FK_DepartamentsDocument_Departament_DepartamentID",
                        column: x => x.DepartamentID,
                        principalTable: "Departament",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartamentsDocument_YearDocumentTitle_YearDocumentTitleID",
                        column: x => x.YearDocumentTitleID,
                        principalTable: "YearDocumentTitle",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DepartamentsDocumentsVersion",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Version = table.Column<short>(nullable: false),
                    DepartamentDocumentID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartamentsDocumentsVersion", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DepartamentsDocumentsVersion_DepartamentsDocument_DepartamentDocumentID",
                        column: x => x.DepartamentDocumentID,
                        principalTable: "DepartamentsDocument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentStatusHistory",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CommentEdits = table.Column<string>(nullable: true),
                    DocumentStatusID = table.Column<Guid>(nullable: false),
                    DepartamentsDocumentID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentStatusHistory", x => new { x.DocumentStatusID, x.DepartamentsDocumentID });
                    table.ForeignKey(
                        name: "FK_DocumentStatusHistory_DepartamentsDocument_DepartamentsDocumentID",
                        column: x => x.DepartamentsDocumentID,
                        principalTable: "DepartamentsDocument",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DocumentStatusHistory_DocumentStatus_DocumentStatusID",
                        column: x => x.DocumentStatusID,
                        principalTable: "DocumentStatus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_DepartamentID",
                table: "AspNetUserRoles",
                column: "DepartamentID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DepartamentsDocument_YearDocumentTitleID",
                table: "DepartamentsDocument",
                column: "YearDocumentTitleID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartamentsDocumentsVersion_DepartamentDocumentID",
                table: "DepartamentsDocumentsVersion",
                column: "DepartamentDocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentStatusHistory_DepartamentsDocumentID",
                table: "DocumentStatusHistory",
                column: "DepartamentsDocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTitle_DocumentTypeID",
                table: "DocumentTitle",
                column: "DocumentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleStatus_DocumentStatusID",
                table: "RoleStatus",
                column: "DocumentStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_YearDocumentTitle_YearID",
                table: "YearDocumentTitle",
                column: "YearID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DepartamentsDocumentsVersion");

            migrationBuilder.DropTable(
                name: "DocumentStatusHistory");

            migrationBuilder.DropTable(
                name: "RoleStatus");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DepartamentsDocument");

            migrationBuilder.DropTable(
                name: "DocumentStatus");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Departament");

            migrationBuilder.DropTable(
                name: "YearDocumentTitle");

            migrationBuilder.DropTable(
                name: "DocumentTitle");

            migrationBuilder.DropTable(
                name: "Year");

            migrationBuilder.DropTable(
                name: "DocumentType");
        }
    }
}
