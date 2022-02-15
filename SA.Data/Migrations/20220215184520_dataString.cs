using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SA.Data.Migrations
{
    public partial class dataString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndHour",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FacebookLink",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InstagramLink",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StartingHour",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "FacebookLink",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "InstagramLink",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "StartingHour",
                table: "Tenants");
        }
    }
}
