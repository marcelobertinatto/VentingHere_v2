using Microsoft.EntityFrameworkCore.Migrations;

namespace VentingHere.Infra.Migrations
{
    public partial class dbWithIdentity_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoginName",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LoginName",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
