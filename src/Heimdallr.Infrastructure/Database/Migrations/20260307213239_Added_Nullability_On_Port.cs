using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heimdallr.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Added_Nullability_On_Port : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Port",
                schema: "heimdallr",
                table: "meter_endpoints",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
