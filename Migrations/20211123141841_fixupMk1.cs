using Microsoft.EntityFrameworkCore.Migrations;

namespace API_mk1.Migrations
{
    public partial class fixupMk1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Projects_Table");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Projects_Table",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
