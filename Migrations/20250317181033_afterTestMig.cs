using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ticket.Migrations
{
    /// <inheritdoc />
    public partial class afterTestMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b373137f-0472-4c79-9826-ff255c5cb57f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f815f8ca-6633-4b84-9176-ed263e2daa9a");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Project",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e93f01d-2df2-4867-84f5-3995241293e8", null, "Student", "STUDENT" },
                    { "dbc4bbae-2645-47d5-9bd8-cabf0c7f7066", null, "Supervisor", "SUPERVISOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e93f01d-2df2-4867-84f5-3995241293e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dbc4bbae-2645-47d5-9bd8-cabf0c7f7066");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "Project",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b373137f-0472-4c79-9826-ff255c5cb57f", null, "Student", "STUDENT" },
                    { "f815f8ca-6633-4b84-9176-ed263e2daa9a", null, "Supervisor", "SUPERVISOR" }
                });
        }
    }
}
