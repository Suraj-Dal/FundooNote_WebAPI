using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class ThirdMigrationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_collaboratorEntities_notesEntities_NotesNoteID",
                table: "collaboratorEntities");

            migrationBuilder.DropIndex(
                name: "IX_collaboratorEntities_NotesNoteID",
                table: "collaboratorEntities");

            migrationBuilder.DropColumn(
                name: "NotesNoteID",
                table: "collaboratorEntities");

            migrationBuilder.CreateIndex(
                name: "IX_collaboratorEntities_NoteID",
                table: "collaboratorEntities",
                column: "NoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_collaboratorEntities_notesEntities_NoteID",
                table: "collaboratorEntities",
                column: "NoteID",
                principalTable: "notesEntities",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_collaboratorEntities_notesEntities_NoteID",
                table: "collaboratorEntities");

            migrationBuilder.DropIndex(
                name: "IX_collaboratorEntities_NoteID",
                table: "collaboratorEntities");

            migrationBuilder.AddColumn<long>(
                name: "NotesNoteID",
                table: "collaboratorEntities",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_collaboratorEntities_NotesNoteID",
                table: "collaboratorEntities",
                column: "NotesNoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_collaboratorEntities_notesEntities_NotesNoteID",
                table: "collaboratorEntities",
                column: "NotesNoteID",
                principalTable: "notesEntities",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
