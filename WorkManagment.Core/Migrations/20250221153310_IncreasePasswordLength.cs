using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkstationManagment.Core.Migrations
{
    /// <inheritdoc />
    public partial class IncreasePasswordLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkPositions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserWorkPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    WorkPositionId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWorkPositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWorkPositions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWorkPositions_WorkPositions_WorkPositionId",
                        column: x => x.WorkPositionId,
                        principalTable: "WorkPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator", "Admin" },
                    { 2, "Regular user", "User" }
                });

            migrationBuilder.InsertData(
                table: "WorkPositions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Software Engineer" },
                    { 2, null, "System Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "RoleId", "Username" },
                values: new object[] { 1, "John", "Doe", "$2a$11$wpezLxjubBo85vWbJ1EyReAIe8RntpL7QMxnsE1gLvEoXjPjB6xT2", 1, "john123" });

            migrationBuilder.InsertData(
                table: "UserWorkPositions",
                columns: new[] { "Id", "Date", "ProductName", "UserId", "WorkPositionId" },
                values: new object[] { -1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkPositions_UserId",
                table: "UserWorkPositions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWorkPositions_WorkPositionId",
                table: "UserWorkPositions",
                column: "WorkPositionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWorkPositions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkPositions");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
