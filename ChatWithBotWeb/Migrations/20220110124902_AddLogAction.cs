using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatWithBotWeb.Migrations
{
    public partial class AddLogAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "LogAction",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FixLog",
                table: "LogAction",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "LogAction",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Chats",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LogAction_UserId",
                table: "LogAction",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogAction_Users_UserId",
                table: "LogAction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogAction_Users_UserId",
                table: "LogAction");

            migrationBuilder.DropIndex(
                name: "IX_LogAction_UserId",
                table: "LogAction");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "LogAction");

            migrationBuilder.DropColumn(
                name: "FixLog",
                table: "LogAction");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LogAction");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Chats",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
