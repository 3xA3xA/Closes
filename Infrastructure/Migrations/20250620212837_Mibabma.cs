using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mibabma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityMembers_Users_UserId",
                table: "ActivityMembers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ActivityMembers",
                newName: "ActivityMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityMembers_UserId",
                table: "ActivityMembers",
                newName: "IX_ActivityMembers_ActivityMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityMembers_ActivityMembers_ActivityMemberId",
                table: "ActivityMembers",
                column: "ActivityMemberId",
                principalTable: "ActivityMembers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityMembers_ActivityMembers_ActivityMemberId",
                table: "ActivityMembers");

            migrationBuilder.RenameColumn(
                name: "ActivityMemberId",
                table: "ActivityMembers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ActivityMembers_ActivityMemberId",
                table: "ActivityMembers",
                newName: "IX_ActivityMembers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityMembers_Users_UserId",
                table: "ActivityMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
