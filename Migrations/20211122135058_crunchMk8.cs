using Microsoft.EntityFrameworkCore.Migrations;

namespace API_mk1.Migrations
{
    public partial class crunchMk8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users_Table",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsContractor = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_Table", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Projects_Table",
                columns: table => new
                {
                    ProjectId = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAtUnix = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects_Table", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Projects_Table_Users_Table_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Users_Table",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Table_UserId",
                table: "Users_Table",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects_Table");

            migrationBuilder.DropTable(
                name: "Users_Table");
        }
    }
}
