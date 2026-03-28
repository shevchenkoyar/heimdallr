using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Heimdallr.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Mergedomainandidentityuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                schema: "heimdallr",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                schema: "heimdallr",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                schema: "heimdallr",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                schema: "heimdallr",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "heimdallr");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "heimdallr",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "heimdallr",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                schema: "heimdallr",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                schema: "heimdallr",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "heimdallr",
                table: "users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                schema: "heimdallr",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                schema: "heimdallr",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                schema: "heimdallr",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                schema: "heimdallr",
                table: "users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                schema: "heimdallr",
                table: "users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                schema: "heimdallr",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "heimdallr",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                schema: "heimdallr",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                schema: "heimdallr",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                schema: "heimdallr",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "heimdallr",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "heimdallr",
                table: "users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_users_UserId",
                schema: "heimdallr",
                table: "AspNetUserClaims",
                column: "UserId",
                principalSchema: "heimdallr",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_users_UserId",
                schema: "heimdallr",
                table: "AspNetUserLogins",
                column: "UserId",
                principalSchema: "heimdallr",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_users_UserId",
                schema: "heimdallr",
                table: "AspNetUserRoles",
                column: "UserId",
                principalSchema: "heimdallr",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_users_UserId",
                schema: "heimdallr",
                table: "AspNetUserTokens",
                column: "UserId",
                principalSchema: "heimdallr",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_users_UserId",
                schema: "heimdallr",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_users_UserId",
                schema: "heimdallr",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_users_UserId",
                schema: "heimdallr",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_users_UserId",
                schema: "heimdallr",
                table: "AspNetUserTokens");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                schema: "heimdallr",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "heimdallr",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "heimdallr",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "heimdallr",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "heimdallr",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "heimdallr",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                schema: "heimdallr",
                table: "AspNetUserClaims",
                column: "UserId",
                principalSchema: "heimdallr",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                schema: "heimdallr",
                table: "AspNetUserLogins",
                column: "UserId",
                principalSchema: "heimdallr",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                schema: "heimdallr",
                table: "AspNetUserRoles",
                column: "UserId",
                principalSchema: "heimdallr",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                schema: "heimdallr",
                table: "AspNetUserTokens",
                column: "UserId",
                principalSchema: "heimdallr",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
