using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace prasApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDateColumnToReportDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8909c026-0e8e-4b59-bc5f-79dfca0b8ed8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3c1f55c-d6ee-4a3c-8a06-5cc488120cfc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc7caf65-30dc-478c-ab06-f61b6a42c105");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reports",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ReportDetail",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "ReportDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Time",
                table: "ReportDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Transcript",
                table: "ReportDetail",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e4e27bb-5cab-4a20-898e-1fd2a0037119", null, "User", "USER" },
                    { "d29fd543-ef7d-4358-8ae2-24d5e300580e", null, "Admin", "ADMIN" },
                    { "e53eb653-3ffe-469c-9cf5-c7ce2bb22265", null, "Police", "POLICE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e4e27bb-5cab-4a20-898e-1fd2a0037119");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d29fd543-ef7d-4358-8ae2-24d5e300580e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e53eb653-3ffe-469c-9cf5-c7ce2bb22265");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ReportDetail");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "ReportDetail");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "ReportDetail");

            migrationBuilder.DropColumn(
                name: "Transcript",
                table: "ReportDetail");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Reports",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8909c026-0e8e-4b59-bc5f-79dfca0b8ed8", null, "Admin", "ADMIN" },
                    { "b3c1f55c-d6ee-4a3c-8a06-5cc488120cfc", null, "User", "USER" },
                    { "cc7caf65-30dc-478c-ab06-f61b6a42c105", null, "Police", "POLICE" }
                });
        }
    }
}
