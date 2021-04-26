using Microsoft.EntityFrameworkCore.Migrations;

namespace API_mk1.Migrations
{
    public partial class UniqueTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId",
                table: "Users",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_UserId",
                table: "Users");
        }
    }
}
