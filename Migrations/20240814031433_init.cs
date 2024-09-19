using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothingPro.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyEmail = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "MenuHeader",
                columns: table => new
                {
                    MenuHeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuHeaderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuHeaderIsActive = table.Column<int>(type: "int", nullable: true),
                    StSortOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuHeader", x => x.MenuHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    StId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StDes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StInActive = table.Column<int>(type: "int", nullable: true),
                    StSortOrder = table.Column<int>(type: "int", nullable: true),
                    StCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StIsPopular = table.Column<int>(type: "int", nullable: true),
                    StColour = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StHSCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StIsShirt = table.Column<int>(type: "int", nullable: true),
                    StIsPant = table.Column<int>(type: "int", nullable: true),
                    StIsOther = table.Column<int>(type: "int", nullable: true),
                    StMenuHeaderId = table.Column<int>(type: "int", nullable: true),
                    StAddedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.StId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "MenuHeader");

            migrationBuilder.DropTable(
                name: "Stock");
        }
    }
}
