using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Co_Mute.Data.Migrations
{
    public partial class BP20221009e : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "BeneficiaryName",
            //    table: "Transactions",
            //    newName: "AccountOwner");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "AccountOwner",
            //    table: "Transactions",
            //    newName: "BeneficiaryName");
        }
    }
}
