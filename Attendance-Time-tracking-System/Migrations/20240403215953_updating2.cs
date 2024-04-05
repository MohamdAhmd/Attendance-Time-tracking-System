using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Attendance_Time_tracking_System.Migrations
{
    /// <inheritdoc />
    public partial class updating2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "User_Status",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User_Status",
                table: "Users");
        }
    }
}
