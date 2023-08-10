using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealTimeTasksReact.Data.Migrations
{
    public partial class RemovedUserIdColumnn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HandledById",
                table: "Tasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HandledById",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
