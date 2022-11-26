using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoCourse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QuestionOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "question_options",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    isright = table.Column<bool>(name: "is_right", type: "boolean", nullable: false, defaultValue: false),
                    questionid = table.Column<Guid>(name: "question_id", type: "uuid", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false, defaultValue: false),
                    creationdate = table.Column<DateTime>(name: "creation_date", type: "timestamp with time zone", nullable: false),
                    updatedate = table.Column<DateTime>(name: "update_date", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_question_options_questions_question_id",
                        column: x => x.questionid,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_question_options_question_id",
                table: "question_options",
                column: "question_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "question_options");
        }
    }
}
