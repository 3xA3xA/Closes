using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWishlistItemToUseGroupMemberId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Users_UserId",
                table: "Wishlists");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Wishlists",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Wishlists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "WishlistItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupMemberId",
                table: "WishlistItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_GroupId",
                table: "Wishlists",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistItems_GroupMemberId",
                table: "WishlistItems",
                column: "GroupMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_GroupMembers_GroupMemberId",
                table: "WishlistItems",
                column: "GroupMemberId",
                principalTable: "GroupMembers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Groups_GroupId",
                table: "Wishlists",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Users_UserId",
                table: "Wishlists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_GroupMembers_GroupMemberId",
                table: "WishlistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Groups_GroupId",
                table: "Wishlists");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Users_UserId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_GroupId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_WishlistItems_GroupMemberId",
                table: "WishlistItems");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "GroupMemberId",
                table: "WishlistItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Wishlists",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "WishlistItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Users_UserId",
                table: "Wishlists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
