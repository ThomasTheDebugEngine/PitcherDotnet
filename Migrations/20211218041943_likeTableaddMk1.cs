using Microsoft.EntityFrameworkCore.Migrations;

namespace API_mk1.Migrations
{
    public partial class likeTableaddMk1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Like_Table",
                columns: table => new
                {
                    DbId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LikedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like_Table", x => x.DbId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Like_Table_ProjectId",
                table: "Like_Table",
                column: "ProjectId",
                unique: true,
                filter: "[ProjectId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Like_Table");
        }
    }
}
