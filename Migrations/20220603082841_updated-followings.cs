using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShortStory.API.Migrations
{
    public partial class updatedfollowings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Followings_FollowingId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FollowingId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FollowingId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "InterestedUserId",
                table: "Followings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterestedUserId",
                table: "Followings");

            migrationBuilder.AddColumn<Guid>(
                name: "FollowingId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FollowingId",
                table: "Users",
                column: "FollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Followings_FollowingId",
                table: "Users",
                column: "FollowingId",
                principalTable: "Followings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
