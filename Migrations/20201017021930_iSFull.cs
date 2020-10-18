using Microsoft.EntityFrameworkCore.Migrations;

namespace Task5.Migrations
{
    public partial class iSFull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isFull",
                table: "Chats",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isFull",
                table: "Chats");
        }
    }
}
