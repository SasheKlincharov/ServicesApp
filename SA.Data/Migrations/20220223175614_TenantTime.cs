using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SA.Data.Migrations
{
    public partial class TenantTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ScheduleTime",
                table: "Tenants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsScheduled",
                table: "Schedules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleTime",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "IsScheduled",
                table: "Schedules");
        }
    }
}
