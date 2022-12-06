using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Co_Mute.Data.Migrations
{
    public partial class BP20221022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "UpdateMyAssignments",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Mark = table.Column<float>(type: "real", nullable: false),
            //        Weight = table.Column<float>(type: "real", nullable: false),
            //        attachment = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_UpdateMyAssignments", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "UpdateMyAssignments");
        }
    }
}
