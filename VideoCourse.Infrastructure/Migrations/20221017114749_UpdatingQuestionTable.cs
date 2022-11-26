using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoCourse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingQuestionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_videos_video_id1",
                table: "questions");

            migrationBuilder.DropIndex(
                name: "IX_questions_video_id1",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "video_id1",
                table: "questions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "video_id1",
                table: "questions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_questions_video_id1",
                table: "questions",
                column: "video_id1");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_videos_video_id1",
                table: "questions",
                column: "video_id1",
                principalTable: "videos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
