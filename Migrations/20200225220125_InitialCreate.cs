using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CroissantApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CoinsCapacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    RuleId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Rules_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeamRule",
                columns: table => new
                {
                    TeamId = table.Column<int>(nullable: false),
                    RuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamRule", x => new { x.TeamId, x.RuleId });
                    table.ForeignKey(
                        name: "FK_TeamRule_Rules_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamRule_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeamId = table.Column<int>(nullable: false),
                    Lastname = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRules",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RuleId = table.Column<int>(nullable: false),
                    CoinsQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRules", x => new { x.UserId, x.RuleId });
                    table.ForeignKey(
                        name: "FK_UserRules_Rules_RuleId",
                        column: x => x.RuleId,
                        principalTable: "Rules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRules_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rules",
                columns: new[] { "Id", "CoinsCapacity", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Fais un Win+L sinon tu paie les croissants, tu as 1 chance !!!", "Ton ordi !" },
                    { 2, 3, "Quand tu as fini la journée, tu met ta chaise sur ta table, sinon tu paie les croissants. Tu as 3 chances.", "La chaise !" },
                    { 3, 3, "Quand tu sors tu ferme la porte derière toi, sinon tu paie les croissants. Tu as 3 chances.", "La porte !" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Name", "RuleId" },
                values: new object[] { 1, "CESI RIL B2 aka Croissanistan", null });

            migrationBuilder.InsertData(
                table: "TeamRule",
                columns: new[] { "TeamId", "RuleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Firstname", "Lastname", "TeamId" },
                values: new object[,]
                {
                    { 1, new DateTime(1997, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sébastien", "Vallet", 1 },
                    { 2, new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sylvain", "Bayon", 1 }
                });

            migrationBuilder.InsertData(
                table: "UserRules",
                columns: new[] { "UserId", "RuleId", "CoinsQuantity" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamRule_RuleId",
                table: "TeamRule",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_RuleId",
                table: "Teams",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRules_RuleId",
                table: "UserRules",
                column: "RuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeamId",
                table: "Users",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamRule");

            migrationBuilder.DropTable(
                name: "UserRules");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Rules");
        }
    }
}
