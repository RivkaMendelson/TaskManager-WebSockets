using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeTasksReact.Data.Migrations
{
    public partial class AddedUserIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_TakenById",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "TakenById",
                table: "Tasks",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_TakenById",
                table: "Tasks",
                newName: "IX_Tasks_UserId");

            migrationBuilder.AddColumn<int>(
                name: "HandledById",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_UserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "HandledById",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tasks",
                newName: "TakenById");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                newName: "IX_Tasks_TakenById");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_TakenById",
                table: "Tasks",
                column: "TakenById",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
