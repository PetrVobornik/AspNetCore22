using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogovaciWeb.Data.Migrations
{
    public partial class Add_Blog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nadpis = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Perex = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Prispevek = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");
        }
    }
}
