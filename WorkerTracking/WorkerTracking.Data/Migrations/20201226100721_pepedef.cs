using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WorkerTracking.Data.Migrations
{
    public partial class pepedef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_workers_by_teams_teams_team_id1",
                table: "workers_by_teams");

            migrationBuilder.DropForeignKey(
                name: "fk_workers_by_teams_workers_worker_id1",
                table: "workers_by_teams");

            migrationBuilder.DropIndex(
                name: "ix_workers_by_teams_team_id1",
                table: "workers_by_teams");

            migrationBuilder.DropIndex(
                name: "ix_workers_by_teams_worker_id1",
                table: "workers_by_teams");

            migrationBuilder.DropColumn(
                name: "team_id1",
                table: "workers_by_teams");

            migrationBuilder.DropColumn(
                name: "worker_id1",
                table: "workers_by_teams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "team_id1",
                table: "workers_by_teams",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "worker_id1",
                table: "workers_by_teams",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_workers_by_teams_team_id1",
                table: "workers_by_teams",
                column: "team_id1");

            migrationBuilder.CreateIndex(
                name: "ix_workers_by_teams_worker_id1",
                table: "workers_by_teams",
                column: "worker_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_workers_by_teams_teams_team_id1",
                table: "workers_by_teams",
                column: "team_id1",
                principalTable: "teams",
                principalColumn: "team_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_workers_by_teams_workers_worker_id1",
                table: "workers_by_teams",
                column: "worker_id1",
                principalTable: "workers",
                principalColumn: "worker_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
