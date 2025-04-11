using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisneyBattle.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ajouttokenetrefreshtoken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "utilisateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "utilisateurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "utilisateurs");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "utilisateurs");
        }
    }
}
