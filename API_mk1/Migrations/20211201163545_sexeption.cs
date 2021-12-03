using Microsoft.EntityFrameworkCore.Migrations;

namespace API_mk1.Migrations
{
    public partial class sexeption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users_Table",
                newName: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users_Table",
                newName: "PasswordHash");
        }
    }
}
