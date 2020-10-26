using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkerTracking.Data.Migrations
{
    public partial class fixworkersbyteamrelationsmappings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkersByTeams_Teams_TeamId1",
                table: "WorkersByTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkersByTeams_Workers_WorkerId1",
                table: "WorkersByTeams");

            migrationBuilder.DropIndex(
                name: "IX_WorkersByTeams_TeamId1",
                table: "WorkersByTeams");

            migrationBuilder.DropIndex(
                name: "IX_WorkersByTeams_WorkerId1",
                table: "WorkersByTeams");

            migrationBuilder.DropColumn(
                name: "TeamId1",
                table: "WorkersByTeams");

            migrationBuilder.DropColumn(
                name: "WorkerId1",
                table: "WorkersByTeams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeamId1",
                table: "WorkersByTeams",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkerId1",
                table: "WorkersByTeams",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkersByTeams_TeamId1",
                table: "WorkersByTeams",
                column: "TeamId1");

            migrationBuilder.CreateIndex(
                name: "IX_WorkersByTeams_WorkerId1",
                table: "WorkersByTeams",
                column: "WorkerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersByTeams_Teams_TeamId1",
                table: "WorkersByTeams",
                column: "TeamId1",
                principalTable: "Teams",
                principalColumn: "TeamsId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersByTeams_Workers_WorkerId1",
                table: "WorkersByTeams",
                column: "WorkerId1",
                principalTable: "Workers",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
