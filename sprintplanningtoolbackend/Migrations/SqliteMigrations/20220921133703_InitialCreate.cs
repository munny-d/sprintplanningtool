using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sprintplanningtoolbackend.Migrations.SqliteMigrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SprintReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SprintStartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SprintEndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TeamSize = table.Column<int>(type: "INTEGER", nullable: true),
                    AbsentDays = table.Column<int>(type: "INTEGER", nullable: true),
                    WorkDays = table.Column<int>(type: "INTEGER", nullable: true),
                    Capacity = table.Column<int>(type: "INTEGER", nullable: true),
                    NewSP = table.Column<int>(type: "INTEGER", nullable: true),
                    CarriedSP = table.Column<int>(type: "INTEGER", nullable: true),
                    TotalSP = table.Column<int>(type: "INTEGER", nullable: true),
                    SprintGoal = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedByUser = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintReports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: true),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    SprintReportId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamMembers_SprintReports_SprintReportId",
                        column: x => x.SprintReportId,
                        principalTable: "SprintReports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_SprintReportId",
                table: "TeamMembers",
                column: "SprintReportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SprintReports");
        }
    }
}
