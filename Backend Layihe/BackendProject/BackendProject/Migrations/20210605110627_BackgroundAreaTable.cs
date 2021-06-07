using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendProject.Migrations
{
    public partial class BackgroundAreaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BackgroundAreas");

            migrationBuilder.DropColumn(
                name: "LearnMore",
                table: "BackgroundAreas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BackgroundAreas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LearnMore",
                table: "BackgroundAreas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
