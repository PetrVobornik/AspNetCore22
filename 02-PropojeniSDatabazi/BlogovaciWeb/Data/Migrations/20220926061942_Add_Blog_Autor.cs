using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogovaciWeb.Data.Migrations
{
    public partial class Add_Blog_Autor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AutorId",
                table: "Blog",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blog_AutorId",
                table: "Blog",
                column: "AutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_AspNetUsers_AutorId",
                table: "Blog",
                column: "AutorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_AspNetUsers_AutorId",
                table: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_AutorId",
                table: "Blog");

            migrationBuilder.DropColumn(
                name: "AutorId",
                table: "Blog");
        }
    }
}
