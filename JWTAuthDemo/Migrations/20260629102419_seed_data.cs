using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAuthDemo.Migrations
{
    /// <inheritdoc />
    public partial class seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "IsActive", "PasswordHash", "Role", "Username" },
                values: new object[] { new Guid("6caa6e81-a3bd-4d83-be5c-464a7d02d9a5"), new DateTime(2026, 6, 29, 10, 24, 19, 671, DateTimeKind.Utc).AddTicks(2470), "khanh@gmail.com", true, "123456", "Admin", "khanh" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6caa6e81-a3bd-4d83-be5c-464a7d02d9a5"));
        }
    }
}
