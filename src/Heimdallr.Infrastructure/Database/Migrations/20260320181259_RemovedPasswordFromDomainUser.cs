using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heimdallr.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPasswordFromDomainUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                schema: "heimdallr",
                table: "users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                schema: "heimdallr",
                table: "users",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }
    }
}
