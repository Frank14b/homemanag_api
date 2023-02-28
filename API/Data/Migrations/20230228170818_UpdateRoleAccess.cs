using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoleAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Roles",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Roles",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BusinesId",
                table: "Roles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppBusiness",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Lng = table.Column<string>(type: "TEXT", nullable: true),
                    Lat = table.Column<string>(type: "TEXT", nullable: true),
                    CountryCode = table.Column<int>(type: "INTEGER", nullable: false),
                    PhoneNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Reference = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBusiness", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_BusinesId",
                table: "Roles",
                column: "BusinesId");

            migrationBuilder.CreateIndex(
                name: "IX_Roleaccess_AccesId",
                table: "Roleaccess",
                column: "AccesId");

            migrationBuilder.CreateIndex(
                name: "IX_Roleaccess_RoleId",
                table: "Roleaccess",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roleaccess_Access_AccesId",
                table: "Roleaccess",
                column: "AccesId",
                principalTable: "Access",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roleaccess_Roles_RoleId",
                table: "Roleaccess",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_AppBusiness_BusinesId",
                table: "Roles",
                column: "BusinesId",
                principalTable: "AppBusiness",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roleaccess_Access_AccesId",
                table: "Roleaccess");

            migrationBuilder.DropForeignKey(
                name: "FK_Roleaccess_Roles_RoleId",
                table: "Roleaccess");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_AppBusiness_BusinesId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "AppBusiness");

            migrationBuilder.DropIndex(
                name: "IX_Roles_BusinesId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roleaccess_AccesId",
                table: "Roleaccess");

            migrationBuilder.DropIndex(
                name: "IX_Roleaccess_RoleId",
                table: "Roleaccess");

            migrationBuilder.DropColumn(
                name: "BusinesId",
                table: "Roles");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Roles",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Roles",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
