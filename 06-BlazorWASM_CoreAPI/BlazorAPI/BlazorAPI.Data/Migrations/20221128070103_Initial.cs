using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ukoly",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazev = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: false),
                    Popis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Termin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hotovo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ukoly", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Ukoly",
                columns: new[] { "Id", "Hotovo", "Nazev", "Popis", "Termin" },
                values: new object[,]
                {
                    { 1, false, "Nakoupit", "3 rohlíky, 1/2 chleba", new DateTime(2022, 11, 28, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 2, false, "Uklidit", "Doma a venku", new DateTime(2022, 12, 5, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 3, false, "Opravit kolo", "Je tam díra", new DateTime(2022, 12, 14, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 4, false, "Vytvořit si web", "Vlastní web s kontakty na mě", null },
                    { 5, true, "Přečíst si knihu", null, new DateTime(2022, 11, 27, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 6, true, "Úkol z matematiky", null, new DateTime(2022, 11, 21, 0, 0, 0, 0, DateTimeKind.Local) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ukoly");
        }
    }
}
