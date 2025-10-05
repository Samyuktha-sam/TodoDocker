using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Task",
                columns: new[] { "Id", "CreatedAt", "Description", "IsCompleted", "Priority", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 5, 11, 18, 20, 384, DateTimeKind.Utc).AddTicks(634), "Buy books for the next school year", false, 0, "Buy books" },
                    { 2, new DateTime(2025, 10, 5, 11, 18, 20, 384, DateTimeKind.Utc).AddTicks(637), "Need to clean the bed room", false, 0, "Clean home" },
                    { 3, new DateTime(2025, 10, 5, 11, 18, 20, 384, DateTimeKind.Utc).AddTicks(639), "Finish the mid-term assignment", false, 0, "Takehome assignment" },
                    { 4, new DateTime(2025, 10, 5, 11, 18, 20, 384, DateTimeKind.Utc).AddTicks(641), "Plan the soft ball cricket match on next Sunday", false, 0, "Play Cricket" },
                    { 5, new DateTime(2025, 10, 5, 11, 18, 20, 384, DateTimeKind.Utc).AddTicks(643), "Saman need help with his software project", false, 0, "Help Saman" },
                    { 6, new DateTime(2025, 10, 5, 11, 18, 20, 384, DateTimeKind.Utc).AddTicks(645), "Schedule a check-up appointment for next month", false, 1, "Call Dentist" },
                    { 7, new DateTime(2025, 10, 5, 11, 18, 20, 384, DateTimeKind.Utc).AddTicks(646), "Need to buy milk, bread, and eggs", false, 2, "Grocery Shopping" },
                    { 8, new DateTime(2025, 10, 5, 11, 18, 20, 384, DateTimeKind.Utc).AddTicks(648), "Complete the first two chapters of the online course", false, 0, "Learn Python" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Task",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
