using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CroissantApi.Migrations
{
    public partial class PaymentRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentRecords",
                columns: table => new
                {
                    UserRuleId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserRuleUserId = table.Column<int>(nullable: false),
                    UserRuleRuleId = table.Column<int>(nullable: false),
                    PayedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRecords", x => x.UserRuleId);
                    table.ForeignKey(
                        name: "FK_PaymentRecords_UserRules_UserRuleUserId_UserRuleRuleId",
                        columns: x => new { x.UserRuleUserId, x.UserRuleRuleId },
                        principalTable: "UserRules",
                        principalColumns: new[] { "UserId", "RuleId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRecords_UserRuleUserId_UserRuleRuleId",
                table: "PaymentRecords",
                columns: new[] { "UserRuleUserId", "UserRuleRuleId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentRecords");
        }
    }
}
