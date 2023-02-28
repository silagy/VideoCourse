using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VideoCourse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "outbox_messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    occurredonutc = table.Column<DateTime>(name: "occurred_on_utc", type: "timestamp with time zone", nullable: false),
                    publishedonutc = table.Column<DateTime>(name: "published_on_utc", type: "timestamp with time zone", nullable: true),
                    error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_outbox_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    firstname = table.Column<string>(name: "first_name", type: "text", nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false, defaultValue: false),
                    creationdate = table.Column<DateTime>(name: "creation_date", type: "timestamp with time zone", nullable: false),
                    updatedate = table.Column<DateTime>(name: "update_date", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permission_role",
                columns: table => new
                {
                    roleid = table.Column<int>(name: "role_id", type: "integer", nullable: false),
                    permissionid = table.Column<int>(name: "permission_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permission_role", x => new { x.roleid, x.permissionid });
                    table.ForeignKey(
                        name: "FK_permission_role_permissions_permission_id",
                        column: x => x.permissionid,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_permission_role_roles_role_id",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    userid = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    roleid = table.Column<int>(name: "role_id", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.userid, x.roleid });
                    table.ForeignKey(
                        name: "FK_user_roles_roles_role_id",
                        column: x => x.roleid,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles_users_user_id",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "videos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false, defaultValue: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    duration = table.Column<int>(type: "integer", nullable: false),
                    creatorid = table.Column<Guid>(name: "creator_id", type: "uuid", nullable: false),
                    ispublished = table.Column<bool>(name: "is_published", type: "boolean", nullable: false, defaultValue: false),
                    publishedonutc = table.Column<DateTime>(name: "published_on_utc", type: "timestamp with time zone", nullable: true),
                    creationdate = table.Column<DateTime>(name: "creation_date", type: "timestamp with time zone", nullable: false),
                    updatedate = table.Column<DateTime>(name: "update_date", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_videos", x => x.id);
                    table.ForeignKey(
                        name: "FK_videos_users_creator_id",
                        column: x => x.creatorid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    questiontypeid = table.Column<int>(name: "question_type_id", type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_questions", x => x.id);
                    table.ForeignKey(
                        name: "FK_questions_videos_video_id",
                        column: x => x.videoid,
                        principalTable: "videos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    starttime = table.Column<int>(name: "start_time", type: "integer", nullable: false),
                    endtime = table.Column<int>(name: "end_time", type: "integer", nullable: false),
                    videoid = table.Column<Guid>(name: "video_id", type: "uuid", nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false, defaultValue: false),
                    creationdate = table.Column<DateTime>(name: "creation_date", type: "timestamp with time zone", nullable: false),
                    updatedate = table.Column<DateTime>(name: "update_date", type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sections", x => x.id);
                    table.ForeignKey(
                        name: "FK_sections_videos_video_id",
                        column: x => x.videoid,
                        principalTable: "videos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "ReadUser" },
                    { 2, "EditUser" },
                    { 3, "DeleteUser" },
                    { 4, "ReadVideos" },
                    { 5, "EditVideos" },
                    { 6, "DeleteVideos" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "UsersManager" },
                    { 3, "VideosCreator" }
                });

            migrationBuilder.InsertData(
                table: "permission_role",
                columns: new[] { "permission_id", "role_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 3 },
                    { 5, 3 },
                    { 6, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_notes_video_id",
                table: "notes",
                column: "video_id");

            migrationBuilder.CreateIndex(
                name: "IX_permission_role_permission_id",
                table: "permission_role",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_question_options_question_id",
                table: "question_options",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_questions_video_id",
                table: "questions",
                column: "video_id");

            migrationBuilder.CreateIndex(
                name: "IX_sections_video_id",
                table: "sections",
                column: "video_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_videos_creator_id",
                table: "videos",
                column: "creator_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.DropTable(
                name: "outbox_messages");

            migrationBuilder.DropTable(
                name: "permission_role");

            migrationBuilder.DropTable(
                name: "question_options");

            migrationBuilder.DropTable(
                name: "sections");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "videos");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
