using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatWithBotWeb.Migrations
{
    public partial class InitLogAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogAction_Chats_ChatId",
                table: "LogAction");

            migrationBuilder.DropForeignKey(
                name: "FK_LogAction_Users_UserId",
                table: "LogAction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LogAction",
                table: "LogAction");

            migrationBuilder.RenameTable(
                name: "LogAction",
                newName: "LogActions");

            migrationBuilder.RenameIndex(
                name: "IX_LogAction_UserId",
                table: "LogActions",
                newName: "IX_LogActions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LogAction_ChatId",
                table: "LogActions",
                newName: "IX_LogActions_ChatId");

            migrationBuilder.AddColumn<bool>(
                name: "Undread",
                table: "LogActions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogActions",
                table: "LogActions",
                column: "LogActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogActions_Chats_ChatId",
                table: "LogActions",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LogActions_Users_UserId",
                table: "LogActions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogActions_Chats_ChatId",
                table: "LogActions");

            migrationBuilder.DropForeignKey(
                name: "FK_LogActions_Users_UserId",
                table: "LogActions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LogActions",
                table: "LogActions");

            migrationBuilder.DropColumn(
                name: "Undread",
                table: "LogActions");

            migrationBuilder.RenameTable(
                name: "LogActions",
                newName: "LogAction");

            migrationBuilder.RenameIndex(
                name: "IX_LogActions_UserId",
                table: "LogAction",
                newName: "IX_LogAction_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LogActions_ChatId",
                table: "LogAction",
                newName: "IX_LogAction_ChatId");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "LogAction",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogAction",
                table: "LogAction",
                column: "LogActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogAction_Chats_ChatId",
                table: "LogAction",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LogAction_Users_UserId",
                table: "LogAction",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
