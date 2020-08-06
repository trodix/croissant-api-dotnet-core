using Microsoft.EntityFrameworkCore.Migrations;

namespace CroissantApi.Migrations
{
    public partial class AuthenticationRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AuthenticatedUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "AuthenticatedUsers");
        }
    }
}
