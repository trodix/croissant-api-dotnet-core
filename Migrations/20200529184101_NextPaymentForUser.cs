using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CroissantApi.Migrations
{
    public partial class NextPaymentForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nextPaymentDate",
                table: "UserRules");

            migrationBuilder.AddColumn<DateTime>(
                name: "nextPaymentDate",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nextPaymentDate",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "nextPaymentDate",
                table: "UserRules",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
