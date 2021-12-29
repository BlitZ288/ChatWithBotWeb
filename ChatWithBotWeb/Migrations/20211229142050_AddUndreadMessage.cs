using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatWithBotWeb.Migrations
{
    public partial class AddUndreadMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Undread",
                table: "Messages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Undread",
                table: "Messages");
        }
    }
}
