using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Time_tracking_System.Migrations
{
    /// <inheritdoc />
    public partial class updating9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionBody",
                table: "Attends");

            migrationBuilder.DropColumn(
                name: "PermissionStatus",
                table: "Attends");

            migrationBuilder.DropColumn(
                name: "PermissionType",
                table: "Attends");

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PermissionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermissionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => new { x.day, x.StudentId });
                    table.ForeignKey(
                        name: "FK_Permissions_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_StudentId",
                table: "Permissions",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.AddColumn<string>(
                name: "PermissionBody",
                table: "Attends",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PermissionStatus",
                table: "Attends",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PermissionType",
                table: "Attends",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);
        }
    }
}
