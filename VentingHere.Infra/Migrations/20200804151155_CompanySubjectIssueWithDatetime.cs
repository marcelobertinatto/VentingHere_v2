using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VentingHere.Infra.Migrations
{
    public partial class CompanySubjectIssueWithDatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateAndTime",
                table: "CompanySubjectIssue",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAndTime",
                table: "CompanySubjectIssue");
        }
    }
}
