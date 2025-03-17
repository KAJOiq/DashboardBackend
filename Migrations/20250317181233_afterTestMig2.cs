using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ticket.Migrations
{
    /// <inheritdoc />
    public partial class afterTestMig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e93f01d-2df2-4867-84f5-3995241293e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dbc4bbae-2645-47d5-9bd8-cabf0c7f7066");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Project",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2b7fb21a-b8ed-4d80-b9f1-261a196d1191", null, "Student", "STUDENT" },
                    { "ea25fb43-d44f-4789-b99b-034f1beecac2", null, "Supervisor", "SUPERVISOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b7fb21a-b8ed-4d80-b9f1-261a196d1191");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea25fb43-d44f-4789-b99b-034f1beecac2");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Project",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e93f01d-2df2-4867-84f5-3995241293e8", null, "Student", "STUDENT" },
                    { "dbc4bbae-2645-47d5-9bd8-cabf0c7f7066", null, "Supervisor", "SUPERVISOR" }
                });
        }
    }
}
