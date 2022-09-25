using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogovaciWeb.Data.Migrations
{
    public partial class Add_Blog_Pripnuto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Pripnuto",
                table: "Blog",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pripnuto",
                table: "Blog");
        }
    }
}
