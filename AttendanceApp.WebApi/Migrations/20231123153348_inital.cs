using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AttendanceApp.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    PasswrodHash = table.Column<string>(type: "TEXT", nullable: true),
                    Role = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttenanceRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AttendanceDay = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClockedIn = table.Column<bool>(type: "INTEGER", nullable: false),
                    ClockedInAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ClockedOut = table.Column<bool>(type: "INTEGER", nullable: false),
                    ClockedOutAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttenanceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttenanceRecords_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName", "PasswrodHash", "Role", "UserName" },
                values: new object[,]
                {
                    { new Guid("04b5a52a-94e6-40f6-b05a-db12371aac61"), "User 2 Full Name", "user 2 password hashed", "employee", "UserName2" },
                    { new Guid("12919d31-d60e-4d1e-bb78-16ab49534266"), "User 3 Full Name", "user 3 password hashed", "employee", "UserName3" },
                    { new Guid("813064c2-c9c6-49b1-98db-fc7baf4ba2e5"), "User 1 Full Name", "user 1 password hashed", "employee", "UserName1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttenanceRecords_UserId",
                table: "AttenanceRecords",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttenanceRecords");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
