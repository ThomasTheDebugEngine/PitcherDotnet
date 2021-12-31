using Microsoft.EntityFrameworkCore.Migrations;

namespace API_mk1.Migrations
{
    public partial class alterMk1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users_Table",
                table: "Users_Table");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects_Table",
                table: "Projects_Table");

            migrationBuilder.AddColumn<long>(
                name: "DbId",
                table: "Users_Table",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<long>(
                name: "DbId",
                table: "Projects_Table",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_Table_UserId",
                table: "Users_Table",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users_Table",
                table: "Users_Table",
                column: "DbId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects_Table",
                table: "Projects_Table",
                column: "DbId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Table_ProjectId",
                table: "Projects_Table",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_Table_UserId",
                table: "Users_Table");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users_Table",
                table: "Users_Table");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects_Table",
                table: "Projects_Table");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Table_ProjectId",
                table: "Projects_Table");

            migrationBuilder.DropColumn(
                name: "DbId",
                table: "Users_Table");

            migrationBuilder.DropColumn(
                name: "DbId",
                table: "Projects_Table");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users_Table",
                table: "Users_Table",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects_Table",
                table: "Projects_Table",
                column: "ProjectId");
        }
    }
}
