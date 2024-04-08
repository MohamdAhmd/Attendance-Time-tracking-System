using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Time_tracking_System.Migrations
{
    /// <inheritdoc />
    public partial class updating10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AbsenceDays",
                table: "Students",
                type: "int",
                nullable: true,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbsenceDays",
                table: "Students");
        }
    }
}
