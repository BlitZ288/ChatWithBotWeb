using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatWithBotWeb.Migrations
{
    public partial class AddNameBot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameBot",
                table: "Messages",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameBot",
                table: "Messages");
        }
    }
}
