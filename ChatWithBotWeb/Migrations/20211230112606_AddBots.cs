using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatWithBotWeb.Migrations
{
    public partial class AddBots : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<List<string>>(
                name: "NameBots",
                table: "Chats",
                type: "text[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameBots",
                table: "Chats");
        }
    }
}
