using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AttendanceApp.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class inital2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("04b5a52a-94e6-40f6-b05a-db12371aac61"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("12919d31-d60e-4d1e-bb78-16ab49534266"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("813064c2-c9c6-49b1-98db-fc7baf4ba2e5"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName", "PasswrodHash", "Role", "UserName" },
                values: new object[,]
                {
                    { new Guid("74dc724f-56b6-47c2-859b-10fc36fbdeb5"), "User 1 Full Name", "user 1 password hashed", "employee", "UserName1" },
                    { new Guid("a03783cb-d63c-4c85-8dbd-c29e31566a7b"), "User 3 Full Name", "user 3 password hashed", "employee", "UserName3" },
                    { new Guid("c66ee6e9-bbca-480b-9bc6-2d52f680b9b5"), "User 2 Full Name", "user 2 password hashed", "employee", "UserName2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("74dc724f-56b6-47c2-859b-10fc36fbdeb5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a03783cb-d63c-4c85-8dbd-c29e31566a7b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c66ee6e9-bbca-480b-9bc6-2d52f680b9b5"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName", "PasswrodHash", "Role", "UserName" },
                values: new object[,]
                {
                    { new Guid("04b5a52a-94e6-40f6-b05a-db12371aac61"), "User 2 Full Name", "user 2 password hashed", "employee", "UserName2" },
                    { new Guid("12919d31-d60e-4d1e-bb78-16ab49534266"), "User 3 Full Name", "user 3 password hashed", "employee", "UserName3" },
                    { new Guid("813064c2-c9c6-49b1-98db-fc7baf4ba2e5"), "User 1 Full Name", "user 1 password hashed", "employee", "UserName1" }
                });
        }
    }
}
