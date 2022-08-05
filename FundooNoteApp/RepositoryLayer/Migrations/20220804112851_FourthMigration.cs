using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "labelEntities",
                columns: table => new
                {
                    LabelID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NoteID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_labelEntities", x => x.LabelID);
                    table.ForeignKey(
                        name: "FK_labelEntities_notesEntities_NoteID",
                        column: x => x.NoteID,
                        principalTable: "notesEntities",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_labelEntities_userEntities_UserId",
                        column: x => x.UserId,
                        principalTable: "userEntities",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_labelEntities_NoteID",
                table: "labelEntities",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_labelEntities_UserId",
                table: "labelEntities",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "labelEntities");
        }
    }
}
