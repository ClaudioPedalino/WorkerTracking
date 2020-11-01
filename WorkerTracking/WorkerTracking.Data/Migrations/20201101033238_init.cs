using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WorkerTracking.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Abbreviation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    WorkerId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 70, nullable: true),
                    Birthday = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    PhotoUrl = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.WorkerId);
                    table.ForeignKey(
                        name: "FK_Workers_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workers_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkersByTeams",
                columns: table => new
                {
                    WorkersByTeamId = table.Column<Guid>(nullable: false),
                    WorkerId = table.Column<Guid>(nullable: false),
                    TeamId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkersByTeams", x => x.WorkersByTeamId);
                    table.ForeignKey(
                        name: "FK_WorkersByTeams_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_WorkersByTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "WorkersByTeams",
                        principalColumn: "WorkersByTeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Abbreviation", "Name" },
                values: new object[,]
                {
                    { 5001, "PO", "Product Owner" },
                    { 50012, "TS", "Technical Support" },
                    { 50011, "HR", "Human Resources" },
                    { 50010, "GD", "Graphic Designer" },
                    { 5008, "UX", "User Experience" },
                    { 5007, "QA", "Quality Assurance" },
                    { 5009, "FA", "Functional Analyst" },
                    { 5005, "BD", "Backeck Developer" },
                    { 5004, "FD", "Frontend Developer" },
                    { 5003, "TL", "Team Leader" },
                    { 5002, "PM", "Project Manager" },
                    { 5006, "FS", "Fullstack Developer" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { 104, "Vacations" },
                    { 101, "Active" },
                    { 102, "Inactive" },
                    { 103, "Pause" },
                    { 105, "In a meeting" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workers_RoleId",
                table: "Workers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_StatusId",
                table: "Workers",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkersByTeams_TeamId",
                table: "WorkersByTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkersByTeams_WorkerId",
                table: "WorkersByTeams",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_WorkersByTeams_WorkerId",
                table: "Workers",
                column: "WorkerId",
                principalTable: "WorkersByTeams",
                principalColumn: "WorkersByTeamId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkersByTeams_Teams_TeamId",
                table: "WorkersByTeams",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "TeamId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_WorkersByTeams_TeamId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_WorkersByTeams_WorkerId",
                table: "Workers");

            migrationBuilder.DropTable(
                name: "WorkersByTeams");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
