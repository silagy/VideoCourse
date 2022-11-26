using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoCourse.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixingSnakeCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "videos",
                newName: "url");

            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "videos",
                newName: "duration");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "sections",
                newName: "start_time");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "sections",
                newName: "end_time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "url",
                table: "videos",
                newName: "Url");

            migrationBuilder.RenameColumn(
                name: "duration",
                table: "videos",
                newName: "Duration");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "start_time",
                table: "sections",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "end_time",
                table: "sections",
                newName: "EndTime");
        }
    }
}
