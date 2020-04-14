using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Oryx.Saas.Framework.Migrations
{
    public partial class updateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppHeader",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    SaasAdminId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    AppId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    FavIco = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaasAdminConsume",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    SaasAdminId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaasAdminConsume", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaasAdminWallet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    SaasAdminId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaasAdminWallet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppStructs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    SaasAdminId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    MetaDate = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AppId = table.Column<string>(nullable: true),
                    AppHeaderId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStructs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppStructs_AppHeader_AppHeaderId",
                        column: x => x.AppHeaderId,
                        principalTable: "AppHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppStructItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    SaasAdminId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    MetaDate = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AppHeaderId = table.Column<Guid>(nullable: false),
                    AppStructId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppStructItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppStructItem_AppHeader_AppHeaderId",
                        column: x => x.AppHeaderId,
                        principalTable: "AppHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppStructItem_AppStructs_AppStructId",
                        column: x => x.AppStructId,
                        principalTable: "AppStructs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppStructItem_AppHeaderId",
                table: "AppStructItem",
                column: "AppHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStructItem_AppStructId",
                table: "AppStructItem",
                column: "AppStructId");

            migrationBuilder.CreateIndex(
                name: "IX_AppStructs_AppHeaderId",
                table: "AppStructs",
                column: "AppHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppStructItem");

            migrationBuilder.DropTable(
                name: "SaasAdminConsume");

            migrationBuilder.DropTable(
                name: "SaasAdminWallet");

            migrationBuilder.DropTable(
                name: "AppStructs");

            migrationBuilder.DropTable(
                name: "AppHeader");
        }
    }
}
