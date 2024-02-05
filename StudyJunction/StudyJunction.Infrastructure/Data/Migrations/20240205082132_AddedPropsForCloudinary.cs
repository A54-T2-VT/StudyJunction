using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyJunction.Web.Data.Migrations
{
    public partial class AddedPropsForCloudinary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserAssignment",
                table: "UsersLectures");

            migrationBuilder.DropColumn(
                name: "Assignment",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "VideoLink",
                table: "Lectures",
                newName: "VideoLinkCloudinaryUri");

            migrationBuilder.AddColumn<string>(
                name: "AssignmentCloudinaryId",
                table: "UsersLectures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignmentCloudinaryUri",
                table: "UsersLectures",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssignmentCloudinaryId",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AssignmentCloudinaryUri",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VideoLinkCloudinaryId",
                table: "Lectures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageCloudinaryId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageCloudinaryUri",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignmentCloudinaryId",
                table: "UsersLectures");

            migrationBuilder.DropColumn(
                name: "AssignmentCloudinaryUri",
                table: "UsersLectures");

            migrationBuilder.DropColumn(
                name: "AssignmentCloudinaryId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "AssignmentCloudinaryUri",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "VideoLinkCloudinaryId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "ProfileImageCloudinaryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImageCloudinaryUri",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "VideoLinkCloudinaryUri",
                table: "Lectures",
                newName: "VideoLink");

            migrationBuilder.AddColumn<byte[]>(
                name: "UserAssignment",
                table: "UsersLectures",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Assignment",
                table: "Lectures",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImage",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
