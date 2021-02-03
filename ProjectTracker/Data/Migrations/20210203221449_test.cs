using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectTracker.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainProjects_AspNetUsers_ProjectManagerId",
                table: "MainProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_STasks_AspNetUsers_DeveloperId",
                table: "STasks");

            migrationBuilder.DropColumn(
                name: "DveloperID",
                table: "STasks");

            migrationBuilder.DropColumn(
                name: "ProjectMangerID",
                table: "MainProjects");

            migrationBuilder.RenameColumn(
                name: "DeveloperId",
                table: "STasks",
                newName: "DeveloperID");

            migrationBuilder.RenameIndex(
                name: "IX_STasks_DeveloperId",
                table: "STasks",
                newName: "IX_STasks_DeveloperID");

            migrationBuilder.RenameColumn(
                name: "ProjectManagerId",
                table: "MainProjects",
                newName: "ProjectManagerID");

            migrationBuilder.RenameIndex(
                name: "IX_MainProjects_ProjectManagerId",
                table: "MainProjects",
                newName: "IX_MainProjects_ProjectManagerID");

            migrationBuilder.AddForeignKey(
                name: "FK_MainProjects_AspNetUsers_ProjectManagerID",
                table: "MainProjects",
                column: "ProjectManagerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_STasks_AspNetUsers_DeveloperID",
                table: "STasks",
                column: "DeveloperID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainProjects_AspNetUsers_ProjectManagerID",
                table: "MainProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_STasks_AspNetUsers_DeveloperID",
                table: "STasks");

            migrationBuilder.RenameColumn(
                name: "DeveloperID",
                table: "STasks",
                newName: "DeveloperId");

            migrationBuilder.RenameIndex(
                name: "IX_STasks_DeveloperID",
                table: "STasks",
                newName: "IX_STasks_DeveloperId");

            migrationBuilder.RenameColumn(
                name: "ProjectManagerID",
                table: "MainProjects",
                newName: "ProjectManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_MainProjects_ProjectManagerID",
                table: "MainProjects",
                newName: "IX_MainProjects_ProjectManagerId");

            migrationBuilder.AddColumn<string>(
                name: "DveloperID",
                table: "STasks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectMangerID",
                table: "MainProjects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MainProjects_AspNetUsers_ProjectManagerId",
                table: "MainProjects",
                column: "ProjectManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_STasks_AspNetUsers_DeveloperId",
                table: "STasks",
                column: "DeveloperId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
