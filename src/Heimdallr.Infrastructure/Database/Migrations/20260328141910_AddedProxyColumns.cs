using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heimdallr.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedProxyColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Port",
                schema: "heimdallr",
                table: "meter_endpoints",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaJson",
                schema: "heimdallr",
                table: "meter_endpoints",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetaJson",
                schema: "heimdallr",
                table: "meter_endpoints");

            migrationBuilder.AlterColumn<int>(
                name: "Port",
                schema: "heimdallr",
                table: "meter_endpoints",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
