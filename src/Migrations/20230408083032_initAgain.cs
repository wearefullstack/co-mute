using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSWebApi.Migrations
{
    /// <inheritdoc />
    public partial class initAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Surname = table.Column<string>(type: "longtext", nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: true),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "carpools",
                columns: table => new
                {
                    CarpoolId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Origin = table.Column<string>(type: "longtext", nullable: false),
                    Destination = table.Column<string>(type: "longtext", nullable: false),
                    DayAvailable = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DepartureTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    ArrivalTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: true),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    OwnerID = table.Column<Guid>(type: "char(36)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carpools", x => x.CarpoolId);
                    table.ForeignKey(
                        name: "FK_carpools_users_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "carpoolMembers",
                columns: table => new
                {
                    CarpoolId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carpoolMembers", x => new { x.CarpoolId, x.UserId });
                    table.ForeignKey(
                        name: "FK_carpoolMembers_carpools_CarpoolId",
                        column: x => x.CarpoolId,
                        principalTable: "carpools",
                        principalColumn: "CarpoolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_carpoolMembers_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_carpoolMembers_UserId",
                table: "carpoolMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_carpools_OwnerID",
                table: "carpools",
                column: "OwnerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "carpoolMembers");

            migrationBuilder.DropTable(
                name: "carpools");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
