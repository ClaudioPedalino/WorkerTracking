using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkerTracking.Data.Migrations
{
    public partial class addauth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_admin",
                table: "users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_admin",
                table: "users");
        }
    }
}
