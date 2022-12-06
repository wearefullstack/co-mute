using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Co_Mute.Data.Migrations
{
    public partial class CL20220911a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");*/

            migrationBuilder.AddColumn<string>(
                name: "TeacherId",
                table: "Course",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Course");

            /*migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");*/
        }
    }
}
