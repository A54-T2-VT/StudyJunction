using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyJunction.Infrastructure.Migrations
{
    public partial class AddedIsApprovedToCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Courses");
        }
    }
}
