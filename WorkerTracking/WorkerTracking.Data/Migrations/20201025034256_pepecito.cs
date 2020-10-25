using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkerTracking.Data.Migrations
{
    public partial class pepecito : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkersByTeams_TeamId",
                table: "WorkersByTeams");

            migrationBuilder.DropIndex(
                name: "IX_WorkersByTeams_WorkerId",
                table: "WorkersByTeams");

            migrationBuilder.CreateIndex(
                name: "IX_WorkersByTeams_TeamId",
                table: "WorkersByTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkersByTeams_WorkerId",
                table: "WorkersByTeams",
                column: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkersByTeams_TeamId",
                table: "WorkersByTeams");

            migrationBuilder.DropIndex(
                name: "IX_WorkersByTeams_WorkerId",
                table: "WorkersByTeams");

            migrationBuilder.CreateIndex(
                name: "IX_WorkersByTeams_TeamId",
                table: "WorkersByTeams",
                column: "TeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkersByTeams_WorkerId",
                table: "WorkersByTeams",
                column: "WorkerId",
                unique: true);
        }
    }
}
