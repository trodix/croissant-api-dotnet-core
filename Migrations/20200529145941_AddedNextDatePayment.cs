using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CroissantApi.Migrations
{
    public partial class AddedNextDatePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "nextPaymentDate",
                table: "UserRules",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nextPaymentDate",
                table: "UserRules");
        }
    }
}
