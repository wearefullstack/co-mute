using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Co_Mute.Data.Migrations
{
    public partial class CL20221206e : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Friday",
                table: "Oppertunities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Monday",
                table: "Oppertunities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Saturday",
                table: "Oppertunities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Sunday",
                table: "Oppertunities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Thursday",
                table: "Oppertunities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Tuesday",
                table: "Oppertunities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Wednesday",
                table: "Oppertunities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Friday",
                table: "Oppertunities");

            migrationBuilder.DropColumn(
                name: "Monday",
                table: "Oppertunities");

            migrationBuilder.DropColumn(
                name: "Saturday",
                table: "Oppertunities");

            migrationBuilder.DropColumn(
                name: "Sunday",
                table: "Oppertunities");

            migrationBuilder.DropColumn(
                name: "Thursday",
                table: "Oppertunities");

            migrationBuilder.DropColumn(
                name: "Tuesday",
                table: "Oppertunities");

            migrationBuilder.DropColumn(
                name: "Wednesday",
                table: "Oppertunities");
        }
    }
}
