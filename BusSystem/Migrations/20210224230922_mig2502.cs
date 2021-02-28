using Microsoft.EntityFrameworkCore.Migrations;

namespace BusSystem.Migrations
{
    public partial class mig2502 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusNum",
                table: "Buses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusNum",
                table: "Buses");
        }
    }
}
