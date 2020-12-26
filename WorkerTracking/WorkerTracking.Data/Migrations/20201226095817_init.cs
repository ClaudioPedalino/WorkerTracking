using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace WorkerTracking.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    role_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(maxLength: 40, nullable: false),
                    abbreviation = table.Column<string>(maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    status_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_status", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                columns: table => new
                {
                    team_id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teams", x => x.team_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    user_name = table.Column<string>(nullable: true),
                    normalized_user_name = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    normalized_email = table.Column<string>(nullable: true),
                    email_confirmed = table.Column<bool>(nullable: false),
                    password_hash = table.Column<string>(nullable: true),
                    security_stamp = table.Column<string>(nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "workers",
                columns: table => new
                {
                    worker_id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(maxLength: 50, nullable: false),
                    last_name = table.Column<string>(maxLength: 50, nullable: false),
                    email = table.Column<string>(maxLength: 50, nullable: false),
                    birthday = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    photo_url = table.Column<string>(nullable: true),
                    status_id = table.Column<int>(nullable: false),
                    role_id = table.Column<int>(nullable: false),
                    last_modification_time = table.Column<DateTime>(nullable: false),
                    is_active = table.Column<bool>(nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workers", x => x.worker_id);
                    table.ForeignKey(
                        name: "fk_workers_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_workers_status_status_id",
                        column: x => x.status_id,
                        principalTable: "status",
                        principalColumn: "status_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workers_by_teams",
                columns: table => new
                {
                    workers_by_team_id = table.Column<Guid>(nullable: false),
                    worker_id = table.Column<Guid>(nullable: false),
                    team_id = table.Column<Guid>(nullable: false),
                    team_id1 = table.Column<Guid>(nullable: true),
                    worker_id1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workers_by_teams", x => x.workers_by_team_id);
                    table.ForeignKey(
                        name: "fk_workers_by_teams_teams_team_id",
                        column: x => x.team_id,
                        principalTable: "teams",
                        principalColumn: "team_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_workers_by_teams_teams_team_id1",
                        column: x => x.team_id1,
                        principalTable: "teams",
                        principalColumn: "team_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_workers_by_teams_workers_worker_id",
                        column: x => x.worker_id,
                        principalTable: "workers",
                        principalColumn: "worker_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_workers_by_teams_workers_worker_id1",
                        column: x => x.worker_id1,
                        principalTable: "workers",
                        principalColumn: "worker_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "role_id", "abbreviation", "name" },
                values: new object[,]
                {
                    { 5001, "PO", "Product Owner" },
                    { 5012, "TS", "Technical Support" },
                    { 5011, "HR", "Human Resources" },
                    { 5010, "GD", "Graphic Designer" },
                    { 5008, "UX", "User Experience" },
                    { 5007, "QA", "Quality Assurance" },
                    { 5009, "FA", "Functional Analyst" },
                    { 5005, "BD", "Backend Developer" },
                    { 5004, "FD", "Frontend Developer" },
                    { 5003, "TL", "Team Leader" },
                    { 5002, "PM", "Project Manager" },
                    { 5006, "FS", "Fullstack Developer" }
                });

            migrationBuilder.InsertData(
                table: "status",
                columns: new[] { "status_id", "name" },
                values: new object[,]
                {
                    { 104, "In a meeting" },
                    { 100, "Active" },
                    { 101, "Inactive" },
                    { 103, "Pause" },
                    { 105, "Vacations" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_workers_role_id",
                table: "workers",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_workers_status_id",
                table: "workers",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "ix_workers_by_teams_team_id",
                table: "workers_by_teams",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "ix_workers_by_teams_team_id1",
                table: "workers_by_teams",
                column: "team_id1");

            migrationBuilder.CreateIndex(
                name: "ix_workers_by_teams_worker_id",
                table: "workers_by_teams",
                column: "worker_id");

            migrationBuilder.CreateIndex(
                name: "ix_workers_by_teams_worker_id1",
                table: "workers_by_teams",
                column: "worker_id1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "workers_by_teams");

            migrationBuilder.DropTable(
                name: "teams");

            migrationBuilder.DropTable(
                name: "workers");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "status");
        }
    }
}
