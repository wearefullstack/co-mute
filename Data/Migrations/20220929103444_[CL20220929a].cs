using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Co_Mute.Data.Migrations
{
    public partial class CL20220929a : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "MyCourses");

            migrationBuilder.DropColumn(
                name: "NewMark",
                table: "MyCourses");

            migrationBuilder.CreateTable(
                name: "AssignmentResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewMark = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentResults", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentResults");

            migrationBuilder.AddColumn<Guid>(
                name: "AssignmentId",
                table: "MyCourses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<float>(
                name: "NewMark",
                table: "MyCourses",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
