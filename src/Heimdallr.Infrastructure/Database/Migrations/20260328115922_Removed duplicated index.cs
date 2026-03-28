using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heimdallr.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Removedduplicatedindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_UserName",
                schema: "heimdallr",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_users_UserName",
                schema: "heimdallr",
                table: "users",
                column: "UserName",
                unique: true);
        }
    }
}
