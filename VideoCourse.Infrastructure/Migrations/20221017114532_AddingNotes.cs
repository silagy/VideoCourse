using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoCourse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false, defaultValue: false),
                    creationdate = table.Column<DateTime>(name: "creation_date", type: "timestamp with time zone", nullable: false),
                    updatedate = table.Column<DateTime>(name: "update_date", type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    time = table.Column<int>(type: "integer", nullable: false),
                    typeid = table.Column<int>(name: "type_id", type: "integer", nullable: false),
                    videoid = table.Column<Guid>(name: "video_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => x.id);
                    table.ForeignKey(
                        name: "FK_notes_videos_video_id",
                        column: x => x.videoid,
                        principalTable: "videos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    feedback = table.Column<string>(type: "text", nullable: true),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false, defaultValue: false),
                    creationdate = table.Column<DateTime>(name: "creation_date", type: "timestamp with time zone", nullable: false),
                    updatedate = table.Column<DateTime>(name: "update_date", type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    time = table.Column<int>(type: "integer", nullable: false),
                    typeid = table.Column<int>(name: "type_id", type: "integer", nullable: false),
                    videoid = table.Column<Guid>(name: "video_id", type: "uuid", nullable: false),
                    videoid1 = table.Column<Guid>(name: "video_id1", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.id);
                    table.ForeignKey(
                        name: "FK_questions_videos_video_id",
                        column: x => x.videoid,
                        principalTable: "videos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_questions_videos_video_id1",
                        column: x => x.videoid1,
                        principalTable: "videos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_notes_video_id",
                table: "notes",
                column: "video_id");

            migrationBuilder.CreateIndex(
                name: "IX_questions_video_id",
                table: "questions",
                column: "video_id");

            migrationBuilder.CreateIndex(
                name: "IX_questions_video_id1",
                table: "questions",
                column: "video_id1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.DropTable(
                name: "questions");
        }
    }
}
