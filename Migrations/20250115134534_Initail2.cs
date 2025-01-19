using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ticket.Migrations
{
    /// <inheritdoc />
    public partial class Initail2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73261b3e-7854-4db3-857b-45864c456dec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8daaa517-57b5-4931-b173-c32a2efc1478");

            migrationBuilder.CreateTable(
                name: "MainProblems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainProblems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainProblems_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TicketId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    Photo_url = table.Column<string>(type: "text", nullable: false),
                    AssignorId = table.Column<string>(type: "text", nullable: false),
                    AssignId = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Replies_AspNetUsers_AssignId",
                        column: x => x.AssignId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Replies_AspNetUsers_AssignorId",
                        column: x => x.AssignorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Replies_Ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubProblems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MainProblemId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProblems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubProblems_MainProblems_MainProblemId",
                        column: x => x.MainProblemId,
                        principalTable: "MainProblems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1fa1ffa2-0433-408e-bf89-ba352903350a", null, "User", "USER" },
                    { "ce10d48e-26de-4bb2-bd11-806b4de1df01", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainProblems_DepartmentId",
                table: "MainProblems",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_AssignId",
                table: "Replies",
                column: "AssignId");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_AssignorId",
                table: "Replies",
                column: "AssignorId");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_TicketId",
                table: "Replies",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProblems_MainProblemId",
                table: "SubProblems",
                column: "MainProblemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "SubProblems");

            migrationBuilder.DropTable(
                name: "MainProblems");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1fa1ffa2-0433-408e-bf89-ba352903350a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce10d48e-26de-4bb2-bd11-806b4de1df01");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "73261b3e-7854-4db3-857b-45864c456dec", null, "User", "USER" },
                    { "8daaa517-57b5-4931-b173-c32a2efc1478", null, "Admin", "ADMIN" }
                });
        }
    }
}
