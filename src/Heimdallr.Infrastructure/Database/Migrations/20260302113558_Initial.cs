using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Heimdallr.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "heimdallr");

            migrationBuilder.CreateTable(
                name: "meters",
                schema: "heimdallr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Model = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SerialNumber = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "proxy_ports",
                schema: "heimdallr",
                columns: table => new
                {
                    Port = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    State = table.Column<int>(type: "integer", nullable: false),
                    ReservedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastUsedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proxy_ports", x => x.Port);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "heimdallr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastLoginAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "meter_endpoints",
                schema: "heimdallr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MeterId = table.Column<Guid>(type: "uuid", nullable: false),
                    TransportType = table.Column<int>(type: "integer", nullable: false),
                    Host = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_meter_endpoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_meter_endpoints_meters_MeterId",
                        column: x => x.MeterId,
                        principalSchema: "heimdallr",
                        principalTable: "meters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "proxy_sessions",
                schema: "heimdallr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MeterId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProxyPort = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    ClientIpPolicy = table.Column<int>(type: "integer", nullable: false),
                    PinnedClientIp = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    StartedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastActivityAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LeaseUntil = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    EndedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    BytesFromClient = table.Column<long>(type: "bigint", nullable: false),
                    BytesFromMeter = table.Column<long>(type: "bigint", nullable: false),
                    FaultReason = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proxy_sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_proxy_sessions_meters_MeterId",
                        column: x => x.MeterId,
                        principalSchema: "heimdallr",
                        principalTable: "meters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_proxy_sessions_proxy_ports_ProxyPort",
                        column: x => x.ProxyPort,
                        principalSchema: "heimdallr",
                        principalTable: "proxy_ports",
                        principalColumn: "Port",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_proxy_sessions_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "heimdallr",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_ip_rules",
                schema: "heimdallr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    IpOrCidr = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_ip_rules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_ip_rules_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "heimdallr",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_meter_endpoints_MeterId_IsEnabled",
                schema: "heimdallr",
                table: "meter_endpoints",
                columns: new[] { "MeterId", "IsEnabled" });

            migrationBuilder.CreateIndex(
                name: "IX_meters_IsEnabled",
                schema: "heimdallr",
                table: "meters",
                column: "IsEnabled");

            migrationBuilder.CreateIndex(
                name: "IX_meters_SerialNumber",
                schema: "heimdallr",
                table: "meters",
                column: "SerialNumber");

            migrationBuilder.CreateIndex(
                name: "IX_proxy_sessions_status",
                schema: "heimdallr",
                table: "proxy_sessions",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_proxy_sessions_UserId",
                schema: "heimdallr",
                table: "proxy_sessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_proxy_sessions_UserId_status",
                schema: "heimdallr",
                table: "proxy_sessions",
                columns: new[] { "UserId", "status" });

            migrationBuilder.CreateIndex(
                name: "ux_proxy_sessions_meter_active",
                schema: "heimdallr",
                table: "proxy_sessions",
                column: "MeterId",
                unique: true,
                filter: "status IN (1, 2)");

            migrationBuilder.CreateIndex(
                name: "ux_proxy_sessions_port_active",
                schema: "heimdallr",
                table: "proxy_sessions",
                column: "ProxyPort",
                unique: true,
                filter: "status IN (1, 2)");

            migrationBuilder.CreateIndex(
                name: "IX_user_ip_rules_UserId_IpOrCidr",
                schema: "heimdallr",
                table: "user_ip_rules",
                columns: new[] { "UserId", "IpOrCidr" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_ip_rules_UserId_IsEnabled",
                schema: "heimdallr",
                table: "user_ip_rules",
                columns: new[] { "UserId", "IsEnabled" });

            migrationBuilder.CreateIndex(
                name: "IX_users_UserName",
                schema: "heimdallr",
                table: "users",
                column: "UserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "meter_endpoints",
                schema: "heimdallr");

            migrationBuilder.DropTable(
                name: "proxy_sessions",
                schema: "heimdallr");

            migrationBuilder.DropTable(
                name: "user_ip_rules",
                schema: "heimdallr");

            migrationBuilder.DropTable(
                name: "meters",
                schema: "heimdallr");

            migrationBuilder.DropTable(
                name: "proxy_ports",
                schema: "heimdallr");

            migrationBuilder.DropTable(
                name: "users",
                schema: "heimdallr");
        }
    }
}
