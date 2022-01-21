using Microsoft.EntityFrameworkCore.Migrations;

namespace MNS.Services.Plan.Migrations
{
    public partial class MobilePlanDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MobilePlans",
                columns: table => new
                {
                    Plan_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerAge = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    CustomerType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ValidityPeriod = table.Column<int>(type: "int", maxLength: 365, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobilePlans", x => x.Plan_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobilePlans");
        }
    }
}
