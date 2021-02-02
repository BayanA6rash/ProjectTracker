using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracker.Data.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MainProjects",
                columns: table => new
                {
                    MainProjectID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectMangerID = table.Column<string>(nullable: true),
                    ProjectManagerId = table.Column<string>(nullable: true),
                    TeamLeaderID = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainProjects", x => x.MainProjectID);
                    table.ForeignKey(
                        name: "FK_MainProjects_AspNetUsers_ProjectManagerId",
                        column: x => x.ProjectManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MainProjects_AspNetUsers_TeamLeaderID",
                        column: x => x.TeamLeaderID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MainProjectDevelopers",
                columns: table => new
                {
                    DeveloperID = table.Column<string>(nullable: false),
                    MainProjectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainProjectDevelopers", x => new { x.MainProjectID, x.DeveloperID });
                    table.ForeignKey(
                        name: "FK_MainProjectDevelopers_AspNetUsers_DeveloperID",
                        column: x => x.DeveloperID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MainProjectDevelopers_MainProjects_MainProjectID",
                        column: x => x.MainProjectID,
                        principalTable: "MainProjects",
                        principalColumn: "MainProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    SprintID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainProjectID = table.Column<int>(nullable: false),
                    TeamLeaderID = table.Column<string>(nullable: true),
                    SprintTitle = table.Column<string>(nullable: true),
                    SprintDescription = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.SprintID);
                    table.ForeignKey(
                        name: "FK_Sprints_MainProjects_MainProjectID",
                        column: x => x.MainProjectID,
                        principalTable: "MainProjects",
                        principalColumn: "MainProjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sprints_AspNetUsers_TeamLeaderID",
                        column: x => x.TeamLeaderID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "STasks",
                columns: table => new
                {
                    STaskID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SprintID = table.Column<int>(nullable: false),
                    DeveloperId = table.Column<string>(nullable: true),
                    DveloperID = table.Column<string>(nullable: true),
                    STaskTitle = table.Column<string>(nullable: true),
                    STaskDescription = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STasks", x => x.STaskID);
                    table.ForeignKey(
                        name: "FK_STasks_AspNetUsers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STasks_Sprints_SprintID",
                        column: x => x.SprintID,
                        principalTable: "Sprints",
                        principalColumn: "SprintID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    WorkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeveloperID = table.Column<string>(nullable: true),
                    STaskID = table.Column<int>(nullable: false),
                    WorkTitle = table.Column<string>(nullable: true),
                    WorkDescription = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    RejectionNote = table.Column<string>(nullable: true),
                    WorkFile = table.Column<byte[]>(nullable: true),
                    WorkFileName = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.WorkID);
                    table.ForeignKey(
                        name: "FK_Works_AspNetUsers_DeveloperID",
                        column: x => x.DeveloperID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Works_STasks_STaskID",
                        column: x => x.STaskID,
                        principalTable: "STasks",
                        principalColumn: "STaskID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainProjectDevelopers_DeveloperID",
                table: "MainProjectDevelopers",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_MainProjects_ProjectManagerId",
                table: "MainProjects",
                column: "ProjectManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_MainProjects_TeamLeaderID",
                table: "MainProjects",
                column: "TeamLeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_MainProjectID",
                table: "Sprints",
                column: "MainProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_TeamLeaderID",
                table: "Sprints",
                column: "TeamLeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_STasks_DeveloperId",
                table: "STasks",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_STasks_SprintID",
                table: "STasks",
                column: "SprintID");

            migrationBuilder.CreateIndex(
                name: "IX_Works_DeveloperID",
                table: "Works",
                column: "DeveloperID");

            migrationBuilder.CreateIndex(
                name: "IX_Works_STaskID",
                table: "Works",
                column: "STaskID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainProjectDevelopers");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "STasks");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "MainProjects");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
