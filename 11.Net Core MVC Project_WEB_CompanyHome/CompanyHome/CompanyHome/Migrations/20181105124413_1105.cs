using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompanyHome.Migrations
{
    public partial class _1105 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    USERNAME = table.Column<string>(maxLength: 15, nullable: false),
                    PASSWORD = table.Column<string>(nullable: false),
                    CREATETIME = table.Column<DateTime>(nullable: true),
                    LASTLOGINTIME = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(maxLength: 15, nullable: true),
                    ORDER = table.Column<int>(nullable: false),
                    ADDTIME = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TITLE = table.Column<string>(maxLength: 100, nullable: true),
                    CONTENT = table.Column<string>(nullable: true),
                    AUTHOR = table.Column<string>(nullable: true),
                    ADDTIME = table.Column<DateTime>(nullable: false),
                    MAXLEVEL = table.Column<int>(nullable: false),
                    CID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.ID);
                    table.ForeignKey(
                        name: "FK_News_Categories_CID",
                        column: x => x.CID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CONTENT = table.Column<string>(maxLength: 400, nullable: true),
                    IP = table.Column<string>(nullable: true),
                    AUTHOR = table.Column<string>(nullable: true),
                    ADDTIME = table.Column<DateTime>(nullable: false),
                    LEVEL = table.Column<int>(nullable: false),
                    NID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comments_News_NID",
                        column: x => x.NID,
                        principalTable: "News",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_NID",
                table: "Comments",
                column: "NID");

            migrationBuilder.CreateIndex(
                name: "IX_News_CID",
                table: "News",
                column: "CID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
