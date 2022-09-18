using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sprintplanningtoolbackend.Migrations.SqliteMigrations
{
    public partial class makeSprintReportTable : Migration
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

            migrationBuilder.CreateIndex(
                name: "IX_Users_SprintReportId",
                table: "Users",
                column: "SprintReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_SprintReports_SprintReportId",
                table: "Users",
                column: "SprintReportId",
                principalTable: "SprintReports",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_SprintReports_SprintReportId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "SprintReports");

            migrationBuilder.DropIndex(
                name: "IX_Users_SprintReportId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SprintReportId",
                table: "Users");
        }
    }
}
