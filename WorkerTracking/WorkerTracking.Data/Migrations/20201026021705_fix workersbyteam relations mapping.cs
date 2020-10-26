using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkerTracking.Data.Migrations
{
    public partial class fixworkersbyteamrelationsmapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Teams_WorkersByTeams_TeamsId",
                table: "Teams",
                column: "TeamsId",
                principalTable: "WorkersByTeams",
                principalColumn: "WorkersByTeamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_WorkersByTeams_WorkerId",
                table: "Workers",
                column: "WorkerId",
                principalTable: "WorkersByTeams",
                principalColumn: "WorkersByTeamId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_WorkersByTeams_TeamsId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_WorkersByTeams_WorkerId",
                table: "Workers");
        }
    }
}
