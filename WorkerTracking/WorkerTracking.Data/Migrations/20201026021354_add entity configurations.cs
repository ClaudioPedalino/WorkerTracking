using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkerTracking.Data.Migrations
{
    public partial class addentityconfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Teams",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Teams",
                newName: "TeamsId");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId1",
                table: "WorkersByTeams",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkerId1",
                table: "WorkersByTeams",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Workers",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Workers",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Workers",
                maxLength: 70,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Workers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Teams",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Status",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Teams",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TeamsId",
                table: "Teams",
                newName: "TeamId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Workers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Workers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Workers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 70,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birthday",
                table: "Workers",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Teams",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Status",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
