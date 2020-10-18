using Microsoft.EntityFrameworkCore.Migrations;

namespace Task5.Migrations
{
    public partial class Last : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "Messages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
