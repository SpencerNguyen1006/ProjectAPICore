using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectAPICore.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Notes_ModifiedBy",
                table: "Notes",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_NoteFiles_FileRepositoryId",
                table: "NoteFiles",
                column: "FileRepositoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NoteFiles_NoteId",
                table: "NoteFiles",
                column: "NoteId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteFiles_FileRepositories_FileRepositoryId",
                table: "NoteFiles",
                column: "FileRepositoryId",
                principalTable: "FileRepositories",
                principalColumn: "FileRepositoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NoteFiles_Notes_NoteId",
                table: "NoteFiles",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_ModifiedBy",
                table: "Notes",
                column: "ModifiedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteFiles_FileRepositories_FileRepositoryId",
                table: "NoteFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_NoteFiles_Notes_NoteId",
                table: "NoteFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_ModifiedBy",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_ModifiedBy",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_NoteFiles_FileRepositoryId",
                table: "NoteFiles");

            migrationBuilder.DropIndex(
                name: "IX_NoteFiles_NoteId",
                table: "NoteFiles");
        }
    }
}
